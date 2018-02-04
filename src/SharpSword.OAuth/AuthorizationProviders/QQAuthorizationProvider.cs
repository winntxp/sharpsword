/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SharpSword.OAuth
{
    public class QQAuthorizationProvider : AuthorizationProvider
    {
        private const string OpenIdApiUrl = "https://graph.qq.com/oauth2.0/me?access_token={0}";
        private const string UserInfoUrl = "https://graph.qq.com/user/get_user_info?access_token={0}&oauth_consumer_key={1}&openid={2}";

        public override string GenerateUrl(App application, string state = "", string view = "")
        {
            return $"{application.Platform.AuthorizationUrl}?response_type=code&client_id={application.AppKey}&redirect_uri={application.RedirectUrl}&state={state}";
        }

        public override async Task<AuthorizationResult> GetTokenAsync(App app, HttpRequestBase callbackRequest)
        {
            if (callbackRequest == null)
                throw new ArgumentNullException(nameof(callbackRequest));
            var code = callbackRequest.QueryString["code"];
            var grant_type = "authorization_code";
            var client_id = app.AppKey;
            var client_secret = app.Secret;
            var redirect_uri = app.RedirectUrl;
            StringBuilder requestPar = new StringBuilder();
            requestPar.Append($"grant_type={grant_type}&");
            requestPar.Append($"client_id={client_id}&");
            requestPar.Append($"client_secret={client_secret}&");
            requestPar.Append($"redirect_uri={redirect_uri}&");
            requestPar.Append($"code={code}");
            var result = await HttpHelp.GetStrAsync(GenerateApiUrl(app, requestPar.ToString())).ConfigureAwait(false);
            ValidateResult(result);
            QQAuthorizationTokenResult tokenResult = new QQAuthorizationTokenResult();
            await Task.Factory.StartNew(() => { tokenResult = AnalyParameter(result); }).ConfigureAwait(false);
            AuthorizationResult authorizationResult = new AuthorizationResult
            {
                ExpireAt = DateTime.Now.AddMinutes(-3).AddSeconds(tokenResult.expires_in),
                RefreshExpireAt = DateTime.Now.AddMinutes(-3).AddSeconds(tokenResult.expires_in),
                Token = tokenResult.access_token
            };
            authorizationResult.OpenId = await GetOpenIdAsync(tokenResult.access_token).ConfigureAwait(false);
            await GetNickNameAsync(tokenResult.access_token, authorizationResult.OpenId, app.AppKey, authorizationResult).ConfigureAwait(false);
            return authorizationResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task<string> GetOpenIdAsync(string token)
        {
            var url = OpenIdApiUrl.With(token);
            var result = await HttpHelp.GetStrAsync(url).ConfigureAwait(false);
            ValidateResult(result);
            result = result.Replace("callback(", "").Replace(");", "");
            var jsonResult = (JObject)JsonConvert.DeserializeObject(result);
            return jsonResult["openid"].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="openId"></param>
        /// <returns></returns>
        private async Task GetNickNameAsync(string token, string openId, string appkey,AuthorizationResult authorizationResult)
        {
            string url = UserInfoUrl.With(token, appkey, openId);
            var result = await HttpHelp.GetStrAsync(url).ConfigureAwait(false);
            ValidateResult(result);
            var jsonResult = (JObject)JsonConvert.DeserializeObject(result);
            authorizationResult.UserName= jsonResult["nickname"].ToString();
            authorizationResult.HeadImg = jsonResult["figureurl_qq_2"].ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        public void ValidateResult(string result)
        {
            if (result.IndexOf("error") > -1) ThrowException(result);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="application"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private string GenerateApiUrl(App application, string parameter)
        {
            return application.Platform.TokenUrl + "?" + parameter.TrimEnd('&');
        }

        /// <summary>
        /// 
        /// </summary>
        [Serializable]
        private struct QQAuthorizationTokenResult
        {
            public string access_token { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="urlResult"></param>
        /// <returns></returns>
        private QQAuthorizationTokenResult AnalyParameter(string urlResult)
        {
            object qAuthorizationTokenResult = new QQAuthorizationTokenResult();
            var urlResults = urlResult.Split('&');
            Parallel.For(0, urlResults.Length, (i) =>
            {
                var attbuiter = urlResults[i].Split('=')[0];
                var value = urlResults[i].Split('=')[1];
                var propertyInfo = qAuthorizationTokenResult.GetType().GetProperty(attbuiter, BindingFlags.Public | BindingFlags.IgnoreCase | BindingFlags.Instance);
                if (propertyInfo != null)
                {
                    propertyInfo.SetValue(qAuthorizationTokenResult, Convert.ChangeType(value, propertyInfo.PropertyType));
                }
            });
            return (QQAuthorizationTokenResult)qAuthorizationTokenResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resut"></param>
        private void ThrowException(string resut)
        {
            var replaceResult = resut.Replace("callback(", "").Replace(");", "");
            var jobj = (JObject)JsonConvert.DeserializeObject(replaceResult);
            var code = jobj["error"];
            var desc = jobj["error_description"];
            throw new QQExceptionBuilder().Create(code.ToString(), desc.ToString());

        }
    }
}
