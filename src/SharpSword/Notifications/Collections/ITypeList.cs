/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/29/2016 2:55:19 PM
 * *******************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword.Notifications
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TBaseType"></typeparam>
    public interface ITypeList<in TBaseType> : IList<Type>
    {
        /// <summary>
        /// 添加一个类型到集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Add<T>() where T : TBaseType;

        /// <summary>
        /// 集合是否包含指定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        bool Contains<T>() where T : TBaseType;

        /// <summary>
        /// 移除指定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        void Remove<T>() where T : TBaseType;
    }
}
