/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/26 11:28:37
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口执行前，接口执行器上下文
    /// </summary>
    public class ActionExecutingContext : ActionContext
    {
        /// <summary>
        /// 初始化当前上下文对象
        /// </summary>
        /// <param name="action">接口实例</param>
        public ActionExecutingContext(IAction action)
            : base(action, action.RequestContext, action.ActionDescriptor, action.RequestDto)
        {
        }
    }
}
