/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/20/2016 11:19:14 AM
 * ****************************************************************/
using SharpSword.Localization.Dictionaries;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Threading;

namespace SharpSword.Localization.Sources
{
    /// <summary>
    /// 
    /// </summary>
    public class DictionaryBasedLocalizationSource : IDictionaryBasedLocalizationSource
    {
        /// <summary>
        /// Key:    Culture-name(区域名称)
        /// Value:  Dictionary(本地资源包键值对)
        /// </summary>
        private readonly Dictionary<string, ILocalizationDictionary> _dictionaries;
        private ILocalizationDictionary _defaultDictionary;
        private LocalizationConfiguration _configuration;
        private readonly ILocalizationDictionaryProvider _dictionaryProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName">资源名称</param>
        /// <param name="dictionaryProvider">资源提供者</param>
        public DictionaryBasedLocalizationSource(string sourceName, ILocalizationDictionaryProvider dictionaryProvider)
        {
            this.Name = sourceName;
            this._dictionaries = new Dictionary<string, ILocalizationDictionary>();
            this._dictionaryProvider = dictionaryProvider;
        }

        /// <summary>
        /// 资源名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 初始化一下当前数据源下面的所有本地语言资源包
        /// </summary>
        /// <param name="configuration"></param>
        public virtual void Initialize(LocalizationConfiguration configuration)
        {
            _configuration = configuration;

            if (_dictionaryProvider.IsNull())
            {
                return;
            }

            var defaultProvided = false;

            //获取所有注册的区域语言资源包
            foreach (var dictionaryInfo in _dictionaryProvider.GetDictionaries(Name))
            {
                //将资源包保存到当前字典,key:区域名称(语言名称) value:本地化资源键值
                _dictionaries[dictionaryInfo.Dictionary.CultureInfo.Name] = dictionaryInfo.Dictionary;

                //当前语言包是默认的，名称和资源名称一致
                if (dictionaryInfo.IsDefault)
                {
                    if (defaultProvided)
                    {
                        throw new SharpSwordCoreException("在此之前已经设置了默认语言包 " + Name);
                    }

                    //设置一下默认的本地化语言包
                    _defaultDictionary = dictionaryInfo.Dictionary;
                    defaultProvided = true;
                }
            }

            if (!defaultProvided)
            {
                throw new SharpSwordCoreException("还未设置任何默认的资源包： " + Name);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetString(string name)
        {
            return this.GetString(name, Thread.CurrentThread.CurrentUICulture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public string GetString(string name, CultureInfo culture)
        {
            //当前区域名称
            var cultureCode = culture.Name;

            //根据当前区域信息，选择对应的本地资源包
            ILocalizationDictionary originalDictionary;
            if (_dictionaries.TryGetValue(cultureCode, out originalDictionary))
            {
                var strOriginal = originalDictionary.Get(name);
                if (!strOriginal.IsNull())
                {
                    return strOriginal.Value;
                }
            }

            // 在我们完整的语言包未找到，我们找没有国家的语言包，所有国家的区域表述可以查阅下面链接
            // https://msdn.microsoft.com/zh-cn/library/kx54z3k7(VS.80).aspx
            if (cultureCode.Length == 5) //Example: "zh-CN" (length=5)
            {
                var langCode = cultureCode.Substring(0, 2);
                ILocalizationDictionary langDictionary;
                if (_dictionaries.TryGetValue(langCode, out langDictionary))
                {
                    var strLang = langDictionary.Get(name);
                    if (!strLang.IsNull())
                    {
                        return strLang.Value;
                    }
                }
            }

            //还找不到，我们只有找默认的语言包了，如果默认语言包不存在，我们直接返回系统框架最原始的字符串
            if (_defaultDictionary.IsNull())
            {
                return name;
            }

            //默认值不存在
            var defaultString = _defaultDictionary.Get(name);
            if (defaultString.IsNull())
            {
                return name;
            }

            //返回默认的语言包值
            return defaultString.Value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<TextString> GetAllStrings()
        {
            return this.GetAllStrings(Thread.CurrentThread.CurrentUICulture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="culture"></param>
        /// <returns></returns>
        public IReadOnlyList<TextString> GetAllStrings(CultureInfo culture)
        {
            
            var dict = new Dictionary<string, TextString>();

            //先添加默认资源包
            if (_defaultDictionary != null)
            {
                foreach (var defaultDictString in _defaultDictionary.GetAllStrings())
                {
                    dict[defaultDictString.Name] = defaultDictString;
                }
            }

            //先保存下不带国家信息语言资源包
            if (culture.Name.Length == 5)
            {
                ILocalizationDictionary langDictionary;
                if (_dictionaries.TryGetValue(culture.Name.Substring(0, 2), out langDictionary))
                {
                    foreach (var langString in langDictionary.GetAllStrings())
                    {
                        dict[langString.Name] = langString;
                    }
                }
            }

            //指定的语言资源包
            ILocalizationDictionary originalDictionary;
            if (_dictionaries.TryGetValue(culture.Name, out originalDictionary))
            {
                foreach (var originalLangString in originalDictionary.GetAllStrings())
                {
                    dict[originalLangString.Name] = originalLangString;
                }
            }

            return dict.Values.ToImmutableList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dictionary"></param>
        public void Extend(ILocalizationDictionary dictionary)
        {
            //不存在我们就添加
            ILocalizationDictionary existingDictionary;
            if (!_dictionaries.TryGetValue(dictionary.CultureInfo.Name, out existingDictionary))
            {
                _dictionaries[dictionary.CultureInfo.Name] = dictionary;
                return;
            }

            //存在我们就覆盖掉原先的字典值
            var localizedStrings = dictionary.GetAllStrings();
            foreach (var localizedString in localizedStrings)
            {
                existingDictionary[localizedString.Name] = localizedString.Value;
            }
        }
    }
}
