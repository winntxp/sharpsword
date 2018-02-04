/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/28 9:27:36
 * ****************************************************************/
using System;
using System.Xml.Serialization;

namespace SharpSword.WebApi
{
    /// <summary>
    /// Dll注释XML文档根节点
    /// </summary>
    [XmlRoot("doc")]
    [Serializable]
    public class DllXmlDoc
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        [XmlElement("assembly")]
        public DllXmlDocAssembly Assembly { get; set; }

        /// <summary>
        /// 类，方法，属性说明集合
        /// </summary>
        [XmlArray("members"), XmlArrayItem("member")]
        public DllXmlDocMember[] Members { get; set; }

    }
}
