/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/17 12:25:27
 * ****************************************************************/
using System;

namespace SharpSword.SDK
{
    /// <summary>
    /// SDK缓存对象
    /// </summary>
    [Serializable]
    public sealed class CacheItem
    {
        /// <summary>
        /// 缓存过期时间
        /// </summary>
        public DateTime ExpiredTime { get; set; }

        /// <summary>
        /// 缓存对象;尽量在定义data的时候，将对象增加以下可序列化特性（Serializable），防止以后需要序列化对象需要
        /// </summary>
        public object Data { get; set; }

    }
}
