/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword.Caching.Redis.StackExchange
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
        public override string DisplayName
        {
            get { return "接口缓存组件(Redis基于StackExchange.Redis实现)"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string IndexUrl
        {
            get
            {
                return "https://www.nuget.org/packages/StackExchange.Redis/1.1.604-alpha";
            }
        }
    }
}
