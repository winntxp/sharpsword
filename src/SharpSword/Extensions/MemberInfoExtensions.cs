/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/9/2016 4:48:30 PM
 * ****************************************************************/
using System;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;

namespace SharpSword
{
    /// <summary>
    /// 
    /// </summary>
    public static class MemberInfoExtensions
    {
        /// <summary>
        /// 获取指定特性信息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="memberInfo"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static T GetSingleAttributeOrNull<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
        {
            if (memberInfo.IsNull())
            {
                throw new ArgumentNullException(nameof(memberInfo));
            }

            var customAttributes = memberInfo.GetCustomAttributes(typeof(T), inherit);
            if (customAttributes.Length > 0)
            {
                return (T)customAttributes[0];
            }

            return default(T);
        }

        /// <summary>
        /// 获取多个特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="memberInfo"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static IEnumerable<T> GetCustomAttributes<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
        {
            return memberInfo.GetCustomAttributes(typeof(T), inherit).Cast<T>();
        }

        /// <summary>
        /// 成员上是否定义了指定的特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="memberInfo"></param>
        /// <param name="inherit"></param>
        /// <returns></returns>
        public static bool IsDefined<T>(this MemberInfo memberInfo, bool inherit = true) where T : Attribute
        {
            return memberInfo.IsDefined(typeof(T), inherit);
        }      
    }
}
