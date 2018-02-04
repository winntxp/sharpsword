/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/16/2017 9:44:20 AM
 * ****************************************************************/
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SharpSword.Configuration;
using SharpSword.Configuration.WebConfig;
using SharpSword.Serializers;
using System;
using System.Text;
using System.Threading;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 订单提交消息队列实现（RabbitMQ必须最低做3台集群，1台磁盘模式，2台内存模式，前端做2台HAProxy负责均衡）
    /// </summary>
    public class RabbitMqMessageManager : IMessageManager
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly OrderMessageManagerConfig _config;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly GlobalConfig _globalConfig;
        private readonly object _lock = new object();

        /// <summary>
        /// 
        /// </summary>
        private static ConnectionFactory _connactionFactory;
        /// <summary>
        /// 
        /// </summary>
        private static IConnection _publicConnection;

        /// <summary>
        /// 消息交换器名称
        /// </summary>
        private const string EXCHANGENAME = "order.topic";

        /// <summary>
        /// 含有限购商品订单队列（包括：商品限购，用户限购）
        /// </summary>
        private const string ORDERLIMITQUEUE = "order.save.limit";

        /// <summary>
        /// 不含有任何限购商品的订单队列
        /// </summary>
        private const string ORDERNORMALQUEUE = "order.save.normal";

        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 系统报警器
        /// </summary>
        public ISystemWarningTrigger WarningTrigger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        /// <param name=""></param>
        /// <param name="globalConfig"></param>
        /// <param name="jsonSerializer"></param>
        public RabbitMqMessageManager(OrderMessageManagerConfig config,
                                           GlobalConfig globalConfig,
                                           IJsonSerializer jsonSerializer = null)
        {
            config.CheckNullThrowArgumentNullException(nameof(config));
            this._config = config;
            this._jsonSerializer = jsonSerializer ?? JsonSerializerManager.Provider;
            this._globalConfig = globalConfig;
            this.Logger = GenericNullLogger<RabbitMqMessageManager>.Instance;
            this.WarningTrigger = NullSystemWarningTrigger.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IConnectionFactory GetConnectionFactory()
        {
            if (!_connactionFactory.IsNull())
            {
                return _connactionFactory;
            }

            _connactionFactory = new ConnectionFactory();
            _connactionFactory.AutomaticRecoveryEnabled = true;
            _connactionFactory.NetworkRecoveryInterval = TimeSpan.FromSeconds(2);
            _connactionFactory.RequestedHeartbeat = 60;
            _connactionFactory.Protocol = Protocols.DefaultProtocol;

            _connactionFactory.HostName = _config.HostName;
            _connactionFactory.Port = _config.Port;
            _connactionFactory.UserName = _config.UserName;
            _connactionFactory.Password = _config.Password;
            _connactionFactory.VirtualHost = _config.VirtualHost;
            //_connactionFactory.Uri = "amqp://user:pass@hostName:port/vhost";
            return _connactionFactory;
        }

        /// <summary>
        /// 获取发布连接对象(tcp我们每隔负载值连接一个，防止连接数过多)
        /// </summary>
        /// <returns></returns>
        private IConnection GetConnection()
        {
            //当连接不为null且连接是打开的，我们直接返回
            if (!_publicConnection.IsNull() && _publicConnection.IsOpen)
            {
                return _publicConnection;
            }

            //否则我们加锁重新创建一个连接
            lock (_lock)
            {
                if (!_publicConnection.IsNull() && _publicConnection.IsOpen)
                {
                    return _publicConnection;
                }

                //创建一个新的socket连接
                var connectionFactory = this.GetConnectionFactory();
                try
                {
                    _publicConnection = connectionFactory.CreateConnection();
                }
                catch (Exception ex)
                {
                    this.Logger.Error(ex);
                }
            }

            //返回发布连接
            return _publicConnection;
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
            try
            {
                //获取连接
                _publicConnection = this.GetConnection();

                //获取连接失败
                if (_publicConnection.IsNull())
                {
                    return false;
                }

                //创建一个新的管道
                using (IModel channel = _publicConnection.CreateModel())
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

                    //异构系统同步队列
                    for (int i = 0; i < this._globalConfig.SyncQueuesNumber; i++)
                    {
                        channel.QueueDeclare(queue: "order.sync.{0}".With(i),
                                             durable: true,
                                             exclusive: false,
                                             autoDelete: false,
                                             arguments: null);
                    }

                    //定义一个topic交换器
                    channel.ExchangeDeclare(EXCHANGENAME, "topic", true);

                    //将交换器和队列进行绑定，并且设置下路由key
                    channel.QueueBind(ORDERLIMITQUEUE, EXCHANGENAME, ORDERLIMITQUEUE);
                    channel.QueueBind(ORDERNORMALQUEUE, EXCHANGENAME, ORDERNORMALQUEUE);

                    //绑定下消息分发
                    for (int i = 0; i < this._globalConfig.SyncQueuesNumber; i++)
                    {
                        channel.QueueBind("order.sync.{0}".With(i), EXCHANGENAME, "order.sync.{0}".With(i));
                    }

                    var body = this._jsonSerializer.Serialize(message).GetBytes();

                    //发布消息到交换器
                    var properties = channel.CreateBasicProperties();
                    properties.DeliveryMode = 2;
                    channel.BasicPublish(exchange: EXCHANGENAME,
                                         routingKey: routeKey,
                                         basicProperties: properties,
                                         body: body);
                }

                //直接返回成功
                return true;
            }
            catch (Exception ex)
            {
                //记录下错误日志
                this.Logger.Error(ex);

                //发送警告给特定人员
                this.WarningTrigger.Warning(this, "消息队列异常，发布消息失败.", ex);

                //反馈发布消息失败
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queueName"></param>
        /// <param name="messageConsumeAction"></param>
        public void Consume<T>(string queueName, Action<T> messageConsumeAction)
        {
            //我们提供下处错误重试功能
            try
            {
                //创建一个连接
                var connection = this.GetConnectionFactory().CreateConnection();

                //断开连接事件
                //connection.ConnectionShutdown += (s, e) => {
                //    this.Logger.Error(e.ReplyText);
                //};

                //创建连接通道
                var channel = connection.CreateModel();

                //每次我们只接受一条消息
                channel.BasicQos(0, 1, false);

                //channel.BasicRecover(true);

                //采取事件的方式进行监听
                var consumer = new EventingBasicConsumer(channel);
               
                //接受消息委托
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var obj = this._jsonSerializer.Deserialize<T>(message);

                    try
                    {
                        //只有外部操作不抛出异常，我们才确认消息完成
                        messageConsumeAction?.Invoke(obj);

                        //回复确认
                        channel.BasicAck(ea.DeliveryTag, false);
                    }
                    catch (Exception ex)
                    {
                        //记录下错误日志
                        this.Logger.Error(ex);

                        //发送消息警告给特定人员
                        this.WarningTrigger.Warning(this, "消息队列异常，消费消息失败.", ex);
                    }
                };

                //开始消费消息
                channel.BasicConsume(queueName, false, consumer);
            }
            catch (Exception ex)
            {
                //记录下错误日志
                this.Logger.Error(ex);

                //发送警告给特定人员
                this.WarningTrigger.Warning(this, "消息队列异常，消费消息失败.", ex);
            }
        }
    }

    /// <summary>
    /// 消息队列实现配置
    /// </summary>
    [ConfigurationSectionName("sharpsword.module.mq.rabbitmq"), Serializable, FailReturnDefault]
    public class OrderMessageManagerConfig : ConfigurationSectionHandlerBase
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string HostName { get; set; } = "127.0.0.1";

        /// <summary>
        /// 虚拟服务器
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
}
