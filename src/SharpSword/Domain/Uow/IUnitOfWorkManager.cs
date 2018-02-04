/* *******************************************************
 * SharpSword zhangliang4629@163.com 11/30/2016 4:58:52 PM
 * ****************************************************************/
using System.Transactions;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// 做单元管理器
    /// </summary>
    public interface IUnitOfWorkManager
    {
        /// <summary>
        /// 获取当前作用域UOW
        /// </summary>
        IActiveUnitOfWork Current { get; }

        /// <summary>
        /// 开启一个新的工作单元作用域
        /// </summary>
        /// <returns></returns>
        IUnitOfWorkCompleteHandle Begin();

        /// <summary>
        /// 开启一个新的工作单元作用域
        /// </summary>
        /// <param name="scope"></param>
        /// <returns></returns>
        IUnitOfWorkCompleteHandle Begin(TransactionScopeOption scope);

        /// <summary>
        /// 开启一个新的工作单元作用域
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        IUnitOfWorkCompleteHandle Begin(UnitOfWorkOptions options);
    }
}
