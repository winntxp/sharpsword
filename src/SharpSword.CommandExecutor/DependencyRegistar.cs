/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2016 12:25:58 PM
 * ****************************************************************/
using Autofac;
using Autofac.Integration.Mvc;
using SharpSword.CommandExecutor.Parameters;
using System.Reflection;

namespace SharpSword.CommandExecutor
{
    /// <summary>
    /// IOC注册
    /// </summary>
    public class DependencyRegistar : DependencyRegistarBase
    {
        /// <summary>
        /// 这里注册顺序我们紧跟系统框架注册后面，防止数据访问上下文覆盖其他数据访问上下文
        /// </summary>
        public override int Priority
        {
            get
            {
                return this.DefaultPriority + 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder"></param>
        /// <param name="globalConfiguration"></param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            containerBuilder.RegisterType<CommandParametersParser>().As<ICommandParametersParser>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //all controller
            containerBuilder.RegisterControllers(Assembly.GetExecutingAssembly())
                            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }
    }
}
