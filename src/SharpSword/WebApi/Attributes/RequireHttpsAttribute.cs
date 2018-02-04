/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 是否开启https连接，如果接口实现类加了此特性，那么只有https连接访问才能访问接口
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class RequireHttpsAttribute : ActionRequestValidatorAttribute
    {
        /// <summary>
        /// 是否开启https连接；默认true
        /// </summary>
        public bool RequireHttps { get; private set; }

        /// <summary>
        /// 是否开启https连接
        /// </summary>
        /// <param name="requireHttps">默认true</param>
        public RequireHttpsAttribute(bool requireHttps = true)
        {
            this.RequireHttps = requireHttps;
        }

        /// <summary>
        /// 校验是否需要安全连接进行访问https
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <returns></returns>
        public override ActionRequestValidatorResult ValidForRequest(RequestContext requestContext)
        {
            if (!this.RequireHttps || requestContext.HttpContext.Request.IsSecureConnection)
            {
                return ActionRequestValidatorResult.Success;
            }
            var errorMessage =
                Resource.CoreResource.DefaultActionValidator_RequireHttps.With(
                    requestContext.ActionDescriptor.ActionName, requestContext.ActionDescriptor.ActionType.FullName);
            return new ActionRequestValidatorResult(new ActionResult() { Flag = ActionResultFlag.FAIL, Info = errorMessage }, false);
        }
    }
}