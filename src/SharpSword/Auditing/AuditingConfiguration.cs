/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/7/2016 10:28:07 AM
 * ****************************************************************/
using SharpSword.Configuration;
using SharpSword.Configuration.WebConfig;

namespace SharpSword.Auditing
{
    /// <summary>
    /// 审计全局配置，如果将此属性设置成false，那么标注在方法上的AuditedAttribute特性将不起作用
    /// </summary>
    [ConfigurationSectionName("sharpsword.auditing.configuration"), FailReturnDefault]
    public class AuditingConfiguration : ConfigurationSectionHandlerBase
    {
        /// <summary>
        /// 是否启用审计;默认开启(true)
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 对于匿名用户是否启用审计;默认开启(true)
        /// </summary>
        public bool IsEnabledForAnonymousUsers { get; set; }

        /// <summary>
        /// 设置一些默认值
        /// </summary>
        public AuditingConfiguration()
        {
            this.IsEnabled = true;
            this.IsEnabledForAnonymousUsers = true;
        }
    }
}
