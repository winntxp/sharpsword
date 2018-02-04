/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/12 10:20:03
 * ****************************************************************/

namespace SharpSword.ViewEngine.Impl
{
    /// <summary>
    /// 
    /// </summary>
    internal enum SectionType
    {
        /// <summary>
        /// represents a ASP.Net page Directive
        /// </summary>
        Directive,
        /// <summary>
        /// represents an ASP.NET function declaration section
        /// </summary>
        Declaration,
        /// <summary>
        /// 代码片段
        /// </summary>
        Code,
        /// <summary>
        /// 文本常量片段
        /// </summary>
        Text
    }
}
