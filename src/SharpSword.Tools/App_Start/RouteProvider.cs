/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/2 13:28:53
 * ****************************************************************/
using System.Web.Mvc;
using System.Web.Routing;

namespace SharpSword.Tools
{
    /// <summary>
    /// 路由注册，系统框架会自动注册此路由
    /// </summary>
    internal class RouteProvider : IRouteProvider
    {
        /// <summary>
        /// 注册接口路由设置
        /// </summary>
        /// <param name="routes"></param>
        public void RegisterRoutes(RouteCollection routes)
        {
            //调试工具
            routes.MapRoute(name: "SharpSword.ApiTestTool_ApiTool",
                            url: "ApiTool",
                            defaults: new { controller = "ApiTest", action = "ApiTool" },
                            namespaces: new string[] { "SharpSword.ApiTestTool.Controllers" });

            routes.MapRoute(name: "SharpSword.ApiTestTool_ActionsGet",
                            url: "ApiTool/ActionsGet",
                            defaults: new { controller = "ApiTest", action = "ActionsGet" },
                            namespaces: new string[] { "SharpSword.ApiTestTool.Controllers" });

            routes.MapRoute(name: "SharpSword.ApiTestTool_GetRequestDto",
                url: "ApiTool/GetRequestDto",
                defaults: new { controller = "ApiTest", action = "GetRequestDto" },
                namespaces: new string[] { "SharpSword.ApiTestTool.Controllers" });
            
        }

        /// <summary>
        /// 优先级，设置为0，覆盖掉系统框架注册的路由
        /// </summary>
        public int Priority
        {
            get { return 0; }
        }
    }
}
