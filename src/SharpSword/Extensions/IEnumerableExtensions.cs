/* *******************************************************
 * SharpSword zhangliang4629@163.com 8/19/2016 11:23:59 AM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword
{
    /// <summary>
    /// 可枚举类型扩展方法
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// 连接指定对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="separator">连接分隔符</param>
        /// <returns>返回执行连接分隔符连接发字符串</returns>
        public static string JoinToString<T>(this IEnumerable<T> source, string separator)
        {
            return string.Join(separator, source);
        }

        /// <summary>
        /// 连接指定对象，默认连接符为:,
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">数据源</param>
        /// <returns>返回执行连接分隔符连接发字符串</returns>
        public static string JoinToString<T>(this IEnumerable<T> source)
        {
            return source.JoinToString(",");
        }

#pragma warning disable CS1574 // XML comment has cref attribute 'condition' that could not be resolved
        /// <summary>
        /// Filters a <see cref="IEnumerable{T}"/> by given predicate if given condition is true.
        /// </summary>
        /// <param name="source">Enumerable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the enumerable</param>
        /// <returns>Filtered or not filtered enumerable based on <see cref="condition"/></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> predicate)
#pragma warning restore CS1574 // XML comment has cref attribute 'condition' that could not be resolved
        {
            return condition ? source.Where(predicate) : source;
        }

#pragma warning disable CS1574 // XML comment has cref attribute 'condition' that could not be resolved
        /// <summary>
        /// Filters a <see cref="IEnumerable{T}"/> by given predicate if given condition is true.
        /// </summary>
        /// <param name="source">Enumerable to apply filtering</param>
        /// <param name="condition">A boolean value</param>
        /// <param name="predicate">Predicate to filter the enumerable</param>
        /// <returns>Filtered or not filtered enumerable based on <see cref="condition"/></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, int, bool> predicate)
#pragma warning restore CS1574 // XML comment has cref attribute 'condition' that could not be resolved
        {
            return condition ? source.Where(predicate) : source;
        }
    }
}
