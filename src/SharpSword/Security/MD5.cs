/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/04/2015 5:04:21 PM
 * ****************************************************************/
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace SharpSword
{
    /// <summary>
    /// MD5数据签名
    /// </summary>
    public class MD5
    {
        /// <summary>
        /// MD5摘要签名
        /// </summary>
        /// <param name="content">待签名的文本</param>
        /// <returns></returns>
        public static string Encrypt(string content)
        {
            content.CheckNullThrowArgumentNullException(nameof(content));
            var cryptoServiceProvider = new MD5CryptoServiceProvider();
            byte[] data = cryptoServiceProvider.ComputeHash(Encoding.GetEncoding("UTF-8").GetBytes(content));
            var stringBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                stringBuilder.Append(data[i].ToString("x2"));
            }
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 对数据流进行签名
        /// </summary>
        /// <param name="stream">待签名的数据流</param>
        /// <returns></returns>
        public static string Encrypt(Stream stream)
        {
            var md5Svr = MD5CryptoServiceProvider.Create();
            byte[] buffer = md5Svr.ComputeHash(stream);
            var stringBuilder = new StringBuilder();
            foreach (byte var in buffer)
            {
                stringBuilder.Append(var.ToString("x2"));
            }
            return stringBuilder.ToString();
        }
    }
}
