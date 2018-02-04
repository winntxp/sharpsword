/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/16/2017 9:44:20 AM
 * ****************************************************************/
using RabbitMQ.Client;
using SharpSword.Configuration;
using SharpSword.Configuration.WebConfig;
using SharpSword.MQ;
using SharpSword.Serializers;
using System;
using System.Text;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 消息队列实现配置
    /// </summary>
    [ConfigurationSectionName("sharpsword.module.mq.rabbitmq"), Serializable, FailReturnDefault]
    public class OrderSubmitManagerConfig : ConfigurationSectionHandlerBase
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string HostName { get; set; } = "127.0.0.1";

        /// <summary>
        /// 
        /// </summary>
        public string VirtualHost { get; set; } = "/";

        /// <summary>
        /// 端口号
        /// </summary>
        public int Port { get; set; } = 5672;

        /// <summary>
        /// 用户
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }

    /// <summary>
    /// 订单提交消息队列实现
    /// </summary>
    public class RabbitMqOrderSubmitManager : IOrderMessageManager, IMessagePublisher, IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly OrderSubmitManagerConfig _config;
        private readonly ConnectionFactory _connectionFactory;
        private readonly IJsonSerializer _jsonSerializer;

        /// <summary>
        /// 
        /// </summary>
        private const string EXCHANGENAME = "order.submit";

        /// <summary>
        /// 
        /// </summary>
        private const string ORDERLIMITQUEUE = "order.save.limit";

        /// <summary>
        /// 
        /// </summary>
        private const string ORDERNORMALQUEUE = "order.save.normal";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name="jsonSerializer"></param>
        public RabbitMqOrderSubmitManager(OrderSubmitManagerConfig config, IJsonSerializer jsonSerializer = null)
        {
            config.CheckNullThrowArgumentNullException(nameof(config));
            this._config = config;
            this._jsonSerializer = jsonSerializer ?? JsonSerializerManager.Provider;
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
        /// <param name="routeKey"></param>
        /// <returns></returns>
        public bool Publish<T>(T message, string routeKey)
        {
            using (IConnection conn = this._connectionFactory.CreateConnection())
            {
                using (IModel channel = conn.CreateModel())
                {
                    //我们定义一个消息队列
                    channel.QueueDeclare(queue: ORDERLIMITQUEUE,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    channel.QueueDeclare(queue: ORDERNORMALQUEUE,
                                         durable: true,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    //定义一个topic交换器
                    channel.ExchangeDeclare(EXCHANGENAME, "topic", true);

                    //将交换器和队列进行绑定，并且设置下路由key
                    channel.QueueBind(ORDERLIMITQUEUE, EXCHANGENAME, ORDERLIMITQUEUE);
                    channel.QueueBind(ORDERNORMALQUEUE, EXCHANGENAME, ORDERNORMALQUEUE);

                    string msg = this._jsonSerializer.Serialize(message);
                    var body = Encoding.UTF8.GetBytes(msg);

                    //发布消息到交换器
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2;
                    channel.BasicPublish(exchange: EXCHANGENAME,
                                         routingKey: routeKey,
                                         basicProperties: properties,
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
        public void Consume<T>(string queueName, Action<T> messageConsumeAction)
        {
            using (var connection = _connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.BasicQos(0, 1, false);

                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queueName, false, consumer);

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
