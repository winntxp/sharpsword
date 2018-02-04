/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/21/2016 10:52:45 AM
 * ****************************************************************/
using System;

namespace SharpSword.Configuration.RemoteConfig
{
    /// <summary>
    /// 基于远程数据配置参数获取工厂
    /// </summary>
    public class RemoteConfigSettingFactory : SettingFactoryBase
    {
        /// <summary>
        /// 工厂支持处理的数据类型
        /// </summary>
        public override Type Supported
        {
            get { return typeof(IRemoteConfiguration); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSetting"></typeparam>
        /// <returns></returns>
        public override TSetting Get<TSetting>()
        {
            throw new NotImplementedException();
        }
    }
}
