/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/28 14:38:30
 * ****************************************************************/

namespace SharpSword.Tasks
{
    /// <summary>
    /// 调度器管理器
    /// </summary>
    public class TaskSchedulerManager
    {
        /// <summary>
        /// 保存作业任务调度器
        /// </summary>
        private static readonly TaskSchedulerCollection Instance = new TaskSchedulerCollection();

        /// <summary>
        /// 获取调度器列表
        /// </summary>
        public static ITaskSchedulerCollection TaskSchedulers
        {
            get
            {
                return Instance;
            }
        }
    }
}
