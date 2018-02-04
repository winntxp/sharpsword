/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/5/31 9:11:59
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.WebApi.ValueProviders
{
    /// <summary>
    /// 值提供器管理器；此过滤器将会将所有注册的值提供器进行管理
    /// </summary>
    public interface IValueProvidersManager
    {
        /// <summary>
        /// 值提供器集合
        /// </summary>
        IEnumerable<IValueProvider> ValueProviders { get; }

        /// <summary>
        /// 获取所有的键
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetAllKeys();

        /// <summary>
        /// 根据键，后去键所对应的值
        /// </summary>
        /// <param name="propertyName">键名称</param>
        /// <returns>如果键不存在就直接返回null</returns>
        object GetValue(string propertyName);
    }
}
