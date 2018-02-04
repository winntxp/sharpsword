/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/21/2016 9:12:37 AM
 * ****************************************************************/
namespace SharpSword.Configuration
{
    /// <summary>
    /// 配置文件读取，比如：XML或者JOSN
    /// </summary>
    public interface IConfigurationReader
    {
        /// <summary>
        /// 读取以资源配置文本信息
        /// </summary>
        /// <param name="virtualPath">文件路径，如果文件类型是FILE的，那么路径必须以：~/开头，否则我们将读取内嵌资源</param>
        /// <param name="virtualPathType">文件类型</param>
        /// <returns>配置文件XML或者JSON字符串配置内容</returns>
        string Read(string virtualPath, ConfigurationVirtualPathType virtualPathType);
    }
}