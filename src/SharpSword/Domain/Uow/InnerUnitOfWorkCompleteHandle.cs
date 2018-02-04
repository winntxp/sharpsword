/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/30/2016 4:57:27 PM
 * ****************************************************************/
using System;
using System.Runtime.InteropServices;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// 用于嵌套工作单元，不新开启新事务情况下使用
    /// </summary>
    internal class InnerUnitOfWorkCompleteHandle : IUnitOfWorkCompleteHandle
    {
        public const string DidNotCallCompleteMethodExceptionMessage = "Did not call Complete method of a unit of work.";
        private volatile bool _isCompleteCalled;
        private volatile bool _isDisposed;

        /// <summary>
        /// 
        /// </summary>
        public void Complete()
        {
            _isCompleteCalled = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (_isDisposed)
            {
                return;
            }

            _isDisposed = true;

            if (!_isCompleteCalled)
            {
                if (HasException())
                {
                    return;
                }

                throw new SharpSwordCoreException(DidNotCallCompleteMethodExceptionMessage);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static bool HasException()
        {
            try
            {
                return Marshal.GetExceptionCode() != 0;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
