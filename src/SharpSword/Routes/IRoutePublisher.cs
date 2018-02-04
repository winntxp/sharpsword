/* ****************************************************************
 * SharpSword zhangliang4629@163.com 12/12/2016 12:57:53 PM
 * ****************************************************************/
using System.Collections.Generic;
using System.Web.Routing;

namespace SharpSword
{
    /// <summary>
    /// 框架路由连接器
    /// </summary>
    public interface IRoutePublisher
    {
        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="routes">全局路由表</param>
        void RegisterRoutes(RouteCollection routes);

        /// <summary>
        /// 获取所有注册的路由信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<IRouteProvider> GetRouteProviders();
    }
}
