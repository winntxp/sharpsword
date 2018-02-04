/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/18 10:39:40
 * ****************************************************************/
using System;
using System.Xml.Serialization;

namespace SharpSword.Localization.Obsoletes
{
    /// <summary>
    /// 接口项目语言包序列化类
    /// </summary>
    [Serializable, XmlRoot("resource")]
    [Obsolete]
    public class LanguageResource
    {
        /// <summary>
        /// 所有的接口集合
        /// </summary>
        [XmlArray("actions"), XmlArrayItem("action")]
        public LanguageResourceAction[] Actions { get; set; }
    }
}
