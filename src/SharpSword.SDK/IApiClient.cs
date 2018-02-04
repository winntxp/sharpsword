/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/2/2015 8:32:16 PM
 * ****************************************************************/

namespace SharpSword.SDK
{
    /// <summary>
    /// SDK接口访问入口接口
    /// </summary>
    public interface IApiClient
    {
        /// <summary>
        /// 请求超时时间，单位：毫秒，默认：100000毫秒（100秒）
        /// </summary>
        int Timeout { get; set; }

        /// <summary>
        /// 获取API接口访问入口(需要使用扩展请添加客户端命名空间引用)
        /// </summary>
        IApiServer Apis { get; }

        /// <summary>
        /// 设置调用链ID，如果设置了，将会覆盖自动自动生成的调用ID
        /// 手动设置调用ID，在调用连的时候需要，便于接口调用深度监控
        /// 手动设置调用ID，在调用连的时候需要，便于接口调用深度监控（注意此方法是全局设置，即：设置一次，后续的Execute若不指定RequestId，则都会使用此请求ID）
        /// </summary>
        /// <param name="requestId"></param>
        void SetRequestId(string requestId);

        /// <summary>
        /// 请注意返回有可能未null，需要判断
        /// </summary>
        /// <typeparam name="T">返回格式化字符串（JSON/XML）对应的输出实体类型，需要继承TopRespBase抽象基类</typeparam>
        /// <param name="request"><![CDATA[请求参数类，需继承：RequestBase<>抽象基类]]></param>
        /// <param name="requestId">请求ID，一般客户端设置成GUID，最好设置成每次调用都不一致，这样可以方便后续调用链跟踪和数据安全，此设置并不会覆盖全局使用SetRequestId方法设置的请求编号</param>
        /// <returns>返回一个继承自：TopRespBase的数据对象</returns>
        /// <param name="cacheOptions">
        /// <![CDATA[
        /// 如果不启用SDK本地缓存，设置null即可
        /// 是否进行SDK本地缓存，如果设置为true(默认false)，返回对象将直接从本地SDK缓存里获取，
        /// 第一次访问不存在的时候，会自动将获取到的输出值压入本地缓存，下次调用同样接口参数相同的情况下
        /// 不会请求接口API，而是会直接获取本地缓存返回，建议将变化很不频繁的字典，配置对象数据可以缓存下，提高访问效率(默认过期时间60分钟)
        /// ]]>
        /// </param>
        T Execute<T>(RequestBase<T> request, string requestId = null, CacheOptions cacheOptions = null) where T : ResponseBase;
    }
}