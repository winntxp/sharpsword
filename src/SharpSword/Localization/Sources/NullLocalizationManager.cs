/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/20/2016 3:20:23 PM
 * ****************************************************************/
using System.Collections.Generic;
using System.Globalization;
using System.Threading;

namespace SharpSword.Localization.Sources
{
    /// <summary>
    /// 
    /// </summary>
    public class NullLocalizationManager : ILocalizationManager
    {
        private static readonly NullLocalizationManager SingletonInstance = new NullLocalizationManager();
        private readonly IReadOnlyList<LanguageInfo> _emptyLanguageArray = new LanguageInfo[0];
        private readonly IReadOnlyList<ILocalizationSource> _emptyLocalizationSourceArray = new ILocalizationSource[0];

        /// <summary>
        /// 
        /// </summary>
        private NullLocalizationManager() { }

        /// <summary>
        /// 
        /// </summary>
        public static NullLocalizationManager Instance { get { return SingletonInstance; } }

        /// <summary>
        /// 
        /// </summary>
        public LanguageInfo CurrentLanguage
        {
            get
            {
                return new LanguageInfo(Thread.CurrentThread.CurrentUICulture.Name,
                    Thread.CurrentThread.CurrentUICulture.DisplayName);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<LanguageInfo> GetAllLanguages()
        {
            return _emptyLanguageArray;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ILocalizationSource GetSource(string name)
        {
            return NullLocalizationSource.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<ILocalizationSource> GetAllSources()
        {
            return _emptyLocalizationSourceArray;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetString(string sourceName, string name)
        {
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="name"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public string GetString(string sourceName, string name, CultureInfo culture)
        {
            return name;
        }
    }
}
