/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/28 12:28:35
 * ****************************************************************/
using SharpSword.AccessRecorder.DB.Controllers;
using System.Web.Mvc;

namespace SharpSword.AccessRecorder.DB
{
    /// <summary>
    /// 插件路由是、配置
    /// </summary>
    public class RouteProvider : RouteProviderBase
    {
        /// <summary>
        /// 路由配置
        /// </summary>
        /// <param name="routes"></param>
        public override void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            var @namespace = this.GetControllerNameSpace<AccessRecoderController>();
            var controllerName = this.GetControllerName<AccessRecoderController>();

            routes.MapRoute(
                name: "{0}.Logs".With(this.AssemblyName),
                url: "logs",
                defaults: new { controller = controllerName, action = "Logs" },
                namespaces: new string[] { @namespace });

            routes.MapRoute(
                name: "{0}.Search".With(this.AssemblyName),
                url: "logs/S",
                defaults: new { controller = controllerName, action = "Search" },
                namespaces: new string[] { @namespace });

            routes.MapRoute(
                name: "{0}.ActionsGet".With(this.AssemblyName),
                url: "logs/ActionsGet",
                defaults: new { controller = controllerName, action = "ActionsGet" },
                namespaces: new string[] { @namespace });

            routes.MapRoute(
                name: "{0}.Get".With(this.AssemblyName),
                url: "logs/{id}",
                defaults: new { controller = controllerName, action = "Get" },
                namespaces: new string[] { @namespace },
                constraints: new { id = "[0-9]{1,15}" });
        }

        /// <summary>
        /// 优先级
        /// </summary>
        public override int Priority => 10;
    }
}
