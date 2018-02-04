/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/14/2016 3:36:32 PM
 * ****************************************************************/
using System;

namespace SharpSword.Tasks
{
    /// <summary>
    /// 用于分布式锁
    /// </summary>
    [Serializable]
    public class TaskSchedulerDistributedLockerObject
    {
        /// <summary>
        /// 作业任务调度器名称，全局唯一
        /// </summary>
        public string TaskSchedulerName { get; set; }

        /// <summary>
        /// 作业调度器再特定运行实例里运行的到期时间
        /// 租约到期时间(默认时间为10分钟，具体实现请看：Task.cs类)
        /// </summary>
        public DateTime? LeasedUntil { get; set; }

        /// <summary>
        /// 作业调度器再特东运行实例里运行的实例名称
        /// 租约机器（在分布式执行的时候，因为每个IIS都会有一套还行任务，
        /// 因此我们先签约第一个执行的机器来执行，其他分布式机器比较此租
        /// 约机器名称来判断是否执行）
        /// </summary>
        public string LeasedByMachineName { get; set; }
    }

    /// <summary>
    /// 此接口用于分布式作业任务锁接口，当我们将应用程序部署在多台机上
    /// 或者IIS站点启动多进行工作模式的时候，我们需要一个机制，
    /// 确认某些作业任务只工作在一台进程里，所以需要一个分布式锁来确认正常工作
    /// 因此实现此类的外部保存环境必须能够被所有运行实例访问，不能放置于运行实例缓存里面等
    /// </summary>
    public interface ITaskSchedulerDistributedLocker
    {
        /// <summary>
        /// 获取调度器分布式锁对象信息
        /// </summary>
        /// <param name="taskSchedulerName">作业任务调度器名称</param>
        TaskSchedulerDistributedLockerObject Get(string taskSchedulerName);

        /// <summary>
        /// 设置调度器分布式锁
        /// </summary>
        /// <param name="locker"></param>
        void Lock(TaskSchedulerDistributedLockerObject locker);
    }
}
