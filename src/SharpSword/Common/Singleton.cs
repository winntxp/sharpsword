/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/19 8:46:58
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword
{
    /// <summary>
    /// 单例集合类
    /// </summary>
    public class Singleton
    {
        /// <summary>
        /// 初始化一下静态字典
        /// </summary>
        static Singleton()
        {
            allSingletons = new Dictionary<Type, object>();
        }

        /// <summary>
        /// 定义一个静态的全局字典
        /// </summary>
        private static readonly IDictionary<Type, object> allSingletons;

        /// <summary>
        /// 
        /// </summary>
        public static IDictionary<Type, object> AllSingletons
        {
            get { return allSingletons; }
        }
    }

    /// <summary>
    /// 泛型单列
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Singleton<T> : Singleton
    {
        /// <summary>
        /// 
        /// </summary>
        private static T _instance;

        /// <summary>
        /// 获取单列
        /// </summary>
        public static T Instance
        {
            get { return _instance; }
            set
            {
                _instance = value;
                AllSingletons[typeof(T)] = value;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingletonList<T> : Singleton<IList<T>>
    {
        /// <summary>
        /// 
        /// </summary>
        static SingletonList()
        {
            Singleton<IList<T>>.Instance = new List<T>();
        }

        /// <summary>
        /// 
        /// </summary>
        public new static IList<T> Instance
        {
            get { return Singleton<IList<T>>.Instance; }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class SingletonDictionary<TKey, TValue> : Singleton<IDictionary<TKey, TValue>>
    {
        /// <summary>
        /// 
        /// </summary>
        static SingletonDictionary()
        {
            Singleton<Dictionary<TKey, TValue>>.Instance = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// 
        /// </summary>
        public new static IDictionary<TKey, TValue> Instance
        {
            get { return Singleton<Dictionary<TKey, TValue>>.Instance; }
        }
    }
}
