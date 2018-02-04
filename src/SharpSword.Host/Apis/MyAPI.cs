/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/10/2017 3:27:12 PM
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
    [ActionName("MyAPI"), ResponseFormat(WebApi.ResponseFormat.JSON | WebApi.ResponseFormat.XML)]
    [Description("MyAPI")]
    public class MyAPI : ActionBase<MyAPI.MyAPIRequestDto, string>
    {
        /// <summary>
        /// 上送参数对象
        /// </summary>
        public class MyAPIRequestDto : RequestDtoBase
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
        /// ctor
        /// </summary>
        public MyAPI(IApiClient apiClient)
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

            //演示快捷访问API接口，并且客户端指定使用服务器端API接口版本
            var resp = this._apiClient.Apis.MALLShopFeedBack(new MALLShopFeedBackRequest("0.0")
            {
                ShopName = "xxx",
                Version = "1.0"
            });

            var resp0 = this._apiClient.Apis.APIServerTimeGet(new APIServerTimeGetRequest());

            return new ActionResult<string>(data: resp.Data.Serialize2FormatJosn(), info: resp.Info, flag: 0);
        }

    }
}
