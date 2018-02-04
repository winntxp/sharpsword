/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/2/2015 09:32:16 PM
 * ****************************************************************/
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace SharpSword.SDK
{
    /// <summary>
    /// TOP XML响应通用解释器。
    /// </summary>
    internal class ApiXmlParser<T> : IApiParser<T> where T : ResponseBase
    {
        private static Regex regex = new Regex("<(\\w+?)[ >]", RegexOptions.Compiled);
        private static Dictionary<string, XmlSerializer> parsers = new Dictionary<string, XmlSerializer>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="body"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public T Parse(string body, System.Text.Encoding encoding)
        {
            XmlSerializer serializer = null;
            string rootTagName = GetRootElement(body);
            bool inc = parsers.TryGetValue(rootTagName, out serializer);
            if (!inc || serializer == null)
            {
                XmlAttributes rootAttrs = new XmlAttributes();
                rootAttrs.XmlRoot = new XmlRootAttribute(rootTagName);
                XmlAttributeOverrides attrOvrs = new XmlAttributeOverrides();
                attrOvrs.Add(typeof(T), rootAttrs);
                serializer = new XmlSerializer(typeof(T), attrOvrs);
                parsers[rootTagName] = serializer;
            }

            object obj = null;
            using (Stream stream = new MemoryStream(encoding.GetBytes(body)))
            {
                obj = serializer.Deserialize(stream);
            }

            T rsp = (T)obj;
            if (null != rsp)
            {
                rsp.Resp_Body = body;
            }
            return rsp;
        }

        /// <summary>
        /// 获取XML响应的根节点名称
        /// </summary>
        private string GetRootElement(string body)
        {
            Match match = regex.Match(body);
            if (match.Success)
            {
                return match.Groups[1].ToString();
            }
            else
            {
                throw new ApiClientException("Invalid XML response format!");
            }
        }
    }
}
