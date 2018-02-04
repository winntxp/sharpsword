/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;
using Autofac.Integration.Mvc;
using SharpSword.O2O.Data;
using SharpSword.O2O.Services;
using SharpSword.O2O.Services.Impl;
using System.Reflection;

namespace SharpSword.Host
{
    /// <summary>
    /// 注册系统默认实现的接口服务类
    /// </summary>
    public class DependencyRegistar : DependencyRegistarBase
    {
        /// <summary>
        /// 注册优先级
        /// </summary>
        public override int Priority => this.DefaultPriority + 1;

        /// <summary>
        /// 注册特定的类型到容器
        /// </summary>
        /// <param name="containerBuilder">注册容器</param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="globalConfiguration">系统框架配置信息</param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //all controller
            containerBuilder.RegisterControllers(Assembly.GetExecutingAssembly())
                            .PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            //注册SDK客户端
            //containerBuilder.RegisterType<DefaultApiClient>()
            //                .As<IApiClient>()
            //                .PropertiesAutowired()
            //                .InstancePerLifetimeScope();

            //containerBuilder.Register(c => new ApiClientConfiguration("http://www.sharpsword.com/Api"))
            //                .As<IApiClientConfiguration>()
            //                .SingleInstance();

            containerBuilder.RegisterType<UserSession>()
                            .AsImplementedInterfaces()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //数据库连接创建工厂
            containerBuilder.RegisterType<MSSQLDbConnectionFactory>()
                            .As<IDbConnectionFactory>()
                            .SingleInstance();

            //订单接收器
            containerBuilder.RegisterType<DefaultOrderSubmitServices>()
                            .As<IOrderSubmitServices>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //订单操作服务
            containerBuilder.RegisterType<DefaultOrderServices>()
                            .As<IOrderServices>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //门店信息
            containerBuilder.RegisterType<RedisStoreServices>()
                            .As<IStoreServices>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //活动及活动商品
            containerBuilder.RegisterType<RedisPresaleActivityServices>()
                            .As<IPresaleActivityServices>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //获取以及商品缓存清理服务
            containerBuilder.RegisterType<RedisPresaleActivityServices>()
                            .As<IPresaleActivityCacheManager>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //用户
            containerBuilder.RegisterType<RedisUserServices>()
                            .As<IUserServices>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //TOKEN票据获取服务
            containerBuilder.RegisterType<DefaultTokenServices>()
                            .As<ITokenServices>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //订单处理器
            containerBuilder.RegisterType<DefaultOrderProgressServices>()
                            .As<IOrderProgressServices>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //订单ID生成器
            containerBuilder.RegisterType<RedisOrderIdGenerator>()
                            .As<IOrderIdGenerator>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //用户ID编号生成器
            containerBuilder.RegisterType<DefaultUserIdGenerator>()
                            .As<IUserIdGenerator>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //排队服务
            containerBuilder.RegisterType<RedisOrderSequenceServices>()
                            .As<IOrderSequenceServices>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //拆分因子管理器
            containerBuilder.Register(c => OrderSplitFactorServices.Instance)
                            .As<IOrderSplitFactorServices>();

            //数据库连接字符串提供器
            containerBuilder.RegisterType<WebConfigDbConnectionStringProvider>()
                            .As<IDbConnectionStringProvider>()
                            .PropertiesAutowired()
                            .SingleInstance();

            //全局库连接字符串查找器
            containerBuilder.RegisterType<DefaultGlobalDbFinder>()
                            .As<IGlobalDbFinder>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //订单用户维度数据库连接字符串查找器
            containerBuilder.RegisterType<DefaultUserOrderDbFinder>()
                            .As<IUserOrderDbFinder>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //订单区域维度数据库连接字符串生成器
            containerBuilder.RegisterType<DefaultAreaOrderDbFinder>()
                            .As<IAreaOrderDbFinder>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //用户数据库连接字符串生成器
            containerBuilder.RegisterType<DefaultUserDbFinder>()
                            .As<IUserDbFinder>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();
            //用户数据库表名称生成器
            containerBuilder.RegisterType<DefaultUserDbTableFinder>()
                            .As<IUserDbTableFinder>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //订单提交消息
            containerBuilder.RegisterType<RabbitMqMessageManager>()
                            .As<IMessageManager>()
                            .SingleInstance();

            //订单过期管理器
            containerBuilder.RegisterType<RedisOrderExpiredManager>()
                            .As<IOrderExpiredManager>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //订单完成管理器
            containerBuilder.RegisterType<RedisOrderFinishedManager>()
                            .As<IOrderFinishedManager>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            //订单事件发布器
            containerBuilder.RegisterType<DefaultEventPublisher>()
                            .As<IEventPublisher>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();
        }
    }
}