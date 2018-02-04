/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/8/2016 10:36:07 AM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace SharpSword.Tasks
{
    /// <summary>
    /// 作业任务管理器
    /// </summary>
    public class TaskThreadManager
    {
        private static readonly TaskThreadManager _taskManager = new TaskThreadManager();
        private readonly List<TaskThread> _taskThreads = new List<TaskThread>();

        /// <summary>
        /// 
        /// </summary>
        private TaskThreadManager() { }

        /// <summary>
        /// 初始化所有作业任务
        /// </summary>
        public void Initialize(ITypeFinder typeFinder)
        {
            //先清空先当前保存的作业任务
            this._taskThreads.Clear();

            //下面的1，2顺序，请不要更改，因为需要在外部通过手工的方式来覆盖作业任务定义的特性作业任务调度器
            //因为一旦我们使用特性的方式在作业任务上定义了特性调度器，发布后，如果我们需要变更，暂停执业任务，
            //我们需要修改源代码来实现调度器，因此我们可以通过过程2来新建扩展，来更新或者覆盖原来的调度器配置

            //1.在获取所有实现了ITask类的实现类
            var taskTypes = typeFinder.FindClassesOfType<IBackgroundTask>()
                .Where(type => !type.IsAbstract && type.IsDefined(typeof(TaskSchedulerAttribute), false))
                .ToList();

            foreach (var taskType in taskTypes)
            {
                //如果作业任务定义了特性调度器，那么就添加到全局调度器集合进行作业任务执行
                var taskSchedulerAttributes = taskType.GetCustomAttributes(typeof(TaskSchedulerAttribute), false)
                    .Cast<TaskSchedulerAttribute>();
                foreach (var taskSchedulerAttribute in taskSchedulerAttributes)
                {
                    TaskSchedulerManager.TaskSchedulers.Register(
                        name: taskSchedulerAttribute.Name,
                        taskType: taskType,
                        seconds: taskSchedulerAttribute.Seconds,
                        enabled: taskSchedulerAttribute.Enabled,
                        stopOnError: taskSchedulerAttribute.StopOnError,
                        runOnOneWebFarmInstance:
                        taskSchedulerAttribute.RunOnOneWebFarmInstance);
                }
            }

            //2.先注册所有调度器
            typeFinder.FindClassesOfType<ITaskSchedulerRegistar>()
                .Select(type => (ITaskSchedulerRegistar)Activator.CreateInstance(type))
                .OrderBy(o => o.Order)
                .ToList().ForEach(a => { a.Register(TaskSchedulerManager.TaskSchedulers); });

            //获取所有注册的调度器
            var taskSchedulers = TaskSchedulerManager.TaskSchedulers.GetAll();

            //把相同的执行时间间隔作业任务归组
            foreach (var taskDescriptorGroup in taskSchedulers.OrderBy(o => o.Seconds).GroupBy(x => x.Seconds))
            {
                //初始化一个作业线程类，保存所有相同的执行时间
                var taskThread = new TaskThread
                {
                    Seconds = taskDescriptorGroup.Key
                };

                //将相同执行间隔时间的作业任务添加到同一作业线程
                foreach (var taskDescriptor in taskDescriptorGroup)
                {
                    taskThread.AddTask(new BackgroundTask(taskDescriptor));
                }

                //添加作业线程
                this._taskThreads.Add(taskThread);
            }
        }

        /// <summary>
        /// 启动TaskManager
        /// </summary>
        public void Start()
        {
            foreach (var taskThread in this._taskThreads)
            {
                taskThread.InitTimer();
            }
        }

        /// <summary>
        /// 停止TaskManager
        /// </summary>
        public void Stop()
        {
            foreach (var taskThread in this._taskThreads)
            {
                taskThread.Dispose();
            }
        }

        /// <summary>
        /// 获取TaskManager实例
        /// </summary>
        public static TaskThreadManager Instance
        {
            get
            {
                return _taskManager;
            }
        }

        /// <summary>
        /// 获取所有的作业线程
        /// </summary>
        public IEnumerable<TaskThread> TaskThreads
        {
            get
            {
                return new ReadOnlyCollection<TaskThread>(this._taskThreads);
            }
        }
    }
}
