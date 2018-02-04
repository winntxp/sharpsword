/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/13 17:04:49
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword
{
    /// <summary>
    /// Represents a NullCache (caches nothing)
    /// </summary>
    public class NullCacheManager : ICacheManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly ICacheManager _instance = new NullCacheManager();

        /// <summary>
        /// 
        /// </summary>
        private NullCacheManager() { }

        /// <summary>
        /// 返回空的缓存管理器
        /// </summary>
        public static ICacheManager Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T Get<T>(string key)
        {
            return default(T);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public bool IsSet(string key)
        {
            return false;
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key)
        {
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern)
        {
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear()
        {
        }
    }
}