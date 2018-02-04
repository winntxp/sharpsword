using Autofac;
using SharpSword.Auditing;
using SharpSword.Data;
using SharpSword.DistributedLock;
using SharpSword.DistributedLock.Redis;
/******************************************************************
* SharpSword zhangliang@sharpsword.com.cn 2015/11/20 18:49:15
* *******************************************************/

namespace SharpSword.Host.Data
{
    /// <summary>
    /// 框架会自动检测到这里的注册类,自动完成注册
    /// </summary>
    public class DependencyRegistar : IDependencyRegistar
    {
        /// <summary>
        /// 系统框架默认的会被覆盖;
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="globalConfiguration"></param>
        public void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //
            containerBuilder.RegisterType<TestSession>()
                            .AsImplementedInterfaces()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            containerBuilder.RegisterType<MvcAuditInfoProvider>()
                            .As<IAuditInfoProvider>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            containerBuilder.RegisterType<LogSqlTraceManager>()
                            .As<ISqlTraceManager>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            containerBuilder.Register<V21DbContext>(c => new V21DbContext(() => "apiV200"))
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();

            containerBuilder.Register(c => c.Resolve<IDbContextFactory>().Create(() => "apiV200"))
                            .As<IDbContext>()
                            .InstancePerLifetimeScope();

            //containerBuilder.Register(c => new DistributedLockerManager("127.0.0.1:6379")) //"127.0.0.1:6380", "127.0.0.1:6381", "127.0.0.1:6382", "127.0.0.1:6383"
            //                .As<IDistributedLockerManager>()
            //                .SingleInstance();

            //containerBuilder.RegisterGeneric(typeof(EfRepository<>))
            //                .As(typeof(IRepository<>))
            //                .PropertiesAutowired()
            //                .InstancePerLifetimeScope();

            //containerBuilder.RegisterType(typeof(EfUnitOfWork))
            //                .As(typeof(IUnitOfWork))
            //                .PropertiesAutowired()
            //                .InstancePerLifetimeScope();

            //containerBuilder.RegisterType<Session>()
            //    .AsImplementedInterfaces()
            //    .PropertiesAutowired()
            //    .InstancePerLifetimeScope();

            ////需要根据实际不同的数据访问提供器进行注册
            //containerBuilder.Register<IDataProvider>(c =>
            //{
            //    //获取当前数据库连接
            //    var dbConnection = c.Resolve<IDbContext>().Connection;

            //    //是否是MYSQL数据库
            //    if (dbConnection is MySqlConnection)
            //    {
            //        return new MySqlDataProvider();
            //    }

            //    //是否是ORACLE数据库
            //    if (dbConnection is OracleConnection)
            //    {
            //        return new OracleDataProvider();
            //    }

            //    //是否是MSSQL数据库
            //    if (dbConnection is SqlConnection)
            //    {
            //        return new SqlServerDataProvider();
            //    }

            //    //PostgreSQL数据库
            //    if (dbConnection is NpgsqlConnection)
            //    {
            //        return new PostgreSqlDataProvider();
            //    }

            //    //未找到数据驱动，直接返回null
            //    throw new ApiException("IDataProvider数据驱动未指定，{0}".With(dbConnection.GetType().FullName));

            //}).InstancePerLifetimeScope();

            //containerBuilder.RegisterType<AopServices>().As<IAopServices>()
            //    .PropertiesAutowired()
            //    .EnableInterfaceInterceptors()
            //    .InterceptedBy(typeof(AuditingInterceptor), typeof(DtoValidInterceptor), typeof(UnitOfWorkInterceptor));
        }

        /// <summary>
        /// 数字越大越后注册
        /// </summary>
        public int Priority => int.MaxValue;
    }
}
