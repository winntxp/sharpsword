/* ****************************************************************
 * SharpSword zhangliang4629@163.com 12/20/2016 9:28:54 AM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Xml;

namespace SharpSword.Localization.Dictionaries
{
    /// <summary>
    /// 用于XML语言包定义资源文件解析
    /// </summary>
    internal class XmlLocalizationDictionary : LocalizationDictionary
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="cultureInfo"></param>
        private XmlLocalizationDictionary(CultureInfo cultureInfo)
            : base(cultureInfo)
        {

        }

        /// <summary>
        /// 根据XML文件路径获取对应的语言包信息
        /// </summary>
        /// <param name="filePath">xml资源包文件路径，请使用物理路径，如：G:\\xx\x.xml</param>
        /// <returns></returns>
        public static XmlLocalizationDictionary BuildFomFile(string filePath)
        {
            try
            {
                return BuildFomXmlString(File.ReadAllText(filePath));
            }
            catch (Exception ex)
            {
                throw new SharpSwordCoreException("读取文件错误 " + filePath, ex);
            }
        }

        /// <summary>
        /// 根据XML字符串获取对应的语言包路径
        /// </summary>
        /// <param name="xmlString">语言资源包xml文件内容</param>
        /// <returns></returns>
        public static XmlLocalizationDictionary BuildFomXmlString(string xmlString)
        {
            var settingsXmlDoc = new XmlDocument();
            settingsXmlDoc.LoadXml(xmlString);

            var localizationDictionaryNode = settingsXmlDoc.SelectNodes("/localizationDictionary");
            if (localizationDictionaryNode == null || localizationDictionaryNode.Count <= 0)
            {
                throw new SharpSwordCoreException("资源xml文件必须包含根节点/localizationDictionary ");
            }

            //获取区域信息
            var cultureName = localizationDictionaryNode[0].GetAttributeValueOrNull("culture");
            if (string.IsNullOrEmpty(cultureName))
            {
                throw new SharpSwordCoreException("culture属性在xml文件里未定义");
            }

            //初始化一下本地化语言字典
            var dictionary = new XmlLocalizationDictionary(new CultureInfo(cultureName));
            var dublicateNames = new List<string>();

            //将语言包所有的键值对添加本地语言字典
            var textNodes = settingsXmlDoc.SelectNodes("/localizationDictionary/texts/text");
            if (!textNodes.IsNull())
            {
                foreach (XmlNode node in textNodes)
                {
                    var name = node.GetAttributeValueOrNull("name");
                    if (string.IsNullOrEmpty(name))
                    {
                        throw new SharpSwordCoreException("name属性未找到");
                    }

                    //将重复的键保存到临时列表
                    if (dictionary.Contains(name))
                    {
                        dublicateNames.Add(name);
                    }

                    dictionary[name] = (node.GetAttributeValueOrNull("value") ?? node.InnerText).NormalizeLineEndings();
                }
            }

            //判断是否有重复
            if (dublicateNames.Count > 0)
            {
                throw new SharpSwordCoreException("已经定义了相同的键: " + dublicateNames.JoinToString(", "));
            }

            //返回本地化资源字典
            return dictionary;
        }
    }
}
