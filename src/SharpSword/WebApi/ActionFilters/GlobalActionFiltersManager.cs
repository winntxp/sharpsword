/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/28 14:38:30
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 全局拦截器管理器
    /// </summary>
    public class GlobalActionFiltersManager
    {
        /// <summary>
        /// 全局拦截器表
        /// </summary>
        private static readonly GlobalActionFiltersCollection Instance = new GlobalActionFiltersCollection();

        /// <summary>
        /// 返回全局拦截器配置表
        /// </summary>
        public static GlobalActionFiltersCollection Filters => Instance;
    }
}
