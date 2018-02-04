/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/9 8:39:37
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 用于搜索所有合法的Action接口信息
    /// </summary>
    public interface IActionSelector
    {
        /// <summary>
        /// 获取全部实现的接口（实现里需要实现将有移除特性ObsoleteAttribute的Action过滤掉）
        /// </summary>
        /// <param name="skipSystemActions">是否需要跳过系统接口</param>
        IEnumerable<ActionDescriptor> GetActionDescriptors(bool skipSystemActions = false);

        /// <summary>
        /// 根据接口名称获取接口信息
        /// </summary>
        /// <param name="actionName">接口名称，具体实现里请实现接口名称大小写不敏感</param>
        /// <returns></returns>
        IEnumerable<ActionDescriptor> GetActionDescriptors(string actionName);

        /// <summary>
        /// 根据接口名称获取接口描述
        /// </summary>
        /// <param name="actionName">接口名称，具体实现里请实现接口名称大小写不敏感</param>
        /// <param name="version">接口版本,如果不指定版本号，请实现类里实现返回版本号最大的接口描述对象</param>
        /// <returns>返回接口描述，如果不存在则返回null</returns>
        ActionDescriptor GetActionDescriptor(string actionName, string version);

        /// <summary>
        /// 重新刷新接口查找器缓存的接口描述数据
        /// </summary>
        void Reset();
    }
}
