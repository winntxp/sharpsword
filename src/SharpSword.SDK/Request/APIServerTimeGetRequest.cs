/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/10/2017 4:59:42 PM
 * ****************************************************************/
using SharpSword.SDK.Resp;

namespace SharpSword.SDK.Request
{
    /// <summary>
    ///请求示例(代码生成模板)
    /// </summary>
    public class APIServerTimeGetRequest : RequestBase<APIServerTimeGetResp>
    {
        /// <summary>
        /// 接口版本号，如：1.0
        /// </summary>
        private string _version = string.Empty;

        /// <summary>
        /// API.ServerTime.Get
        /// </summary>
        /// <param name="version">API接口版本号，如：1.0。默认为空，不指定版本号，API接口自动选择同名接口最高版本提供服务</param>
        public APIServerTimeGetRequest(string version = "")
        {
            this._version = version;
        }

        /// <summary>
        /// 重写接口API.ServerTime.Get版本，默认不指定版本号
        /// </summary>
        /// <returns></returns>
        public override string GetVersion()
        {
            return this._version;
        }

        /// <summary>
        /// 调用接口名称，API.ServerTime.Get
        /// </summary>
        /// <returns></returns>
        public override string GetApiName()
        {
            return "API.ServerTime.Get";
        }

        /// <summary>
        /// 请求参数json化
        /// </summary>
        /// <returns></returns>
        public override string GetRequestJsonData()
        {
            return this.ToJson();
        }
    }
}
