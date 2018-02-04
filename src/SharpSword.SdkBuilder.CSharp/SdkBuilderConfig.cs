/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 17:05:48
 * ****************************************************************/
using SharpSword.Configuration.WebConfig;

namespace SharpSword.SdkBuilder.CSharp
{
    /// <summary>
    /// 
    /// </summary>
    [ConfigurationSectionName("sharpsword.module.sdkbuilderconfig.csharp")]
    public class SdkBuilderConfig : ConfigurationSectionHandlerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public SdkBuilderConfig()
        {
            this.SdkNamespace = "SharpSword.Api.SDK";
        }

        /// <summary>
        /// 配置SDK开发包命名空间
        /// </summary>
        public string SdkNamespace { get; set; }
    }
}
