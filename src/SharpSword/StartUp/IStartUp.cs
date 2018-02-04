/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/16 15:23:12
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 用于系统初始化，此接口会在系统启动的时候自动进行初始化，全局只会执行一次
    /// 注意：此接口不会自动进行属性注入，需要注入的请使用构造函数进行注入
    /// </summary>
    public interface IStartUp
    {
        /// <summary>
        /// 需要在系统启动的时候初始化方法，启动时会自动执行此方法，IOC容器注册完毕后执行
        /// </summary>
        void Init();

        /// <summary>
        /// 优先级，数字越大越先启动
        /// </summary>
        int Priority { get; }
    }
}
