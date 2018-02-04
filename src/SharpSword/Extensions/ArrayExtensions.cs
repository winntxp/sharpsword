/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/25 19:24:10
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword
{
    /// <summary>
    /// Array.Extensions
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// 比较2个数字是否相等
        /// </summary>
        /// <typeparam name="T">泛型类型</typeparam>
        /// <param name="arr1">数组1</param>
        /// <param name="arr2">数组2</param>
        /// <returns></returns>
        public static bool ArraysEquals<T>(this T[] arr1, T[] arr2)
        {
            if (ReferenceEquals(arr1, arr2))
            {
                return true;
            }

            if (arr1.IsNull() || arr2.IsNull())
            {
                return false;
            }

            if (arr1.Length != arr2.Length)
            {
                return false;
            }

            var comparer = EqualityComparer<T>.Default;
            for (int i = 0; i < arr1.Length; i++)
            {
                if (!comparer.Equals(arr1[i], arr2[i])) return false;
            }

            return true;
        }
    }
}
