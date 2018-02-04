/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/28 11:48:59
 * ****************************************************************/
using System.Web.Routing;

namespace SharpSword
{
    /// <summary>
    /// 路由注册接口，API接口扩展如果需要注册路由，需要继承此注册类，系统框架在第一次启动时候自动进行注册
    /// </summary>
    public interface IRouteProvider
    {
        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="routes">路由表</param>
        void RegisterRoutes(RouteCollection routes);

        /// <summary>
        /// 优先级，优先级越高越先注册(这是由于MVC的路由框架决定的，
        /// 因为一旦定义了多个路由，系统一旦找到了和URL匹配的路由规则，将会直接返回，而不会继续寻找后续的路由配置)
        /// </summary>
        int Priority { get; }
    }
}
