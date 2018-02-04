using Autofac;
using Autofac.Integration.Mvc;
/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/20 18:49:15
 * ****************************************************************/
using System.Reflection;

namespace SharpSword.Tools
{
    /// <summary>
    /// API框架会自动检测到这里的注册类,自动完成注册
    /// </summary>
    public class DependencyRegistar : IDependencyRegistar
    {
        /// <summary>
        /// 系统框架默认的会被覆盖;
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="globalConfiguration">系统框架配置信息</param>
        public void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //all controller
            containerBuilder.RegisterControllers(Assembly.GetExecutingAssembly())
                            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);
        }

        /// <summary>
        /// 数字越大越后注册
        /// </summary>
        public int Priority
        {
            get { return 1; }
        }
    }
}
