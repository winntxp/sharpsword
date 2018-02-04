/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/9/2017 2:17:33 PM
 * ****************************************************************/
using SharpSword.Api.SDK;
using SharpSword.Api.SDK.Request;
using SharpSword.WebApi;
using System.Collections.Generic;
using System.ComponentModel;

namespace SharpSword.Host.Apis
{
    /// <summary>
    /// 
    /// </summary>
    [ActionName("SDKTest"), ResponseFormat(WebApi.ResponseFormat.JSON | WebApi.ResponseFormat.XML), ResultCache(1, true)]
    [Description("演示接口层层调用，调用深度5")]
    public class SDKTest : ActionBase<SDKTest.SDKTestRequestDto, string>
    {
        /// <summary>
        /// 上送参数对象
        /// </summary>
        public class SDKTestRequestDto : RequestDtoBase
        {
            /// <summary>
            /// 自定义校验上送参数
            /// </summary>
            /// <returns></returns>
            public override IEnumerable<DtoValidatorResultError> Valid()
            {
                return base.Valid();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly IApiClient _apiClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiClient"></param>
        public SDKTest(IApiClient apiClient)
        {
            this._apiClient = apiClient;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<string> Execute()
        {
            this._apiClient.SetRequestId(this.RequestId);

            //老的调用方式
            var r = this._apiClient.Execute(new MyAPIRequest("0.0"));

            //演示快捷访问API接口，并且客户端指定使用服务器端API接口版本
            var resp = this._apiClient.Apis.MyAPI(new MyAPIRequest("0.0"));

            var resp0 = this._apiClient.Apis.APIServerTimeGet(new APIServerTimeGetRequest());

            var resp1 = this._apiClient.Apis.TestServiceDapper(new TestServiceDapperRequest());

            //动态调用
            //var resp1 = this._apiClient.Apis.API.ServerTime.Get();

            return new ActionResult<string>(data: resp.Data, info: resp.Info, flag: 0);

        }
    }
}
