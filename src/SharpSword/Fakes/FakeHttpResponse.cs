using System.Text;
using System.Web;

namespace SharpSword.Fakes
{
    /// <summary>
    /// 
    /// </summary>
    public class FakeHttpResponse : HttpResponseBase
    {
        private readonly HttpCookieCollection _cookies;
        /// <summary>
        /// 
        /// </summary>
        public FakeHttpResponse()
        {
            this._cookies = new HttpCookieCollection();
        }
        private readonly StringBuilder _outputString = new StringBuilder();

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpResponse.ResponseOutput'
        public string ResponseOutput
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpResponse.ResponseOutput'
        {
            get { return _outputString.ToString(); }
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpResponse.StatusCode'
        public override int StatusCode { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpResponse.StatusCode'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpResponse.RedirectLocation'
        public override string RedirectLocation { get; set; }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpResponse.RedirectLocation'

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpResponse.Write(string)'
        public override void Write(string s)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpResponse.Write(string)'
        {
            _outputString.Append(s);
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpResponse.ApplyAppPathModifier(string)'
        public override string ApplyAppPathModifier(string virtualPath)
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpResponse.ApplyAppPathModifier(string)'
        {
            return virtualPath;
        }

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpResponse.Cookies'
        public override HttpCookieCollection Cookies
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member 'FakeHttpResponse.Cookies'
        {
            get
            {
                return _cookies;
            }
        }
    }
}