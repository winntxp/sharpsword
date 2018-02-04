/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/20/2016 9:23:07 AM
 * ****************************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;

namespace SharpSword.Localization.Dictionaries
{
    /// <summary>
    /// 用于表达一个本地语言包资源对象
    /// </summary>
    public class LocalizationDictionary : ILocalizationDictionary, IEnumerable<TextString>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Dictionary<string, TextString> _dictionary;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cultureInfo"></param>
        public LocalizationDictionary(CultureInfo cultureInfo)
        {
            this.CultureInfo = cultureInfo;
            this._dictionary = new Dictionary<string, TextString>();
        }

        /// <summary>
        /// 当前语言包的区域信息
        /// </summary>
        public CultureInfo CultureInfo { get; private set; }

        /// <summary>
        /// 获取或者设置语言包翻译字典
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public virtual string this[string name]
        {
            get
            {
                var localizedString = this.Get(name);
                return localizedString.IsNull() ? null : localizedString.Value;
            }
            set
            {
                _dictionary[name] = new TextString(name, value, CultureInfo);
            }
        }

        /// <summary>
        /// 根据键获取指定的本地语言值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TextString Get(string name)
        {
            TextString localizedString;
            return _dictionary.TryGetValue(name, out localizedString) ? localizedString : null;
        }

        /// <summary>
        /// 获取当前语言包里所有的键值
        /// </summary>
        /// <returns></returns>
        public virtual IReadOnlyList<TextString> GetAllStrings()
        {
            return _dictionary.Values.ToImmutableList();
        }

        /// <summary>
        /// 用于枚举当前语言包信息
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerator<TextString> GetEnumerator()
        {
            return GetAllStrings().GetEnumerator();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetAllStrings().GetEnumerator();
        }

        /// <summary>
        /// 语言包里是否含有指定的键
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        protected bool Contains(string name)
        {
            return _dictionary.ContainsKey(name);
        }
    }
}
