using System;
using System.Collections.Specialized;
using System.Web;
using System.Web.Mvc;

namespace SharpSword.Fakes
{
    /// <summary>
    /// HttpRequestBase模拟类
    /// </summary>
    public class FakeHttpRequest : HttpRequestBase
    {
        private readonly HttpCookieCollection _cookies;
        private readonly NameValueCollection _formParams;
        private readonly NameValueCollection _queryStringParams;
        private readonly NameValueCollection _headers;
        private readonly NameValueCollection _serverVariables;
        private readonly string _relativeUrl;
        private readonly Uri _url;
        private readonly Uri _urlReferrer;
        private readonly string _httpMethod;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="relativeUrl"></param>
        /// <param name="method"></param>
        /// <param name="formParams"></param>
        /// <param name="queryStringParams"></param>
        /// <param name="cookies"></param>
        /// <param name="serverVariables"></param>
        public FakeHttpRequest(string relativeUrl, string method,
            NameValueCollection formParams, NameValueCollection queryStringParams,
            HttpCookieCollection cookies, NameValueCollection serverVariables)
        {
            _httpMethod = method;
            _relativeUrl = relativeUrl;
            _formParams = formParams;
            _queryStringParams = queryStringParams;
            _cookies = cookies;
            _serverVariables = serverVariables;
            //ensure collections are not null
            if (_formParams == null)
                _formParams = new NameValueCollection();
            if (_queryStringParams == null)
                _queryStringParams = new NameValueCollection();
            if (_cookies == null)
                _cookies = new HttpCookieCollection();
            if (_serverVariables == null)
                _serverVariables = new NameValueCollection();
            if (_headers == null)
                _headers = new NameValueCollection();
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.FakeHttpRequest(string, string, Uri, Uri, NameValueCollection, NameValueCollection, HttpCookieCollection, NameValueCollection)'
        public FakeHttpRequest(string relativeUrl, string method, Uri url, Uri urlReferrer,
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.FakeHttpRequest(string, string, Uri, Uri, NameValueCollection, NameValueCollection, HttpCookieCollection, NameValueCollection)'
            NameValueCollection formParams, NameValueCollection queryStringParams,
            HttpCookieCollection cookies, NameValueCollection serverVariables)
            : this(relativeUrl, method, formParams, queryStringParams, cookies, serverVariables)
        {
            _url = url;
            _urlReferrer = urlReferrer;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.FakeHttpRequest(string, Uri, Uri)'
        public FakeHttpRequest(string relativeUrl, Uri url, Uri urlReferrer)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.FakeHttpRequest(string, Uri, Uri)'
            : this(relativeUrl, HttpVerbs.Get.ToString("g"), url, urlReferrer, null, null, null, null)
        {
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.ServerVariables'
        public override NameValueCollection ServerVariables
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.ServerVariables'
        {
            get
            {
                return _serverVariables;
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.Form'
        public override NameValueCollection Form
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.Form'
        {
            get { return _formParams; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.QueryString'
        public override NameValueCollection QueryString
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.QueryString'
        {
            get { return _queryStringParams; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.Headers'
        public override NameValueCollection Headers
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.Headers'
        {
            get { return _headers; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.Cookies'
        public override HttpCookieCollection Cookies
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.Cookies'
        {
            get { return _cookies; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.AppRelativeCurrentExecutionFilePath'
        public override string AppRelativeCurrentExecutionFilePath
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.AppRelativeCurrentExecutionFilePath'
        {
            get { return _relativeUrl; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.Url'
        public override Uri Url
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.Url'
        {
            get
            {
                return _url;
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.UrlReferrer'
        public override Uri UrlReferrer
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.UrlReferrer'
        {
            get
            {
                return _urlReferrer;
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.PathInfo'
        public override string PathInfo
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.PathInfo'
        {
            get { return ""; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.ApplicationPath'
        public override string ApplicationPath
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.ApplicationPath'
        {
            get
            {
                //we know that relative paths always start with ~/
                //ApplicationPath should start with /
                if (_relativeUrl != null && _relativeUrl.StartsWith("~/"))
                    return _relativeUrl.Remove(0, 1);
                return null;
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.HttpMethod'
        public override string HttpMethod
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.HttpMethod'
        {
            get
            {
                return _httpMethod;
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.UserHostAddress'
        public override string UserHostAddress
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.UserHostAddress'
        {
            get { return null; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.RawUrl'
        public override string RawUrl
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.RawUrl'
        {
            get { return null; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.IsSecureConnection'
        public override bool IsSecureConnection
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.IsSecureConnection'
        {
            get { return false; }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.IsAuthenticated'
        public override bool IsAuthenticated
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.IsAuthenticated'
        {
            get
            {
                return false;
            }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.UserLanguages'
        public override string[] UserLanguages
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpRequest.UserLanguages'
        {
            get { return null; }
        }
    }
}