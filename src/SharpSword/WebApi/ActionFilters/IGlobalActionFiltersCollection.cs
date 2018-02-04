/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/15 19:14:13
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 全局接口过滤器集合接口
    /// </summary>
    public interface IGlobalActionFiltersCollection
    {
        /// <summary>
        /// 添加一个全局接口过滤器
        /// </summary>
        /// <param name="actionFilters">过滤器</param>
        void Add(params IActionFilter[] actionFilters);

        /// <summary>
        /// 清空所有过滤器
        /// </summary>
        void Clear();

        /// <summary>
        /// 全局接口过滤器数
        /// </summary>
        int Count { get; }

        /// <summary>
        /// 移除特定对象的第一个匹配项。
        /// </summary>
        /// <param name="item">继承自ActionFilterBaseAttribute的过滤器</param>
        /// <returns></returns>
        bool Remove(IActionFilter item);

        /// <summary>
        /// 获取所有的全局结果过滤器
        /// </summary>
        IEnumerable<IActionFilter> GetActionFilters();
    }
}
