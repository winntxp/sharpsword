/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/12/2016 11:39:18 AM
 * ****************************************************************/
using SharpSword.Tasks;

namespace SharpSword.O2O.Tasks
{
    /// <summary>
    /// 注册作业任务调度
    /// </summary>
    public class TaskSchedulerRegistar : ITaskSchedulerRegistar
    {
        /// <summary>
        /// 优先级
        /// </summary>
        public int Order { get { return 0; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskSchedulerCollection"></param>
        public void Register(ITaskSchedulerCollection taskSchedulerCollection)
        {
            //更新系统作业任务管理器，将系统框架默认的30分钟改成1秒钟
            //TaskSchedulerManager.TaskSchedulers.Update(name: "SYS.KeyAliveTask", seconds: 10);

            ////注册
            //TaskSchedulerManager.TaskSchedulers.Register<Tasks.TestTask>(name: "SYS.TestTask", seconds: 2);
            //TaskSchedulerManager.TaskSchedulers.Register<Tasks.TestTask>(name: "SYS.TestTask_01", seconds: 5, runOnOneWebFarmInstance: true);
            //TaskSchedulerManager.TaskSchedulers.Register<Tasks.TestTask>(name: "SYS.TestTask_02", seconds: 5, runOnOneWebFarmInstance: true);
            //TaskSchedulerManager.TaskSchedulers.Register<Tasks.TestTask>(name: "SYS.TestTask_03", seconds: 5, runOnOneWebFarmInstance: true);
            //TaskSchedulerManager.TaskSchedulers.Register<Tasks.TestTask>(name: "SYS.TestTask_04", seconds: 5, runOnOneWebFarmInstance: true);
            //TaskSchedulerManager.TaskSchedulers.Register<Tasks.TestTask>(name: "清理内存数据库", seconds: 56);
        }
    }
}
