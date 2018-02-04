/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/20 16:49:33
 * ****************************************************************/
using System;
using System.Security.Cryptography;

namespace SharpSword
{
    /// <summary>
    /// 根据帮助类
    /// </summary>
    public class RandomHelper
    {
        /// <summary>
        /// 获取指定长度的随机数字字符串;适用环境：比如在需要生成手机验证码等需要随机数字字符串的地方；长度无限制
        /// </summary>
        /// <param name="length">数字字符串长度</param>
        /// <returns></returns>
        public static string GenerateRandomDigitCode(int length)
        {
            var random = new Random();
            string str = string.Empty;
            for (var i = 0; i < length; i++)
            {
                str = string.Concat(str, random.Next(10).ToString());
            }
            return str;
        }

        /// <summary>
        /// 获取一个随机数字
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

    }
}
