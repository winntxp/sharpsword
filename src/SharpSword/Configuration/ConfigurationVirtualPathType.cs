/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/21/2016 9:16:46 AM
 * ****************************************************************/
using System.ComponentModel;

namespace SharpSword.Configuration
{
    /// <summary>
    /// 配置文件路径来源
    /// </summary>
    public enum ConfigurationVirtualPathType
    {
        /// <summary>
        /// 来源本地文件
        /// </summary>
        [Description("本地文件")]
        FILE,

        /// <summary>
        /// 来源程序集内嵌资源
        /// </summary>
        [Description("程序集内嵌资源")]
        Assembly
    }
}
