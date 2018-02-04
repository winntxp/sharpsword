/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/20/2016 12:30:08 PM
 * ****************************************************************/
using SharpSword.Localization.Dictionaries;
using System.Collections.Generic;
using System.Globalization;

namespace SharpSword.Localization.Sources
{
    /// <summary>
    /// 空本地化数据源
    /// </summary>
    internal class NullLocalizationSource : ILocalizationSource
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly NullLocalizationSource SingletonInstance = new NullLocalizationSource();
        private readonly IReadOnlyList<TextString> _emptyStringArray = new TextString[0];

        /// <summary>
        /// 
        /// </summary>
        private NullLocalizationSource() { }

        /// <summary>
        /// 
        /// </summary>
        public static NullLocalizationSource Instance { get { return SingletonInstance; } }

        /// <summary>
        /// 
        /// </summary>
        public string Name { get { return null; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public void Initialize(LocalizationConfiguration configuration) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetString(string name)
        {
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public string GetString(string name, CultureInfo culture)
        {
            return name;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<TextString> GetAllStrings()
        {
            return _emptyStringArray;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IReadOnlyList<TextString> GetAllStrings(CultureInfo culture)
        {
            return _emptyStringArray;
        }
    }
}
