/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/5/9 10:02:36
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SharpSword.SDK
{
    /// <summary>
    /// 
    /// </summary>
    public class Utils
    {
        /// <summary>
        /// 对数据进行摘要签名
        /// </summary>
        /// <param name="content">待进行MD5签名的原始数据</param>
        /// <returns>返回大写的32位摘要数据</returns>
        internal static string MD5(string content)
        {
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
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dict"></param>
        /// <returns></returns>
        public static IDictionary<string, T> CleanupDictionary<T>(IDictionary<string, T> dict)
        {
            IDictionary<string, T> dictionary = new Dictionary<string, T>(dict.Count);
            IEnumerator<KeyValuePair<string, T>> enumerator = dict.GetEnumerator();
            while (enumerator.MoveNext())
            {
                KeyValuePair<string, T> current = enumerator.Current;
                string key = current.Key;
                KeyValuePair<string, T> current2 = enumerator.Current;
                T value = current2.Value;
                if (value != null)
                {
                    dictionary.Add(key, value);
                }
            }
            return dictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public static string GetFileSuffix(byte[] fileData)
        {
            if (fileData == null || fileData.Length < 10)
            {
                return null;
            }
            if (fileData[0] == 71 && fileData[1] == 73 && fileData[2] == 70)
            {
                return "GIF";
            }
            if (fileData[1] == 80 && fileData[2] == 78 && fileData[3] == 71)
            {
                return "PNG";
            }
            if (fileData[6] == 74 && fileData[7] == 70 && fileData[8] == 73 && fileData[9] == 70)
            {
                return "JPG";
            }
            if (fileData[0] == 66 && fileData[1] == 77)
            {
                return "BMP";
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileData"></param>
        /// <returns></returns>
        public static string GetMimeType(byte[] fileData)
        {
            string fileSuffix = Utils.GetFileSuffix(fileData);
            string text = fileSuffix;
            string result;
            switch (text)
            {
                case "JPG":
                    result = "image/jpeg";
                    return result;
                case "GIF":
                    result = "image/gif";
                    return result;
                case "PNG":
                    result = "image/png";
                    return result;
                case "BMP":
                    result = "image/bmp";
                    return result;
            }
            result = "application/octet-stream";
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static string GetMimeType(string fileName)
        {
            fileName = fileName.ToLower();
            string result;
            if (fileName.EndsWith(".bmp", StringComparison.CurrentCulture))
            {
                result = "image/bmp";
            }
            else
            {
                if (fileName.EndsWith(".gif", StringComparison.CurrentCulture))
                {
                    result = "image/gif";
                }
                else
                {
                    if (fileName.EndsWith(".jpg", StringComparison.CurrentCulture) || fileName.EndsWith(".jpeg", StringComparison.CurrentCulture))
                    {
                        result = "image/jpeg";
                    }
                    else
                    {
                        if (fileName.EndsWith(".png", StringComparison.CurrentCulture))
                        {
                            result = "image/png";
                        }
                        else
                        {
                            result = "application/octet-stream";
                        }
                    }
                }
            }
            return result;
        }
    }
}
