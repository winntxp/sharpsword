/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 17:05:48
 * ****************************************************************/

namespace SharpSword.DistributedLock.Redis
{
    /// <summary>
    /// 用于适配DistributedLockConfig
    /// </summary>
    internal interface IDistributedLockConfig
    {
        /// <summary>
        /// 获取redis服务器列表
        /// </summary>
        string[] RedisServers { get; }
    }
}
