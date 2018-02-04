/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/30/2016 4:54:40 PM
 * ****************************************************************/
using System;
using System.Collections.Concurrent;
using System.Runtime.Remoting.Messaging;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// 注意：不要进行属性注入
    /// </summary>
    public class CallContextCurrentUnitOfWorkProvider : ICurrentUnitOfWorkProvider
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly Func<ILogger> _logger;

        /// <summary>
        /// 
        /// </summary>
        private const string ContextKey = "SharpSword.UnitOfWork.Current";

        /// <summary>
        /// 
        /// </summary>
        private static readonly ConcurrentDictionary<string, IUnitOfWork> UnitOfWorkDictionary = new ConcurrentDictionary<string, IUnitOfWork>();

        /// <summary>
        /// 这里我们不将日志管理器作为属性注入，而作为构造函数注入是因为此类不能注册成自动属性注入
        /// </summary>
        /// <param name="logger"></param>
        public CallContextCurrentUnitOfWorkProvider(Func<ILogger<CallContextCurrentUnitOfWorkProvider>> logger)
        {
            this._logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <returns></returns>
        private static IUnitOfWork GetCurrentUow(ILogger logger)
        {
            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (unitOfWorkKey.IsNull())
            {
                return null;
            }

            IUnitOfWork unitOfWork;
            if (!UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out unitOfWork))
            {
                if (logger.IsEnabled(LogLevel.Warning))
                {
                    logger.Warning("在CallContext有一个unitofworkkey但不在UnitOfWorkDictionary！");
                }
                CallContext.FreeNamedDataSlot(ContextKey);
                return null;
            }

            if (unitOfWork.IsDisposed)
            {
                if (logger.IsEnabled(LogLevel.Warning))
                {
                    logger.Warning("在CallContext有一个unitofworkkey但UOW已经被释放了");
                }
                UnitOfWorkDictionary.TryRemove(unitOfWorkKey, out unitOfWork);
                CallContext.FreeNamedDataSlot(ContextKey);
                return null;
            }

            return unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="logger"></param>
        private static void SetCurrentUow(IUnitOfWork value, ILogger logger)
        {
            if (value.IsNull())
            {
                ExitFromCurrentUowScope(logger);
                return;
            }

            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;
            if (!unitOfWorkKey.IsNull())
            {
                IUnitOfWork outer;
                if (UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out outer))
                {
                    if (outer == value)
                    {
                        if (logger.IsEnabled(LogLevel.Warning))
                        {
                            logger.Warning("设定相同UOW CallContext，无需重新设置！");
                        }
                        return;
                    }

                    value.Outer = outer;
                }
            }

            unitOfWorkKey = value.Id;
            if (!UnitOfWorkDictionary.TryAdd(unitOfWorkKey, value))
            {
                throw new SharpSwordCoreException("不能设置工作单元！unitofworkdictionary.tryadd返回false！");
            }

            CallContext.LogicalSetData(ContextKey, unitOfWorkKey);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        private static void ExitFromCurrentUowScope(ILogger logger)
        {
            var unitOfWorkKey = CallContext.LogicalGetData(ContextKey) as string;

            if (unitOfWorkKey.IsNull())
            {
                if (logger.IsEnabled(LogLevel.Warning))
                {
                    logger.Warning("当前工作单元不存在");
                }
                return;
            }

            IUnitOfWork unitOfWork;
            if (!UnitOfWorkDictionary.TryGetValue(unitOfWorkKey, out unitOfWork))
            {
                if (logger.IsEnabled(LogLevel.Warning))
                {
                    logger.Warning("在CallContext有一个unitofworkkey但不在UnitOfWorkDictionary！");
                }
                CallContext.FreeNamedDataSlot(ContextKey);
                return;
            }

            UnitOfWorkDictionary.TryRemove(unitOfWorkKey, out unitOfWork);
            if (unitOfWork.Outer.IsNull())
            {
                CallContext.FreeNamedDataSlot(ContextKey);
                return;
            }

            var outerUnitOfWorkKey = unitOfWork.Outer.Id;
            if (!UnitOfWorkDictionary.TryGetValue(outerUnitOfWorkKey, out unitOfWork))
            {
                if (logger.IsEnabled(LogLevel.Warning))
                {
                    logger.Warning("Outer UOW 在工作单元字典 UnitOfWorkDictionary 里不存在");
                }
                CallContext.FreeNamedDataSlot(ContextKey);
                return;
            }

            CallContext.LogicalSetData(ContextKey, outerUnitOfWorkKey);
        }

        /// <summary>
        /// 运行时赋值，所以不要将此类进行属性注入
        /// </summary>
        public IUnitOfWork Current
        {
            get { return GetCurrentUow(this._logger()); }
            set { SetCurrentUow(value, this._logger()); }
        }
    }
}
