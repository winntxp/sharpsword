/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/23/2016 1:56:32 PM
 * *******************************************************/

namespace SharpSword.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public static class GlobalConfigurationExcetions
    {
        /// <summary>
        /// 系统框架级别审计配置
        /// </summary>
        /// <param name="globalConfiguration"></param>
        /// <param name="config"></param>
        public static void UseAudited(this GlobalConfiguration globalConfiguration, AuditingConfiguration config)
        {
            globalConfiguration.SetConfig(config);
        }

        /// <summary>
        /// 审计默认值
        /// </summary>
        /// <param name="globalConfiguration"></param>
        public static void UseAudited(this GlobalConfiguration globalConfiguration)
        {
            globalConfiguration.SetConfig(new AuditingConfiguration());
        }
    }
}
