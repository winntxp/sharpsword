/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/20/2016 10:37:41 AM
 * ****************************************************************/
using SharpSword.Localization.Sources;

namespace SharpSword.Localization
{
    /// <summary>
    /// 用户设置数据源提供者
    /// </summary>
    public class LocalizationSourceExtensionInfo
    {
        /// <summary>
        /// 资源名称
        /// </summary>
        public string SourceName { get; private set; }

        /// <summary>
        /// 资源对应的语言包提供者
        /// </summary>
        public ILocalizationDictionaryProvider DictionaryProvider { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="dictionaryProvider"></param>
        public LocalizationSourceExtensionInfo(string sourceName,
                                               ILocalizationDictionaryProvider dictionaryProvider)
        {
            this.SourceName = sourceName;
            this.DictionaryProvider = dictionaryProvider;
        }
    }
}
