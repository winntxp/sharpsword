/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/21 13:21:02
 * ****************************************************************/

namespace SharpSword.SDK
{
    /// <summary>
    /// SDK配置类
    /// </summary>
    public class ApiClientConfiguration : IApiClientConfiguration
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        public string ServerUrl { get; set; }

        /// <summary>
        /// 客户KEY
        /// </summary>
        public string AppKey { get; set; }

        /// <summary>
        /// 客户端签名密钥
        /// </summary>
        public string AppSecret { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiServerUrl">服务器地址</param>
        /// <param name="appKey">客户端调用者唯一key</param>
        /// <param name="appSecret">唯一key对应的数据签名密钥</param>
        public ApiClientConfiguration(string apiServerUrl, string appKey = "", string appSecret = "")
        {
            this.ServerUrl = apiServerUrl;
            this.AppKey = appKey;
            this.AppSecret = appSecret;
        }
    }
}
