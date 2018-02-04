/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/20/2016 12:29:33 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Linq;
using System.Threading;

namespace SharpSword.Localization.Sources
{
    /// <summary>
    /// 本地化管理器，注意此类在IOC里需要注册成全局单列。
    /// </summary>
    internal class LocalizationManager : ILocalizationManager
    {
        private readonly LocalizationConfiguration _configuration;
        private readonly IDictionary<string, ILocalizationSource> _sources;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public LocalizationManager(LocalizationConfiguration configuration)
        {
            this.Logger = NullLogger.Instance;
            this._configuration = configuration;
            this._sources = new Dictionary<string, ILocalizationSource>();
        }

        /// <summary>
        /// 
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 获取当前正在使用的语言
        /// </summary>
        public LanguageInfo CurrentLanguage
        {
            get
            {
                return this.GetCurrentLanguage();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Initialize()
        {
            InitializeSources();
        }

        /// <summary>
        /// 我们先初始化系统框架注册的所有数据源，并且同时初始化一下每个数据源下面的所有语言包
        /// </summary>
        private void InitializeSources()
        {
            foreach (var source in _configuration.Sources)
            {
                if (_sources.ContainsKey(source.Name))
                {
                    throw new SharpSwordCoreException("存在多个相同的数据源 " + source.Name + "! 数据源必须唯一！");
                }

                //保存数据源
                _sources[source.Name] = source;

                //初始化一下
                source.Initialize(_configuration);

                //动态添加多个数据源
                if (source is IDictionaryBasedLocalizationSource)
                {
                    var dictionaryBasedSource = source as IDictionaryBasedLocalizationSource;
                    var extensions = _configuration.Sources.Extensions.Where(e => e.SourceName == source.Name).ToList();
                    foreach (var extension in extensions)
                    {
                        foreach (var dictionaryInfo in extension.DictionaryProvider.GetDictionaries(source.Name))
                        {
                            dictionaryBasedSource.Extend(dictionaryInfo.Dictionary);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 获取所有已经注册的语言
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<LanguageInfo> GetAllLanguages()
        {
            return _configuration.Languages.ToImmutableList();
        }

        /// <summary>
        /// 根据指定资源名称，或者对应的资源对象
        /// </summary>
        /// <param name="sourceName">资源名称</param>
        /// <returns></returns>
        public ILocalizationSource GetSource(string sourceName)
        {
            //if (!_configuration.IsEnabled)
            //{
            //    return NullLocalizationSource.Instance;
            //}

            if (sourceName.IsNull())
            {
                throw new ArgumentNullException("sourceName");
            }

            ILocalizationSource source;
            if (!_sources.TryGetValue(sourceName, out source))
            {
                throw new SharpSwordCoreException("未找到数据源 " + sourceName);
            }

            return source;
        }

        /// <summary>
        /// 获取当前系统注册的所有资源对象
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<ILocalizationSource> GetAllSources()
        {
            return _sources.Values.ToImmutableList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetString(string sourceName, string name)
        {
            return this.GetSource(sourceName).GetString(name);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceName"></param>
        /// <param name="name"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public string GetString(string sourceName, string name, CultureInfo culture)
        {
            return this.GetSource(sourceName).GetString(name, culture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private LanguageInfo GetCurrentLanguage()
        {
            //没有注册，我们就默认返回一个当前系统区域的语言信息
            if (_configuration.Languages.IsEmpty())
            {
                return new LanguageInfo(Thread.CurrentThread.CurrentUICulture.Name,
                                    Thread.CurrentThread.CurrentUICulture.DisplayName);
            }

            var currentCultureName = Thread.CurrentThread.CurrentUICulture.Name;

            //先精确匹配查找区域语言
            var currentLanguage = _configuration.Languages.FirstOrDefault(x => x.Name == currentCultureName);
            if (!currentLanguage.IsNull())
            {
                return currentLanguage;
            }

            //匹配开始
            currentLanguage = _configuration.Languages.FirstOrDefault(x => currentCultureName.StartsWith(x.Name));
            if (!currentLanguage.IsNull())
            {
                return currentLanguage;
            }

            //上述都找不到，我们选择一个默认的设置
            currentLanguage = _configuration.Languages.FirstOrDefault(x => x.IsDefault);
            if (!currentLanguage.IsNull())
            {
                return currentLanguage;
            }

            //还找不到，我们只能返回第一个
            return _configuration.Languages.First();
        }
    }
}
