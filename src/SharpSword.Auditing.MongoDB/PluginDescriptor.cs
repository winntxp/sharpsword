/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword.Auditing.MongoDB
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
        public PluginDescriptor(IResourceFinderManager resourceFinderManager)
            : base(resourceFinderManager)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override string DisplayName => "服务层方法审计日志记录器(记录到MongoDB)";

        /// <summary>
        /// 
        /// </summary>
        public override string IndexUrl => "http://mongodb.github.io/mongo-csharp-driver/";
    }
}
