/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/18/2017 10:54:29 AM
 * ****************************************************************/
using StackExchange.Redis;
using System.Linq;
using System;

namespace SharpSword.DistributedLock.Redis
{
    /// <summary>
    /// 分布式锁管理器，注意：此管理器调用需要设置成单例模式
    /// </summary>
    public class DistributedLockerManager : IDistributedLockerManager
    {
        /// <summary>
        /// REDIS服务器设置多个单实例，这样方式单机故障
        /// </summary>
        private RedisLock redlock;

        /// <summary>
        /// 分布式锁管理器，注意：此管理器调用需要设置成单例模式
        /// </summary>
        /// <param name="redisServers">
        /// redis服务器地址，如：127.0.0.1:6379，为了防止单点故障，
        /// 一般配置成多台单独redis锁服务器（建议5台，只要其中三台可用，分布式锁就可以正常工作，当然如果不考虑单点故障，我们设置一台效率是最高的）
        /// </param>
        public DistributedLockerManager(params string[] redisServers)
        {
            var connectionMultiplexers = redisServers.Select(s => ConnectionMultiplexer.Connect(s)).ToArray();
            redlock = new RedisLock(connectionMultiplexers);
        }

        /// <summary>
        /// 获取锁并且执行
        /// </summary>
        /// <param name="resource">需要锁定的资源名称</param>
        /// <param name="ttl">超时时间：new TimeSpan(0, 0, 5)</param>
        /// <param name="lockedSuccessAction">获取锁执行的委托，封装业务逻辑</param>
        /// <param name="lockedFailAction">未获取锁的情况下执行此业务逻辑</param>
        public void Lock(string resource, TimeSpan ttl, Action lockedSuccessAction, Action lockedFailAction = null)
        {
            Lock lockObject;

            //先需要获取分布式锁，相当于先拿到票再进行操作
            var locked = redlock.Lock(resource, ttl, out lockObject);

            //取到了票据
            if (locked)
            {
                try
                {
                    lockedSuccessAction?.Invoke();
                }
                catch
                {
                    throw;
                }
                finally
                {
                    redlock.Unlock(lockObject);
                }
            }
            else
            {
                lockedFailAction?.Invoke();
            }
        }
    }
}
