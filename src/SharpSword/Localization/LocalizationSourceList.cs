/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/20/2016 12:39:04 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.Localization
{
    /// <summary>
    /// 
    /// </summary>
    internal class LocalizationSourceList : List<ILocalizationSource>, ILocalizationSourceList
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<LocalizationSourceExtensionInfo> Extensions { get; private set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        public LocalizationSourceList()
        {
            Extensions = new List<LocalizationSourceExtensionInfo>();
        }
    }
}
