/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword.FluentValidation
{
    /// <summary>
    /// 模块描述对象
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
        public override string DisplayName => "RequestDto对象属性校验组件(基于FluentValidation实现)";

        /// <summary>
        /// 
        /// </summary>
        public override string IndexUrl => "https://github.com/JeremySkinner/FluentValidation/wiki";

    }
}
