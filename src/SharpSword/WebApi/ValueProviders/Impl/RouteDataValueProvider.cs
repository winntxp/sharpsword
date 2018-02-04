/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/5 14:42:55
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SharpSword.WebApi.ValueProviders.Impl
{
    /// <summary>
    /// 路由数据字典值提供器
    /// </summary>
    public class RouteDataValueProvider : ValueProviderBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDictionary<string, object> _valueDictionary =
            new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        public RouteDataValueProvider(HttpContextBase httpContext)
        {
            httpContext.CheckNullThrowArgumentNullException(nameof(httpContext));
            var mvcHandler = httpContext.CurrentHandler as MvcHandler;
            if (mvcHandler.IsNull())
            {
                return;
            }
            //var routeData = mvcHandler.RequestContext.RouteData;
            //获取路由数据
            var routeData = RouteTable.Routes.GetRouteData(httpContext);
            if (!routeData.IsNull())
            {
                foreach (var item in routeData.Values)
                {
                    _valueDictionary.Add(item.Key, item.Value);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IDictionary<string, object> GetValueDictionary()
        {
            return this._valueDictionary;
        }

        /// <summary>
        /// 
        /// </summary>
        public override int Order
        {
            get { return -1; }
        }
    }
}
