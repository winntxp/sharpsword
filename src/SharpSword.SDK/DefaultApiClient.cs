/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/2/2015 8:32:16 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace SharpSword.SDK
{
    /// <summary>
    /// 默认的SDK访问实现;注意：请不要将此实现类设置成全局单例类
    /// </summary>
    public class DefaultApiClient : IApiClient
    {
        private readonly ResponseFormat _responseFormat = ResponseFormat.JSON;
        private readonly Encoding _encoding = Encoding.UTF8;
        private readonly IApiClientConfiguration _config;
        private readonly IApiClientSecurityProvider _apiClientSecurityProvider;
        private readonly IApiClientSignProvider _apiClientSignProvider;
        private readonly IServerUrlFactory _serverUrlFactory;
        private IApiServer _apiServer;
        private string _currentGlobalRequestId;

        /// <summary>
        /// 默认的SDK访问实现类;
        /// </summary>
        /// <param name="config">客户端配置信息</param>
        /// <param name="securityProvider">加密解密服务，公开出来，让调用者去实现具体的加解密过程</param>
        /// <param name="signProvider">数据签名服务，公开出来，让调用者去实现具体的数据签名流程</param>
        /// <param name="serverUrlFactory"></param>
        public DefaultApiClient(IApiClientConfiguration config,
                                IApiClientSignProvider signProvider = null,
                                IApiClientSecurityProvider securityProvider = null,
                                IServerUrlFactory serverUrlFactory = null)
        {
            this._config = config;
            this._apiClientSecurityProvider = (securityProvider ?? DefaultApiClientSecurityProvider.Instance);
            this._apiClientSignProvider = (signProvider ?? new DefaultApiClientSignProvider(config));
            this._serverUrlFactory = serverUrlFactory;
            this.Logger = NullApiLogger.Instance;
            this.CacheManager = NullCacheManager.Instance;
            this.Timeout = 100000; //100秒
        }

        /// <summary>
        /// API客户端缓存管理器
        /// </summary>
        public IApiClientCacheManager CacheManager { get; set; }

        /// <summary>
        /// API客户端日志记录器
        /// </summary>
        public IApiClientLogger Logger { get; set; }

        /// <summary>
        /// 请求超时时间，单位：毫秒，默认100秒
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// 获取快捷API接口访问
        /// </summary>
        public IApiServer Apis
        {
            get
            {
                if (null == this._apiServer)
                {
                    this._apiServer = new ApiServer(this);
                }
                return this._apiServer;
            }
        }

        /// <summary>
        /// 手动设置调用ID，在调用连的时候需要，便于接口调用深度监控（注意此方法是全局设置，即：设置一次，后续的Execute若不指定RequestId，则都会使用此请求ID）
        /// 自动生成的调用链会进行深度增加，如：RequestId.0->RequestId.1->RequestId.2->RequestId.3
        /// </summary>
        /// <param name="requestId"></param>
        public void SetRequestId(string requestId)
        {
            this._currentGlobalRequestId = requestId;
        }

        /// <summary>
        /// 构造提交参数
        /// </summary>
        /// <typeparam name="T">请求返回的对象类型</typeparam>
        /// <param name="request">请求参数包</param>
        /// <param name="requestId"></param>
        /// <returns></returns>
        private IDictionary<string, string> BuildPostData<T>(RequestBase<T> request, string requestId) where T : ResponseBase
        {
            string actionName = request.GetApiName();
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
            string format = this._responseFormat.ToString();
            //版本，预留空，方便服务器接口版本切换
            string version = request.GetVersion();
            //先对data数据加密
            string data = this._apiClientSecurityProvider.RequestEncrypt(request.GetRequestJsonData());
            //构造上送参数
            IDictionary<string, string> requestParams = new SortedDictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            requestParams.Add("AppKey", this._config.AppKey);
            requestParams.Add("ActionName", actionName);
            requestParams.Add("TimeStamp", timeStamp);
            requestParams.Add("Format", format);
            requestParams.Add("Version", version);
            requestParams.Add("Data", data);
            requestParams.Add("RequestId", requestId);
            //根据签名的上送参数进行数据签名
            string sign = this._apiClientSignProvider.Sign(requestParams);
            //加入签名参数
            requestParams.Add("Sign", sign);
            //返回
            return requestParams;
        }

        /// <summary>
        /// 根据请求获取缓存键;确保缓存键的唯一性
        /// </summary>
        /// <typeparam name="T">返回类型</typeparam>
        /// <param name="request">请求参数包对象</param>
        /// <returns>根据请求参数包计算出缓存键，防止缓存冲突</returns>
        private string GetRequestCacheKey<T>(RequestBase<T> request) where T : ResponseBase
        {
            return "{0}.{1}".With(request.GetApiName(), Utils.MD5(request.GetVersion() + request.GetRequestJsonData()));
        }

        /// <summary>
        /// 创建输出Response对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="flag"></param>
        /// <param name="flagDescription"></param>
        /// <param name="info"></param>
        /// <param name="reqData"></param>
        /// <param name="respBody"></param>
        /// <returns></returns>
        private T CreateResponse<T>(int flag, string flagDescription, string info, string reqData, string respBody) where T : ResponseBase
        {
            var response = Activator.CreateInstance<T>();
            response.Flag = flag;
            response.FlagDescription = flagDescription;
            response.Info = info;
            response.Resp_ReqData = reqData;
            response.Resp_Body = respBody;
            return response;
        }

        /// <summary>
        /// 获取调用ID，如果已经设置了全局就直接取全局，没有就自动生成一个
        /// </summary>
        /// <returns></returns>
        private string BuildRequestId()
        {
            //如果已经手工设置了，就证明是接口调用接口，我们直接使用手工设置的调用ID
            if (!this._currentGlobalRequestId.IsNullOrEmpty())
            {
                return this._currentGlobalRequestId;
            }

            //生成调用ID，默认作为起始请求
            return "{0}.{1}".With(Guid.NewGuid().ToString("N").ToUpper(), 0);
        }

        /// <summary>
        /// 重新整理下请求编号,我们将符合一定格式的请求ID递增+1
        /// </summary>
        /// <param name="requestId">客户端自动生成的请求ID格式为：EWR3RFEFEFAFARWTWQT.1</param>
        /// <returns></returns>
        private string IncreaseRequestId(string requestId)
        {
            var match = Regex.Match(requestId, "^([0-9A-Za-z]{5,100}).([0-9]{1,5})$");
            if (match.Success)
            {
                return "{0}.{1}".With(match.Groups[1].Value, int.Parse(match.Groups[2].Value) + 1);
            }
            return requestId;
        }

        /// <summary>
        /// 请注意返回有可能未null，需要判断
        /// </summary>
        /// <typeparam name="T">
        /// <![CDATA[返回格式化字符串（JSON/XML）对应的输出实体类型，需要继承TopRespBase抽象基类]]>
        /// </typeparam>
        /// <param name="request">
        /// <![CDATA[请求参数类，需继承：RequestBase<>抽象基类]]>
        /// </param>
        /// <param name="requestId">请求ID，一般客户端设置成GUID，最好设置成每次调用都不一致，这样可以方便后续调用链跟踪和数据安全</param>
        /// <param name="cacheOptions">
        /// <![CDATA[
        /// 如果不启用SDK本地缓存，设置null即可，系统将会自动设置成默认设置
        /// 是否进行SDK本地缓存，如果设置为true(默认false)，返回对象将直接从本地SDK缓存里获取，
        /// 第一次访问不存在的时候，会自动将获取到的输出值压入本地缓存，下次调用同样接口参数相同的情况下
        /// 不会请求接口API，而是会直接获取本地缓存返回，建议将变化很不频繁的字典，配置对象数据可以缓存下，提高访问效率(默认过期时间60分钟)
        /// ]]>
        /// TODO:2017.08.11 建议不启用缓存策略，而是将缓存策略放到应用程序业务中去处理，不排除后续版本将此参数移出
        /// </param>
        /// <returns>返回一个继承自：TopRespBase的数据对象</returns>
        public T Execute<T>(RequestBase<T> request, string requestId, CacheOptions cacheOptions = null) where T : ResponseBase
        {
            //服务器地址
            var svrUrl = this._config.ServerUrl;

            //检测是否定义获取服务器地址获取服务
            if (null != this._serverUrlFactory)
            {
                svrUrl = this._serverUrlFactory.GetApiServerUrl(request.GetApiName(), request.GetVersion());
            }

            //系统缓存键，默认为空，只有当CacheOptions.UserLocalCache为true的时候才生成
            var cacheKey = string.Empty;
            var useCache = null != this.CacheManager && null != cacheOptions && cacheOptions.UseLocalCache && cacheOptions.CacheTime > 0;

            //尝试直接从SDK本地缓存里读取数据
            if (useCache)
            {
                //根据请求数据包生成对应的缓存键，防止缓存键的冲突
                cacheKey = this.GetRequestCacheKey(request);

                //直接从本地缓存里获取缓存对象
                var cacheItem = this.CacheManager.Get(cacheKey);

                //存在缓存对象，直接返回对象，无需连接API接口访问
                if (null != cacheItem && cacheItem.Data is T)
                {
                    return (T)cacheItem.Data;
                }
            }

            //参数组装
            var postData = this.BuildPostData(request, this.IncreaseRequestId(requestId ?? this.BuildRequestId()));

            //请求数据
            string requestData = string.Join("&", (from item in postData select item.Key + "=" + item.Value).ToArray());

            //HTTP
            //IHttpWebUtils httpWebUtils = new HttpWebUtils { Timeout = this.Timeout };
            IHttpWebUtils httpWebUtils = new RestWebUtils(svrUrl) { Timeout = this.Timeout };

            //是否指定是用POST提交数据
            HttpRespBody respBody = null;

            try
            {
                if (HttpMethod.POST == request.GetHttpMethod())
                {
                    if (request is IApiUploadRequest<T>)
                    {
                        IApiUploadRequest<T> uploadRequest = request as IApiUploadRequest<T>;
                        var fileParams = Utils.CleanupDictionary(uploadRequest.GetFileParameters());
                        respBody = httpWebUtils.DoPost(svrUrl, postData, fileParams);
                    }
                    else
                    {
                        respBody = httpWebUtils.DoPost(svrUrl, postData);
                    }
                }
                else
                {
                    respBody = httpWebUtils.DoGet(svrUrl, postData);
                }

                //状态码不是200()网络异常
                if (respBody == null || string.IsNullOrWhiteSpace(respBody.Body) || respBody.StatusCode != HttpStatusCode.OK)
                {
                    //记录下日志
                    this.Logger.Error("网络异常，错误详情：{0}".With(respBody.Body));

                    //反馈给用户端
                    return this.CreateResponse<T>(flag: 500,
                                                  flagDescription: "网络异常",
                                                  info: "网络异常",
                                                  reqData: "{0}?{1}".With(svrUrl, requestData),
                                                  respBody: null == respBody ? "" : respBody.Body);
                }

                //对下送的数据进行解密
                var responseDtoDecryptResult = this._apiClientSecurityProvider.ResponseDecrypt(respBody.Body);

                //解密失败
                if (!responseDtoDecryptResult.IsSuccess)
                {
                    return this.CreateResponse<T>(flag: 400,
                                                  flagDescription: "SDK解密失败",
                                                  info: "解密失败，原始数据：" + responseDtoDecryptResult.Data,
                                                  reqData: "{0}?{1}".With(svrUrl, requestData),
                                                  respBody: null == respBody ? "" : respBody.Body);
                }

                //解密成功，使用解密后的数据
                respBody.Body = responseDtoDecryptResult.Data;

                //替换掉不规则的json特殊字符串文件
                foreach (var item in request.GetReplaces())
                {
                    respBody.Body = respBody.Body.Replace(item.Key, item.Value);
                }

                //反序列化出对象
                T respObj = (this._responseFormat == ResponseFormat.JSON) ?
                            new ApiJsonParser<T>().Parse(respBody.Body, this._encoding)
                            :
                            new ApiXmlParser<T>().Parse(respBody.Body, this._encoding);

                //记录下返回的JSON数据，方便调试
                respObj.Resp_Body = respBody.Body;

                //记录下上送的JSON数据包，方便调试
                respObj.Resp_ReqData = "{0}?{1}".With(svrUrl, requestData);

                //服务器HTTPHeader头信息(里面包含了服务器一些执行情况信息，比如Api-Time-Used（执行此次API花费的毫秒数）)
                respObj.Resp_Headers = respBody.Headers;

                //配置了缓存，并且返回数据为成功，设置了压入缓存到本地SDK缓存器就将对象缓存起来，方便下次直接使用
                if (useCache && null != respObj && respObj.Flag == 0)
                {
                    this.CacheManager.Set(cacheKey, respObj, cacheOptions.CacheTime);
                }

                //返回反序列化对象
                return respObj;

            }
            catch (Exception ex)
            {
                //记录下日志
                this.Logger.Error(ex.StackTrace);

                //返回给调用者
                return this.CreateResponse<T>(flag: 300,
                                              flagDescription: ex.Message,
                                              info: ex.StackTrace,
                                              reqData: "{0}?{1}".With(svrUrl, requestData),
                                              respBody: null == respBody ? "" : respBody.Body);
            }
        }
    }
}