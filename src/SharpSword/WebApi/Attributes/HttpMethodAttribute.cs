/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/28/2015 2:08:55 PM
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 用于限定http提交方式，如果接口实现类添加了此特性，那么只有对应的http请求方式才能访问接口
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class HttpMethodAttribute : ActionRequestValidatorAttribute
    {
        /// <summary>
        /// GET/POST
        /// </summary>
        public HttpMethod HttpMethod { get; private set; }

        /// <summary>
        /// 现在接口的http请求方式:POST/GET
        /// </summary>
        /// <param name="httpMethod">GET/POST</param>
        public HttpMethodAttribute(HttpMethod httpMethod)
        {
            this.HttpMethod = httpMethod;
        }

        /// <summary>
        /// 校验当前http提交方式是否有权限访问
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <returns></returns>
        public override ActionRequestValidatorResult ValidForRequest(RequestContext requestContext)
        {
            if (this.HttpMethod.ToString().Contains(requestContext.HttpContext.Request.HttpMethod))
            {
                return ActionRequestValidatorResult.Success;
            }
            var errorMessage =
                Resource.CoreResource.DefaultActionValidator_HttpMethod.With(
                    requestContext.ActionDescriptor.ActionName, requestContext.ActionDescriptor.ActionType.FullName,
                    this.HttpMethod.ToString());
            return new ActionRequestValidatorResult(new ActionResult() { Flag = ActionResultFlag.FAIL, Info = errorMessage }, false);
        }
    }
}