/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/8/2016 10:36:20 AM
 * ****************************************************************/
using SharpSword.Timing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;

namespace SharpSword.Tasks
{
    /// <summary>
    /// 作业线程（每个线程可能包含多个作业任务）
    /// </summary>
    public class TaskThread : IDisposable
    {
        private Timer _timer;
        private bool _disposed;
        private readonly Dictionary<string, BackgroundTask> _tasks;

        /// <summary>
        /// 
        /// </summary>
        internal TaskThread()
        {
            this._tasks = new Dictionary<string, BackgroundTask>();
            this.Seconds = 10 * 60;
            this.Started = Clock.Now;
        }

        /// <summary>
        /// 真正执行作业线程里的所有作业任务
        /// </summary>
        private void Run()
        {
            if (Seconds <= 0)
                return;

            this.LastRuned = Clock.Now;
            this.IsRunning = true;

            foreach (BackgroundTask task in this._tasks.Values)
            {
                task.Execute(taskThread: this);
            }

            this.IsRunning = false;
        }

        /// <summary>
        /// timer每次触发的时候执行的委托方法
        /// </summary>
        /// <param name="state"></param>
        private void TimerHandler(object state)
        {
            //停止timer触发
            this._timer.Change(-1, -1);

            //执行作业任务
            this.Run();

            if (this.RunOnlyOnce)
            {
                this.Dispose();
            }
            else
            {
                this._timer.Change(this.Interval, this.Interval);
            }
        }

        /// <summary>
        /// 释放作业线程
        /// </summary>
        public void Dispose()
        {
            if ((this._timer != null) && !this._disposed)
            {
                lock (this)
                {
                    this._timer.Dispose();
                    this._timer = null;
                    this._disposed = true;
                }
            }
        }

        /// <summary>
        /// 初始化作业线程（创建timer）
        /// </summary>
        public void InitTimer()
        {
            if (this._timer == null)
            {
                this._timer = new Timer(new TimerCallback(this.TimerHandler), null, this.Interval, this.Interval);
            }
        }

        /// <summary>
        /// 添加一个封装后的作业任务到作业线程
        /// </summary>
        /// <param name="task">作业</param>
        public void AddTask(BackgroundTask task)
        {
            if (!this._tasks.ContainsKey(task.Name))
            {
                this._tasks.Add(task.Name, task);
            }
        }

        /// <summary>
        /// 作业执行间隔，单位：秒
        /// </summary>
        public int Seconds { get; set; }

        /// <summary>
        /// 作业线程启动时间，一旦启动器时间不会变化
        /// </summary>
        public DateTime Started { get; private set; }

        /// <summary>
        /// 最后一次执行作业任务时间
        /// </summary>
        public DateTime LastRuned { get; private set; }

        /// <summary>
        /// 显示当前作业线程是否正在执行作业
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// 获取所有作业任务列表（封装后的作业任务）
        /// </summary>
        public IList<BackgroundTask> Tasks
        {
            get
            {
                var list = new List<BackgroundTask>();
                foreach (var task in this._tasks.Values)
                {
                    list.Add(task);
                }
                return new ReadOnlyCollection<BackgroundTask>(list);
            }
        }

        /// <summary>
        /// 执行作业任务间隔，单位：毫秒，内部使用
        /// </summary>
        private int Interval
        {
            get
            {
                return this.Seconds * 1000;
            }
        }

        /// <summary>
        /// 作业任务线程是否值运行一次，而不重复间隔执行
        /// </summary>
        public bool RunOnlyOnce { get; set; }
    }
}
