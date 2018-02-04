/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/14/2016 10:00:44 AM
 * ****************************************************************/
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using SharpSword.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpSword.Configuration
{
    /// <summary>
    /// 配置参数自动注入
    /// </summary>
    internal class SettingsSource : IRegistrationSource
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod("BuildRegistration",
            BindingFlags.Static | BindingFlags.NonPublic);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="service">待创建的类型</param>
        /// <param name="registrations"></param>
        /// <returns></returns>
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service,
            Func<Service, IEnumerable<IComponentRegistration>> registrations)
        {

            var typedService = service as TypedService;

            if (typedService.IsNull())
            {
                yield break;
                //return Enumerable.Empty<IComponentRegistration>();
            }

            //需要反转出来的类型
            // ReSharper disable once PossibleNullReferenceException
            var serviceType = typedService.ServiceType;

            //只能是类(不能是接口或者抽象类)
            if (!typeof(ISetting).IsAssignableFrom(serviceType) || typeof(ISetting) == serviceType ||
                serviceType.IsAbstract || !serviceType.IsClass)
            {
                yield break;
            }

            //根据指定配置文件类型获取配置文件对象
            var buildMethod = BuildMethod.MakeGenericMethod(serviceType);
            yield return (IComponentRegistration)buildMethod.Invoke(null, null);
        }

        /// <summary>
        /// 泛型进行调用
        /// 获取参数顺序说明：
        /// 在对象创建的时候，如果需要制定配置参数，我们首先会从系统配置模块获取参数，如果系统参数配置模块获取参数失败
        /// 我们再从系统启动的配置里获取，获取都失败了，我们再看是否需要返回默认参数对象。为什么会定义成这样的顺序策略
        /// 是因为将外部配置文件最优先便于我们发布后进行参数配置，因为，启动项配置编译后将变得不可修改
        /// </summary>
        /// <typeparam name="TSettings">配置参数对象类型</typeparam>
        /// <returns></returns>
        private static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ISetting, new()
        {
            var settingType = typeof(TSettings);
            return RegistrationBuilder.ForDelegate((c, p) =>
            {
                try
                {
                    //0.我们直接系统配置模块来获取
                    var settingFactoryBuilder = c.Resolve<ISettingFactoryBuilder>();
                    var settingFactorys = settingFactoryBuilder.Find<TSettings>().OrderByDescending(o => o.Priority);

                    //如果全部未找到配置，直接抛出下异常
                    if (!settingFactorys.Any())
                    {
                        throw new SharpSwordCoreException("配置文件 {0} 创建工厂(ISettingFactory)未找到，未注册？".With(settingType.FullName));
                    }
                   
                    IList<Exception> exceptions = new List<Exception>();

                    //实现针对同一配置接口采取多个参数创建类，我们循环处理下
                    foreach (var settingFactory in settingFactorys)
                    {
                        if (settingFactory.IsNull())
                        {
                            continue;
                        }

                        //我们处理下每个参数创建工厂的异常，因为有些创建工厂可能会抛出异常。
                        try
                        {
                            var setting = settingFactory.Get<TSettings>();
                            if (setting.IsNull())
                            {
                                continue;
                            }
                            return setting;
                        }
                        catch (Exception exc)
                        {
                            exceptions.Add(exc);
                        }
                    }

                    //我们抛出优先级最高的异常
                    if (exceptions.Any())
                    {
                        throw exceptions.First();
                    }

                    //所有的配置参数处理工厂都处理失败了
                    throw new SharpSwordCoreException("自动获取配置文件失败");
                }
                catch (Exception exc)
                {
                    //1.从配置参数失败了，我们再次从系统启动配置获取参数对象
                    var setting = GlobalConfiguration.Instance.GetConfig<TSettings>();
                    if (!setting.IsNull())
                    {
                        return (TSettings)setting;
                    }

                    //2.兜底我们看配置参数对象是否定义了失败返回默认特性
                    if (FailReturnDefault(settingType))
                    {
                        return new TSettings();
                    }

                    //如果未定义失败返回默认配置信息的，我们直接抛出下异常
                    throw new SharpSwordCoreException(exc.Message, exc);
                }
            })
            //我们将所有的参数配置注册成单例模式，这样不需要每次都去读取数据源
            .SingleInstance().CreateRegistration();
        }

        /// <summary>
        /// 检测是否定义了，当创建配置对象失败的时候，是否返回一个空的新的配置对象特性
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool FailReturnDefault(Type type)
        {
            return type.IsDefined<FailReturnDefaultAttribute>();
        }

        /// <summary>
        /// 
        /// </summary>
        public bool IsAdapterForIndividualComponents
        {
            get { return false; }
        }
    }
}
