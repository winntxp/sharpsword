/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/13/2016 2:18:09 PM
 * ****************************************************************/
using SharpSword.Auditing;
using SharpSword.Domain.Services;
using SharpSword.Domain.Uow;
using System;

namespace SharpSword
{
    /// <summary>
    /// 用于定义类的拦截器集合，这里的特性类，系统框架实现为可被继承和重写；方便扩展，便于用户自定义新的拦截器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class InterceptedByAttribute : Attribute
    {
        /// <summary>
        /// 系统默认的拦截器
        /// </summary>
        public static Type[] Default { get; private set; }

        /// <summary>
        /// 定义的拦截器
        /// </summary>
        public Type[] InterceptorServiceTypes { get; private set; }

        /// <summary>
        /// 初次实例化的时候我们初始化一下系统框架定义的拦截器
        /// </summary>
        static InterceptedByAttribute()
        {
            Default = new Type[] { typeof(AuditingInterceptor),
                                   typeof(DtoValidInterceptor),
                                   typeof(UnitOfWorkInterceptor)
                                 };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="interceptorServiceTypes"></param>
        public InterceptedByAttribute(params Type[] interceptorServiceTypes)
        {
            InterceptorServiceTypes.CheckNullThrowArgumentNullException(nameof(interceptorServiceTypes));
            this.InterceptorServiceTypes = interceptorServiceTypes;
        }
    }
}
