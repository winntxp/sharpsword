/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/23/2016 1:56:32 PM
 * *******************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public static class GlobalConfigurationExcetions
    {
        /// <summary>
        /// 系统框架级别WEBAPI配置
        /// </summary>
        /// <param name="globalConfiguration"></param>
        /// <param name="config"></param>
        public static void UseWebApi(this GlobalConfiguration globalConfiguration, ApiConfiguration config)
        {
            globalConfiguration.SetConfig(config);
        }

        /// <summary>
        /// 使用默认的WEBAPI参数配置
        /// </summary>
        /// <param name="globalConfiguration"></param>
        public static void UseWebApi(this GlobalConfiguration globalConfiguration)
        {
            globalConfiguration.SetConfig(new ApiConfiguration());
        }
    }
}
