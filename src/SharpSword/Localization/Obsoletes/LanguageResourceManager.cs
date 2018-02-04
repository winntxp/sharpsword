/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/18 14:45:50
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace SharpSword.Localization.Obsoletes
{
    /// <summary>
    /// ACTION自定义接口使用的语言包对象管理器
    /// </summary>
    [Obsolete]
    public class LanguageResourceManager
    {
        /// <summary>
        /// 用于缓存，忽略大小写
        /// </summary>
        private readonly IDictionary<string, string> _languageResourceKeyValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private static readonly object Locker = new object();
        private static LanguageResourceManager _instance;

        /// <summary>
        /// 获取接口语言包管理对象
        /// </summary>
        public static LanguageResourceManager Instance
        {
            get
            {
                if (!_instance.IsNull())
                {
                    return _instance;
                }
                lock (Locker)
                {
                    if (_instance.IsNull())
                    {
                        _instance = new LanguageResourceManager(""); //SystemOptionsManager.Current.LanguageResourcePath);
                    }
                }
                return _instance;
            }
        }

        /// <summary>
        /// ACTION自定义接口使用的语言包对象管理器
        /// </summary>
        /// <param name="languageResourcePath">语言包路径</param>
        private LanguageResourceManager(string languageResourcePath)
        {
            this.Init(this.GetLanguageResource(languageResourcePath));
        }

        /// <summary>
        /// 获取用户自定义的语言资源包
        /// </summary>
        /// <param name="languageResourcePath">语言资源包路径</param>
        /// <returns>获取xml语言包对象，当语言包路径不存在情况下，返回null</returns>
        private LanguageResource GetLanguageResource(string languageResourcePath)
        {
            //语言包路径不存在直接返回空的集合
            if (languageResourcePath.IsNullOrEmpty() || !File.Exists(languageResourcePath))
            {
                return null;
            }
            LanguageResource languageResource = null;
            //读取语言资源包
            using (var streamReader = new StreamReader(languageResourcePath))
            {
                try
                {
                    languageResource = (LanguageResource)new XmlSerializer(typeof(LanguageResource)).Deserialize(streamReader);
                }
                catch
                {
                    // ignored
                }
            }

            return languageResource;
        }

        /// <summary>
        /// 初始化语言包对象获取对应的字典
        /// </summary>
        private void Init(LanguageResource languageResource)
        {
            if (languageResource.IsNull() || languageResource.Actions.IsNull())
            {
                return;
            }

            foreach (var action in languageResource.Actions)
            {
                if (action.Items.IsNull() || action.Items.IsEmpty())
                {
                    continue;
                }

                foreach (var item in action.Items)
                {
                    //获取键信息
                    string key = "{0}.{1}".With(action.Name, item.Key);

                    //已经存在键，直接抛出异常，防止在正式环境出现错误
                    if (this._languageResourceKeyValues.ContainsKey(key))
                    {
                        this._languageResourceKeyValues.Clear();
                        throw new SharpSwordCoreException("语言资源包存在相同的键，键：{0},值：{1}".With(key, item.Value));
                    }
                    this._languageResourceKeyValues.Add(key, item.Value);
                }
            }
        }

        /// <summary>
        /// 完整的键值名称
        /// </summary>
        /// <param name="fullKey">资源键：完整的键名称，比如：Api.Core.System.ERR_01</param>
        /// <param name="defaultValue">资源文件不存在对应的键，就直接返回defaultValue</param>
        /// <returns>从字典里返回指定键的值，不存在就返回默认值defaultValue</returns>
        public string GetLanguageResourceValue(string fullKey, string defaultValue)
        {
            //没有指定键
            if (fullKey.IsNullOrEmpty())
            {
                return defaultValue ?? string.Empty;
            }

            //不存在指定的键
            if (!this._languageResourceKeyValues.ContainsKey(fullKey))
            {
                return defaultValue ?? string.Empty;
            }

            //返回指定的键值
            return this._languageResourceKeyValues[fullKey];
        }
    }
}
