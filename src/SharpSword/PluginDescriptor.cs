/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class PluginDescriptor : PluginDescriptorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceFinderManager"></param>
        public PluginDescriptor(IResourceFinderManager resourceFinderManager) : base(resourceFinderManager) { }

        /// <summary>
        /// 
        /// </summary>
        public override string DisplayName => "SharpSword核心模块";

        /// <summary>
        /// 
        /// </summary>
        public override int DisplayIndex => int.MaxValue;
    }
}
