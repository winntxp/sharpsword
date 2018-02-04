/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/5/31 8:40:34
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 过滤器接口扩展
    /// </summary>
    internal static class ActionFilterExtensions
    {
        /// <summary>
        /// 将过滤器转换成内部过滤器包装类
        /// </summary>
        /// <param name="actionFilter">接口过滤器</param>
        /// <returns>返回过滤器包装类</returns>
        public static ActionFilterWrapper Wrapped(this IActionFilter actionFilter)
        {
            actionFilter.CheckNullThrowArgumentNullException(nameof(actionFilter));
            return new ActionFilterWrapper(actionFilter);
        }
    }
}
