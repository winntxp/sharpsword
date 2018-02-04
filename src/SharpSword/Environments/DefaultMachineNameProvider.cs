/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/6/13 9:50:51
 * ****************************************************************/
using System;
using System.Diagnostics;

namespace SharpSword.Environments
{
    /// <summary>
    /// 系统框架默认的运行实例名称获取器实现
    /// </summary>
    public class DefaultMachineNameProvider : IMachineNameProvider
    {
        /// <summary>
        /// 获取当前应用实例的机器名称
        /// </summary>
        public string GetMachineName()
        {
            //在同一台物理机上面可能会运行多个虚拟站点(比如web园)，因此我们使用站点的进程ID作为标识
            return string.Format("{0}:{1}", Environment.MachineName, Process.GetCurrentProcess().Id);
        }
    }
}
