/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/30/2015 5:28:06 PM
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 类型检查
    /// </summary>
    public static class ActionTypeExtensions
    {
        /// <summary>
        /// 类型是否继承了ActionBase
        /// </summary>
        /// <param name="type">当前类型</param>
        /// <returns></returns>
        public static bool IsAssignableToActionBase(this Type type)
        {
            return !type.IsAbstract
                && typeof(IAction).IsAssignableFrom(type)
                && !type.BaseType.IsNull()
                && type.BaseType.IsGenericType
                && type.BaseType.GetGenericTypeDefinition() == typeof(ActionBase<,>);
        }

        /// <summary>
        /// 类型是否继承自IRequestDto
        /// </summary>
        /// <param name="type">当前类型</param>
        /// <returns></returns>
        public static bool IsAssignableToIRequestDto(this Type type)
        {
            return !type.IsAbstract && typeof(IRequestDto).IsAssignableFrom(type);
        }

        /// <summary>
        /// 类型是否继承自IResponseDto
        /// </summary>
        /// <param name="type">当前类型</param>
        /// <returns></returns>
        public static bool IsAssignableToIResponseDto(this Type type)
        {
            return !type.IsAbstract && typeof(IResponseDto).IsAssignableFrom(type);
        }
    }
}
