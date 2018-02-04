/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/15/2017 4:38:41 PM
 * ****************************************************************/
using System;

namespace SharpSword.MQ.Impl
{
    /// <summary>
    /// 默认空实现
    /// </summary>
    public class NullMQ : IMessagePublisher, IMessageConsumer
    {
        /// <summary>
        /// 空实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageConsumeAction"></param>
        public void Consume<T>(Action<T> messageConsumeAction) { }

        /// <summary>
        /// 空实现
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="messageLabel"></param>
        /// <returns></returns>
        public bool Publish<T>(T message, string messageLabel)
        {
            return true;
        }
    }
}
