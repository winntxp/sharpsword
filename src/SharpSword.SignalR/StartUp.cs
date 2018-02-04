/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/27/2016 9:39:47 AM
 * *******************************************************/
using Autofac.Integration.SignalR;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SharpSword.SignalR.StartUp))]
namespace SharpSword.SignalR
{
    /// <summary>
    /// 
    /// </summary>
    public class StartUp : IStartUp
    {
        /// <summary>
        /// 
        /// </summary>
        public int Priority { get { return 0; } }

        /// <summary>
        /// 
        /// </summary>
        public void Init()
        {
            GlobalHost.DependencyResolver = new AutofacDependencyResolver(ServicesContainer.Current.Container);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            app.Use((context, next) =>
            {
                context.Response.Headers.Append("X-Power", "SharpSword-SignalR");
                return next();
            });
        }
    }
}
