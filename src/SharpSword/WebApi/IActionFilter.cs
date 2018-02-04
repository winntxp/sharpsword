/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/5/9 12:57:18
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口执行过滤器接口
    /// </summary>
    public interface IActionFilter
    {
        /// <summary>
        /// 执行方法前；在适当时候进行接口拦截
        /// </summary>
        /// <param name="actionExecutingContext">执行上下文</param>
        void OnActionExecuting(ActionExecutingContext actionExecutingContext);

        /// <summary>
        /// 执行方法后;可以修改接口执行结果
        /// </summary>
        /// <param name="actionExecutedContext">执行上下文</param>
        void OnActionExecuted(ActionExecutedContext actionExecutedContext);
    }
}
