/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/20/2016 9:22:13 AM
 * ****************************************************************/
using System.Collections.Generic;
using System.Globalization;

namespace SharpSword.Localization.Dictionaries
{
    /// <summary>
    /// 用于定义语言包的文件，对应一个资源文件
    /// </summary>
    public interface ILocalizationDictionary
    {
        /// <summary>
        /// 语言包区域
        /// </summary>
        CultureInfo CultureInfo { get; }

        /// <summary>
        /// 根据语言获取对应的区域语言翻译信息
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        string this[string name] { get; set; }

        /// <summary>
        /// 根据原始信息获取到对应的区域翻译值，如果不存在具体实现类里返回null
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        TextString Get(string name);

        /// <summary>
        /// 获取所有区域语言信息
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<TextString> GetAllStrings();
    }
}
