/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口创建器
    /// </summary>
    public interface IActionFactory
    {
        /// <summary>
        /// 根据指定接口名称创建一个调用接口
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <param name="actionName">接口名称，实现类尽量实现大小写不敏感</param>
        /// <param name="version">接口版本，实现类里需要实现，如果未指定接口版本，那么就获取接口最新版本号</param>
        /// <returns></returns>
        IAction Create(RequestContext requestContext, string actionName, string version);

        /// <summary>
        /// 创建一个接口实例
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <param name="actionDescriptor">接口描述对象</param>
        /// <returns></returns>
        IAction Create(RequestContext requestContext, ActionDescriptor actionDescriptor);

        /// <summary>
        /// 释放action占用的资源，框架在执行完IAction.Execute()方法后，执行此方法
        /// </summary>
        /// <param name="action">action实例</param>
        void ReleaseAction(IAction action);
    }
}