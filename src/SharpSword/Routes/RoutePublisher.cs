/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/28 11:16:49
 * ****************************************************************/
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace SharpSword
{
    /// <summary>
    /// 路由规则发布器
    /// </summary>
    public sealed class RoutePublisher : IRoutePublisher
    {
        /// <summary>
        /// WEBAPI路由名称
        /// </summary>
        public const string WebApiRouteName = "SHARPSWORD_WEBAPI";

        /// <summary>
        /// 
        /// </summary>
        private readonly ITypeFinder _typeFinder;
        private readonly IEnumerable<IRouteProvider> _routeProviders;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="routeProviders">所有路由注册提供者集合</param>
        public RoutePublisher(ITypeFinder typeFinder, IEnumerable<IRouteProvider> routeProviders)
        {
            this._typeFinder = typeFinder;
            this._routeProviders = routeProviders ?? new List<IRouteProvider>();
        }

        /// <summary>
        /// 批量注册所有实现IRouteProvider接口的路由规则
        /// </summary>
        /// <param name="routes">MVC全局静态路由表</param>
        public void RegisterRoutes(RouteCollection routes)
        {
            this._routeProviders.OrderByDescending(x => x.Priority)
                                .ToList()
                                .ForEach(x => x.RegisterRoutes(routes));
        }

        /// <summary>
        /// 所有注册的路由注册器
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IRouteProvider> GetRouteProviders()
        {
            return this._routeProviders;
        }
    }
}
