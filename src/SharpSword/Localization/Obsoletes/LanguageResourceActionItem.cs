/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/18 10:40:12
 * ****************************************************************/
using System;
using System.Xml.Serialization;

namespace SharpSword.Localization.Obsoletes
{
    /// <summary>
    /// 语言包键值对
    /// </summary>
    [Serializable]
    [Obsolete]
    public class LanguageResourceActionItem
    {
        /// <summary>
        /// 键
        /// </summary>
        [XmlAttribute("key")]
        public string Key { get; set; }

        /// <summary>
        /// 值
        /// </summary>
        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}
