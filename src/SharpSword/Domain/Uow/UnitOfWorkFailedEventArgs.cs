/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/24/2016 3:51:35 PM
 * ****************************************************************/
using System;

namespace SharpSword.Domain.Uow
{
    /// <summary>
    /// 工作单元提交失败事件参数
    /// </summary>
    public class UnitOfWorkFailedEventArgs : EventArgs
    {
        /// <summary>
        /// 
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public UnitOfWorkFailedEventArgs(Exception exception)
        {
            this.Exception = exception;
        }
    }
}
