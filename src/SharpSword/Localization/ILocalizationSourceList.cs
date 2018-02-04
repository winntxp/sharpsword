/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/20/2016 12:38:42 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.Localization
{
    /// <summary>
    /// 
    /// </summary>
    public interface ILocalizationSourceList : IList<ILocalizationSource>
    {
        /// <summary>
        /// 
        /// </summary>
        IList<LocalizationSourceExtensionInfo> Extensions { get; }
    }
}
