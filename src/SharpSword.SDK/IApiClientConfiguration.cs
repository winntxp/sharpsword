/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/21 13:21:02
 * ****************************************************************/

namespace SharpSword.SDK
{
    /// <summary>
    /// API客户端配置
    /// </summary>
    public interface IApiClientConfiguration
    {
        /// <summary>
        /// 服务器地址
        /// </summary>
        string ServerUrl { get; set; }

        /// <summary>
        /// 客户KEY
        /// </summary>
        string AppKey { get; set; }

        /// <summary>
        /// 客户端加密密钥
        /// </summary>
        string AppSecret { get; set; }
    }
}
