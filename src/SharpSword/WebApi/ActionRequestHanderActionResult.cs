/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/19 11:54:05
 * ****************************************************************/
using System;
using System.Web.Mvc;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 重写一个ActionResult实现，用于处理请求
    /// </summary>
    internal class ActionRequestHanderActionResult : System.Web.Mvc.ActionResult
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; private set; }

        /// <summary>
        /// API接口入口委托
        /// </summary>
        private readonly Action _actionHandler;

        /// <summary>
        /// 初始化一个处理委托，用户后续的ExecuteResult执行
        /// </summary>
        /// <param name="actionHandler">接口请求处理类</param>
        /// <param name="logger">日志记录器</param>
        public ActionRequestHanderActionResult(Action actionHandler, ILogger logger)
        {
            this._actionHandler = actionHandler;
            this.Logger = logger;
        }

        /// <summary>
        /// 将执行的输出交给MVC的Executeresult去执行
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {
            try
            {
                this._actionHandler();
            }
            catch (Exception ex)
            {
                //整个框架级别的错误
                this.Logger.Error(ex);

                //直接输出json到客户端
                context.HttpContext.Response.Write(new ActionResult()
                {
                    Flag = ActionResultFlag.EXCEPTION,
                    Info = Resource.CoreResource.ActionRequestHanderActionResult_Error.With(ex.Message, ex.StackTrace)
                }.Serialize2Josn());

                //输出停止
                context.HttpContext.Response.End();
            }
        }
    }
}
