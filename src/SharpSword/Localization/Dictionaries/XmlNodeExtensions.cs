/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/20/2016 9:30:19 AM
 * ****************************************************************/
using System;
using System.Linq;
using System.Xml;

namespace SharpSword.Localization.Dictionaries
{
    /// <summary>
    /// 
    /// </summary>
    internal static class XmlNodeExtensions
    {
        /// <summary>
        /// 获取XML节点属性的值
        /// </summary>
        /// <param name="node"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public static string GetAttributeValueOrNull(this XmlNode node, string attributeName)
        {
            if (node.Attributes.IsNull() || node.Attributes.Count <= 0)
            {
                throw new ApplicationException(node.Name + " 不存在 [" + attributeName + "] 属性");
            }

            return node.Attributes.Cast<XmlAttribute>()
                                  .Where(attr => attr.Name == attributeName)
                                  .Select(attr => attr.Value)
                                  .FirstOrDefault();
        }
    }
}
