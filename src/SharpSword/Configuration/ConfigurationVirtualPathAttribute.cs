/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/21/2016 9:16:46 AM
 * ****************************************************************/
using SharpSword.Localization;
using System;

namespace SharpSword.Configuration
{
    /// <summary>
    /// 配置参数特性配置类(设置配置文件查找路径)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class ConfigurationVirtualPathAttribute : Attribute
    {
        /// <summary>
        /// 配置文件保存的虚拟文件夹：路径如：~/App_Data/Config/Serv.Api.Core.json.smtp.xml等
        /// </summary>
        public string VirtualPath { get; private set; }

        /// <summary>
        /// VirtualPath程序集来源
        /// </summary>
        public ConfigurationVirtualPathType VirtualPathType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="virtualPath">配置文件保存的虚拟路径,请按照~开头，不能是物理路径</param>
        /// <exception cref="SharpSwordCoreException">如果VirtualPathType=ConfigurationVirtualPathType.FILE，虚拟路径必须以~/开头</exception>
        /// <param name="virtualPathType">配置文件类型，从本地读取或者从内嵌资源读取</param>
        public ConfigurationVirtualPathAttribute(string virtualPath, ConfigurationVirtualPathType virtualPathType = ConfigurationVirtualPathType.FILE)
        {
            virtualPath.CheckNullThrowArgumentNullException(nameof(virtualPath));
            if (virtualPathType == ConfigurationVirtualPathType.FILE && !virtualPath.StartsWith("~/"))
            {
                throw new SharpSwordCoreException(ServicesContainer.Current.Resolve<ITextFormatter>().Get("虚拟路径必须以~/开头"));
            }
            this.VirtualPath = virtualPath;
            this.VirtualPathType = virtualPathType;
        }
    }
}
