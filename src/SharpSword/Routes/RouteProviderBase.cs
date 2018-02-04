/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/12/2016 3:06:59 PM
 * ****************************************************************/
using System.Web.Mvc;
using System.Web.Routing;

namespace SharpSword
{
    /// <summary>
    /// 路由注册器基类
    /// </summary>
    public abstract class RouteProviderBase : IRouteProvider
    {
        /// <summary>
        /// 优先级(优先级越高越先注册)
        /// </summary>
        public abstract int Priority { get; }

        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="routes"></param>
        public abstract void RegisterRoutes(RouteCollection routes);

        /// <summary>
        /// 获取控制器名称，去掉了:Controller结尾
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected string GetControllerName<T>() where T : IController
        {
            var controllerType = typeof(T);
            return controllerType.Name.Substring(0, controllerType.Name.Length - "Controller".Length);
        }

        /// <summary>
        /// 获取控制器所在的命名空间
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected string GetControllerNameSpace<T>() where T : IController
        {
            return typeof(T).Namespace;
        }

        /// <summary>
        /// 获取当前程序集的命名空间
        /// </summary>
        /// <returns></returns>
        protected string AssemblyName
        {
            get
            {
                return this.GetType().Assembly.GetName().Name;
            }
        }

        /// <summary>
        /// 系统框架默认注册的路由器位置，默认：int.MinValue + 9999
        /// </summary>
        protected int DefaultPriority
        {
            get { return int.MinValue + 9999; }
        }
    }
}
