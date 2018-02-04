/******************************************************************
 * zhangliang@sharpsword.com.cn 11/01 10:04:21 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword
{
    /// <summary>
    /// IDictionary Extensions
    /// </summary>
    public static class IDictionaryExtensions
    {
        /// <summary>
        /// 添加一个键值并且返回字段对象
        /// </summary>
        /// <typeparam name="TKey">键数据类型</typeparam>
        /// <typeparam name="TValue">值数据类型</typeparam>
        /// <param name="source">字典对象</param>
        /// <param name="key">当前加入key键</param>
        /// <param name="value">当前加入的值</param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> Append<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, TValue value)
        {
            source.Add(key, value);
            return source;
        }

        /// <summary>
        /// 添加一个键值并且返回字段对象
        /// </summary>
        /// <typeparam name="TKey">键数据类型</typeparam>
        /// <typeparam name="TValue">值数据类型</typeparam>
        /// <param name="source">字典对象</param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> Append<TKey, TValue>(this IDictionary<TKey, TValue> source, params KeyValuePair<TKey, TValue>[] items)
        {
            if (items.IsNull())
            {
                return source;
            }
            foreach (var kp in items)
            {
                source.Add(kp.Key, kp.Value);
            }
            return source;
        }

        /// <summary>
        /// 添加一个键值并且返回字段对象
        /// </summary>
        /// <typeparam name="TKey">键数据类型</typeparam>
        /// <typeparam name="TValue">值数据类型</typeparam>
        /// <param name="source">字典对象</param>
        /// <param name="items"></param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> Append<TKey, TValue>(this IDictionary<TKey, TValue> source, params Tuple<TKey, TValue>[] items)
        {
            foreach (var kp in items)
            {
                source.Add(kp.Item1, kp.Item2);
            }
            return source;
        }

        /// <summary>
        /// 添加一个键值并且返回字段对象
        /// </summary>
        /// <typeparam name="TKey">键数据类型</typeparam>
        /// <typeparam name="TValue">值数据类型</typeparam>
        /// <param name="source">字典对象</param>
        /// <param name="default"></param>
        /// <returns></returns>
        public static IDictionary<TKey, TValue> Append<TKey, TValue>(this IDictionary<TKey, TValue> source, Func<KeyValuePair<TKey, TValue>> @default)
        {
            var kv = @default();
            source.Add(kv.Key, kv.Value);
            return source;
        }

        /// <summary>
        /// 判断一个字段对象是否为空，注意此扩展方法仅仅判断字段里是否为0个集合
        /// </summary>
        /// <param name="source">字段对象</param>
        /// <returns>包含0个元素，返回true</returns>
        /// <exception cref="ArgumentNullException">ArgumentNullException</exception>
        public static bool IsEmpty<TKey, TValue>(this IDictionary<TKey, TValue> source)
        {
            source.CheckNullThrowArgumentNullException(nameof(source));
            return !source.Any();
        }

        /// <summary>
        /// 根据键获取字典值，如果指定的键不存在，就从后续的默认委托获取值
        /// </summary>
        /// <param name="source">字典对象</param>
        /// <typeparam name="TKey">键数据类型</typeparam>
        /// <typeparam name="TValue">值数据类型</typeparam>
        /// <param name="key">键</param>
        /// <param name="notFoundFactory">返回默认值委托</param>
        /// <returns></returns>
        public static TValue GetValue<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key, Func<TKey, TValue> notFoundFactory)
        {
            source.CheckNullThrowArgumentNullException(nameof(source));
            return source.ContainsKey(key) ? source[key] : notFoundFactory(key);
        }

        /// <summary>
        /// 根据键获取字典值，如果指定的键不存在，就从后续的默认委托获取值
        /// </summary>
        /// <typeparam name="TKey">键数据类型</typeparam>
        /// <typeparam name="TValue">值数据类型</typeparam>
        /// <typeparam name="TResult">返回值类型</typeparam>
        /// <param name="source">字典对象</param>
        /// <param name="key">键</param>
        /// <param name="foundFactory">当字典里含有键的时候，我们使用委托重新加工返回值，key为：key,Value:为返回的字典值</param>
        /// <param name="notFoundFactory">当不存在的时候，我们使用默认委托来返回</param>
        /// <returns></returns>
        public static TResult GetValue<TKey, TValue, TResult>(this IDictionary<TKey, TValue> source, TKey key, Func<TKey, TValue, TResult> foundFactory, Func<TKey, TResult> notFoundFactory)
        {
            source.CheckNullThrowArgumentNullException(nameof(source));
            return source.ContainsKey(key) ? foundFactory(key, source[key]) : notFoundFactory(key);
        }

        /// <summary>
        /// 根据键获取值信息，如果键不存在，就直接返回默认的值。引用类型，返回null；值类型返回值的默认值
        /// </summary>
        /// <typeparam name="TKey"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="source">字典对象</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static TValue GetValueOrDefault<TKey, TValue>(this IDictionary<TKey, TValue> source, TKey key)
        {
            TValue obj;
            return source.TryGetValue(key, out obj) ? obj : default(TValue);
        }
    }
}

