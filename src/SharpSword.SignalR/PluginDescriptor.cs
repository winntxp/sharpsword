/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword.SignalR
{
    [Serializable]
    public class PluginDescriptor : PluginDescriptorBase
    {
        public PluginDescriptor(IResourceFinderManager resourceFinderManager)
            : base(resourceFinderManager)
        {
        }

        public override string DisplayName
        {
            get { return "基于SignalR实时通讯"; }
        }
    }
}
