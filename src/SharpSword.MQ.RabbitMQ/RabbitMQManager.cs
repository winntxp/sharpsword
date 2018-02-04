/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/16/2017 9:44:20 AM
 * ****************************************************************/
using RabbitMQ.Client;
using SharpSword.Serializers;
using System;
using System.Text;

namespace SharpSword.MQ.RabbitMQ
{
    /// <summary>
    /// RabbitMQ消息队列实现
    /// </summary>
    public class RabbitMQManager : IMessagePublisher, IMessageConsumer, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly RabbitMQConfig _config;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IJsonSerializer _jsonSerializer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public RabbitMQManager(RabbitMQConfig config, IJsonSerializer jsonSerializer)
        {
            config.CheckNullThrowArgumentNullException(nameof(config));
            jsonSerializer.CheckNullThrowArgumentNullException(nameof(jsonSerializer));
            this._config = config;
            this._jsonSerializer = jsonSerializer;
            this._connectionFactory = new ConnectionFactory();
            this._connectionFactory.HostName = config.HostName;
            this._connectionFactory.Port = config.Port;
            if (!config.UserName.IsNullOrEmpty())
            {
                this._connectionFactory.UserName = config.UserName;
            }
            if (!config.Password.IsNullOrEmpty())
            {
                this._connectionFactory.Password = config.Password;
            }
            if (!config.VirtualHost.IsNullOrEmpty())
            {
                this._connectionFactory.VirtualHost = config.VirtualHost;
            }
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
            using (IConnection conn = this._connectionFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    //我们定义一个消息队列
                    channel.QueueDeclare(queue: this._config.QueueName,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);
                    //另一一个topic交换器
                    channel.ExchangeDeclare("SharpSword", "topic", true);

                    //将交换器和队列进行绑定，并且设置下路由key
                    channel.QueueBind(this._config.QueueName, "SharpSword", "SharpSword");

                    string msg = this._jsonSerializer.Serialize(message);
                    var body = Encoding.UTF8.GetBytes(msg);

                    //发布消息到交换器
                    var b = channel.CreateBasicProperties();
                    b.DeliveryMode = 2;
                    channel.BasicPublish(exchange: "SharpSword",
                                         routingKey: "SharpSword",
                                         basicProperties: b,
                                         body: body);
                }
            }

            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="messageConsumeAction"></param>
        public void Consume<T>(Action<T> messageConsumeAction)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    //channel.QueueDeclare(queue: this._config.QueueName,
                    //                     durable: true,
                    //                     exclusive: false,
                    //                     autoDelete: false,
                    //                     arguments: null);

                    //var consumer = new EventingBasicConsumer(channel);
                    //consumer.Received += (model, ea) =>
                    //{
                    //    var body = ea.Body;
                    //    var message = Encoding.UTF8.GetString(body);
                    //    var obj = this._jsonSerializer.Deserialize<T>(message);
                    //    messageConsumeAction?.Invoke(obj);
                    //};
                    //channel.BasicConsume(queue: this._config.QueueName,
                    //                     noAck: true,
                    //                     consumer: consumer);

                    channel.BasicQos(0, 1, false);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(this._config.QueueName, false, consumer);

                    while (true)
                    {
                        var ea = consumer.Queue.Dequeue();
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        var obj = this._jsonSerializer.Deserialize<T>(message);

                        messageConsumeAction?.Invoke(obj);

                        //回复确认
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                }
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
