/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword.DynamicApi
{
    /// <summary>
    /// 插件描述信息
    /// </summary>
    [Serializable]
    public class PluginDescriptor : PluginDescriptorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceFinderManager">资源查找器</param>
        public PluginDescriptor(IResourceFinderManager resourceFinderManager)
            : base(resourceFinderManager)
        {
        }

        /// <summary>
        /// 友好的显示名称
        /// </summary>
        public override string DisplayName
        {
            get { return "动态接口(DynamicApi)"; }
        }
    }
}
