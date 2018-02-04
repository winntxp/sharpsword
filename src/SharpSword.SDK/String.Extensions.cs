/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/12 11:35:44
 * ****************************************************************/

namespace SharpSword.SDK
{
    /// <summary>
    /// 
    /// </summary>
    internal static class StringExtensions
    {
        /// <summary>
        /// 对个需要格式化的字符串格式化
        /// </summary>
        /// <param name="value">待格式化的字符串</param>
        /// <param name="args">格式化参数</param>
        /// <returns></returns>
        public static string With(this string value, params object[] args)
        {
            return string.Format(value, args);
        }

        /// <summary>
        /// 当前字符串是否为空或者为null
        /// </summary>
        /// <param name="value">当前字符串</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
