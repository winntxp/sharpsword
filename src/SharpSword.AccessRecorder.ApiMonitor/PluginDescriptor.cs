/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword.AccessRecorder.ApiMonitor
{
    /// <summary>
    /// 接口日志记录器扩展
    /// </summary>
    [Serializable]
    public class PluginDescriptor : PluginDescriptorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceFinderManager"></param>
        public PluginDescriptor(IResourceFinderManager resourceFinderManager)
            : base(resourceFinderManager)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override string DisplayName => "接口访问日志记录组件（输出到监控控制台）";

    }
}
