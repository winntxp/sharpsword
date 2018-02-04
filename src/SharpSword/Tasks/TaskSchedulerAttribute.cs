/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/21/2016 2:49:29 PM
 * ****************************************************************/
using System;

namespace SharpSword.Tasks
{
    /// <summary>
    /// 作业任务调度器配置特性，作用于实现了ITask的具体作业任务
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class TaskSchedulerAttribute : Attribute
    {
        /// <summary>
        /// 调度器友好的名称(全局唯一)
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 多少秒执行一次
        /// </summary>
        public int Seconds { get; private set; }

        /// <summary>
        /// 调度器是否启用
        /// </summary>
        public bool Enabled { get; private set; }

        /// <summary>
        /// 当遇到错误的时候，是否停止执行
        /// </summary>
        public bool StopOnError { get; private set; }

        /// <summary>
        /// 在分布式站点中，确保一个作业任务只运行一个实例，防止并发重复作业
        /// </summary>
        public bool RunOnOneWebFarmInstance { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">调度器友好的名称(全局唯一)</param>
        /// <param name="seconds">多少秒执行一次</param>
        /// <param name="enabled">调度器是否启用</param>
        /// <param name="stopOnError">当遇到错误的时候，是否停止执行</param>
        /// <param name="runOnOneWebFarmInstance">在分布式站点中，确保一个作业任务只运行一个实例，防止并发重复作业</param>
        public TaskSchedulerAttribute(string name, int seconds = 30 * 60, bool enabled = true, bool stopOnError = false, bool runOnOneWebFarmInstance = false)
        {
            this.Name = name;
            this.Seconds = seconds;
            this.Enabled = enabled;
            this.StopOnError = stopOnError;
            this.RunOnOneWebFarmInstance = runOnOneWebFarmInstance;
        }
    }
}
