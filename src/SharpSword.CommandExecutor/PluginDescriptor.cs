/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword.CommandExecutor
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
            get { return "系统框架Command命令行执行器"; }
        }

        public override string IndexUrl
        {
            get { return "/CommandExecutor"; }
        }

        public override bool Hotswap { get { return true; } }
    }
}
