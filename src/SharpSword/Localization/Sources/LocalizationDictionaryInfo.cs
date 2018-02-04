/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/20/2016 10:16:01 AM
 * ****************************************************************/
using SharpSword.Localization.Dictionaries;

namespace SharpSword.Localization.Sources
{
    /// <summary>
    /// 用来描述一个本地语言包
    /// </summary>
    public class LocalizationDictionaryInfo
    {
        /// <summary>
        /// 对应的语言包字典信息
        /// </summary>
        public ILocalizationDictionary Dictionary { get; private set; }
        /// <summary>
        /// 是否是默认
        /// </summary>
        public bool IsDefault { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="isDefault"></param>
        public LocalizationDictionaryInfo(ILocalizationDictionary dictionary, bool isDefault = false)
        {
            this.Dictionary = dictionary;
            this.IsDefault = isDefault;
        }
    }
}
