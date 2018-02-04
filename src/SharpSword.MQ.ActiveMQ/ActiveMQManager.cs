/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/16/2017 9:44:20 AM
 * ****************************************************************/
using Apache.NMS;
using Apache.NMS.ActiveMQ;
using Apache.NMS.ActiveMQ.Commands;
using SharpSword.Serializers;
using System;

namespace SharpSword.MQ.ActiveMQ
{
    /// <summary>
    /// 消息队列实现
    /// </summary>
    public class ActiveMQManager : IMessagePublisher, IMessageConsumer, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ActiveMQConfig _config;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IConnectionFactory _connectionFactory;

        /// <summary>
        /// 
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public ActiveMQManager(ActiveMQConfig config, IJsonSerializer jsonSerializer)
        {
            config.CheckNullThrowArgumentNullException(nameof(config));
            jsonSerializer.CheckNullThrowArgumentNullException(nameof(jsonSerializer));
            this._config = config;
            this._jsonSerializer = jsonSerializer;
            this._connectionFactory = new ConnectionFactory(config.ProviderURI);
            this.Logger = GenericNullLogger<ActiveMQManager>.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="message"></param>
        /// <param name="messageLabel"></param>
        /// <returns></returns>
        public bool Publish<T>(T message, string messageLabel)
        {
            try
            {
                using (IConnection connection = this._connectionFactory.CreateConnection())
                {
                    using (var session = connection.CreateSession())
                    {
                        IMessageProducer prod = session.CreateProducer(new ActiveMQTopic(this._config.ActiveMQTopic));
                        ITextMessage msg = prod.CreateTextMessage();
                        msg.Text = this._jsonSerializer.Serialize(message);
                        prod.Send(msg, MsgDeliveryMode.NonPersistent, MsgPriority.Normal, TimeSpan.MinValue);

                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                this.Logger.Error(ex);
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageConsumeAction"></param>
        public void Consume<T>(Action<T> messageConsumeAction)
        {
            using (IConnection connection = this._connectionFactory.CreateConnection())
            {
                connection.ClientId = "SharpSword-MQ";
                connection.Start();

                using (var session = connection.CreateSession())
                {
                    var consumer = session.CreateDurableConsumer(new ActiveMQTopic(this._config.ActiveMQTopic),
                        "SharpSword-MQ", null, false);
                    consumer.Listener += (message) =>
                    {
                        ITextMessage textMessage = (ITextMessage)message;
                        var obj = this._jsonSerializer.Deserialize<T>(textMessage.Text);
                        messageConsumeAction?.Invoke(obj);
                    };
                }

                connection.Stop();
                connection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            //throw new NotImplementedException();
        }

    }
}
