/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using Autofac;
using SharpSword.Data;
using SharpSword.Domain.Entitys;
using SharpSword.Domain.Uow;
using System;

namespace SharpSword.EntityFramework
{
    /// <summary>
    /// 默认注册下一个空实现
    /// </summary>
    internal class DependencyRegistar : DependencyRegistarBase
    {
        /// <summary>
        /// 优先级比较低，但是又不配置为最低，给外部有机会以最低的方式来注入(比如：DBContext的扩展项目等)
        /// 方便外部程序重写框架里的实现，覆盖掉系统默认的实现
        /// 最先注册下系统默认的实现；这样外部实现才能覆盖掉原始的实现
        /// </summary>
        public override int Priority { get { return -1; } }

        /// <summary>
        /// 注册特定的类型到容器
        /// </summary>
        /// <param name="containerBuilder">注册容器</param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="globalConfiguration">系统框架配置参数</param>
        public override void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //注意工作单元注册成瞬态
            containerBuilder.RegisterType<EfUnitOfWork>().As<IUnitOfWork>();

            //注册数据访问上下文提供者
            containerBuilder.RegisterGeneric(typeof(UnitOfWorkDbContextProvider<>))
                            .As(typeof(IDbContextProvider<>))
                            .InstancePerLifetimeScope();

            //查找所有数据库访问上下文
            var dbContextTypes = typeFinder.FindClassesOfType<DbContextBase>();
            foreach (var dbContextType in dbContextTypes)
            {
                RegisterForDbContext(dbContextType, containerBuilder);
            }

            //自定义sql数据操作创建器
            containerBuilder.RegisterType<EfDbContextFactory>()
                            .As<IDbContextFactory>()
                            .PropertiesAutowired()
                            .InstancePerLifetimeScope();
        }

        /// <summary>
        /// 注册上下文里的表对象，注册到具体的仓储
        /// </summary>
        /// <param name="dbContextType"></param>
        /// <param name="containerBuilder"></param>
        private static void RegisterForDbContext(Type dbContextType, ContainerBuilder containerBuilder)
        {
            var autoRepositoryAttr = dbContextType.GetSingleAttributeOrNull<AutoRepositoryTypesAttribute>();
            if (autoRepositoryAttr.IsNull())
            {
                autoRepositoryAttr = AutoRepositoryTypesAttribute.Default;
            }

            //注册所有的实体到具体的仓储
            foreach (var entityType in dbContextType.GetEntityTypes())
            {
                //必须首先IEntity接口
                if (entityType.BaseType.IsNull() || !((typeof(IEntity)).IsAssignableFrom(entityType)))
                {
                    continue;
                }

                //获取所有接口，获取到主键类型
                var interfaces = entityType.GetInterfaces();
                foreach (var interfaceType in entityType.GetInterfaces())
                {
                    if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == typeof(IEntity<>))
                    {
                        var primaryKeyType = interfaceType.GenericTypeArguments[0];
                    }
                }

                //仓储接口类
                var genericRepositoryType = autoRepositoryAttr.RepositoryInterface
                                                              .MakeGenericType(entityType);

                //仓储接口实现类
                var ImplementationType = autoRepositoryAttr.RepositoryImplementation
                                                           .MakeGenericType(dbContextType, entityType);

                //注册到IOC容器
                containerBuilder.RegisterType(ImplementationType)
                                .As(genericRepositoryType)
                                .PropertiesAutowired()
                                .InstancePerLifetimeScope();
            }
        }

    }
}

