/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/23/2016 2:33:59 PM
 * *******************************************************/
using SharpSword.Configuration;
using SharpSword.Configuration.WebConfig;
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// API模块配置参数
    /// </summary>
    [ConfigurationSectionName("sharpsword.webapi.configuration"), FailReturnDefault, Serializable]
    public class ApiConfiguration : ConfigurationSectionHandlerBase
    {
        /// <summary>
        /// 设置默认值
        /// </summary>
        public ApiConfiguration() { }

        /// <summary>
        /// 默认开启;默认开启
        /// </summary>
        public bool EnabledAccessRecod { get; set; } = true;

        /// <summary>
        /// 是否校验客户端IP，默认不校验
        /// </summary>
        public bool ValidClientIp { get; set; } = false;

        /// <summary>
        /// 接口文档描述XML;默认返回空数组
        /// </summary>
        public string[] ActionDocResourcePaths { get; set; } = new string[] { };

        /// <summary>
        /// 此属性指示：当指定接口后，未找到对应的接口是否再次搜索最高的同名接口版本;系统框架默认为false
        /// </summary>
        public bool DefaultActionVersionFailToHighestActionVersion { get; set; } = false;
    }
}
