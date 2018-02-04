/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/15/2017 4:06:15 PM
 * ****************************************************************/
using System;
using System.Messaging;

namespace SharpSword.MQ.MSMQ
{
    /// <summary>
    /// 微软消息队列具体实现
    /// </summary>
    public class MSMQManager : IMessagePublisher, IMessageConsumer, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private MessageQueue _messageQueue;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path">消息队列地址，如： .\\private$\\TEST </param>
        public MSMQManager(MSMQConfig config)
        {
            this._messageQueue = new MessageQueue(config.ConnectionString);
            this._messageQueue.Formatter = new BinaryMessageFormatter();
        }

        /// <summary>
        /// 消费消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageConsumeAction"></param>
        public void Consume<T>(Action<T> messageConsumeAction)
        {
            while (true)
            {
                T body = (T)((object)this._messageQueue.Receive().Body);
                messageConsumeAction(body);
            }
        }

        /// <summary>
        /// 发布消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="messageLabel"></param>
        /// <returns></returns>
        public bool Publish<T>(T message, string messageLabel)
        {
            try
            {
                if (this._messageQueue.CanWrite)
                {
                    Message myMessage = new Message(message, new BinaryMessageFormatter());
                    myMessage.Label = messageLabel;
                    myMessage.Recoverable = true;
                    this._messageQueue.Send(myMessage);
                    return true;
                }
            }
            catch { }

            return false;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this._messageQueue.Close();
            this._messageQueue.Dispose();
        }
    }
}
