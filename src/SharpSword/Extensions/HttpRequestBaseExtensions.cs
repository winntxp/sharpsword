/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/17 11:12:20
 * ****************************************************************/
using System.Web;

namespace SharpSword
{
    /// <summary>
    /// HttpRequest扩展类
    /// </summary>
    public static class HttpRequestBaseExtensions
    {
        /// <summary>
        /// 判断一个HttpRequest是否是AJAX请求
        /// </summary>
        /// <param name="request">当前httpRequest</param>
        /// <returns>返回是否是AJAX请求</returns>
        public static bool IsAjaxRequest(this HttpRequestBase request)
        {
            //request参数不能为null
            request.CheckNullThrowArgumentNullException(nameof(request));
            //直接判断下请求头是否包含Ajax请求头
            return request["X-Requested-With"] == "XMLHttpRequest" ||
                   (request.Headers != null && request.Headers["X-Requested-With"] == "XMLHttpRequest");
        }

        /// <summary>
        /// 将URL地址转换成本地地址
        /// </summary>
        /// <param name="request">当前httpRequest</param>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsUrlLocalToHost(this HttpRequestBase request, string url)
        {
            return !url.IsNullOrEmpty() &&
                    ((url[0] == '/' && (url.Length == 1 || (url[1] != '/' && url[1] != '\\'))) || (url.Length > 1 && url[0] == '~' && url[1] == '/'));
        }

        /// <summary>
        /// 获取客户端IP地址；如果都未找到就会返回string.empty
        /// </summary>
        /// <returns></returns>
        public static string GetClientIp(this HttpRequestBase request)
        {
            //request参数不能为null
            request.CheckNullThrowArgumentNullException(nameof(request));

            try
            {
                //环境变量里有客户端IP信息，直接返回
                string clientIp = request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (!clientIp.IsNullOrEmpty())
                {
                    return clientIp;
                }

                //客户端提交了远程客户端地址
                clientIp = request.ServerVariables["REMOTE_ADDR"];
                if (!clientIp.IsNullOrEmpty())
                {
                    if (clientIp == "::1")
                    {
                        return "127.0.0.1";
                    }

                    return clientIp;
                }

                clientIp = request.UserHostAddress;
                if (!clientIp.IsNullOrEmpty())
                {
                    return clientIp;
                }

                //IPV6
                if (clientIp == "::1")
                {
                    clientIp = "127.0.0.1";
                }
            }
            catch
            {
                // ignored
            }
            return string.Empty;
        }

        /// <summary>
        /// 获取服务器地址，比如：192.168.0.1
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string LocalAddr(this HttpRequestBase request)
        {
            var localAddr = request.ServerVariables["LOCAL_ADDR"];
            return localAddr.IsNullOrEmptyForDefault(() => string.Empty, str => str.Equals("::1") ? "127.0.0.1" : str);
        }

        /// <summary>
        /// 获取请求的域名信息，比如：www.api.com
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string HttpHost(this HttpRequestBase request)
        {
            return request.ServerVariables["HTTP_HOST"] ?? request.LocalAddr();
        }

        /// <summary>
        /// 获取当前页码的来源
        /// </summary>
        /// <returns></returns>
        public static string GetUrlReferrer(this HttpRequestBase request)
        {
            if (!request.UrlReferrer.IsNull())
                return request.UrlReferrer.PathAndQuery;
            return string.Empty;
        }

        /// <summary>
        /// 获取查询参数值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <param name="name">URL参数名称</param>
        /// <returns></returns>
        public static T QueryString<T>(this HttpRequestBase request, string name)
        {
            request.CheckNullThrowArgumentNullException(nameof(request));

            string queryParamValue = null;

            if (!request.QueryString[name].IsNull())
            {
                queryParamValue = request.QueryString[name];
            }

            if (!queryParamValue.IsNullOrEmpty())
            {
                return queryParamValue.As<T>();
            }

            //返回泛型的默认值
            return default(T);
        }
    }
}
