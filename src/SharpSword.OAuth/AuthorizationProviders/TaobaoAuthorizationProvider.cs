﻿/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SharpSword.OAuth
{
    /// <summary>
    /// 适用于淘宝的授权提供程序。
    /// 参考资料：http://open.taobao.com/doc2/detail?spm=a219a.7386781.3.8.
    /// mYMgk6&docType=1&articleId=102635&treeId=1
    /// </summary>
    public class TaobaoAuthorizationProvider : AuthorizationProvider
    {
        /// <summary>
        /// 生成授权Url。
        /// </summary>
        /// <param name="state">状态参数。</param>
        /// <param name="view">授权页面的类型，如淘宝将授权页面分为web、tmall和wap三种类型。</param>
        public override string GenerateUrl(App app, string state = "", string view = "")
        {
            string url = string.Format(app.Platform.AuthorizationUrl + "?response_type=code&client_id={0}&redirect_uri={1}&state={2}&view={3}",
               app.AppKey, app.RedirectUrl, state, view ?? "web");

            return url;
        }

        /// <summary>
        /// 接受回调请求后向平台换取Token。
        /// </summary>
        /// <param name="app">应用信息。</param>
        /// <param name="callbackRequest">回调请求。</param>
        public override async Task<AuthorizationResult> GetTokenAsync(App app, HttpRequestBase callbackRequest)
        {
            //获取授权码
            string authorizationCode = callbackRequest.QueryString["code"];
            string view = callbackRequest.QueryString["view"];
            string appSecret = app.Secret;
            string gateway = app.Platform.TokenUrl;
            string grantType = "authorization_code";
            string appKey = app.AppKey;
            string redirectUrl = app.RedirectUrl;
            string state = callbackRequest.QueryString["state"];
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("?grant_type={0}&", grantType);
            sb.AppendFormat("code={0}&", authorizationCode);
            sb.AppendFormat("view={0}&", view);
            sb.AppendFormat("client_id={0}&", appKey);
            sb.AppendFormat("client_secret={0}&", appSecret);
            sb.AppendFormat("redirect_uri={0}", redirectUrl);
            if (!string.IsNullOrEmpty(state))
            {
                sb.AppendFormat("&state={0}", state);
            }
            byte[] dataBytes = Encoding.UTF8.GetBytes(sb.ToString());
            gateway += sb.ToString();
            //s.Close();
            string json = await HttpHelp.GetStrAsync(gateway);
            Result result = await Task.Factory.StartNew(() => { return JsonConvert.DeserializeObject<Result>(json); });
            result.taobao_user_nick = HttpUtility.UrlDecode(result.taobao_user_nick);
            AuthorizationResult authorizationResult = new AuthorizationResult()
            {
                Token = result.access_token,
                TokenType = result.token_type,
                RefreshToken = result.refresh_token,
                ExpireAt = DateTime.Now.AddMinutes(-3).AddSeconds(result.expires_in),
                OpenId = result.taobao_user_id,
                UserName = result.taobao_user_nick
            };
            return authorizationResult;
        }

        /// <summary>
        /// 通讯函数
        /// </summary>
        /// <param name="url">URL</param>
        /// <param name="method">请求方式 GET/POST</param>
        /// <returns></returns>
        private string GetWebRequest(string url, string method)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.ContentType = "application/x-www-form-urlencoded";
            req.ServicePoint.Expect100Continue = false;
            req.Method = method;
            req.Timeout = 10000;
            var test = req.GetResponse();

            string json = "";
            if (test != null)
            {
                using (Stream stream = test.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(stream);
                    json = reader.ReadToEnd();
                }
            }
            return json;
        }

        /// <summary>
        /// 
        /// </summary>
        private struct Result
        {
            public string access_token { get; set; }
            public string token_type { get; set; }
            public int expires_in { get; set; }
            public string refresh_token { get; set; }
            public int re_expires_in { get; set; }
            public int r1_expires_in { get; set; }
            public int r2_expires_in { get; set; }
            public int w1_expires_in { get; set; }
            public int w2_expires_in { get; set; }
            public string taobao_user_nick { get; set; }
            public string taobao_user_id { get; set; }
        }

    }
}