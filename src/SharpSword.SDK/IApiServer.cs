/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/9/2017 2:36:11 PM
 * ****************************************************************/

namespace SharpSword.SDK
{
    /// <summary>
    /// 用于直接调用api接口
    /// </summary>
    public interface IApiServer
    {
        /// <summary>
        /// API客户端接口
        /// </summary>
        IApiClient ApiClient { get; }
    }
}
