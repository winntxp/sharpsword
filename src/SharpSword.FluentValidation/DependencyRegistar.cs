/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/26 11:59:37
 * ****************************************************************/
using Autofac;
using FluentValidation;
using System;
using System.Linq;

namespace SharpSword.FluentValidation
{
    /// <summary>
    /// 注册验证类以及验证配置
    /// </summary>
    public class DependencyRegistar : IDependencyRegistar
    {
        /// <summary>
        ///  检测类型是否继承 RequestDtoFluentValidationBase<> 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsAssignableToRequestDtoFluentValidationBase(Type type)
        {
            return !type.IsAbstract
                && !type.BaseType.IsNull()
                && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(RequestDtoFluentValidationBase<>);
        }

        /// <summary>
        /// 注册所有的验证类
        /// </summary>
        /// <param name="typeFinder"></param>
        private void RegisterValidator(ITypeFinder typeFinder)
        {
            //注册所有继承了 RequestDtoFluentValidationBase 的验证配置类
            var fluentValidationTypes = typeFinder.FindClassesOfType<IValidator>()
                                                  .Where(type => IsAssignableToRequestDtoFluentValidationBase(type))
                                                  .ToList();
            foreach (var type in fluentValidationTypes)
            {
                //RequestDto参数类型
                var requestDtoType = type.BaseType.GenericTypeArguments[0];
                //存在就删除，然后重新添加
                if (FluentValidationManager.Configs.Keys.Contains(requestDtoType))
                {
                    FluentValidationManager.Configs.Remove(requestDtoType);
                }
                FluentValidationManager.Configs.Add(requestDtoType, type);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="containerBuilder"></param>
        /// <param name="typeFinder"></param>
        /// <param name="globalConfiguration">系统框架配置信息</param>
        public void Register(ContainerBuilder containerBuilder, ITypeFinder typeFinder, GlobalConfiguration globalConfiguration)
        {
            //注册所有校验配置
            this.RegisterValidator(typeFinder);

            //注册验证器实现
            containerBuilder.RegisterType<FluentRequestDtoValidator>()
                            .AsImplementedInterfaces()
                            .InstancePerLifetimeScope();
        }

        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority => 0;
    }
}
