/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/23/2016 1:56:32 PM
 * *******************************************************/

namespace SharpSword.Localization
{
    /// <summary>
    /// 模块配置扩展
    /// </summary>
    public static class GlobalConfigurationExcetions
    {
        /// <summary>
        /// 使用本地化配置
        /// </summary>
        /// <param name="globalConfiguration"></param>
        /// <param name="config"></param>
        public static void UseLocalization(this GlobalConfiguration globalConfiguration, LocalizationConfiguration config)
        {
            globalConfiguration.SetConfig(config);
        }
    }
}
