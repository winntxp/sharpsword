/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/24/2016 3:51:35 PM
 * ****************************************************************/
using SharpSword.Domain.Repositories;
using SharpSword.Domain.Services;
using System;
using System.Reflection;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public static class UnitOfWorkHelper
    {
        /// <summary>
        /// 方法是否定义了工作单元特性
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static bool HasUnitOfWorkAttribute(this MemberInfo methodInfo)
        {
            return methodInfo.IsDefined(typeof(UnitOfWorkAttribute), true);
        }

        /// <summary>
        /// 指定类型是否按照约定定义工作单元
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static bool IsConventionalUowClass(this Type type)
        {
            //如果注册成接口代理模式实现方法可以不需实现虚拟类，如果实现类代理，需要加virtual
            return typeof(IRepository).IsAssignableFrom(type) || typeof(ISharpSwordServices).IsAssignableFrom(type);
        }

        /// <summary>
        /// 获取方法上定义的工作单元特性，我们约定实现ISharpSwordServices的所有接口方法都是工作单元的
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        public static UnitOfWorkAttribute GetUnitOfWorkAttributeOrDefault(this MemberInfo methodInfo)
        {
            //检测方法上是否定义了工作单元特性
            var unitOfWorkAttribute = methodInfo.GetCustomAttributes(typeof(UnitOfWorkAttribute), false);
            if (unitOfWorkAttribute.Length > 0)
            {
                return (UnitOfWorkAttribute)unitOfWorkAttribute[0];
            }

            //继承自ISharpSwordServices的所有方法都会被代理
            if (methodInfo.DeclaringType.IsConventionalUowClass())
            {
                return new UnitOfWorkAttribute();
            }

            return null;
        }
    }
}