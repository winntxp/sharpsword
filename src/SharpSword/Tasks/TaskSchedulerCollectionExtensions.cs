/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/12/2016 9:00:09 AM
 * ****************************************************************/
using System;

namespace SharpSword.Tasks
{
    /// <summary>
    /// 作业任务调度器集合类扩展
    /// </summary>
    public static class TaskSchedulerCollectionExtensions
    {
        /// <summary>
        /// 注册一个作业任务调度器
        /// </summary>
        /// <param name="taskSchedulerCollection">调度器集合</param>
        /// <param name="name">调度名称</param>
        /// <param name="taskType">调度器关联的作业任务类型</param>
        /// <param name="seconds">间隔多少秒执行一次，默认30分钟</param>
        /// <param name="enabled">是否启用</param>
        /// <param name="stopOnError">遇到错误是否停止改作业任务</param>
        /// <param name="runOnOneWebFarmInstance">是否值运转在一个实例上</param>
        /// <returns></returns>
        public static ITaskSchedulerCollection Register(
            this ITaskSchedulerCollection taskSchedulerCollection,
            string name,
            Type taskType,
            int seconds = 30 * 60,
            bool enabled = true,
            bool stopOnError = false,
            bool runOnOneWebFarmInstance = false)
        {
            taskSchedulerCollection.CheckNullThrowArgumentNullException("taskSchedulerCollection");
            taskSchedulerCollection.Register(new TaskScheduler()
            {
                Name = name,
                Enabled = enabled,
                Seconds = seconds,
                StopOnError = stopOnError,
                TaskType = "{0},{1}".With(taskType.FullName, taskType.Assembly.GetName().Name),
                RunOnOneWebFarmInstance = runOnOneWebFarmInstance
            });

            return taskSchedulerCollection;
        }

        /// <summary>
        /// 注册一个作业任务调度器
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="taskSchedulerCollection">调度器集合</param>
        /// <param name="name">调度名称</param>
        /// <param name="seconds">间隔多少秒执行一次，默认30分钟</param>
        /// <param name="enabled">是否启用</param>
        /// <param name="stopOnError">遇到错误是否停止改作业任务</param>
        /// <param name="runOnOneWebFarmInstance">是否值运转在一个实例上</param>
        /// <returns></returns>
        public static ITaskSchedulerCollection Register<T>(
            this ITaskSchedulerCollection taskSchedulerCollection,
            string name,
            int seconds = 30 * 60,
            bool enabled = true,
            bool stopOnError = false,
            bool runOnOneWebFarmInstance = false) where T : IBackgroundTask
        {
            taskSchedulerCollection.Register(name, typeof(T), seconds, enabled, stopOnError, runOnOneWebFarmInstance);
            return taskSchedulerCollection;
        }

        /// <summary>
        /// 更新作业任务调度器
        /// </summary>
        /// <param name="taskSchedulerCollection">调度器集合</param>
        /// <param name="name">调度器名称</param>
        /// <param name="taskType">调度器类型</param>
        /// <param name="seconds">执行间隔，单位：秒</param>
        /// <param name="enabled">是否启用</param>
        /// <param name="stopOnError">遇到错误是否停止改作业任务</param>
        /// <param name="runOnOneWebFarmInstance">是否值运转在一个实例上</param>
        /// <returns></returns>
        public static ITaskSchedulerCollection Update(
            this ITaskSchedulerCollection taskSchedulerCollection,
            string name,
            Type taskType = null,
            int? seconds = null,
            bool? enabled = null,
            bool? stopOnError = null,
            bool? runOnOneWebFarmInstance = null)
        {
            taskSchedulerCollection.CheckNullThrowArgumentNullException(nameof(taskSchedulerCollection));
            var taskScheduler = taskSchedulerCollection.Get(name);
            if (taskScheduler.IsNull())
            {
                return taskSchedulerCollection;
            }

            taskSchedulerCollection.Update(new TaskScheduler()
            {
                Name = name,
                TaskType = taskType.IsNull() ? taskScheduler.TaskType : taskType.FullName,
                Enabled = enabled ?? taskScheduler.Enabled,
                Seconds = seconds ?? taskScheduler.Seconds,
                StopOnError = stopOnError ?? taskScheduler.StopOnError,
                RunOnOneWebFarmInstance = runOnOneWebFarmInstance ?? taskScheduler.RunOnOneWebFarmInstance
            });

            return taskSchedulerCollection;
        }
    }
}
