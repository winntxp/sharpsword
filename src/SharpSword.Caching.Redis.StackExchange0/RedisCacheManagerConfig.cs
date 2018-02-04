/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/24/2016 12:35:00 PM
 * ****************************************************************/
using SharpSword.Configuration.WebConfig;

namespace SharpSword.Caching.Redis.StackExchange
{
    /// <summary>
    /// 
    /// </summary>
    [ConfigurationSectionName("sharpsword.module.cachemanager.redis")]
    public class RedisCacheManagerConfig : ConfigurationSectionHandlerBase
    {
        /// <summary>
        /// REDIS服务器连接: 127.0.0.1:6379,allowAdmin = true
        /// </summary>
        public string ConnectionString { get; set; }

    }
}
