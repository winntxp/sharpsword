/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/15 10:21:38
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口配置表接口，请注意实现类里获取接口配置对象，都需要实现为拷贝，不能引用
    /// </summary>
    public interface IActionConfigCollection
    {
        /// <summary>
        /// 添加一个接口配置，此配置为系统框架级别全局配置，可以多次调用，但是后注册的会覆盖掉前面注册的属性，但是系统缓存只会保存一份配置文件
        /// </summary>
        /// <param name="value">接口配置对象</param>
        IActionConfigCollection Register(ActionConfigItem value);

        /// <summary>
        /// 添加一个接口配置，此配置为单一接口全局配置，可以多次调用，但是后注册的会覆盖掉前面注册的属性，但是系统缓存只会保存一份配置文件
        /// </summary>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="value">接口配置对象</param>
        IActionConfigCollection Register(string actionName, ActionConfigItem value);

        /// <summary>
        /// 添加一个接口配置，此接口只对特定版本起作用，会覆盖掉未指定版本号的配置，可以多次调用，但是后注册的会覆盖掉前面注册的属性，但是系统缓存只会保存一份配置文件
        /// </summary>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="version">接口版本，接口版本格式为：1.0</param>
        /// <param name="value">接口配置对象</param>
        IActionConfigCollection Register(string actionName, string version, ActionConfigItem value);

        /// <summary>
        /// 在不存在键的时候，不抛出异常，而直接返回null
        /// 此索引器返回的是一个全新的配置对象，而不是一个引用配置表里的对象；目的防止程序运行时调用者意外的修改原始配置
        /// </summary>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="version">接口版本</param>
        /// <returns>返回接口配置对象，配置表里不存在的情况下会返回null</returns>
        ActionConfigItem this[string actionName, string version] { get; }

        /// <summary>
        /// 重写索引器，在不存在键的时候，不抛出异常，而直接返回null
        /// 此索引器返回的是一个全新的配置对象，而不是一个引用配置表里的对象；目的防止程序运行时调用者意外的修改原始配置
        /// </summary>
        /// <param name="actionName">接口名称</param>
        /// <returns>在配置表不存在的情况下会返回null</returns>
        ActionConfigItem this[string actionName] { get; }

        /// <summary>
        /// 返回所有的配置信息，此配置信息获取的是全新的（即原始配置文件的一个拷贝集合）
        /// </summary>
        /// <returns></returns>
        IEnumerable<KeyValuePair<string, ActionConfigItem>> GetConfigs();

        /// <summary>
        /// 移除所有的键和值
        /// </summary>
        void Clear();
    }
}
