/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/18/2016 1:44:08 PM
 * ****************************************************************/
using Autofac;
using System;

namespace SharpSword
{
    /// <summary>
    /// IOC容器接口
    /// </summary>
    public interface IIocResolver
    {
        /// <summary>
        /// 判断一个类型是否在IOC里注册了
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="lifetimeScope"></param>
        /// <returns></returns>
        bool IsRegistered(Type serviceType, ILifetimeScope lifetimeScope = null);

        /// <summary>
        /// 反转出指定类型对象，如果未注册则返回null
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="lifetimeScope"></param>
        /// <returns></returns>
        object ResolveOptional(Type serviceType, ILifetimeScope lifetimeScope = null);

        /// <summary>
        /// 反转出指定类型对象
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="lifetimeScope"></param>
        /// <returns></returns>
        object Resolve(Type serviceType, ILifetimeScope lifetimeScope = null);

        /// <summary>
        /// 反转出指定类型对象
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="lifetimeScope"></param>
        /// <returns></returns>
        TService Resolve<TService>(ILifetimeScope lifetimeScope = null);

        /// <summary>
        /// 反转出指定类型的所有IOC注册实现
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="namedServices"></param>
        /// <param name="lifetimeScope"></param>
        /// <returns></returns>
        TService[] ResolveAll<TService>(string namedServices = "", ILifetimeScope lifetimeScope = null);

        /// <summary>
        /// 根据类型创建出未注册的实现
        /// </summary>
        /// <param name="type"></param>
        /// <param name="lifetimeScope"></param>
        /// <returns></returns>
        object ResolveUnregistered(Type type, ILifetimeScope lifetimeScope = null);

        /// <summary>
        /// 获取生命周期
        /// </summary>
        /// <returns></returns>
        ILifetimeScope Scope();

        /// <summary>
        /// 获取生命周期
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        ILifetimeScope Scope(object tag);

        /// <summary>
        /// 尝试创建指定类型对象
        /// </summary>
        /// <param name="serviceType"></param>
        /// <param name="lifetimeScope"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        bool TryResolve(Type serviceType, ILifetimeScope lifetimeScope, out object instance);

        /// <summary>
        /// 释放对象
        /// </summary>
        /// <param name="obj"></param>
        void Release(object obj);
    }
}