/* ****************************************************************
 * SharpSword zhangliang4629@sharpsword.com.cn 12/9/2016 4:41:43 PM
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 
    /// </summary>
    public static class ComparableExtensions
    {
        /// <summary>
        /// 指定的值，是否在某个区间里
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value">指定值</param>
        /// <param name="minInclusiveValue">最小值</param>
        /// <param name="maxInclusiveValue">最大值</param>
        /// <returns></returns>
        public static bool IsBetween<T>(this T value, T minInclusiveValue, T maxInclusiveValue) where T : IComparable<T>
        {
            return value.CompareTo(minInclusiveValue) >= 0 && value.CompareTo(maxInclusiveValue) <= 0;
        }
    }
}
