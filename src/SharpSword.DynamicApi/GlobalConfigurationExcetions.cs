/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/23/2016 1:56:32 PM
 * *******************************************************/

namespace SharpSword.DynamicApi
{
    public static class GlobalConfigurationExcetions
    {
        public static void UseDynamicApi(this GlobalConfiguration globalConfiguration, DynamicApiConfig config)
        {
            globalConfiguration.SetConfig(config);
        }
    }
}
