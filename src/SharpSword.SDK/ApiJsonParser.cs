/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/2/2015 8:32:16 PM
 * ****************************************************************/
using System.Text;
using Newtonsoft.Json;

namespace SharpSword.SDK
{
    /// <summary>
    /// JSON反序列化
    /// </summary>
    /// <typeparam name="T">与JSON对应的实体对象</typeparam>
    internal class ApiJsonParser<T> : IApiParser<T>
    {
        /// <summary>
        /// 反序列化，反序列化失败有可能会出现null情况，请注意
        /// </summary>
        /// <param name="body">待反序列化的JSON数据</param>
        /// <param name="encoding">JSON数据编码格式如：UTF-8</param>
        /// <returns></returns>
        public T Parse(string body, Encoding encoding)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(body);
            }
            catch
            {
                return default(T);
            }
        }
    }
}
