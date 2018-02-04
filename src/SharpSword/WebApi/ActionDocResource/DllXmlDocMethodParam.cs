/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/28 12:34:59
 * ****************************************************************/
using System;
using System.Xml.Serialization;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 方法参数对象说明
    /// </summary>
    [Serializable]
    public class DllXmlDocMethodParam
    {
        /// <summary>
        /// 参数名称
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}
