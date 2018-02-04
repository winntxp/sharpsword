/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/24/2016 3:51:35 PM
 * ****************************************************************/
using System;
using System.Transactions;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// 用于标注被代理的方法开启工作单元参数配置
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class UnitOfWorkAttribute : Attribute
    {
        /// <summary>
        /// 是否开启全局事务
        /// </summary>
        public bool? IsTransactional { get; private set; }

        /// <summary>
        /// 事务超时设置
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// 指定事务的隔离级别
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }

        /// <summary>
        /// 提供用于创建事务范围的附加选项
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// 是否禁用了工作单元
        /// </summary>
        public bool IsDisabled { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isTransactional">默认不启用事务，但是针对每一次的SaveChange()，同样是一个事务的</param>
        public UnitOfWorkAttribute(bool isTransactional = false)
        {
            this.IsTransactional = isTransactional;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="timeout"></param>
        public UnitOfWorkAttribute(int timeout)
        {
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isTransactional"></param>
        /// <param name="timeout"></param>
        public UnitOfWorkAttribute(bool isTransactional, int timeout)
        {
            IsTransactional = isTransactional;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isolationLevel"></param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        {
            IsTransactional = false;
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="isolationLevel"></param>
        /// <param name="timeout"></param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel, int timeout)
        {
            IsTransactional = false;
            IsolationLevel = isolationLevel;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        public UnitOfWorkAttribute(TransactionScopeOption scope)
        {
            IsTransactional = false;
            Scope = scope;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="scope"></param>
        /// <param name="timeout"></param>
        public UnitOfWorkAttribute(TransactionScopeOption scope, int timeout)
        {
            IsTransactional = false;
            Scope = scope;
            Timeout = TimeSpan.FromMilliseconds(timeout);
        }

        /// <summary>
        /// 创建工作单元参数
        /// </summary>
        /// <returns></returns>
        internal UnitOfWorkOptions CreateOptions()
        {
            return new UnitOfWorkOptions
            {
                IsTransactional = IsTransactional,
                IsolationLevel = IsolationLevel,
                Timeout = Timeout,
                Scope = Scope
            };
        }
    }
}