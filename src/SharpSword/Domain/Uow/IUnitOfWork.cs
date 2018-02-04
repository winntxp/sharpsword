/* ****************************************************************
 * SharpSword zhangliang4629@163.com 10/24/2016 3:51:35 PM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// 工作单元接口
    /// </summary>
    public interface IUnitOfWork : IActiveUnitOfWork, IUnitOfWorkCompleteHandle
    {
        /// <summary>
        /// 工作单元编号
        /// </summary>
        string Id { get; }

        /// <summary>
        /// 工作单元链表，用于工作单元嵌套管理（后进先出）
        /// </summary>
        IUnitOfWork Outer { get; set; }

        /// <summary>
        /// 开启工作单元
        /// </summary>
        /// <param name="options"></param>
        void Begin(UnitOfWorkOptions options);
    }
}