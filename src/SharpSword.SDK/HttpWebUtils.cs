/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/3/2015 8:29:56 AM
 * ****************************************************************/
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace SharpSword.SDK
{
    /// <summary>
    /// SDK�ͻ���HTTP������
    /// </summary>
    internal class HttpWebUtils : IHttpWebUtils
    {
        /// <summary>
        /// 
        /// </summary>
        private static string _version;

        /// <summary>
        /// HTTP����ʱʱ�䣬��λ������
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        static HttpWebUtils()
        {
            _version = typeof(IApiClient).Assembly.GetName().Version.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public HttpWebUtils()
        {
            this.Timeout = 100000;
        }

        /// <summary>
        /// ִ��HTTP POST����
        /// </summary>
        /// <param name="url">�����ַ</param>
        /// <param name="parameters">�������</param>
        /// <returns>HTTP��Ӧ</returns>
        public HttpRespBody DoPost(string url, IDictionary<string, string> parameters)
        {
            HttpWebRequest httpWebRequest = GetHttpWebRequest(url, "POST");
            httpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            byte[] postData = Encoding.UTF8.GetBytes(BuildQuery(parameters));
            Stream reqStream = httpWebRequest.GetRequestStream();
            reqStream.Write(postData, 0, postData.Length);
            reqStream.Close();
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Encoding encoding = Encoding.GetEncoding(httpWebResponse.CharacterSet);
            return HttpWebResponseToString(httpWebResponse, encoding);
        }

        /// <summary>
        /// ִ��HTTP POST����
        /// </summary>
        /// <param name="url">�����ַ</param>
        /// <param name="textParams">�������</param>
        /// <param name="fileParams">�ϴ��ļ�</param>
        /// <returns>HTTP��Ӧ</returns>
        public HttpRespBody DoPost(string url, IDictionary<string, string> textParams, IDictionary<string, FileItem> fileParams)
        {
            if (fileParams == null || fileParams.Count == 0)
            {
                return this.DoPost(url, textParams);
            }
            string boundary = DateTime.Now.Ticks.ToString("X");
            HttpWebRequest webRequest = this.GetHttpWebRequest(url, "POST");
            webRequest.ContentType = "multipart/form-data;charset=utf-8;boundary=" + boundary;
            Stream requestStream = webRequest.GetRequestStream();
            byte[] bytes = Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
            string contentDisposition = "Content-Disposition:form-data;name=\"{0}\"\r\nContent-Type:text/plain\r\n\r\n{1}";
            IEnumerator<KeyValuePair<string, string>> enumerator = textParams.GetEnumerator();
            while (enumerator.MoveNext())
            {
                string content = string.Format(contentDisposition, enumerator.Current.Key, enumerator.Current.Value);
                byte[] bytes3 = Encoding.UTF8.GetBytes(content);
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Write(bytes3, 0, bytes3.Length);
            }
            string format = "Content-Disposition:form-data;name=\"{0}\";filename=\"{1}\"\r\nContent-Type:{2}\r\n\r\n";
            IEnumerator<KeyValuePair<string, FileItem>> enumerator2 = fileParams.GetEnumerator();
            while (enumerator2.MoveNext())
            {
                string s2 = string.Format(format, enumerator2.Current.Key, enumerator2.Current.Value.GetFileName(), enumerator2.Current.Value.GetMimeType());
                byte[] bytes4 = Encoding.UTF8.GetBytes(s2);
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Write(bytes4, 0, bytes4.Length);
                byte[] content = enumerator2.Current.Value.GetContent();
                requestStream.Write(content, 0, content.Length);
            }
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();
            Encoding encoding = Encoding.GetEncoding(httpWebResponse.CharacterSet);
            return this.HttpWebResponseToString(httpWebResponse, encoding);
        }

        /// <summary>
        /// ִ��HTTP GET����
        /// </summary>
        /// <param name="url">�����ַ</param>
        /// <param name="parameters">�������</param>
        /// <returns>HTTP��Ӧ</returns>
        public HttpRespBody DoGet(string url, IDictionary<string, string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                if (url.Contains("?"))
                {
                    url = url + "&" + BuildQuery(parameters);
                }
                else
                {
                    url = url + "?" + BuildQuery(parameters);
                }
            }
            HttpWebRequest httpWebRequest = GetHttpWebRequest(url, "GET");
            httpWebRequest.ContentType = "application/x-www-form-urlencoded;charset=utf-8";
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            Encoding encoding = Encoding.GetEncoding(httpWebResponse.CharacterSet);
            return HttpWebResponseToString(httpWebResponse, encoding);
        }

        /// <summary>
        /// ����HttpWebRequest
        /// </summary>
        /// <param name="url">�����ַ</param>
        /// <param name="method">POST/GET</param>
        /// <returns></returns>
        private HttpWebRequest GetHttpWebRequest(string url, string method)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ServicePoint.Expect100Continue = false;
            httpWebRequest.Method = method;
            httpWebRequest.KeepAlive = true;
            httpWebRequest.UserAgent = "User-Agent: SharpSword-SDK-Client/{0}".With(_version);
            httpWebRequest.Referer = "";
            httpWebRequest.Timeout = this.Timeout;
            httpWebRequest.Proxy = null;
            return httpWebRequest;
        }

        /// <summary>
        /// ����Ӧ��ת��Ϊ�ı���
        /// </summary>
        /// <param name="httpResponse">��Ӧ������</param>
        /// <param name="encoding">���뷽ʽ</param>
        /// <returns>��Ӧ�ı�</returns>
        private HttpRespBody HttpWebResponseToString(HttpWebResponse httpResponse, Encoding encoding)
        {
            Stream stream = null;
            StreamReader reader = null;
            try
            {
                //���ַ����ķ�ʽ��ȡHTTP��Ӧ
                stream = httpResponse.GetResponseStream();
                reader = new StreamReader(stream, encoding);
                string body = reader.ReadToEnd();
                //�����µ��������
                HttpRespBody respBody = new HttpRespBody(httpResponse.StatusCode, body);
                //���һ��http״̬��
                respBody.Headers.Add("StatusCode", httpResponse.StatusCode.ToString());
                //��httpHeader���ϱ��浽�������
                httpResponse.Headers.AllKeys.ToList().ForEach(key =>
                {
                    if (!respBody.Headers.ContainsKey(key))
                    {
                        respBody.Headers.Add(key, httpResponse.Headers[key]);
                    }
                });
                return respBody;
            }
            finally
            {
                // �ͷ���Դ
                if (reader != null) reader.Close();
                if (stream != null) stream.Close();
                if (httpResponse != null) httpResponse.Close();
            }
        }

        /// <summary>
        /// ��װGET����URL��
        /// </summary>
        /// <param name="url">�����ַ</param>
        /// <param name="parameters">�������</param>
        /// <returns>��������GET����URL</returns>
        private string BuildGetUrl(string url, IDictionary<string, string> parameters)
        {
            if (parameters != null && parameters.Count > 0)
            {
                if (url.Contains("?"))
                {
                    url = url + "&" + BuildQuery(parameters);
                }
                else
                {
                    url = url + "?" + BuildQuery(parameters);
                }
            }
            return url;
        }

        /// <summary>
        /// ��װ��ͨ�ı����������
        /// </summary>
        /// <param name="parameters">Key-Value��ʽ��������ֵ�</param>
        /// <returns>URL��������������</returns>
        private string BuildQuery(IDictionary<string, string> parameters)
        {
            StringBuilder postData = new StringBuilder();
            bool hasParam = false;
            IEnumerator<KeyValuePair<string, string>> dem = parameters.GetEnumerator();
            while (dem.MoveNext())
            {
                string name = dem.Current.Key;
                string value = dem.Current.Value;
                // ���Բ����������ֵΪ�յĲ���
                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(value))
                {
                    if (hasParam)
                    {
                        postData.Append("&");
                    }

                    postData.Append(name);
                    postData.Append("=");
                    postData.Append(HttpUtility.UrlEncode(value)); //Uri.EscapeDataString(value)
                    hasParam = true;
                }
            }

            return postData.ToString();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    internal class RestWebUtils : IHttpWebUtils
    {
        /// <summary>
        /// 
        /// </summary>
        private static IRestClient _restClient;
        private string _baseUrl;
        private static object locker = new object();

        /// <summary>
        /// 
        /// </summary>
        public RestWebUtils(string url)
        {
            this.Timeout = 100000;
            this._baseUrl = url;
        }

        /// <summary>
        /// ��ʱʱ�䣬��λ����
        /// </summary>
        public int Timeout { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private IRestClient RestClient
        {
            get
            {
                if (_restClient == null)
                {
                    lock (locker)
                    {
                        _restClient = new RestClient(this._baseUrl);
                        _restClient.Timeout = this.Timeout;
                        _restClient.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.81 Safari/537.36";
                        _restClient.Encoding = Encoding.GetEncoding("UTF-8");
                    }
                }
                return _restClient;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public HttpRespBody DoGet(string url, IDictionary<string, string> parameters)
        {
            var postRequest = new RestRequest("", Method.GET);
            postRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded;charset:UTF-8");
            postRequest.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*;q=0.8");
            foreach (var item in parameters)
            {
                postRequest.AddParameter(item.Key, item.Value);
            }
            var resp = this.RestClient.Execute(postRequest);
            return new HttpRespBody(resp.StatusCode, resp.Content);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public HttpRespBody DoPost(string url, IDictionary<string, string> parameters)
        {
            var postRequest = new RestRequest("", Method.POST);
            postRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded;charset:UTF-8");
            postRequest.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*;q=0.8");
            foreach (var item in parameters)
            {
                postRequest.AddParameter(item.Key, item.Value);
            }
            var resp = this.RestClient.Execute(postRequest);
            return new HttpRespBody(resp.StatusCode, resp.Content);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <param name="textParams"></param>
        /// <param name="fileParams"></param>
        /// <returns></returns>
        public HttpRespBody DoPost(string url, IDictionary<string, string> parameters, IDictionary<string, FileItem> fileParams)
        {
            var postRequest = new RestRequest("", Method.POST);
            postRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded;charset:UTF-8");
            postRequest.AddHeader("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*;q=0.8");
            foreach (var item in parameters)
            {
                postRequest.AddParameter(item.Key, item.Value);
            }
            foreach (var item in fileParams)
            {
                postRequest.AddFile(item.Key, item.Value.GetContent(), item.Value.GetFileName());
            }
            var resp = this.RestClient.Execute(postRequest);
            return new HttpRespBody(resp.StatusCode, resp.Content);
        }
    }
}
