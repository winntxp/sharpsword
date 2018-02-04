/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword.SdkBuilder.CSharp
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
            get { return "WEBAPI接口文档/C#客户端SDK生成器/文档生成器"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string IndexUrl
        {
            get { return "/CSharpSdkBuilder"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int DisplayIndex
        {
            get { return int.MaxValue - 1; }
        }
    }
}
