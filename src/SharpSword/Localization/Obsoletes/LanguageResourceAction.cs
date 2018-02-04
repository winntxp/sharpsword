/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/18 10:39:55
 * ****************************************************************/
using System;
using System.Xml.Serialization;

namespace SharpSword.Localization.Obsoletes
{
    /// <summary>
    /// 语言包接口信息
    /// </summary>
    [Serializable]
    [Obsolete]
    public class LanguageResourceAction
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        [XmlAttribute("name")]
        public string Name { get; set; }

        /// <summary>
        /// 接口类里使用的语言包键值对信息
        /// </summary>
        [XmlArray("items"), XmlArrayItem("item")]
        public LanguageResourceActionItem[] Items { get; set; }

    }
}
