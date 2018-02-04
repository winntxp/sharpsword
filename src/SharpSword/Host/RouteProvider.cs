/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/2 13:28:53
 * ****************************************************************/
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
            var @namespace = this.GetControllerNameSpace<ResourceController>();

            //所有资源获取类  /Resource?resourceName=jquery-1.9.1.min.js
            routes.MapRoute(name: "SharpSword_GetResource",
                                    url: "GetResource",
                                    defaults: new { controller = "Resource", action = "GetResource" },
                                    namespaces: new string[] { @namespace });

            //获取资源的另外一种方式：/Resource/jquery-1.9.1.min.js
            routes.MapRoute(name: "SharpSword_GetResource_01",
                                    url: "Resource/{*resourceName}",
                                    defaults: new { controller = "Resource", action = "GetResource" },
                                    namespaces: new string[] { @namespace });
        }

        /// <summary>
        /// 优先级，将优先级改成最小，方便后续创建可以重写框架注册的路由
        /// </summary>
        public override int Priority
        {
            get { return this.DefaultPriority + 1; }
        }
    }
}
