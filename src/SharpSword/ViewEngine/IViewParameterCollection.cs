/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/17/2016 12:50:53 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.ViewEngine
{
    /// <summary>
    /// 视图对象模型集合类
    /// </summary>
    public interface IViewParameterCollection : IEnumerable<ViewParameter>
    {
        /// <summary>
        /// 根据VIEW公开定义的属性名称获取值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        ViewParameter this[string name] { get; }

        /// <summary>
        /// 添加一个参数对象,值默认为null
        /// </summary>
        /// <param name="paramName"></param>
        void Add(string paramName);

        /// <summary>
        /// 添加一个参数对象
        /// </summary>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        void Add(string paramName, object paramValue);
    }
}