/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/26 11:59:08
 * ****************************************************************/
using StackExchange.Redis.Extensions.Core;
using System;
using System.Collections.Generic;

namespace SharpSword.Caching.Redis.StackExchange
{
    /// <summary>
    /// StackExchange实现
    /// </summary>
    public class RedisCacheManager : CacheManagerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ICacheClient _cacheClient;

        /// <summary>
        /// Redis缓存服务器客户端实现
        /// </summary>
        /// <param name="cacheClient"></param>
        public RedisCacheManager(ICacheClient cacheClient)
        {
            this._cacheClient = cacheClient;
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Clear()
        {
            this._cacheClient.FlushDb();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public override T Get<T>(string key)
        {
            return this._cacheClient.Get<T>(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public override bool IsSet(string key)
        {
            return this._cacheClient.Exists(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public override void Remove(string key)
        {
            this._cacheClient.Remove(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        public override void RemoveByPattern(string pattern)
        {
            var keys = this._cacheClient.SearchKeys("*{0}*".With(pattern));
            foreach (var key in keys)
            {
                this.Remove(key);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        public override void Set(string key, object data, int cacheTime)
        {
            if (null == data)
            {
                return;
            }

            //如果存在就先删除，然后添加新缓存
            if (this.IsSet(key))
            {
                this.Remove(key);
            }

            //保存到缓存
            this._cacheClient.Add(key, data, DateTimeOffset.Now.AddMinutes(cacheTime));
        }

        /// <summary>
        /// 获取服务器信息
        /// </summary>
        internal IDictionary<string, string> GetServerInformation()
        {
            return this._cacheClient.GetInfo();
        }

        /// <summary>
        /// 获取缓存键集合
        /// </summary>
        /// <param name="pattern">搜索模式，如：*Sys, *Sys*</param>
        /// <returns></returns>
        internal IEnumerable<string> GetKeys(string pattern)
        {
            return this._cacheClient.SearchKeys(pattern.IsNullOrEmptyForDefault(() => "*", (key) => pattern));
        }
    }
}
