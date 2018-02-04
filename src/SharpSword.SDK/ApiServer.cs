/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/9/2017 2:36:57 PM
 * ****************************************************************/

namespace SharpSword.SDK
{
    /// <summary>
    /// 便捷访问API接口服务类
    /// </summary>
    public class ApiServer : IApiServer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiClient"></param>
        public ApiServer(IApiClient apiClient)
        {
            this.ApiClient = apiClient;
        }

        /// <summary>
        /// API接口访问客户端
        /// </summary>
        public IApiClient ApiClient { get; private set; }
    }
}
