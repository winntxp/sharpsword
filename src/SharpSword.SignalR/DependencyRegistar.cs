/******************************************************************
* SharpSword zhangliang@sharpsword.com.cn 2015/11/25 11:48:48
* *****************************************************************/
using Autofac;
using Autofac.Integration.SignalR;
using System.Reflection;

namespace SharpSword.SignalR
{
    public class DependencyRegistar : IDependencyRegistar
    {
        public void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.RegisterHubs(Assembly.GetExecutingAssembly()).PropertiesAutowired();
        }

        public int Priority
        {
            get { return 1; }
        }
    }
}
