/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/5/23 10:36:33
 * ****************************************************************/
using System;

namespace SharpSword.Configuration.WebConfig
{
    /// <summary>
    /// 用户指定ConfigurationSection节点配置名称映射
    /// 用于继承ConfigurationSectionHandlerBase了类的实现具体配置实现
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationSectionNameAttribute : Attribute
    {
        /// <summary>
        /// Web.config节点configuration.configSections.section名称
        /// </summary>
        public string SectionName { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sectionName">Web.config节点configuration.configSections.section名称</param>
        public ConfigurationSectionNameAttribute(string sectionName)
        {
            sectionName.CheckNullThrowArgumentNullException("sectionName");
            this.SectionName = sectionName;
        }
    }
}
