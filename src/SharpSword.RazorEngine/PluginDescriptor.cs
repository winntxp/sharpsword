/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword.RazorEngine
{
    /// <summary>
    /// 视图引擎
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
            get { return "RazorEngine视图引擎"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string IndexUrl
        {
            get
            {
                return "https://www.nuget.org/packages/RazorEngine/3.8.0";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Author
        {
            get { return "zhangliang@sharpsword.com.cn"; }
        }
    }
}
