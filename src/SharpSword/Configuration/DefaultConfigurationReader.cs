/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/21/2016 9:16:46 AM
 * ****************************************************************/
using System.IO;
using System.Text;

namespace SharpSword.Configuration
{
    /// <summary>
    /// 默认的配置文件读取器
    /// </summary>
    public class DefaultConfigurationReader : IConfigurationReader
    {
        /// <summary>
        /// 
        /// </summary>
        private IResourceFinderManager _resourceFinderManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceFinderManager">资源查找器</param>
        public DefaultConfigurationReader(IResourceFinderManager resourceFinderManager)
        {
            this._resourceFinderManager = resourceFinderManager;
        }

        /// <summary>
        /// 此实现业务逻辑为
        /// 1.当我们配置的是本地资源的时候，我们会直接读取本地文件来回去配置字符串
        /// 2.当我们配置的是内嵌资源的时候，我们会从资源管理器里读取内嵌资源
        /// </summary>
        /// <param name="virtualPath"></param>
        /// <param name="virtualPathType"></param>
        /// <returns></returns>
        public virtual string Read(string virtualPath, ConfigurationVirtualPathType virtualPathType)
        {
            switch (virtualPathType)
            {
                //读取本地文件
                case ConfigurationVirtualPathType.FILE:

                    if (!virtualPath.StartsWith("~/"))
                    {
                        throw new SharpSwordCoreException("路径必须以~/开头");
                    }

                    //获取配置文件路径
                    var configPhysicalPath = HostHelper.MapPath(virtualPath);

                    //配置文件不存在，直接返回null
                    if (!File.Exists(configPhysicalPath))
                    {
                        throw new SharpSwordCoreException("未找到配置文件路径：{0}".With(configPhysicalPath));
                    }

                    //配置文件信息
                    string settingString = string.Empty;

                    //读取配置文件
                    using (var streamReader = new StreamReader(configPhysicalPath, Encoding.UTF8))
                    {
                        settingString = streamReader.ReadToEnd();
                    }

                    return settingString;

                //直接读取内嵌资源
                case ConfigurationVirtualPathType.Assembly:

                    return this._resourceFinderManager.GetResource(virtualPath);

                default:

                    return null;
            }
        }
    }
}
