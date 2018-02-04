/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/18/2016 1:44:28 PM
 * ****************************************************************/
using SharpSword.Domain.Uow;
using SharpSword.Events;
using SharpSword.Localization;
using SharpSword.Runtime;

namespace SharpSword.Domain.Services
{
    /// <summary>
    /// 服务组件抽象基类，服务类基类我们定义一些常用的组件，用于方便业务代码开发
    /// </summary>
    public abstract class SharpSwordServicesBase : ISharpSwordServices
    {
        /// <summary>
        /// 
        /// </summary>
        private IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 工作单元管理器
        /// </summary>
        public IUnitOfWorkManager UnitOfWorkManager
        {
            get
            {
                if (_unitOfWorkManager.IsNull())
                {
                    throw new SharpSwordCoreException("UnitOfWorkManager未初始化");
                }
                return _unitOfWorkManager;
            }
            set { _unitOfWorkManager = value; }
        }

        /// <summary>
        /// 当前活动的工作单元
        /// </summary>
        protected IActiveUnitOfWork CurrentUnitOfWork
        {
            get { return this.UnitOfWorkManager.Current; }
        }

        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { protected get; set; }

        /// <summary>
        /// 缓存记录器
        /// </summary>
        public ICacheManager CacheManager { protected get; set; }

        /// <summary>
        /// 领域事件总线
        /// </summary>
        public IEventBus EventBus { protected get; set; }

        /// <summary>
        /// 当前登录相关信息
        /// </summary>
        public ISession Session { protected get; set; }

        /// <summary>
        /// 本地语言委托接口
        /// </summary>
        public Localizer L { protected get; set; }

        /// <summary>
        /// 构造一些空实现
        /// </summary>
        protected SharpSwordServicesBase()
        {
            this.Logger = NullLogger.Instance;
            this.CacheManager = NullCacheManager.Instance;
            this.Session = NullSession.Instance;
            this.EventBus = NullEventBus.Instance;
            this.L = NullLocalizer.Instance;
        }
    }
}
