/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/23/2016 1:56:32 PM
 * *******************************************************/

namespace SharpSword.Auditing.SqlServer
{
    /// <summary>
    /// 
    /// </summary>
    public static class GlobalConfigurationExcetions
    {
        /// <summary>
        /// 设置审计配置信息
        /// </summary>
        /// <param name="globalConfiguration"></param>
        /// <param name="config"></param>
        public static void UseSqlServerAudited(this GlobalConfiguration globalConfiguration, AuditingStoreConfig config)
        {
            globalConfiguration.SetConfig(config);
        }
    }
}
