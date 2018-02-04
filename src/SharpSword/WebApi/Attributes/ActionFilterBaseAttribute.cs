/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/20 13:37:59
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// IAction.Execute()方法执行前后自定义执行方法；所有自定义进行接口拦截的类，都需要继承此抽象基类，
    /// 接口可以附加多个拦截器，按照定义的优先级先后顺序执行
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public abstract class ActionFilterBaseAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 执行优先级，数字越大优先级越高
        /// </summary>
        public virtual int Order { get; set; }

        /// <summary>
        /// 执行方法前；在适当时候进行接口拦截
        /// </summary>
        /// <param name="actionExecutingContext">执行上下文</param>
        public virtual void OnActionExecuting(ActionExecutingContext actionExecutingContext) { }

        /// <summary>
        /// 执行方法后;可以修改接口执行结果
        /// </summary>
        /// <param name="actionExecutedContext">执行上下文</param>
        public virtual void OnActionExecuted(ActionExecutedContext actionExecutedContext) { }
    }
}
