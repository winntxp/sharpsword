/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2016 12:25:58 PM
 * ****************************************************************/

namespace SharpSword.ValidateCoder
{
    /// <summary>
    /// 验证码管理器
    /// </summary>
    public interface IValidateCoderGenerator
    {
        /// <summary>
        /// 生成验证码
        /// </summary>
        /// <param name="length">验证码成都</param>
        /// <param name="codeType">验证码类型</param>
        /// <returns></returns>
        ValidateCoderResult Generator(int length, ValidateCodeType codeType = ValidateCodeType.NumberAndLetter);
    }
}
