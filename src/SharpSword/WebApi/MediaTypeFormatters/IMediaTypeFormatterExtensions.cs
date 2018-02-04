/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/20 8:43:03
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 格式化扩展类
    /// </summary>
    public static class IMediaTypeFormatterExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediaTypeFormatter">返回对象格式化器</param>
        /// <param name="requestContext">当前请求上下文</param>
        /// <param name="flag">接口返回代码枚举</param>
        /// <param name="info">接口返回消息</param>
        /// <param name="data">接口返回的数据</param>
        /// <returns></returns>
        public static string SerializedActionResultToString(this IMediaTypeFormatter mediaTypeFormatter, RequestContext requestContext, ActionResultFlag flag, string info, object data = null)
        {
            return mediaTypeFormatter.SerializedActionResultToString(requestContext, new ActionResult(data, flag, info));
        }
    }
}
