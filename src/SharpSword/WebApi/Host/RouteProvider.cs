/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/2 13:28:53
 * ****************************************************************/
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace SharpSword.WebApi.Host
{
    /// <summary>
    /// 路由注册，系统框架会自动注册此路由
    /// </summary>
    internal class RouteProvider : RouteProviderBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IActionSelector _actionSelector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionSelector"></param>
        public RouteProvider(IActionSelector actionSelector)
        {
            _actionSelector = actionSelector;
        }

        /// <summary>
        /// 注册接口路由设置
        /// </summary>
        /// <param name="routes"></param>
        public override void RegisterRoutes(RouteCollection routes)
        {
            //API接口框架统一接口访问入口 /api? 扩展框架需要注意下，/api 开头为系统框架使用的，请勿占用
            //routes.MapRoute(
            //    name: "ServiceCenter.Api.Core.V20_API_01",
            //    url: "Api/{ActionName}/{Format}/{Version}",
            //    defaults: new { controller = "Api", action = "RequestHander", version = "", format = "View" },
            //    namespaces: new string[] { "ServiceCenter.Api.Core.Host" });

            //只在点命名空间里查找
            var @namespace = this.GetControllerNameSpace<ApiController>();

            var controllerName = this.GetControllerName<ApiController>();

            routes.MapRoute(
                name: "SharpSword_Index",
                url: "Api/Index",
                defaults: new { controller = controllerName, action = "Index" },
                namespaces: new string[] { @namespace });

            //框架内部帮助创插件入口 /help
            routes.MapRoute(
                name: "SharpSword_Help",
                url: "Api/Help",
                defaults: new { controller = controllerName, action = "Help" },
                namespaces: new string[] { @namespace });
            routes.MapRoute(
                name: "SharpSword_Help_XML",
                url: "Api/Help/XML",
                defaults: new { controller = controllerName, action = "HelpXml" },
                namespaces: new string[] { @namespace });
            routes.MapRoute(
                name: "SharpSword_Help_JSON",
                url: "Api/Help/JSON",
                defaults: new { controller = controllerName, action = "HelpJson" },
                namespaces: new string[] { @namespace });
            routes.MapRoute(
                name: "SharpSword_Help_VIEW",
                url: "Api/Help/VIEW",
                defaults: new { controller = controllerName, action = "HelpView" },
                namespaces: new string[] { @namespace });

            //注册有接口的特性路由器注册
            foreach (var actionDescriptor in this._actionSelector.GetActionDescriptors()
                .Where(o => !o.Route.IsNull() && !o.Route.Url.IsNullOrEmpty()))
            {
                //如果不包含自动添加默认接口名称作为接口参数
                if (actionDescriptor.Route.Url.Contains("{") || actionDescriptor.Route.Url.Contains("}"))
                {
                    throw new SharpSwordCoreException(Resource.CoreResource.RoutePublisher_ForbiddenCharacter.With(actionDescriptor.ActionName, "{", "}"));
                }
                //设置路由
                routes.MapRoute(
                    name: "Route.{0}.{1}".With(actionDescriptor.ActionName, actionDescriptor.Version),
                    url: actionDescriptor.Route.Url,
                    defaults: new { Controller = "Api", Action = "RequestHander", ActionName = actionDescriptor.ActionName },
                    namespaces: new string[] { @namespace });
            }

            //统一的API接口我们放后面
            routes.MapRoute(
                name: RoutePublisher.WebApiRouteName,
                url: "Api",
                defaults: new { controller = controllerName, action = "RequestHander" },
                namespaces: new string[] { @namespace });
        }

        /// <summary>
        /// 优先级，将优先级改成最小，方便后续创建可以重写框架注册的路由
        /// </summary>
        public override int Priority
        {
            get { return this.DefaultPriority; }
        }
    }
}
