/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/20 8:44:48
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// XML格式化器
    /// </summary>
    public class XmlMediaTypeFormatter : IMediaTypeFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        private static XmlMediaTypeFormatter _instance = new XmlMediaTypeFormatter();

        /// <summary>
        /// 
        /// </summary>
        public static IMediaTypeFormatter Instance => _instance;

        /// <summary>
        /// 
        /// </summary>
        private XmlMediaTypeFormatter() { }

        /// <summary>
        /// XML格式化器
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <param name="actionResult">ActionResult对象</param>
        /// <returns>输出序列化后的字符串</returns>
        public virtual string SerializedActionResultToString(RequestContext requestContext, ActionResult actionResult)
        {
            return actionResult.ToXml();
        }
    }
}
