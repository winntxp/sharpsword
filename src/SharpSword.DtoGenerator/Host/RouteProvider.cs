/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/2 13:28:53
 * ****************************************************************/
using System.Web.Mvc;
using System.Web.Routing;

namespace SharpSword.DtoGenerator.Host
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
            var @namespace = this.GetControllerNameSpace<DtoGeneratorController>();

            //下载模板
            routes.MapRoute(name: "SharpSword_DtoGenerator_TempDown",
                            url: "DtoGenerator/TempleteDown",
                            defaults: new { controller = "DtoGenerator", action = "TempleteDown" },
                            namespaces: new string[] { @namespace });

            //DTO生成器地址
            routes.MapRoute(name: "SharpSword_DtoGenerator",
                            url: "DtoGenerator",
                            defaults: new { controller = "DtoGenerator", action = "DtoGenerator" },
                            namespaces: new string[] { @namespace });
        }

        /// <summary>
        /// 优先级，设置为0，覆盖掉系统框架注册的路由
        /// </summary>
        public override int Priority
        {
            get { return 0; }
        }
    }
}
