/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/28 9:28:10
 * ****************************************************************/
using System;
using System.Xml.Serialization;

namespace SharpSword.WebApi
{
    /// <summary>
    /// DLL注释文档Member集合对象
    /// </summary>
    [Serializable]
    public class DllXmlDocMember
    {
        /// <summary>
        /// 类，方法，属性类型名称
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// 注释信息
        /// </summary>
        [XmlElement("summary")]
        public string Summary { get; set; }

        /// <summary>
        /// 返回值说明
        /// </summary>
        [XmlElement("returns")]
        public string Returns { get; set; }
    }
}
