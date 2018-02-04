/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/25 13:23:07
 * ****************************************************************/
using System;
using System.IO;
using System.Net;
using System.Text;

namespace SharpSword
{
    /// <summary>
    /// 模拟登录抓取页面
    /// </summary>
    public class WebHttpHelper
    {
        /// <summary>
        /// 
        /// </summary>
        public class HttpHeader
        {
            /// <summary>
            /// application/x-www-form-urlencoded
            /// </summary>
            public string ContentType { get; set; }
            /// <summary>
            /// image/gif, image/x-xbitmap, image/jpeg, image/pjpeg, application/x-shockwave-flash, application/x-silverlight, application/vnd.ms-excel, application/vnd.ms-powerpoint, application/msword, application/x-ms-application, application/x-ms-xbap, application/vnd.ms-xpsdocument, application/xaml+xml, application/x-silverlight-2-b1, */*
            /// </summary>
            public string Accept { get; set; }
            /// <summary>
            /// Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 5.1; .NET CLR 2.0.50727; .NET CLR 3.0.04506.648; .NET CLR 3.5.21022)
            /// </summary>
            public string UserAgent { get; set; }
            /// <summary>
            /// POST/GET
            /// </summary>
            public string Method { get; set; }
            /// <summary>
            /// 300
            /// </summary>
            public int MaxTry { get; set; }
        }

        /// <summary>
        /// 获取登录获取CooKie
        /// </summary>
        /// <param name="loginUrl"></param>
        /// <param name="postdata"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static CookieContainer GetCooKie(string loginUrl, string postdata, HttpHeader header)
        {
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            try
            {
                CookieContainer cookieContainer = new CookieContainer();
                request = (HttpWebRequest)WebRequest.Create(loginUrl);
                request.Method = header.Method;
                request.ContentType = header.ContentType;
                byte[] postdatabyte = Encoding.UTF8.GetBytes(postdata);
                request.ContentLength = postdatabyte.Length;
                request.AllowAutoRedirect = false;
                request.CookieContainer = cookieContainer;
                request.KeepAlive = true;

                //提交请求
                Stream stream;
                stream = request.GetRequestStream();
                stream.Write(postdatabyte, 0, postdatabyte.Length);
                stream.Close();

                //接收响应
                response = (HttpWebResponse)request.GetResponse();
                response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);

                CookieCollection cook = response.Cookies;

                //Cookie字符串格式
                string strcrook = request.CookieContainer.GetCookieHeader(request.RequestUri);

                return cookieContainer;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取html
        /// </summary>
        /// <param name="getUrl"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static string GetHtml(string getUrl, CookieContainer cookieContainer, HttpHeader header)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;
            try
            {
                httpWebRequest = (HttpWebRequest)WebRequest.Create(getUrl);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = header.ContentType;
                httpWebRequest.ServicePoint.ConnectionLimit = header.MaxTry;
                httpWebRequest.Referer = getUrl;
                httpWebRequest.Accept = header.Accept;
                httpWebRequest.UserAgent = header.UserAgent;
                httpWebRequest.Method = "GET";
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();
                StreamReader streamReader = new StreamReader(responseStream, Encoding.UTF8);
                string html = streamReader.ReadToEnd();
                streamReader.Close();
                responseStream.Close();
                httpWebRequest.Abort();
                httpWebResponse.Close();
                return html;
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                if (httpWebRequest != null)
                    httpWebRequest.Abort();
                if (httpWebResponse != null)
                    httpWebResponse.Close();
                return string.Empty;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="getUrl"></param>
        /// <param name="cookieContainer"></param>
        /// <param name="header"></param>
        /// <returns></returns>
        public static byte[] DownFile(string getUrl, CookieContainer cookieContainer, HttpHeader header)
        {
            HttpWebRequest httpWebRequest = null;
            HttpWebResponse httpWebResponse = null;

            try
            {

                httpWebRequest = (HttpWebRequest)WebRequest.Create(getUrl);
                httpWebRequest.CookieContainer = cookieContainer;
                httpWebRequest.ContentType = header.ContentType;
                httpWebRequest.ServicePoint.ConnectionLimit = header.MaxTry;
                httpWebRequest.Referer = getUrl;
                httpWebRequest.Accept = header.Accept;
                httpWebRequest.UserAgent = header.UserAgent;
                httpWebRequest.Method = "GET";
                httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream responseStream = httpWebResponse.GetResponseStream();

                MemoryStream fs = new MemoryStream();

                int bufferSize = 2048;
                byte[] bytes = new byte[bufferSize];

                try
                {
                    int length = responseStream.Read(bytes, 0, bufferSize);
                    while (length > 0)
                    {
                        fs.Write(bytes, 0, length);
                        length = responseStream.Read(bytes, 0, bufferSize);
                    }
                    responseStream.Close();
                    httpWebResponse.Close();
                }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
                catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
                {
                    return null;
                }


                return fs.GetBytes();
            }
#pragma warning disable CS0168 // The variable 'ex' is declared but never used
            catch (Exception ex)
#pragma warning restore CS0168 // The variable 'ex' is declared but never used
            {
                if (httpWebRequest != null)
                    httpWebRequest.Abort();
                if (httpWebResponse != null)
                    httpWebResponse.Close();
                return null;
            }
        }
    }
}
