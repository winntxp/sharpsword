/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.WebApi.ValueProviders
{
    /// <summary>
    /// 值提供器；此接口属于协作接口，即：多个注册的实现，会依次根据值提供器优先级来进行获取值
    /// </summary>
    public interface IValueProvider
    {
        /// <summary>
        /// 根据对象属性名称从值提供器里获取值(注意有可能会返回null，不存在值)
        /// </summary>
        /// <param name="key">键名称，一般对应于绑定对象的属性名称</param>
        /// <returns></returns>
        object GetValue(string key);

        /// <summary>
        /// 获取到所有的键信息
        /// </summary>
        IEnumerable<string> GetAllKeys();

        /// <summary>
        /// 值提供器优先级
        /// </summary>
        int Order { get; }
    }
}