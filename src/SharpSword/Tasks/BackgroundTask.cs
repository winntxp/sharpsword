/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/8/2016 10:35:35 AM
 * ****************************************************************/
using SharpSword.Timing;
using System;

namespace SharpSword.Tasks
{
    /// <summary>
    /// 作业任务封装类
    /// </summary>
    public class BackgroundTask
    {
        /// <summary>
        /// 作业任务调度器
        /// </summary>
        private TaskScheduler _taskScheduler;

        /// <summary>
        /// Ctor for Task
        /// </summary>
        /// <param name="taskScheduler">作业任务调度器</param>
        public BackgroundTask(TaskScheduler taskScheduler)
        {
            this._taskScheduler = taskScheduler;
            this.Name = taskScheduler.Name;
            this.Enabled = taskScheduler.Enabled;
            this.StopOnError = taskScheduler.StopOnError;
            this.Seconds = taskScheduler.Seconds;
            this.TaskType = taskScheduler.TaskType;
            this.RunOnOneWebFarmInstance = taskScheduler.RunOnOneWebFarmInstance;
        }

        /// <summary>
        /// 执行作业任务
        /// </summary>
        /// <param name="taskThread">当前作业任务工作线程</param>
        /// <param name="throwException">遇到错误是否直接抛出异常</param>
        /// <param name="dispose">执行完作业任务后，是否释放资源</param>
        public void Execute(TaskThread taskThread = null, bool throwException = false, bool dispose = true)
        {
            //对象生命作用域
            var scope = ServicesContainer.Current.Scope();

            //获取分布式协调器
            var distributedLocker = ServicesContainer.Current
                .Resolve<ITaskSchedulerDistributedLocker>(scope);

            //获取协调器保存的调度信息
            var taskSchedulerLocker = distributedLocker.Get(this.Name) ??
                new TaskSchedulerDistributedLockerObject()
                {
                    TaskSchedulerName = null,
                    LeasedUntil = new DateTime(1900, 1, 1),
                    LeasedByMachineName = null
                };

            try
            {
                //检测是否只能一个运行实例可以作业
                if (this.RunOnOneWebFarmInstance)
                {
                    //获取当前运行实例信息
                    var machineNameProvider = ServicesContainer.Current.Resolve<IMachineNameProvider>(scope);
                    var machineName = machineNameProvider.GetMachineName();

                    //必须要存在实例
                    if (String.IsNullOrEmpty(machineName))
                    {
                        throw new SharpSwordCoreException("运行实例名称必须存在");
                    }

                    //调度器被其他运行实例锁住了
                    if (taskSchedulerLocker.LeasedUntil.HasValue
                        && taskSchedulerLocker.LeasedUntil.Value >= Clock.Now
                        && taskSchedulerLocker.LeasedByMachineName != machineName)
                        return;

                    //没有设置过期时间，或者租期已经超期，重新竞争设置
                    if (taskSchedulerLocker.LeasedUntil.Value < Clock.Now)
                    {
                        //设置租期 10 分钟，太长的话怕实例出现问题，其他实例一致没有机会去执行
                        //也就是说，在分布式的情况下，一个作业任务最大出现问题的机会只会有30分钟
                        //这样防止作业任务一致不起作用
                        DateTime leasedUntil = Clock.Now.AddMinutes(30);
                        //构造一个锁对象
                        taskSchedulerLocker = new TaskSchedulerDistributedLockerObject()
                        {
                            LeasedUntil = leasedUntil,
                            TaskSchedulerName = this.Name,
                            LeasedByMachineName = machineName
                        };
                        //锁定当前运行实例为此任务调度器执行宿主
                        distributedLocker.Lock(taskSchedulerLocker);
                        //设置当前属性
                        this.LeasedUntil = leasedUntil;
                        this.LeasedByMachineName = machineName;
                    }
                }

                //当前正在作业
                this.IsRunning = true;

                //获取作业任务类型
                Type taskType = Type.GetType(this._taskScheduler.TaskType);

                //创建作业任务实例
                var instance = (IBackgroundTask)ServicesContainer.Current.ResolveUnregistered(taskType, scope);

                //执行作业任务
                if (!instance.IsNull())
                {
                    instance.Execute(new TaskExecuteContext(this._taskScheduler, taskThread));
                }

            }
            catch (Exception exc)
            {
                //遇到错误是否不继续执行
                this.Enabled = !this.StopOnError;

                //log error
                var logger = NullLogger.Instance;

                object _logger = null;
                if (ServicesContainer.Current.TryResolve(typeof(ILogger), scope, out _logger))
                {
                    logger = (ILogger)_logger;
                }

                logger.Error(string.Format("运行调度错误: '{0}' 错误信息: {1}", this.Name, exc.StackTrace), exc);

                if (throwException)
                {
                    throw;
                }
            }
            finally
            {
                this.IsRunning = false;
            }

            //dispose all resources
            if (dispose)
            {
                scope.Dispose();
            }
        }

        /// <summary>
        /// 当前作业任务是否正在执行中....
        /// </summary>
        public bool IsRunning { get; private set; }

        /// <summary>
        /// 遇到错误是否定制作业任务执行
        /// </summary>
        public bool StopOnError { get; private set; }

        /// <summary>
        /// 获取作业任务名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 获取当前任务是否允许运行
        /// </summary>
        public bool Enabled { get; private set; }

        /// <summary>
        /// 多少秒执行一次
        /// </summary>
        public int Seconds { get; private set; }

        /// <summary>
        /// IBackgroundTask作业任务的类型
        /// </summary>
        public string TaskType { get; private set; }

        /// <summary>
        /// 在分布式站点中，确保一个作业任务只运行一个实例，防止并发重复作业
        /// </summary>
        public bool RunOnOneWebFarmInstance { get; private set; }

        /// <summary>
        /// 作业调度器再特定运行实例里运行的到期时间
        /// 租约到期时间(一般默认租约到期时间为30天)
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
}
