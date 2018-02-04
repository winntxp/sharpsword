/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/11 8:54:16
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace SharpSword
{
    /// <summary>
    /// 类型扩展
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// C#基元类型和FCL映射表
        /// https://msdn.microsoft.com/zh-cn/library/ms228360(v=vs.90).aspx
        /// </summary>
        internal readonly static Dictionary<string, string> CSharpType2FCLMap = new Dictionary<string, string>() {
            {"System.SByte", "sbyte" },
            {"System.Byte", "byte"},
            {"System.Int16", "short"},
            {"System.UInt16", "ushort"},
            {"System.Int32", "int"},
            {"System.UInt32", "uint"},
            {"System.Int64", "long"},
            {"System.UInt64", "ulong"},
            {"System.Char", "char"},
            {"System.Single", "float"},
            {"System.Double", "double"},
            {"System.Boolean", "bool"},
            {"System.String", "string"},
            {"System.Object", "object"},
            {"System.Decimal", "decimal"},
            {"System.DateTime", "DateTime" }
        };

        /// <summary>
        /// 判断一个类型是否是匿名类型
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        public static bool IsAnonymousType(this Type type)
        {
            type.CheckNullThrowArgumentNullException(nameof(type));
            return Attribute.IsDefined(type, typeof(CompilerGeneratedAttribute), false)
                                                && type.IsGenericType
                                                && type.Name.Contains("AnonymousType")
                                                && (type.Name.StartsWith("<>", StringComparison.OrdinalIgnoreCase)
                                                    ||
                                                    type.Name.StartsWith("VB$", StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 是否是可空类型
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">参数type为null</exception>
        public static bool IsNullable(this Type type)
        {
            type.CheckNullThrowArgumentNullException(nameof(type));
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        /// 返回可空类型泛型参数类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns>
        /// 如果非空类型，返回当前类型；如果是可空类型，返回可空类型的泛型参数类型
        /// </returns>
        /// <exception cref="SharpSwordCoreException">非可空类型</exception>
        /// <exception cref="ArgumentNullException">参数type为null</exception>
        public static Type GetNullableGenericType(this Type type)
        {
            //如果非空类型，返回当前类型
            if (!type.IsNullable())
            {
                throw new SharpSwordCoreException("参数:{0}为非可空类型".With(type.FullName));
            }

            //如果是可空类型，返回可空类型的泛型参数类型
            return type.GetGenericArguments().First();
        }

        /// <summary>
        /// 获取类型的字符串表示形式(FCL形式)
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <returns>
        /// <![CDATA[
        /// 返回类型的字符串表达形式,比如：System.Int32或者Nullable<System.Int32>
        /// ]]>
        /// </returns>
        /// <exception cref="ArgumentNullException">参数type为null</exception>
        public static string GetFCLTypeName(this Type type)
        {
            return type.IsNullable() ? "Nullable<{0}>".With(type.GetNullableGenericType().FullName) : type.FullName;
        }

        /// <summary>
        /// 获取类型的字符串表示形式(C#基元类型和自定义类型)
        /// </summary>
        /// <param name="type">数据类型</param>
        /// <returns>
        /// 系将内置的FCL类型会转换成C#基元类型返回，如：System.Int32或返回int基元类型
        /// </returns>
        public static string GetTypeName(this Type type)
        {
            return type.IsNullable() ? CSharpType2FCLMap.GetValue(type.GetNullableGenericType().FullName,
                                                       (key, value) => "{0}?".With(value),
                                                       (key) => "Nullable<{0}>".With(key)) :
                                       CSharpType2FCLMap.GetValue(type.FullName, (key) => key);
        }

        /// <summary>
        /// 是否是代理类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsProxyType(this Type type)
        {
            return type.FullName.StartsWith("Castle.Proxies.") //Castle 代理
                   ||
                   type.FullName.Contains("System.Data.Entity.DynamicProxies."); //EF 实体代理
        }

        /// <summary>
        /// 获取类型的默认值，引用类型返回null,值类型返回对应的默认值
        /// </summary>
        /// <param name="targetType"></param>
        /// <returns></returns>
        public static object DefaultValue(this Type targetType)
        {
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
        }

        /// <summary>
        /// 获取所有实例属性，默认参数：
        /// System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance
        /// </summary>
        /// <param name="type">当前类型</param>
        /// <returns></returns>
        public static PropertyInfo[] GetPropertiesInfo(this Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// 获取所有公开的实例方法
        /// </summary>
        /// <param name="type">当前类型</param>
        /// <returns></returns>
        public static MethodInfo[] GetMethodInfos(this Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
        }

        /// <summary>
        /// 是否注册成类代理
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool EnableClassInterceptor(this Type type)
        {
            return typeof(IEnableClassInterceptor).IsAssignableFrom(type);
        }

        /// <summary>
        /// 是否注册成接口代理
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool EnableInterfaceInterceptor(this Type type)
        {
            return typeof(IEnableInterfaceInterceptor).IsAssignableFrom(type);
        }

        /// <summary>
        /// 是否注册成类代理或者接口代理
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static bool EnableInterceptorProxy(this Type type)
        {
            return type.EnableClassInterceptor() || type.EnableInterfaceInterceptor();
        }
    }
}
