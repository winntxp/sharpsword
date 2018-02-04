/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/12/2016 10:06:15 AM
 * ****************************************************************/

namespace SharpSword.Tasks
{
    /// <summary>
    /// 作业任务执行上下文
    /// </summary>
    public class TaskExecuteContext
    {
        /// <summary>
        /// 当前作业任务依赖的调度器
        /// </summary>
        public TaskScheduler TaskScheduler { get; private set; }

        /// <summary>
        /// 执行点作业任务的作业线程，可能为null，所以在作业任务实现类里使用需要先判断下是否未null
        /// </summary>
        public TaskThread TaskThread { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskScheduler"></param>
        /// <param name="taskThread"></param>
        public TaskExecuteContext(TaskScheduler taskScheduler, TaskThread taskThread)
        {
            this.TaskScheduler = taskScheduler;
            this.TaskThread = taskThread;
        }
    }
}
