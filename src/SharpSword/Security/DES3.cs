/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/21 8:41:07
 * ****************************************************************/
using System;
using System.Security.Cryptography;
using System.Text;

namespace SharpSword
{
    /// <summary>
    /// 3DES加密解密类(编码使用UTF-8)
    /// </summary>
    public class DES3
    {
        /// <summary>
        /// 加密矢量
        /// </summary>
        private static readonly byte[] IV = { 0xB0, 0xA2, 0xB8, 0xA3, 0xDA, 0xCC, 0xDA, 0xCC };

        /// <summary>
        /// 3DES加密
        /// </summary>
        /// <param name="key">加密key(24字符)</param>
        /// <param name="content">待机密明文</param>
        /// <returns></returns>
        public static string Encrypt(string key, string content)
        {
            TripleDESCryptoServiceProvider des3 = new TripleDESCryptoServiceProvider();
            //des3.KeySize = 192;
            des3.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des3.Mode = CipherMode.CBC;
            des3.Padding = PaddingMode.PKCS7;
            des3.IV = IV; // IV未设置就会重新生产
            ICryptoTransform desEncrypt = des3.CreateEncryptor();
            byte[] buffer = ASCIIEncoding.UTF8.GetBytes(content);
            return Convert.ToBase64String(desEncrypt.TransformFinalBlock(buffer, 0, buffer.Length));
        }

        /// <summary>
        /// 3DES解密
        /// </summary>
        /// <param name="key">解密key（24字符）</param>
        /// <param name="encryptString">待解密密文</param>
        /// <returns>解密失败会返回null，调用请注意判断</returns>
        public static string Decrypt(string key, string encryptString)
        {
            TripleDESCryptoServiceProvider des3 = new TripleDESCryptoServiceProvider();
            des3.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des3.Mode = CipherMode.CBC;
            des3.Padding = PaddingMode.PKCS7;
            des3.IV = IV;
            ICryptoTransform DESDecrypt = des3.CreateDecryptor();
            try
            {
                byte[] buffer = Convert.FromBase64String(encryptString);
                return ASCIIEncoding.UTF8.GetString(DESDecrypt.TransformFinalBlock(buffer, 0, buffer.Length));
            }
            catch (Exception exception)
            {
                throw new SharpSwordCoreException(exception.Message, exception.InnerException);
            }
        }
    }
}
