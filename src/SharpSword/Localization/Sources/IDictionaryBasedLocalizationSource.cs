/* ****************************************************************
 * SharpSword zhangliang4629@163.com 12/20/2016 11:18:38 AM
 * ****************************************************************/
using SharpSword.Localization.Dictionaries;

namespace SharpSword.Localization.Sources
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDictionaryBasedLocalizationSource : ILocalizationSource
    {
        /// <summary>
        /// 连接指定语言包资源
        /// </summary>
        /// <param name="dictionary"></param>
        void Extend(ILocalizationDictionary dictionary);
    }
}
