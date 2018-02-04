/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/6/2016 10:32:59 AM
 * ****************************************************************/
using System;
using System.Linq;

namespace SharpSword
{
    /// <summary>
    /// OBJECT对象扩展方法
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// 类型转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T As<T>(this object source) where T : class
        {
            return source.Is<T>() ? (T)source : default(T);
        }

        /// <summary>
        /// 指定对象是否可转型到泛型指定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static bool Is<T>(this object source) where T : class
        {
            return source is T;
        }

        /// <summary>
        /// 基元类型之间相互转换
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static T To<T>(this object source) where T : struct
        {
            return (T)Convert.ChangeType(source, typeof(T));
        }

        /// <summary>
        /// 指定值是否存在指定集合当中
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool In<T>(this T source, params T[] list)
        {
            return list.Contains(source);
        }
    }
}
