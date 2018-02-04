/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword.TaskManagement
{
    /// <summary>
    /// 调试工具扩展
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
        public override string DisplayName
        {
            get { return "框架作业任务管理器"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string IndexUrl
        {
            get { return "/TaskManager"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int DisplayIndex
        {
            get { return 99; }
        }
    }
}
