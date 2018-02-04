/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/22/2016 2:24:25 PM
 * ****************************************************************/
using System;
using System.Globalization;
using System.Linq;
using System.Web;

namespace SharpSword.Localization
{
    /// <summary>
    /// 
    /// </summary>
    internal class DefaultTextFormatter : ITextFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILocalizedStringManager _localizedStringManager;
        private readonly LocalizationConfiguration _localizationConfig;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="localizationConfig"></param>
        /// <param name="localizedStringManager"></param>
        public DefaultTextFormatter(LocalizationConfiguration localizationConfig,
                                    ILocalizedStringManager localizedStringManager)
        {
            this._localizationConfig = localizationConfig;
            this._localizedStringManager = localizedStringManager;
            this.Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public LocalizedString Get(string text, params object[] args)
        {
            var localizedFormat = _localizedStringManager.GetLocalizedString(text, this._localizationConfig.CultureName);
            return args.Length == 0
                ? new LocalizedString(localizedFormat, args)
                : new LocalizedString(string.Format(GetFormatProvider(this._localizationConfig.CultureName),
                                                        localizedFormat, args.Select(Encode).ToArray()), args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentCulture"></param>
        /// <returns></returns>
        private static IFormatProvider GetFormatProvider(string currentCulture)
        {
            try
            {
                return CultureInfo.GetCultureInfo(currentCulture);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private static object Encode(object arg)
        {
            if (arg is IFormattable || arg is IHtmlString)
            {
                return arg;
            }
            return HttpUtility.HtmlEncode(arg);
        }
    }
}
