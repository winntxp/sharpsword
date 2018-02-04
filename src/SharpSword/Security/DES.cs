/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/6 11:01:09
 * ****************************************************************/
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SharpSword
{
    /// <summary>
    /// DES加密解密类
    /// </summary>
    public class DES
    {
        /// <summary> 
        /// DES加密 
        /// </summary> 
        ///<param name="key">加密key(密钥为8位长度)</param>
        /// <param name="content"></param> 
        /// <returns></returns> 
        public static string Encrypt(string key, string content)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Encoding.UTF8.GetBytes(content);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateEncryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Convert.ToBase64String(mStream.ToArray());
        }

        /// <summary> 
        /// DES解密 
        /// </summary> 
        /// <param name="key">解密key(密钥为8位长度)</param>
        /// <param name="encryptString">密文</param> 
        /// <returns></returns> 
        public static string Decrypt(string key, string encryptString)
        {
            byte[] keyBytes = Encoding.UTF8.GetBytes(key.Substring(0, 8));
            byte[] keyIV = keyBytes;
            byte[] inputByteArray = Convert.FromBase64String(encryptString);
            DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
            MemoryStream mStream = new MemoryStream();
            CryptoStream cStream = new CryptoStream(mStream, provider.CreateDecryptor(keyBytes, keyIV), CryptoStreamMode.Write);
            cStream.Write(inputByteArray, 0, inputByteArray.Length);
            cStream.FlushFinalBlock();
            return Encoding.UTF8.GetString(mStream.ToArray());
        }
    }
}
