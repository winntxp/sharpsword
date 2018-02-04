/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 17:05:48
 * ****************************************************************/
using SharpSword.Configuration;
using SharpSword.Configuration.WebConfig;
using System;

namespace SharpSword.DistributedLock.Redis
{
    /// <summary>
    /// 消息队列实现配置
    /// </summary>
    [ConfigurationSectionName("sharpsword.module.distributedlock.redis"), Serializable, FailReturnDefault]
    public class DistributedLockConfig : ConfigurationSectionHandlerBase, IDistributedLockConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public DistributedLockConfig() { }

        /// <summary>
        /// 连接字符串,多个请使用分开:"127.0.0.1:6380, 127.0.0.1:6381, 127.0.0.1:6382,127.0.0.1:6383"
        /// </summary>
        public string RedisServers { get; set; } = "127.0.0.1:6379";

        /// <summary>
        /// 
        /// </summary>
        string[] IDistributedLockConfig.RedisServers => this.RedisServers
                                                            .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
    }
}
