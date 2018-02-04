/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/28 9:37:25
 * ****************************************************************/
using System;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace SharpSword.Serializers
{
    /// <summary>
    /// 从程序集里的内切XML文件资源反序列化
    /// </summary>
    internal sealed class DefaultXmlSerializer
    {
        /// <summary>
        /// 程序集内切文件路径，多文件夹请使用.隔开如：ServiceCenter.Api.Core.ActionsDesFile.Actions.XML
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="assemblyStaticFile">程序集内嵌资源文件名称；请注意大小写</param>
        /// <returns></returns>
        public static T XmlDeserializeFromAssemblyStaticFile<T>(string assemblyStaticFile)
        {
            if (assemblyStaticFile.IsNullOrEmpty())
            {
                throw new ArgumentNullException("assemblyStaticFile");
            }
            using (Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(assemblyStaticFile))
            {
                if (!stream.IsNull()) return (T)new XmlSerializer(typeof(T)).Deserialize(stream);
            }
            return default(T);
        }

        /// <summary>
        /// 从文件读取，并反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="xmlFile">文件路径</param>
        /// <returns></returns>
        public static T XmlDeserializeFromFile<T>(string xmlFile)
        {
            if (xmlFile.IsNullOrEmpty())
            {
                throw new ArgumentNullException("xmlFile");
            }
            using (var stream = new FileStream(xmlFile, FileMode.Open))
            {
                return (T)new XmlSerializer(typeof(T)).Deserialize(stream);
            }
        }
    }
}
