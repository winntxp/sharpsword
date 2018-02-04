/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/17 12:27:22
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.SDK
{
    /// <summary>
    /// 缓存集合对象
    /// </summary>
    public interface IApiClientCacheManager
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存内容</param>
        /// <param name="cacheTime">缓存时间，相对应与当前时间的分钟数</param>
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>不存在获取到了绝对过期时间返回null</returns>
        CacheItem Get(string key);

        /// <summary>
        /// 获取所有的缓存键
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetAllKeys();
    }
}
