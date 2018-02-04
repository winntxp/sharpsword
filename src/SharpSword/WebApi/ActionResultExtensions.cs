/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace SharpSword.WebApi
{
    /// <summary>
    /// ActionResult扩展类
    /// </summary>
    internal static class ActionResultExtensions
    {
        /// <summary>
        /// 序列化成JSON格式
        /// </summary>
        /// <param name="actionResult">ActionResult对象</param>
        /// <returns></returns>
        public static string ToJson(this ActionResult actionResult)
        {
            return actionResult.Serialize2Josn();
        }

        /// <summary>
        /// 默认使用UTF-8进行格式化
        /// </summary>
        /// <param name="actionResult">ActionResult对象</param>
        /// <param name="encode">字符编码;默认使用UTF-8</param>
        /// <returns></returns>
        public static string ToXml(this ActionResult actionResult, string encode = "UTF-8")
        {
            //由于输出的对象可能含有匿名对象，.NET框架提供的XML序列化类无法对匿名对象序列化
            //所以这里直接使用JSON序列化成XML，因此生成的客户端XML数据与JSON数据有一定的差异
            var xmlDocument = JsonConvert.DeserializeXmlNode(actionResult.ToJson(), "response");
            using (var memoryStream = new MemoryStream())
            {
                var xmlSerializer = new XmlSerializer(xmlDocument.GetType());
                xmlSerializer.Serialize(memoryStream, xmlDocument);
                //输出格式化XML字符串
                return Encoding.GetEncoding("UTF-8").GetString(memoryStream.ToArray());
            }
        }

        /// <summary>
        /// 将数据转换成Byte[]进行传输（先将其序列化成功JSON然后转换成byte[]）
        /// </summary>
        /// <param name="actionResult">ActionResult对象</param>
        /// <returns></returns>
        public static byte[] ToByte(this ActionResult actionResult)
        {
            return actionResult.ToJson().GetBytes();
        }
    }
}