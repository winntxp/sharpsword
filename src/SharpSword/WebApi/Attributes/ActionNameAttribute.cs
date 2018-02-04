/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 用于手工指定接口名称；如果接口类加上此特性，接口名称将会使用此特性指定的接口名称
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ActionNameAttribute : Attribute
    {
        /// <summary>
        /// 接口名称
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 接口名称特性
        /// </summary>
        /// <param name="name">接口名称,客户端需要调用的接口名称，框架忽略大小写</param>
        /// <exception cref="Exception"></exception>
        /// <returns></returns>
        public ActionNameAttribute(string name)
        {
            if (name.IsNullOrEmpty())
            {
                throw new ArgumentNullException("name");
            }
            this.Name = name;
        }
    }

    /// <summary>
    /// 此定义和ActionNameAttribute作用一模一样，后续我们将逐渐过度使用ApiNameAttribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ApiNameAttribute : ActionNameAttribute
    {
        /// <summary>
        /// 接口名称特性
        /// </summary>
        /// <param name="name">接口名称,客户端需要调用的接口名称，框架忽略大小写</param>
        public ApiNameAttribute(string name) : base(name) { }
    }
}