/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/21 8:46:43
 * ****************************************************************/
using System;
using System.Text;

namespace SharpSword
{
    /// <summary>
    /// Base64加密解密
    /// </summary>
    public class Base64
    {
        /// <summary>
        /// Base64加密；默认使用UTF-8编码
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <returns></returns>
        public static string Encrypt(string input)
        {
            return Encrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="input">需要加密的字符串</param>
        /// <param name="encode">字符编码</param>
        /// <returns></returns>
        public static string Encrypt(string input, Encoding encode)
        {
            return Convert.ToBase64String(encode.GetBytes(input));
        }

        /// <summary>
        /// Base64解密；默认使用UTF-8解码
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <returns></returns>
        public static string Decrypt(string input)
        {
            return Decrypt(input, new UTF8Encoding());
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="input">需要解密的字符串</param>
        /// <param name="encode">字符的编码</param>
        /// <returns></returns>
        public static string Decrypt(string input, Encoding encode)
        {
            return encode.GetString(Convert.FromBase64String(input));
        }
    }
}
