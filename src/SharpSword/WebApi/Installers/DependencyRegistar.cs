/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;
using SharpSword.WebApi.ValueProviders;
using SharpSword.WebApi.ValueProviders.Impl;
using System.Linq;

namespace SharpSword.WebApi.Installers
{
    internal class DependencyRegistar : DependencyRegistarBase
    {
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //接口处理器
            containerBuilder.RegisterType<ActionRequestHander>()
                            .As<IActionRequestHander>()
                            .InstancePerLifetimeScope();

            //接口查找器
            containerBuilder.RegisterType<DefaultActionSelector>()
                            .As<IActionSelector>()
                            .InstancePerLifetimeScope();

            //接口创建工厂
            containerBuilder.RegisterType<DefaultActionFactory>()
                            .As<IActionFactory>()
                            .InstancePerLifetimeScope();

            //接口激活器
            containerBuilder.RegisterType<DefaultActionActivator>()
                            .As<IActionActivator>()
                            .InstancePerLifetimeScope();

            //接口执行器
            containerBuilder.RegisterType<DefaultActionInvoker>()
                            .As<IActionInvoker>()
                            //.PropertiesAutowired()
                            .SingleInstance();

            //接口执行结果格式化器
            containerBuilder.RegisterType<DefaultMediaTypeFormatterFactory>()
                            .As<IMediaTypeFormatterFactory>()
                            .SingleInstance();

            //接口格式化字符串输出器
            containerBuilder.RegisterType<DefaultResponse>()
                            .As<IResponse>()
                            .SingleInstance();

            //接口访问记录器，默认访问记录器未实现任何功能
            containerBuilder.RegisterType<DefaultApiAccessRecorder>()
                            .As<IApiAccessRecorder>()
                            .SingleInstance();

            //上送请求参数绑定器
            containerBuilder.RegisterType<DefaultRequestParamsBinder>()
                            .As<IRequestParamsBinder>()
                            .InstancePerLifetimeScope();

            //上送业务参数绑定器
            containerBuilder.RegisterType<DefaultRequestDtoBinder>()
                            .As<IRequestDtoBinder>()
                            .InstancePerLifetimeScope();

            //上送参数接口校验器
            containerBuilder.RegisterType<DefaultRequestDtoValidator>()
                            .As<IRequestDtoValidator>()
                            .InstancePerLifetimeScope();

            //http-post参数值提供器
            containerBuilder.RegisterType<FormValueProvider>()
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope();
            //http-get参数值提供器
            containerBuilder.RegisterType<QueryStringValueProvider>()
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope();
            //服务器环境变量值提供器
            containerBuilder.RegisterType<ServerVariablesValueProvider>()
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope();
            //路由值提供器
            containerBuilder.RegisterType<RouteDataValueProvider>()
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope();
            //值提供器管理器
            containerBuilder.RegisterType<DefaultValueProvidersManager>()
                            .As<IValueProvidersManager>()
                            .InstancePerLifetimeScope();

            //默认身份验证器（空实现）
            containerBuilder.RegisterType<DefaultAuthentication>()
                            .As<IAuthentication>()
                            .SingleInstance();

            //接口加密解密器(空实现)
            containerBuilder.RegisterType<DefaultApiSecurity>()
                            .AsImplementedInterfaces()
                            .SingleInstance();

            ////json格式化器
            //containerBuilder.RegisterType<JsonMediaTypeFormatter>()
            //                .As<IMediaTypeFormatter>()
            //                .Named<IMediaTypeFormatter>("Json_MediaTypeFormatter")
            //                .SingleInstance();
            ////xml格式化器
            //containerBuilder.RegisterType<XmlMediaTypeFormatter>()
            //                .As<IMediaTypeFormatter>()
            //                .Named<IMediaTypeFormatter>("Xml_MediaTypeFormatter")
            //                .SingleInstance();

            //html格式化器
            containerBuilder.RegisterType<ViewMediaTypeFormatter>()
                            .As<IMediaTypeFormatter>()
                            .Named<IMediaTypeFormatter>("View_MediaTypeFormatter")
                            .InstancePerLifetimeScope();

            containerBuilder.RegisterType<ActionDocResourceManager>()
                            .As<ActionDocResourceManager>()
                            .SingleInstance();
            //SDK代码生成器
            containerBuilder.RegisterType<DefaultCodeGeneratorFactory>()
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope();
            //安卓
            containerBuilder.RegisterType<AndroidSdkCodeGenerator>()
                            .AsSelf()
                            .InstancePerLifetimeScope();
            //C#
            containerBuilder.RegisterType<CSharpSdkCodeGenerator>()
                            .AsSelf()
                            .InstancePerLifetimeScope();

            //接口仿真数据生成器
            containerBuilder.RegisterType<DefaultIApiDocBuilder>()
                            .As<IApiDocBuilder>()
                            .SingleInstance();

            //框架自动搜索bin目录程序集，注册所有实现了IAction接口的类；
            typeFinder.FindClassesOfType(typeof(IAction)).Where(type => type.IsAssignableToActionBase()).ToList().ForEach(type =>
            {
                containerBuilder.RegisterType(type)
                                .PropertiesAutowired()
                                .InstancePerLifetimeScope();
            });
        }
    }
}