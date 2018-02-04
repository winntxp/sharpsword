using System;

namespace SharpSword.DistributedLock
{
    /// <summary>
    /// 分布式锁管理器接口
    /// </summary>
    public class NullDistributedLockerManager : IDistributedLockerManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static NullDistributedLockerManager _instance = new NullDistributedLockerManager();

        /// <summary>
        /// 
        /// </summary>
        public static NullDistributedLockerManager Instance => _instance;

        /// <summary>
        /// 
        /// </summary>
        private NullDistributedLockerManager() { }

        /// <summary>
        /// 获取锁并且执行
        /// </summary>
        /// <param name="resource">需要锁定的资源名称</param>
        /// <param name="ttl">超时时间：new TimeSpan(0, 0, 5)</param>
        /// <param name="lockedSuccessAction">获取锁执行的委托，封装业务逻辑</param>
        /// <param name="lockedFailAction">未获取锁的情况下执行此业务逻辑</param>
        public void Lock(string resource, TimeSpan ttl, Action lockedSuccessAction, Action lockedFailAction = null)
        {
            lockedSuccessAction();
        }
    }
}