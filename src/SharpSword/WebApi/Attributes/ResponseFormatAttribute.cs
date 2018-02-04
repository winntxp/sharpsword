/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/29/2016 12:51:52 PM
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 允许输出的格式
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ResponseFormatAttribute : ActionRequestValidatorAttribute
    {
        /// <summary>
        /// 允许输出的格式
        /// </summary>
        public ResponseFormat Format { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseFormat">VIEW/XML/JSON</param>
        public ResponseFormatAttribute(ResponseFormat responseFormat)
        {
            this.Format = responseFormat;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <returns></returns>
        public override ActionRequestValidatorResult ValidForRequest(RequestContext requestContext)
        {
            //将上述的格式转换成枚举
            var format = new Enum<ResponseFormat>().GetItem(requestContext.RequestParams.Format, () => ResponseFormat.JSON);

            //进行与运算
            if ((this.Format & format) == format)
            {
                return ActionRequestValidatorResult.Success;
            }
            var errorMessage =
                Resource.CoreResource.ResponseFormatAttribute_ResponseFormat_Error.With(
                    requestContext.ActionDescriptor.ActionName, requestContext.ActionDescriptor.ActionType.FullName,
                    this.Format.ToString());
            return new ActionRequestValidatorResult(new ActionResult() { Flag = ActionResultFlag.FAIL, Info = errorMessage }, false);
        }
    }
}
