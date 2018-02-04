/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/2 13:28:53
 * ****************************************************************/
using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace SharpSword.Host
{
    /// <summary>
    /// 路由注册，系统框架会自动注册此路由
    /// </summary>
    internal class RouteProvider : RouteProviderBase
    {
        /// <summary>
        /// 注册接口路由设置
        /// </summary>
        /// <param name="routes"></param>
        public override void RegisterRoutes(RouteCollection routes)
        {
            //API接口框架欢迎页入口 /
            routes.MapRoute(
                                name: "SharpSword.Host.MVC_01",
                                url: "{controller}/{action}",
                                defaults: new { controller = "Home", action = "Index" },
                                namespaces: new string[] { "SharpSword.Host.Controllers" });
        }

        /// <summary>
        /// 优先级，将优先级改成最小，方便后续创建可以重写框架注册的路由
        /// </summary>
        public override int Priority
        {
            get { return Int32.MinValue; }
        }
    }
}
