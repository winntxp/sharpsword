/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/6/12 17:51:30
 * ****************************************************************/
using SharpSword.Tasks;

namespace SharpSword.O2O.Tasks
{
    /// <summary>
    /// 此专业任务用来管理缓存，比如：商品，门店，等，每15分钟作业一次
    /// </summary>
    [TaskScheduler("CacheManagerTask", seconds: 60 * 15, enabled: true)]
    public class CacheManagerTask : IBackgroundTask
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger _logger;
        private readonly IMachineNameProvider _machineNameProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="machineNameProvider"></param>
        /// <param name="logger"></param>
        public CacheManagerTask(IMachineNameProvider machineNameProvider, ILogger<CacheManagerTask> logger)
        {
            this._machineNameProvider = machineNameProvider;
            this._logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskExecuteContext"></param>
        public void Execute(TaskExecuteContext taskExecuteContext)
        {
            //我们每隔一段将所有活动商品（已经过期的活动）从缓存里清理出去

            //1.线获取缓存里的商品缓存键，然后检测商品过期时间，如果已经过期，我们将缓存键删除 

        }
    }
}
