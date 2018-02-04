/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/20/2016 9:34:26 AM
 * ****************************************************************/
using System.Globalization;

namespace SharpSword.Localization.Dictionaries
{
    /// <summary>
    /// 语言包定义的键值信息
    /// </summary>
    public class TextString
    {
        /// <summary>
        /// 区域信息
        /// </summary>
        public CultureInfo CultureInfo { get; internal set; }

        /// <summary>
        /// 原始text信息
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 对应区域的翻译值
        /// </summary>
        public string Value { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">待翻译的键</param>
        /// <param name="value">区域语言资源值</param>
        /// <param name="cultureInfo">当前键值所属的区域</param>
        public TextString(string name, string value, CultureInfo cultureInfo)
        {
            this.Name = name;
            this.Value = value;
            this.CultureInfo = cultureInfo;
        }

        /// <summary>
        /// 我们将TextString对账隐式转换成string对象
        /// </summary>
        /// <param name="textString"></param>
        public static implicit operator string (TextString textString)
        {
            return textString.Value;
        }
    }
}
