/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/11 14:07:23
 * ****************************************************************/
using System.Web;
using System.Net;

namespace SharpSword
{
    /// <summary>
    /// HttpResponseBase Extensions
    /// </summary>
    public static class HttpResponseBaseExtensions
    {
        /// <summary>
        /// 直接设置返回消息状态码
        /// </summary>
        /// <param name="response">HttpResponseBase</param>
        /// <param name="httpStatusCode">http状态码</param>
        public static void SetStatus(this HttpResponseBase response, HttpStatusCode httpStatusCode)
        {
            response.SetStatus((int)httpStatusCode);
        }

        /// <summary>
        /// 直接设置返回消息状态码
        /// </summary>
        /// <param name="response">HttpResponseBase</param>
        /// <param name="httpStatusCode">http状态码值</param>
        public static void SetStatus(this HttpResponseBase response, int httpStatusCode)
        {
            response.StatusCode = httpStatusCode;
            response.End();
        }

        /// <summary>
        /// 直接输出字节流给客户端
        /// </summary>
        /// <param name="response">HttpResponseBase</param>
        /// <param name="data"></param>
        /// <param name="mimeType">MimeType类型</param>
        public static void WriteBinary(this HttpResponseBase response, byte[] data, string mimeType)
        {
            response.ContentType = mimeType;
            response.WriteBinary(data);
        }

        /// <summary>
        /// 直接输出字节流给客户端
        /// </summary>
        /// <param name="response">HttpResponseBase</param>
        /// <param name="data"></param>
        public static void WriteBinary(this HttpResponseBase response, byte[] data)
        {
            response.OutputStream.Write(data, 0, data.Length);
        }
    }
}
