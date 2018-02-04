/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/2 13:28:53
 * ****************************************************************/
using System.Web.Mvc;
using System.Web.Routing;

namespace SharpSword.Routes.Installers
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
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.Ignore("favicon.ico");
            //routes.RouteExistingFiles = true;         
        }

        /// <summary>
        /// 将此优先级设置为最大，将此注册放在第一个注册
        /// </summary>
        public override int Priority
        {
            get { return int.MaxValue; }
        }
    }
}
