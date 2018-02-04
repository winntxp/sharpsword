/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/20/2016 10:31:55 AM
 * ****************************************************************/
using SharpSword.Configuration;
using System.Collections.Generic;

namespace SharpSword.Localization
{
    /// <summary>
    /// 本地化模块配置信息，此配置信息需要注册成单例
    /// </summary>
    public class LocalizationConfiguration : ISetting
    {
        /// <summary>
        /// 当前程序运行区域名称，如：zh-CN
        /// </summary>
        public string CultureName { get; set; }

        /// <summary>
        /// 当前本地化数据源名称
        /// </summary>
        public string LocalizerSourceName { get; set; }

        /// <summary>
        /// 当前是否启用本地语言
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 当前注册的所有语言
        /// </summary>
        public IList<LanguageInfo> Languages { get; private set; }

        /// <summary>
        /// 当前注册的所有本地化资源源
        /// </summary>
        public ILocalizationSourceList Sources { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public LocalizationConfiguration()
        {
            this.CultureName = "zh-CN";
            this.LocalizerSourceName = "SharpSword";
            this.IsEnabled = true;
            this.Languages = new List<LanguageInfo>();
            this.Sources = new LocalizationSourceList();
        }
    }
}
