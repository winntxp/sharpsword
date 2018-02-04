/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/12/2016 9:00:09 AM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.Tasks
{
    /// <summary>
    /// 作业任务调度器集合类
    /// </summary>
    public interface ITaskSchedulerCollection
    {
        /// <summary>
        /// 注册一个作业任务调度器，实现的时候，直接返回当前调度器集合，方便链式调用
        /// </summary>
        /// <param name="taskScheduler">调度器</param>
        void Register(TaskScheduler taskScheduler);

        /// <summary>
        /// 更新一个作业任务调度器
        /// </summary>
        /// <param name="taskScheduler"></param>
        void Update(TaskScheduler taskScheduler);

        /// <summary>
        /// 根据调度器名称删除调度器
        /// </summary>
        /// <param name="name">调度器名称</param>
        void Remove(string name);

        /// <summary>
        /// 获取作业任务调度器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        TaskScheduler Get(string name);
   
        /// <summary>
        /// 获取所有的调度器
        /// </summary>
        /// <returns></returns>
        IEnumerable<TaskScheduler> GetAll();
    }
}
