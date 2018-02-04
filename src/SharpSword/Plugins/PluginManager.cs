/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 11:48:56
 * ****************************************************************/
using System.Collections.Generic;
using System.Linq;

namespace SharpSword
{
    /// <summary>
    /// API接口扩展插件管理类
    /// </summary>
    public class PluginManager
    {
        /// <summary>
        /// 缓存所有扩展项目信息；
        /// </summary>
        private static readonly IDictionary<string, IPluginDescriptor> CachedApiPlugins = new Dictionary<string, IPluginDescriptor>();
        private static bool _initializationed = false;
        private static readonly object Locker = new object();

        /// <summary>
        /// 获取插件
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IPluginDescriptor> GetApiPlugins()
        {
            //还未初始化
            if (_initializationed)
            {
                return CachedApiPlugins.Select(o => o.Value);
            }

            lock (Locker)
            {
                //二次检测下
                if (_initializationed)
                {
                    return CachedApiPlugins.Select(o => o.Value);
                }

                //已经初始化了标志
                _initializationed = true;

                var typeFinder = ServicesContainer.Current.Resolve<ITypeFinder>();

                //搜索所有扩展插件，返回扩展插件集合
                typeFinder.FindClassesOfType<IPluginDescriptor>().ToList().ForEach(type =>
                {
                    var apiPluginDescriptor = ServicesContainer.Current.ResolveUnregistered(type);
                    CachedApiPlugins.Add(type.FullName, (IPluginDescriptor)apiPluginDescriptor);
                });
            }

            //直接返回缓存
            return CachedApiPlugins.Select(o => o.Value);
        }
    }
}
