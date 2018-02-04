/* ****************************************************************
 * SharpSword zhangliang4629@163.com 10/24/2016 1:41:37 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SharpSword
{
    /// <summary>
    /// 
    /// </summary>
    public static class ReflectionHelper
    {
        /// <summary>
        /// 判断某个给定类型，是否继承或者实现某个泛型类
        /// </summary>
        /// <param name="givenType">给定类型</param>
        /// <param name="genericType">泛型类</param>
        /// <returns></returns>
        public static bool IsAssignableToGenericType(Type givenType, Type genericType)
        {
            //如果给定类型为泛型，并且类型为指定的泛型类型
            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
            {
                return true;
            }

            //检测接口
            foreach (var interfaceType in givenType.GetInterfaces())
            {
                if (interfaceType.IsGenericType && interfaceType.GetGenericTypeDefinition() == genericType)
                {
                    return true;
                }
            }

            if (givenType.BaseType == null)
            {
                return false;
            }

            //基类不为null，我们继续判断下继承链
            return IsAssignableToGenericType(givenType.BaseType, genericType);
        }

        /// <summary>
        /// 获取定义在MemberInfo上的特性并且同时将获取到该成员所属类的特性
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="memberInfo"></param>
        /// <returns></returns>
        public static List<TAttribute> GetAttributesOfMemberAndDeclaringType<TAttribute>(MemberInfo memberInfo)
            where TAttribute : Attribute
        {
            var attributeList = new List<TAttribute>();

            //获取成员上定义的特性
            if (memberInfo.IsDefined(typeof(TAttribute), true))
            {
                attributeList.AddRange(memberInfo.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>());
            }

            //获取成员所属类上定义的特性
            if (memberInfo.DeclaringType != null && memberInfo.DeclaringType.IsDefined(typeof(TAttribute), true))
            {
                attributeList.AddRange(memberInfo.DeclaringType.GetCustomAttributes(typeof(TAttribute), true).Cast<TAttribute>());
            }

            //返回集合
            return attributeList;
        }
    }
}
