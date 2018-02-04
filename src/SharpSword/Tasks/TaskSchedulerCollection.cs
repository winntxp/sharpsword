/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/12/2016 9:00:09 AM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.Tasks
{
    /// <summary>
    /// 作业任务调度器集合类
    /// </summary>
    internal class TaskSchedulerCollection : List<TaskScheduler>, ITaskSchedulerCollection
    {
        /// <summary>
        /// 获取任务调度器
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public TaskScheduler Get(string name)
        {
            return this.FirstOrDefault(o => o.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 获取所有注册的任务调度器
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TaskScheduler> GetAll()
        {
            return this;
        }

        /// <summary>
        /// 注册一个任务调度器
        /// </summary>
        /// <param name="taskScheduler"></param>
        public void Register(TaskScheduler taskScheduler)
        {
            taskScheduler.CheckNullThrowArgumentNullException(nameof(taskScheduler));
            taskScheduler.Name.CheckNullThrowArgumentNullException(nameof(taskScheduler.Name));
            taskScheduler.TaskType.CheckNullThrowArgumentNullException(nameof(taskScheduler.TaskType));

            //获取作业任务实现类的类型
            Type taskType = Type.GetType(taskScheduler.TaskType);
            taskType.CheckNullThrowArgumentNullException(nameof(taskScheduler.TaskType));

            //类型必须实现ITask接口
            if (!typeof(IBackgroundTask).IsAssignableFrom(taskType))
            {
                throw new SharpSwordCoreException("注册的作业类型必须要实现ITask接口");
            }

            if (this.IsRegisted(taskScheduler.Name))
            {
                throw new SharpSwordCoreException("已经存在了名称为：{0} 的调度器".With(taskScheduler.Name));
            }

            //add
            this.Add(taskScheduler);
        }

        /// <summary>
        /// 删除作业任务调度
        /// </summary>
        /// <param name="name">调度器名称</param>
        public void Remove(string name)
        {
            var item = this.Get(name);
            if (!item.IsNull())
            {
                ((List<TaskScheduler>)this).Remove(item);
            }
        }

        /// <summary>
        /// 更新作业任务调度器
        /// </summary>
        /// <param name="taskScheduler"></param>
        public void Update(TaskScheduler taskScheduler)
        {
            taskScheduler.CheckNullThrowArgumentNullException(nameof(taskScheduler));
            taskScheduler.Name.CheckNullThrowArgumentNullException(nameof(taskScheduler.Name));

            //是否存在调度器
            var item = this.Get(taskScheduler.Name);

            //存在就更新
            if (!item.IsNull())
            {
                item.Seconds = taskScheduler.Seconds;
                item.Enabled = taskScheduler.Enabled;
                item.TaskType = taskScheduler.TaskType;
                item.StopOnError = taskScheduler.StopOnError;
                item.RunOnOneWebFarmInstance = taskScheduler.RunOnOneWebFarmInstance;
            }
        }

        /// <summary>
        /// 检测任务调度器是否重复添加
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsRegisted(string name)
        {
            return !this.Get(name).IsNull();
        }
    }
}
