/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/20 16:03:59
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口上送，下送数据仿真器接口
    /// </summary>
    public interface IApiDocBuilder
    {
        /// <summary>
        /// 获取上送对象或者下送数据对象，并且已经将各个属性赋值（测试值）
        /// </summary>
        /// <param name="type">任意类型，一般为：RequestDtoType和ResponseDtoType类型</param>
        /// <returns></returns>
        object CreateInstance(Type type); 
    }
}
