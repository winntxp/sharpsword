/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 12:18:52 PM
 * ****************************************************************/
using System;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 消息队列管理器
    /// </summary>
    public interface IMessageManager
    {
        /// <summary>
        /// 发布消息
        /// </summary>
        /// <param name="message">消息对象实体</param>
        /// <param name="routeKey">消息标签</param>
        /// <returns></returns>
        bool Publish<T>(T message, string routeKey);

        /// <summary>
        /// 接受消息
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queueName">队列名称</param>
        /// <param name="messageConsumeAction">消费消息委托</param>
        void Consume<T>(string queueName, Action<T> messageConsumeAction);
    }
}