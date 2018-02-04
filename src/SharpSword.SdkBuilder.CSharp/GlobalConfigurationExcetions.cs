/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/23/2016 1:56:32 PM
 * *******************************************************/

namespace SharpSword.SdkBuilder.CSharp
{
    public static class GlobalConfigurationExcetions
    {
        public static void UseCSharpSDK(this GlobalConfiguration globalConfiguration, SdkBuilderConfig config)
        {
            globalConfiguration.SetConfig(config);
        }
    }
}
