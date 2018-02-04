/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword.Tools
{
    /// <summary>
    /// 调试工具扩展
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
            get { return "接口调试工具"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string IndexUrl
        {
            get { return "/apitool"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int DisplayIndex
        {
            get { return int.MaxValue - 1; }
        }
    }

    /// <summary>
    /// 系统事件定义插件查看页面
    /// </summary>
    [Serializable]
    public class EventsPluginDescriptor : PluginDescriptorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceFinderManager"></param>
        public EventsPluginDescriptor(IResourceFinderManager resourceFinderManager)
            : base(resourceFinderManager)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override string DisplayName
        {
            get { return "系统事件查看器"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string IndexUrl
        {
            get { return "/api/events"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Description
        {
            get { return "可以查看所有实现IEventData的事件"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int DisplayIndex
        {
            get { return int.MaxValue - 1; }
        }
    }

    /// <summary>
    /// 自定义SQL方法查看器
    /// </summary>
    public class SqlUsedPluginDescriptor : PluginDescriptorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceFinderManager"></param>
        public SqlUsedPluginDescriptor(IResourceFinderManager resourceFinderManager)
            : base(resourceFinderManager)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override string DisplayName
        {
            get { return "系统自定义SQL方法查看器"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string IndexUrl
        {
            get { return "/api/sqlused"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Description
        {
            get { return "可以查看所有自定义SQL的方法"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override int DisplayIndex
        {
            get { return int.MaxValue - 1; }
        }
    }

    /// <summary>
    /// 使用了事务方法查看器
    /// </summary>
    public class TransUsedPluginDescriptor : PluginDescriptorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceFinderManager"></param>
        public TransUsedPluginDescriptor(IResourceFinderManager resourceFinderManager)
            : base(resourceFinderManager)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public override string DisplayName
        {
            get { return "系统事务方法查看器"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string IndexUrl
        {
            get { return "/api/transused"; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Description
        {
            get { return "可以查看所有自动事务的方法"; }
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
