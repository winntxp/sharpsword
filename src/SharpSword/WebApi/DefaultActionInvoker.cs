/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/26/2015 8:38:48 AM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.WebApi
{
    /// <summary>
    /// Action接口执行器（对Action接口进行代理封装），方便对action执行方法进行AOP横切
    /// </summary>
    internal class DefaultActionInvoker : IActionInvoker
    {
        /// <summary>
        /// 默认Action激活器
        /// </summary>
        public DefaultActionInvoker()
        {
            //this.Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 此方法里进行更多的http条件判断
        /// </summary>
        /// <param name="action">实现IAction的实例</param>
        /// <returns></returns>
        public virtual ActionResult Execute(IAction action)
        {
            //action不能为null
            action.CheckNullThrowArgumentNullException(nameof(action));

            //当前action的描述信息
            ActionDescriptor actionDescriptor = action.ActionDescriptor;

            //用于保存执行结果
            ActionResult actionResult = null;

            /* 过滤器执行顺序规则
             * 
             * Action.Executing() --> Exception --> return ErrResult
             * 
             * - A.OnActionExecuting()
             * 
             * ----B.OnActionExecuting()
             *    
             * --------C.OnActionExecuting()
             *   
             * ------------Action.Execute() --> Exception --> return ErrResult
             * 
             * --------C.OnActionExecuted()
             *        
             * ----B.OnActionExecuted()
             *    
             * - A.OnActionExecuted()
             * 
             */

            #region 执行 action.Executing()

            try
            {
                //执行下框架判断
                action.Executing();
            }
            catch (Exception ex)
            {
                //抛出异常返回
                return new ActionResult() { Flag = ActionResultFlag.EXCEPTION, Info = ex.Message };
            }

            //先获取到全部的过滤器，包括（接口自身，全局过滤器，特性过滤器）
            var actionFilters = new List<ActionFilterWrapper> { ((IActionFilter)action).Wrapped() };

            //添加特性过滤器和全局过滤器
            actionFilters.AddRange(
                actionDescriptor.ActionFilters.Select(actionFilter => actionFilter.Wrapped()));

            #endregion

            #region 执行 OnActionExecuting

            //执行前拦截
            foreach (var item in
                    actionFilters.OrderByDescending(o => o.InternalOrder)
                        .ThenByDescending(o => o.Order)
                        .Select(o => o.ActionFilter))
            {
                //一个拦截器出现错误，不能影响到其他拦截器的执行
                try
                {
                    var actionExecutingContext = new ActionExecutingContext(action);
                    item.OnActionExecuting(actionExecutingContext);
                    //如果执行完毕后，直接结果不为null，值直接返回了，不会进行下面的action执行了
                    if (!actionExecutingContext.Result.IsNull())
                    {
                        return actionExecutingContext.Result;
                    }
                }
                catch (Exception ex)
                {
                    //this.Logger.Error(ex);
                }
            }

            #endregion

            #region 执行 action.Execute()

            try
            {
                //正式执行业务逻辑
                var executedActionResult = action.Execute();

                //转型下
                actionResult = new ActionResult()
                {
                    Data = executedActionResult.Data,
                    Flag = executedActionResult.Flag,
                    Info = executedActionResult.Info
                };
            }
            catch (Exception exception)
            {
                string errorMessage =
                    Resource.CoreResource.DefaultActionInvoker_ActionExecuteError.With(actionDescriptor.ActionName,
                        actionDescriptor.ActionType.FullName, exception.Message);

                //抛出异常返回
                actionResult = new ActionResult()
                {
                    Flag = ActionResultFlag.EXCEPTION,
                    Info = "Message：{0}，StackTrace：{1}".With(errorMessage, exception.StackTrace)
                };

                //记录下日志
                //this.Logger.Error(exception);
            }

            #endregion

            #region 执行 OnActionExecuted

            //执行后拦截
            foreach (var item in actionFilters.OrderBy(o => o.InternalOrder).ThenBy(o => o.Order).Select(o => o.ActionFilter))
            {
                try
                {
                    var actionExecutedContext = new ActionExecutedContext(action, actionResult);
                    item.OnActionExecuted(actionExecutedContext);
                    actionResult = actionExecutedContext.Result;
                }
                catch (Exception ex)
                {
                    //this.Logger.Error(ex);
                }
            }

            #endregion

            //返回值
            return actionResult;
        }
    }
}