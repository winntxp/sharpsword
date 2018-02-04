/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/14 9:25:18 From Nop
 * ****************************************************************/
using SharpSword.Timing;
using System;
using System.Linq;
using System.Runtime.Caching;
using System.Text.RegularExpressions;

namespace SharpSword.Caching.Impl
{
    /// <summary>
    /// ASP.NET自带内存缓存器(系统框架自带的缓存器实现),注意使用内存缓存，需要T类型为可以序列化的类型；建议在实体类上面都加上可以序列化特性
    /// </summary>
    public class MemoryCacheManager : CacheManagerBase
    {
        /// <summary>
        /// Cache object
        /// </summary>
        protected ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public override T Get<T>(string key)
        {
            return (T)this.Cache[key];
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public override void Set(string key, object data, int cacheTime)
        {
            //为空的话，系统将直接返回，不进行设置缓存
            if (data.IsNull())
            {
                return;
            }

            //如果存在就先删除，然后添加新缓存
            if (this.IsSet(key))
            {
                this.Remove(key);
            }

            //绝对过期时间
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = Clock.Now + TimeSpan.FromMinutes(cacheTime),
                RemovedCallback = this.RemovedCallback
            };

            //设置缓存
            this.Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// 当缓存移除的时候触发
        /// </summary>
        /// <param name="arguments"></param>
        public void RemovedCallback(CacheEntryRemovedArguments arguments)
        {
            //ServicesContainer.Current.Resolve<ILogger>().Debug(arguments.CacheItem.Key);
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public override bool IsSet(string key)
        {
            return this.Cache.Contains(key);
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public override void Remove(string key)
        {
            this.Cache.Remove(key);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public override void RemoveByPattern(string pattern)
        {
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = (from item in this.Cache where regex.IsMatch(item.Key) select item.Key).ToList();
            foreach (string key in keysToRemove)
            {
                this.Remove(key);
            }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public override void Clear()
        {
            foreach (var item in this.Cache)
            {
                this.Remove(item.Key);
            }
        }

        /// <summary>
        /// 释放，直接释放掉所有的缓存键
        /// </summary>
        public void Dispose()
        {
            //this.Clear();
        }
    }
}