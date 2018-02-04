/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/2 13:28:53
 * ****************************************************************/
using System.Web.Mvc;
using System.Web.Routing;

namespace SharpSword.SdkBuilder.CSharp.Host
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
            //文档生成器
            routes.MapRoute(
                name: "SharpSword.SdkBuilder.CSharp_DocBuilder",
                url: "DocBuilder",
                defaults: new { controller = "SdkBuilderCSharp", action = "DocBuilder" },
                namespaces: new string[] { "SharpSword.SdkBuilder.CSharp.Host" });

            //下载SDK（c#系统框架默认的SDK输出）
            routes.MapRoute(
                name: "SharpSword.SdkBuilder.CSharp_CSharpDownSdk",
                url: "CSharpDownSdk",
                defaults: new { controller = "SdkBuilderCSharp", action = "CSharpDownSdk" },
                namespaces: new string[] { "SharpSword.SdkBuilder.CSharp.Host" });

            //下载SDK源码
            routes.MapRoute(
                name: "SharpSword.SdkBuilder.CSharp_CSharpDownSource",
                url: "CSharpDownSource",
                defaults: new { controller = "SdkBuilderCSharp", action = "CSharpDownSource" },
                namespaces: new string[] { "SharpSword.SdkBuilder.CSharp.Host" });

            //调试工具
            routes.MapRoute(
                name: "SharpSword.SdkBuilder.CSharp_ApiTool",
                url: "CSharpSdkBuilder",
                defaults: new { controller = "SdkBuilderCSharp", action = "CSharpSdkBuilder" },
                namespaces: new string[] { "SharpSword.SdkBuilder.CSharp.Host" });
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
