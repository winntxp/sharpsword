/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword.DtoGenerator
{
    /// <summary>
    /// 插件描述对象
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
            get { return "DTO生成器(DTO Generator)"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string IndexUrl
        {
            get
            {
                 return "/DtoGenerator";
            }
        }
    }
}
