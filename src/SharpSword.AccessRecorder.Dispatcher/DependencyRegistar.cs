/******************************************************************
* SharpSword zhangliang@sharpsword.com.cn 2015/11/20 18:49:15
* *****************************************************************/
using Autofac;
using SharpSword.WebApi;

namespace SharpSword.AccessRecorder.Dispatcher
{
    /// <summary>
    /// API框架会自动检测到这里的注册类,自动完成注册
    /// </summary>
    public class DependencyRegistar : DependencyRegistarBase
    {
        /// <summary>
        /// 系统框架默认的会被覆盖
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="globalConfiguration">系统框架配置信息</param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //注册下接口调试记录器
            containerBuilder.RegisterType<ApiAccessRecorder>()
                            .As<IApiAccessRecorder>()
                            //http://docs.autofac.org/en/latest/advanced/circular-dependencies.html
                            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies) // 设置可以循环依赖
                            .InstancePerLifetimeScope();
        }

        /// <summary>
        /// 数字越大越后注册(分发器的优先级我们设置为最大，这样其他记录器才能被分发器调用到)
        /// </summary>
        public override int Priority => int.MaxValue;
    }
}
