/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/5 14:51:22
 * ****************************************************************/
using System;
using System.Security.Cryptography;
using System.Text;

namespace SharpSword
{
    /// <summary>
    /// RSA加密解密类
    /// </summary>
    public class RSA
    {
        /// <summary>
        /// 生成密钥对;
        ///  array[0] 私钥
        ///  array[1] 公钥
        /// </summary>
        /// <returns></returns>
        public static string[] GenerateKeys()
        {
            string[] sKeys = new String[2];
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            sKeys[0] = rsa.ToXmlString(true);
            sKeys[1] = rsa.ToXmlString(false);
            return sKeys;
        }

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="content">待加密的数据</param>
        /// <returns>RSA公钥加密后的数据</returns>
        public static string Encrypt(string xmlPublicKey, string content)
        {
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPublicKey);
                byte[] bytes = new UnicodeEncoding().GetBytes(content);
                return Convert.ToBase64String(provider.Encrypt(bytes, false));
            }
            catch (Exception exception)
            {
                throw new SharpSwordCoreException(exception.Message, exception.InnerException);
            }
        }

        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="strDecryptString">待解密的数据</param>
        /// <returns>解密后的结果</returns>
        public static string Decrypt(string xmlPrivateKey, string strDecryptString)
        {
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.FromXmlString(xmlPrivateKey);
                byte[] rgb = Convert.FromBase64String(strDecryptString);
                byte[] buffer2 = provider.Decrypt(rgb, false);
                return new UnicodeEncoding().GetString(buffer2);
            }
            catch (Exception exception)
            {
                throw new SharpSwordCoreException(exception.Message, exception.InnerException);
            }
        }
    }
}
