/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/21 9:43:43
 * ****************************************************************/
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace SharpSword.Localization.Obsoletes
{
    /// <summary>
    /// 语言包对象扩展类
    /// </summary>
    [Obsolete]
    internal static class LanguageResourceExtensions
    {
        /// <summary>
        /// 序列化成XML文件
        /// </summary>
        /// <param name="languageResource">本地语言资源包对象</param>
        /// <param name="xmlSavePath">保存成XML路径</param>
        public static void SerializerToXmlFile(this LanguageResource languageResource, string xmlSavePath)
        {
            languageResource.CheckNullThrowArgumentNullException(nameof(languageResource));
            using (FileStream fileStream = new FileStream(xmlSavePath, FileMode.Create))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(languageResource.GetType());
                xmlSerializer.Serialize(fileStream, languageResource);
            }
        }

        /// <summary>
        /// 序列化成XML字符串
        /// </summary>
        /// <param name="languageResource">本地语言资源包对象</param>
        /// <param name="en">编码 如GBK,UTF-8</param>
        /// <returns></returns>
        public static string SerializerToXmlStr(this LanguageResource languageResource, string en = "UTF-8")
        {
            languageResource.CheckNullThrowArgumentNullException(nameof(languageResource));
            var serializer = new XmlSerializer(languageResource.GetType());
            MemoryStream stream = new MemoryStream();
            XmlTextWriter writer = new XmlTextWriter(stream, System.Text.Encoding.GetEncoding(en));
            XmlSerializerNamespaces xmlSerializerNamespaces = new XmlSerializerNamespaces();
            xmlSerializerNamespaces.Add("", "");
            serializer.Serialize(writer, languageResource, xmlSerializerNamespaces);
            var xmlStr = System.Text.Encoding.GetEncoding(en).GetString(stream.ToArray());
            writer.Close();
            return xmlStr;
        }
    }
}

