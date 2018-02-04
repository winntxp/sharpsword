/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/4/2016 1:44:31 PM
 * ****************************************************************/
using System;

namespace SharpSword.Events.Exceptions
{
    /// <summary>
    /// 当系统出现异常后触发的事件，一般需要继承此事件，然后定义自己的异常事件
    /// </summary>
    [Serializable]
    public class ExceptionEventData : EventData
    {
        /// <summary>
        /// 异常对象
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception">异常对象</param>
        public ExceptionEventData(Exception exception)
        {
            this.Exception = exception;
        }
    }
}
