/* *******************************************************
 * SharpSword zhangliang4629@163.com 10/24/2016 3:51:35 PM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public interface IActiveUnitOfWork
    {
        /// <summary>
        /// 工作单元提交完成触发事件
        /// </summary>
        event EventHandler Completed;

        /// <summary>
        /// 工作单元提交失败触发事件
        /// </summary>
        event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// 当前工作单元被释放后触发事件
        /// </summary>
        event EventHandler Disposed;

        /// <summary>
        /// 工作单元参数
        /// </summary>
        UnitOfWorkOptions Options { get; }

        /// <summary>
        /// 当前工作单元是否已经释放
        /// </summary>
        bool IsDisposed { get; }

        /// <summary>
        /// 用于手工提交跟踪改变（一般用于当我们需要及时获取数据库自增值的时候调用一次）
        /// </summary>
        void SaveChanges();
    }
}