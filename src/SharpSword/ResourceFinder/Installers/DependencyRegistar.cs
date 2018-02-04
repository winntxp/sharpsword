/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;
using SharpSword.ResourceFinder.Impl;

namespace SharpSword.ResourceFinder.Installers
{
    internal class DependencyRegistar : DependencyRegistarBase
    {
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //资源文件查找器（注意必须将自身也注册成服务）-资源搜索器使用HOST内存
            containerBuilder.RegisterType<LocalFileViewResourceFinder>()
                            .As<IResourceFinder>()
                            .InstancePerLifetimeScope();
            //搜索内嵌资源
            containerBuilder.RegisterType<EmbeddedFileResourceFinder>()
                            .As<IResourceFinder>()
                            .InstancePerLifetimeScope();
            //资源查找管理器
            containerBuilder.RegisterType<DefaultResourceFinderManager>()
                            .As<IResourceFinderManager>()
                            .InstancePerLifetimeScope();
        }
    }
}