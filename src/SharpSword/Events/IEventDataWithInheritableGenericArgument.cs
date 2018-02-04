/* ****************************************************************
 * SharpSword zhangliang4629@163.com 10/3/2016 4:28:58 PM
 * ****************************************************************/
using System;

namespace SharpSword.Events
{
    /// <summary>
    /// 此接口返回对象构造函数入参数据，方便在运行时创建此对象的时候作为入参创建对象
    /// 注意：运行时创建对象的时候，会根据参数GetConstructorArgs（）方法返回的参数个数来选择正确的构造函数进行创建
    /// </summary>
    public interface IEventDataWithInheritableGenericArgument
    {
        /// <summary>
        /// 获取构造函数参数值
        /// </summary>
        /// <returns></returns>
        object[] GetConstructorArgs();
    }
}
