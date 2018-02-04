/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/12/2016 11:39:18 AM
 * ****************************************************************/
using SharpSword.Tasks;

namespace SharpSword.WebApi.Tasks
{
    /// <summary>
    /// 注册作业任务调度器
    /// </summary>
    public class TaskSchedulerRegistar : ITaskSchedulerRegistar
    {
        /// <summary>
        /// 默认最小，方便后面插件有机会修改
        /// </summary>
        public int Order => int.MinValue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskSchedulerCollection"></param>
        public void Register(ITaskSchedulerCollection taskSchedulerCollection)
        {
            //注册下作业任务
            taskSchedulerCollection.Register<KeepAliveTask>(name: "SYS.KeyAliveTask", seconds: 60, stopOnError: false);
        }
    }
}
