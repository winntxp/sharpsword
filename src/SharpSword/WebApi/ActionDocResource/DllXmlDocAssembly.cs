/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/28 9:27:55
 * ****************************************************************/
using System;
using System.Xml.Serialization;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 程序集
    /// </summary>
    [Serializable]
    public class DllXmlDocAssembly
    {
        /// <summary>
        /// 程序集名称
        /// </summary>
        [XmlElement("name")]
        public string Name { get; set; }
    }
}
