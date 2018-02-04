/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/24/2016 3:51:35 PM
 * ****************************************************************/
using System;
using System.Transactions;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// 工作单元配置参数
    /// </summary>
    public class UnitOfWorkOptions
    {
        /// <summary>
        /// 提供用于创建事务范围的附加选项
        /// </summary>
        public TransactionScopeOption? Scope { get; set; }

        /// <summary>
        /// 是否支持事务？默认:false
        /// </summary>
        public bool? IsTransactional { get; set; }

        /// <summary>
        /// 事务超时时间
        /// </summary>
        public TimeSpan? Timeout { get; set; }

        /// <summary>
        /// 指定事务的隔离级别
        /// </summary>
        public IsolationLevel? IsolationLevel { get; set; }
    }
}