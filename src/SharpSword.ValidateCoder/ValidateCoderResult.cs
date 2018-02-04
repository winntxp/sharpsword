/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/9/2017 10:24:44 AM
 * ****************************************************************/
using System.Drawing;

namespace SharpSword.ValidateCoder
{
    /// <summary>
    /// 生成验证码结果对象
    /// </summary>
    public class ValidateCoderResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code">验证码</param>
        /// <param name="codeImage">验证码图片文件</param>
        public ValidateCoderResult(string code, Bitmap codeImage)
        {
            this.Code = code;
            this.CodeImage = codeImage;
        }

        /// <summary>
        /// 验证码
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// 验证码图片文件
        /// </summary>
        public Bitmap CodeImage { get; private set; }

    }
}
