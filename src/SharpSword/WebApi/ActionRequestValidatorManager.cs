/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/16 8:51:11
 * ****************************************************************/
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 默认的请求验证器
    /// </summary>
    internal class ActionRequestValidatorManager
    {
        /// <summary>
        /// 校验action定义的一些拦截设置
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <returns></returns>
        public ActionRequestValidatorResult Valid(RequestContext requestContext)
        {
            requestContext.CheckNullThrowArgumentNullException(nameof(requestContext));

            //三个接口配置(因为需要在外部配置文件里配置，所有重新构造下需要验证的请求验证类)
            var actionRequestValidatorAttributes = new List<ActionRequestValidatorAttribute>
            {
                new EnableAjaxRequestAttribute(requestContext.ActionDescriptor.EnableAjaxRequest),
                new HttpMethodAttribute(requestContext.ActionDescriptor.HttpMethod),
                new RequireHttpsAttribute(requestContext.ActionDescriptor.RequireHttps),

                //下面的这个特性，可以删除，因为后续会自动进行扫描特性进行判断
                new ResponseFormatAttribute(requestContext.ActionDescriptor.ResponseFormat)
            };

            //外部配置校验开始
            foreach (
                var result in
                    actionRequestValidatorAttributes.Select(item => item.ValidForRequest(requestContext))
                        .Where(result => !result.IsValid))
            {
                return result;
            }

            //(内部接口直接指定了验证特性)继承了ActionRequestValidatorAttribute特性类
            var customAttributes =
                (ActionRequestValidatorAttribute[])
                    requestContext.ActionDescriptor.ActionType.GetCustomAttributes(
                        typeof(ActionRequestValidatorAttribute), false);

            //检测是否授权成功
            foreach (var result in customAttributes
                .Select(c => (c).ValidForRequest(requestContext))
                .Where(result => !result.IsValid))
            {
                return result;
            }

            //校验通过
            return ActionRequestValidatorResult.Success;
        }
    }
}
