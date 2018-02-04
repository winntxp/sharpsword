/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/20/2016 10:15:26 AM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.Localization.Sources
{
    /// <summary>
    /// 区域语言包字典提供者，比如：XML文件，或者内嵌资源等等
    /// </summary>
    public interface ILocalizationDictionaryProvider
    {
        /// <summary>
        /// 获取所有语言包字典，如果sourceName相同则定义成默认
        /// </summary>
        /// <param name="sourceName"></param>
        /// <returns></returns>
        IEnumerable<LocalizationDictionaryInfo> GetDictionaries(string sourceName);
    }
}
