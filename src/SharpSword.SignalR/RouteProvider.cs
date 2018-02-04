/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/28 12:28:35
 * ****************************************************************/
using Microsoft.AspNet.SignalR;
using System.Web.Routing;

namespace SharpSword.SignalR
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
        public override void RegisterRoutes(RouteCollection routes)
        {
            //RouteTable.Routes.MapHubs();
        }

        /// <summary>
        /// 优先级
        /// </summary>
        public override int Priority
        {
            get { return 10; }
        }
    }
}
