/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/20 16:49:33
 * ****************************************************************/
using System;
using System.Security.Cryptography;

namespace SharpSword
{
    /// <summary>
    /// ���ݰ�����
    /// </summary>
    public class RandomHelper
    {
        /// <summary>
        /// ��ȡָ�����ȵ���������ַ���;���û�������������Ҫ�����ֻ���֤�����Ҫ��������ַ����ĵط�������������
        /// </summary>
        /// <param name="length">�����ַ�������</param>
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
        /// ��ȡһ���������
        /// </summary>
        /// <param name="min">��Сֵ</param>
        /// <param name="max">���ֵ</param>
        /// <returns></returns>
        public static int GenerateRandomInteger(int min = 0, int max = int.MaxValue)
        {
            var randomNumberBuffer = new byte[10];
            new RNGCryptoServiceProvider().GetBytes(randomNumberBuffer);
            return new Random(BitConverter.ToInt32(randomNumberBuffer, 0)).Next(min, max);
        }

    }
}
