/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/20 11:07:13
 * ****************************************************************/
using System.Configuration;

namespace SharpSword.Configuration.WebConfig
{
    /// <summary>
    /// 自定义的web.config节点section信息
    /// </summary>
    internal class ConfigurationSectionManager
    {
        /// <summary>
        /// 自定义的web.config节点section信息
        /// </summary>
        /// <param name="sectionName">注意：区分大小写</param>
        /// <returns>请注意返回值有可能是null，外部调用需要注意判断</returns>
        public static object GetSection(string sectionName)
        {
            var sectionInstance = ConfigurationManager.GetSection(sectionName);
            if (sectionInstance.IsNull())
            {
                return null;
            }
            if (!(sectionInstance is IConfigurationSectionHandler))
            {
                return null;
            }
            return sectionInstance;
        }

        /// <summary>
        /// 根据Section节点名称，获取到对应配置XML文件对应的对象信息
        /// </summary>
        /// <typeparam name="T">配置对象信息</typeparam>
        /// <param name="sectionName">注意：区分大小写</param>
        /// <returns>返回配置文件节点对应的对象；有可能返回null，调用请注意判断</returns>
        public static T GetSection<T>(string sectionName = null) where T : IConfigurationSectionHandler
        {
            var sectionInstance = GetSection(sectionName.IsNullOrEmpty() ? typeof(T).Name : sectionName);
            if (sectionInstance.IsNull())
            {
                return default(T);
            }
            if (!(sectionInstance is T))
            {
                return default(T);
            }
            return (T)sectionInstance;
        }
    }
}