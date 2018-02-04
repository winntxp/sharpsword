/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/12/2016 11:32:46 AM
 * ****************************************************************/

namespace SharpSword.Tasks
{
    /// <summary>
    /// 此接口为作业任务调度器系统自动注册接口，系统框架会自动寻找实现此接口的所有类，然后进行注册
    /// </summary>
    public interface ITaskSchedulerRegistar
    {
        /// <summary>
        /// 优先级，优先级越高在执行的时候，会后注册，用于覆盖前面的注册（比如更新操作）
        /// </summary>
        int Order { get; }

        /// <summary>
        /// 此方法实现模块注册，系统框架会自动调用此方法就行注册
        /// </summary>
        void Register(ITaskSchedulerCollection taskSchedulerCollection);
    }
}
