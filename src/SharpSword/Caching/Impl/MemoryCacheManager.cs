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
    /// ASP.NET�Դ��ڴ滺����(ϵͳ����Դ��Ļ�����ʵ��),ע��ʹ���ڴ滺�棬��ҪT����Ϊ�������л������ͣ�������ʵ�������涼���Ͽ������л�����
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
            //Ϊ�յĻ���ϵͳ��ֱ�ӷ��أ����������û���
            if (data.IsNull())
            {
                return;
            }

            //������ھ���ɾ����Ȼ������»���
            if (this.IsSet(key))
            {
                this.Remove(key);
            }

            //���Թ���ʱ��
            var policy = new CacheItemPolicy
            {
                AbsoluteExpiration = Clock.Now + TimeSpan.FromMinutes(cacheTime),
                RemovedCallback = this.RemovedCallback
            };

            //���û���
            this.Cache.Add(new CacheItem(key, data), policy);
        }

        /// <summary>
        /// �������Ƴ���ʱ�򴥷�
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
        /// �ͷţ�ֱ���ͷŵ����еĻ����
        /// </summary>
        public void Dispose()
        {
            //this.Clear();
        }
    }
}