/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/17 12:30:45
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword.SDK
{
    /// <summary>
    /// 默认SDK缓存器，默认使用静态字典对象作为缓存器；
    /// 作为默认的缓存器，在预知内容不会变化频繁的情况下可以使用下；
    /// </summary>
    internal class DefaultApiClientCacheManager : IApiClientCacheManager
    {
        /// <summary>
        /// 缓存容器，使用当前进程内存来缓存数据
        /// </summary>
        private readonly static Dictionary<string, CacheItem> _cacheDictionary = new Dictionary<string, CacheItem>();

        /// <summary>
        /// 用于设置缓存的时候，锁定缓存容器，防止并发
        /// </summary>
        private static object locker = new object();

        /// <summary>
        /// 注意直接设置会覆盖原始的缓存内容
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存对象</param>
        /// <param name="cacheTime">相对应与当前时间的分钟数</param>
        public void Set(string key, object data, int cacheTime)
        {
            if (null == data)
            {
                return;
            }
            if (_cacheDictionary.ContainsKey(key))
            {
                lock (locker)
                {
                    _cacheDictionary.Remove(key);
                }
            }
            lock (locker)
            {
                _cacheDictionary.Add(key, new CacheItem() { Data = data, ExpiredTime = DateTime.Now.AddMinutes(cacheTime) });
            }
        }

        /// <summary>
        /// 获取缓存内容
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>缓存存在且绝对过期时间未过期的情况下返回缓存对象，否则返回null</returns>
        public CacheItem Get(string key)
        {
            if (_cacheDictionary.ContainsKey(key))
            {
                //本地缓存存在指定缓存键
                var cacheItem = (CacheItem)_cacheDictionary[key];
                //未过期，直接返回本地SDK缓存对象
                if (cacheItem.ExpiredTime >= DateTime.Now)
                {
                    return cacheItem;
                }
                //过期直接删除过期缓存键
                lock (locker)
                {
                    _cacheDictionary.Remove(key);
                }
            }
            return null;
        }

        /// <summary>
        /// 获取所有的缓存键
        /// </summary>
        /// <returns>当前缓存器里的所有缓存键</returns>
        public IEnumerable<string> GetAllKeys()
        {
            return _cacheDictionary.Keys;
        }
    }
}
