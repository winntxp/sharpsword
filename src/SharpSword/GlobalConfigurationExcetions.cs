/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/23/2016 1:56:32 PM
 * *******************************************************/
using System.Collections.Generic;
using System.Collections.Immutable;

namespace SharpSword
{
    /// <summary>
    /// 方便配置系统参数对象
    /// </summary>
    public static class GlobalConfigurationExcetions
    {
        /// <summary>
        /// 往globalConfiguration附加数据里添加一个配置字典
        /// </summary>
        /// <typeparam name="T">配置参数类型</typeparam>
        /// <param name="globalConfiguration">全局参数配置容器</param>
        /// <param name="config">指定类型的参数对象</param>
        public static void SetConfig<T>(this GlobalConfiguration globalConfiguration, T config)
        {
            globalConfiguration.Properties.AddOrUpdate(typeof(T).FullName, key => config, (key, value) => config);
        }

        /// <summary>
        /// 从进程配置里获取指定配置对象，如果不存在直接返回null
        /// </summary>
        /// <typeparam name="T">配置参数类型</typeparam>
        /// <param name="globalConfiguration">全局参数配置容器</param>
        /// <returns>指定类型的参数对象</returns>
        public static T GetConfig<T>(this GlobalConfiguration globalConfiguration)
        {
            return globalConfiguration.Properties.GetValue(typeof(T).FullName, (key, value) => (T)value, key => default(T));
        }

        /// <summary>
        /// 获取所有注册的对象信息
        /// </summary>
        /// <param name="globalConfiguration">全局参数配置容器</param>
        /// <returns>系统所有注册在参数容器里的参数对象（只读）</returns>
        public static IReadOnlyDictionary<string, object> GetAllConfig(this GlobalConfiguration globalConfiguration)
        {
            return globalConfiguration.Properties.ToImmutableDictionary();
        }
    }
}
