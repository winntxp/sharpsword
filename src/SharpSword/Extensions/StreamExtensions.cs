/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/9/2016 4:44:47 PM
 * ****************************************************************/
using System;
using System.IO;

namespace SharpSword
{
    /// <summary>
    /// 
    /// </summary>
    public static class StreamExtensions
    {
        /// <summary>
        /// 获取流字节数据
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <returns>字节数组</returns>
        public static byte[] GetBytes(this Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.Position = 0;
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        /// <summary>
        /// 将字节数组转换成BASE64字符串
        /// </summary>
        /// <param name="bytes">字节数组</param>
        /// <returns></returns>
        public static string ToBase64(this byte[] bytes)
        {
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 将数据流转换成BASE64字符串
        /// </summary>
        /// <param name="stream">数据流</param>
        /// <returns></returns>
        public static string ToBase64(this Stream stream)
        {
            return ToBase64(stream.GetBytes());
        }
    }
}
