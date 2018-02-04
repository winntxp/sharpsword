/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SharpSword
{
    /// <summary>
    /// 类型查找器类型查找器
    /// </summary>
    public interface ITypeFinder
    {
        /// <summary>
        /// 获取当前应用程序域所有程序集
        /// </summary>
        /// <returns></returns>
        IList<Assembly> GetAssemblies();

        /// <summary>
        /// 根据类型查找所有的实现类
        /// </summary>
        /// <param name="assignTypeFrom">待检索类型（接口或者类型）</param>
        /// <param name="onlyConcreteClasses">排除抽象类</param>
        /// <returns></returns>
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, bool onlyConcreteClasses = true);

        /// <summary>
        /// 根据类型和指定程序集查找所有实现类
        /// </summary>
        /// <param name="assignTypeFrom">待检索类型（接口或者类型）</param>
        /// <param name="assemblies"></param>
        /// <param name="onlyConcreteClasses">排除抽象类</param>
        /// <returns></returns>
        IEnumerable<Type> FindClassesOfType(Type assignTypeFrom, IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);

        /// <summary>
        /// 根据类型模板查找所有实现类
        /// </summary>
        /// <typeparam name="T">待检索类型（接口或者类型）</typeparam>
        /// <param name="onlyConcreteClasses">排除抽象类</param>
        /// <returns></returns>
        IEnumerable<Type> FindClassesOfType<T>(bool onlyConcreteClasses = true);

        /// <summary>
        /// 根据类型模板在指定的程序集里查找所有实现类型
        /// </summary>
        /// <typeparam name="T">待检索类型（接口或者类型）</typeparam>
        /// <param name="assemblies">指定程序集</param>
        /// <param name="onlyConcreteClasses">排除抽象类</param>
        /// <returns></returns>
        IEnumerable<Type> FindClassesOfType<T>(IEnumerable<Assembly> assemblies, bool onlyConcreteClasses = true);
    }
}
