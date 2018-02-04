/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System;
using System.Runtime.Serialization;

namespace SharpSword
{
    /// <summary>
    /// API系统框架定义的错误类，系统错误，尽量使用此类来抛出异常，
    /// 便于框架捕捉框架错误消息
    /// </summary>
    [Serializable]
    public class SharpSwordCoreException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public SharpSwordCoreException() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public SharpSwordCoreException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageFormat"></param>
        /// <param name="args"></param>
        public SharpSwordCoreException(string messageFormat, params object[] args)
            : base(string.Format(messageFormat, args))
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SharpSwordCoreException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public SharpSwordCoreException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
