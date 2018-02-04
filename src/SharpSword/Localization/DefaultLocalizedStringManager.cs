/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/22/2016 2:38:29 PM
 * ****************************************************************/
using SharpSword.Localization.Sources;
using System.Globalization;

namespace SharpSword.Localization
{
    /// <summary>
    /// 默认的本地化字符串获取器
    /// </summary>
    internal class DefaultLocalizedStringManager : ILocalizedStringManager
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly LocalizationConfiguration _localizationConfig;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 本地化资源管理器
        /// </summary>
        public ILocalizationManager LocalizationManager { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localizationConfig">系统框架配置信息</param>
        public DefaultLocalizedStringManager(LocalizationConfiguration localizationConfig)
        {
            this._localizationConfig = localizationConfig;
            this.Logger = GenericNullLogger<DefaultLocalizedStringManager>.Instance;
            this.LocalizationManager = NullLocalizationManager.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        public string GetLocalizedString(string text, string cultureName)
        {
            //不进行本地化操作
            if (!this._localizationConfig.IsEnabled)
            {
                return text;
            }

            //获取本地化资源
            var localizedString = this.LocalizationManager.GetString(
                                                       sourceName: this._localizationConfig.LocalizerSourceName,
                                                       name: text,
                                                       culture: new CultureInfo(cultureName));
            //获取成功，返回本地化资源
            if (!localizedString.IsNullOrEmpty())
            {
                return localizedString;
            }

            //直接返回框架提供的信息
            return text;
        }
    }
}
