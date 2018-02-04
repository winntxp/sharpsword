/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/14/2016 4:35:23 PM
 * ****************************************************************/
 using System;

namespace SharpSword.Tasks.Impl
{
    /// <summary>
    /// 默认的分布式协调器
    /// </summary>
    internal class DefaultTaskSchedulerDistributedLocker : ITaskSchedulerDistributedLocker
    {
        /// <summary>
        /// 用于保存分布式锁的key
        /// </summary>
        private const string Key = "Task.Scheduler.Distributed.Locker.{0}";

        /// <summary>
        /// 缓存器
        /// </summary>
        private ICacheManager _cacheManager;

        /// <summary>
        /// 默认使用缓存来保存锁定
        /// </summary>
        /// <param name="cacheManager">缓存器必须为单独的公共缓存服务器，不能使用单独的内存缓存器</param>
        public DefaultTaskSchedulerDistributedLocker(ICacheManager cacheManager)
        {
            this._cacheManager = cacheManager;
        }

        /// <summary>
        /// 获取分布式作业任务锁
        /// </summary>
        /// <param name="taskSchedulerName">作业任务调度器名称</param>
        /// <returns></returns>
        public TaskSchedulerDistributedLockerObject Get(string taskSchedulerName)
        {
            //return this._cacheManager.Get<TaskSchedulerDistributedLockerObject>(Key.With(taskSchedulerName), () => null);
            return null;
        }

        /// <summary>
        /// 设置分布式作业任务锁
        /// </summary>
        /// <param name="locker"></param>
        public void Lock(TaskSchedulerDistributedLockerObject locker)
        {
            //this._cacheManager.Set(Key.With(locker.TaskSchedulerName), locker, 60);
        }
    }
}
