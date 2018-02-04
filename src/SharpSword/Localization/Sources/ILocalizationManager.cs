/* ****************************************************************
 * SharpSword zhangliang4629@163.com 12/20/2016 12:28:54 PM
 * ****************************************************************/
using System.Collections.Generic;
using System.Globalization;

namespace SharpSword.Localization.Sources
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILocalizationManager
    {
        /// <summary>
        /// 当前语言信息
        /// </summary>
        LanguageInfo CurrentLanguage { get; }

        /// <summary>
        /// 获取当前安装的所有语言包
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<LanguageInfo> GetAllLanguages();

        /// <summary>
        /// 获取当前语言包对应的资源源
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ILocalizationSource GetSource(string name);

        /// <summary>
        /// 获取所有注册的资源
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<ILocalizationSource> GetAllSources();

        /// <summary>
        /// 获取本地化资源
        /// </summary>
        /// <param name="sourceName">本地化数据源</param>
        /// <param name="name">key</param>
        /// <returns></returns>
        string GetString(string sourceName, string name);

        /// <summary>
        /// 获取本地化资源
        /// </summary>
        /// <param name="sourceName">本地化数据源</param>
        /// <param name="name">key</param>
        /// <param name="culture">区域</param>
        /// <returns></returns>
        string GetString(string sourceName, string name, CultureInfo culture);
    }
}
