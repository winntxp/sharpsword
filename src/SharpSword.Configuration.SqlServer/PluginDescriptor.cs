/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword.Configuration.SqlServer
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
            get { return "系统参数配置(基于数据库)"; }
        }

        public override bool Hotswap { get { return false; } }
    }
}
