/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/22 20:37:56
 * ****************************************************************/
using System;

namespace SharpSword.MQ
{
    /// <summary>
    /// 消息接收者
    /// </summary>
    public interface IMessageConsumer
    {
        /// <summary>
        /// 接受消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageConsumeAction">消费消息委托</param>
        void Consume<T>(Action<T> messageConsumeAction);
    }
}
