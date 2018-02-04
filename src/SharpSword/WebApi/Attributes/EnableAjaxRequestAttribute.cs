/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/25 11:06:41
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 标注接口是否允许Ajax形式访问，默认true
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class EnableAjaxRequestAttribute : ActionRequestValidatorAttribute
    {
        /// <summary>
        /// 是否允许Ajax访问
        /// </summary>
        public bool EnableAjaxRequest { get; private set; }

        /// <summary>
        /// 是否允许AJAX访问
        /// </summary>
        /// <param name="enableAjaxReques">默认true，允许访问</param>
        public EnableAjaxRequestAttribute(bool enableAjaxReques = true)
        {
            this.EnableAjaxRequest = enableAjaxReques;
        }

        /// <summary>
        /// 校验是否有权限进行Ajax请求访问
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <returns></returns>
        public override ActionRequestValidatorResult ValidForRequest(RequestContext requestContext)
        {
            if (this.EnableAjaxRequest || !requestContext.HttpContext.Request.IsAjaxRequest())
            {
                return ActionRequestValidatorResult.Success;
            }
            var errorMessage =
                Resource.CoreResource.DefaultActionValidator_EnableAjaxRequest.With(
                    requestContext.ActionDescriptor.ActionName, requestContext.ActionDescriptor.ActionType.FullName);
            return new ActionRequestValidatorResult(new ActionResult() { Flag = ActionResultFlag.FAIL, Info = errorMessage }, false);
        }
    }
}
