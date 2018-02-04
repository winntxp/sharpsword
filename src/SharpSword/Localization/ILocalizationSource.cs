/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/20/2016 10:30:44 AM
 * ****************************************************************/
using SharpSword.Localization.Dictionaries;
using System.Collections.Generic;
using System.Globalization;

namespace SharpSword.Localization
{
    /// <summary>
    /// 资源获取
    /// </summary>
    public interface ILocalizationSource
    {
        /// <summary>
        /// 语言包数据源名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        void Initialize(LocalizationConfiguration configuration);

        /// <summary>
        /// 根据键获取对应的语言包值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string GetString(string name);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        string GetString(string name, CultureInfo culture);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<TextString> GetAllStrings();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        IReadOnlyList<TextString> GetAllStrings(CultureInfo culture);
    }
}
