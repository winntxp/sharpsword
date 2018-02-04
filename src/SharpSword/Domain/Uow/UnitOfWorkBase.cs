/* *******************************************************
 * SharpSword zhangliang4629@163.com 10/24/2016 3:51:35 PM
 * ****************************************************************/
using SharpSword.Events;
using System;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// 所有具体工作单元实现基类
    /// </summary>
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        /// <summary>
        /// 当前工作单元唯一编号
        /// </summary>
        public string Id { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IUnitOfWork Outer { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Completed;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler<UnitOfWorkFailedEventArgs> Failed;

        /// <summary>
        /// 
        /// </summary>
        public event EventHandler Disposed;

        /// <summary>
        /// 
        /// </summary>
        public UnitOfWorkOptions Options { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        private bool _succeed;

        /// <summary>
        /// 
        /// </summary>
        private Exception _exception;

        /// <summary>
        /// 
        /// </summary>
        protected UnitOfWorkBase()
        {
            this.Id = Guid.NewGuid().ToString("N");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public void Begin(UnitOfWorkOptions options)
        {
            if (options == null)
            {
                throw new ArgumentNullException("options");
            }

            this.Options = options;

            this.BeginUow();
        }

        /// <summary>
        /// 
        /// </summary>
        public abstract void SaveChanges();

        /// <summary>
        /// 
        /// </summary>
        public void Complete()
        {
            try
            {
                this.CompleteUow();
                _succeed = true;
                this.OnCompleted();
            }
            catch (Exception ex)
            {
                _exception = ex;
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (!_succeed)
            {
                this.OnFailed(_exception);
            }

            this.DisposeUow();
            this.OnDisposed();
        }

        /// <summary>
        /// 
        /// </summary>
        protected abstract void BeginUow();

        /// <summary>
        /// 
        /// </summary>
        protected abstract void CompleteUow();

        /// <summary>
        /// 
        /// </summary>
        protected abstract void DisposeUow();

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnCompleted()
        {
            Completed.InvokeSafely(this);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        protected virtual void OnFailed(Exception exception)
        {
            Failed.InvokeSafely(this, new UnitOfWorkFailedEventArgs(exception));
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void OnDisposed()
        {
            Disposed.InvokeSafely(this);
        }
    }
}