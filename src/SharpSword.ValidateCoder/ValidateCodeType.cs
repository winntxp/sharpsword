/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2016 12:25:58 PM
 * ****************************************************************/

namespace SharpSword.ValidateCoder
{
    /// <summary>
    /// 验证码类型
    /// </summary>
    public enum ValidateCodeType
    {
        /// <summary>
        /// 纯数值
        /// </summary>
        Number,

        /// <summary>
        /// 数值与字母的组合
        /// </summary>
        NumberAndLetter,

        /// <summary>
        /// 汉字
        /// </summary>
        ChineseCharacter
    }
}