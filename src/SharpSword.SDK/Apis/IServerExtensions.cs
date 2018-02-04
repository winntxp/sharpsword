/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/9/2017 2:18:57 PM
 * ****************************************************************/
using SharpSword.SDK.Request;
using SharpSword.SDK.Resp;

namespace SharpSword.SDK
{
    /// <summary>
    /// 自动生成便捷入口示例(代码生成模板)
    /// </summary>
    public static class IServerExtensions
    {
        /// <summary>
        /// API.ServerTime.Get 获取服务器时间；返回一个字符串时间数据，格式为：yyyy-MM-dd HH:mm:ss.ffffff
        /// </summary>
        /// <param name="server">IApiServer</param>
        /// <param name="request">inherit from RequestBase{T}</param>
        /// <param name="requestId">请求编号</param>
        /// <param name="cacheOptions">本地缓存器设置</param>
        /// <returns></returns>
        public static APIServerTimeGetResp APIServerTimeGet(this IApiServer server, APIServerTimeGetRequest request, string requestId = null, CacheOptions cacheOptions = null)
        {
            return server.ApiClient.Execute(request, requestId, cacheOptions);
        }
    }
}
