/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/23/2016 1:56:32 PM
 * *******************************************************/

namespace SharpSword.Configuration.SqlServer
{
    public static class GlobalConfigurationExcetions
    {
        public static void UseSqlServerConfiguration(this GlobalConfiguration globalConfiguration, ConfigurationConfig config)
        {
            globalConfiguration.SetConfig(config);
        }
    }
}
