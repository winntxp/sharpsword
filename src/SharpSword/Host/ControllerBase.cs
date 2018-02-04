/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/14/2016 2:21:05 PM
 * ****************************************************************/
using SharpSword.Localization;
using System.Web.Mvc;

namespace SharpSword.Host
{
    /// <summary>
    /// 控制器基类
    /// </summary>
    public abstract class ControllerBase : Controller
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 缓存器
        /// </summary>
        public ICacheManager CacheManager { get; set; }

        /// <summary>
        /// 本地化器
        /// </summary>
        public Localizer L { get; set; }

        /// <summary>
        /// 初始化一下空日志接口，防止null错误
        /// </summary>
        protected ControllerBase()
        {
            this.Logger = NullLogger.Instance;
            this.CacheManager = NullCacheManager.Instance;
            this.L = NullLocalizer.Instance;
        }
    }
}
