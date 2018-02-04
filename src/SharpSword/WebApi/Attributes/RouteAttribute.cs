/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/28/2016 12:30:55 PM
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// Action接口特定的路由信息
    /// </summary>
    public class RouteAttribute : Attribute
    {
        /// <summary>
        /// 自定义接口特定的路由URL
        /// </summary>
        public string Url { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url">自定义接口特定的路由URL</param>
        public RouteAttribute(string url)
        {
            url.CheckNullThrowArgumentNullException(nameof(url));
            this.Url = url;
        }
    }
}
