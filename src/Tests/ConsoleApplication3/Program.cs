/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:11:14 PM
 * ****************************************************************/
using SharpSword.Caching.Redis.StackExchange;
using SharpSword.Logging.Log4Net;
using SharpSword.O2O.Services;
using SharpSword.O2O.Services.Domain;
using SharpSword.O2O.Services.Impl;
using System;
using System.Linq;

namespace SharpSword.O2O.OrderProgress
{
    class Program
    {
        /// <summary>
        /// TODO:订单处理演示，需要将下面代码进行整理，采取注入方式，减少手工调用
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //系统设置
            var globalConfig = new GlobalConfig();

            //日志记录器
            ILogger logger = new Log4NetLoggerFactory().CreateLogger(typeof(RabbitMqMessageManager));

            //缓存配置
            IRedisConnectionWrapper redisConnectionWrapper = new RedisConnectionWrapper(new RedisCacheManagerConfig()
            {
                ConnectionString = "192.168.6.233:6388,allowAdmin=true"
            });

            //消息管理器
            IMessageManager messageManager = new RabbitMqMessageManager(new OrderMessageManagerConfig()
            {
                HostName = "192.168.6.149",
                UserName = "frxs",
                Password = "frxs@123",
                Port = 5672
            }, globalConfig)
            {
                Logger = logger
            };

            //数据库字符串提供器
            IDbConnectionStringProvider connectionStringProvider = new WebConfigDbConnectionStringProvider();

            //数据库连接对象创建工厂
            IDbConnectionFactory dbConnectionFactory = new MSSQLDbConnectionFactory();
            IGlobalDbFinder globalDbFinder = new DefaultGlobalDbFinder(connectionStringProvider, globalConfig);
            IGlobalDbConnectionFactory globalDbConnectionFactory = new DefaultGlobalDbConnectionFactory(dbConnectionFactory, globalDbFinder);
            IUserOrderDbFinder userOrderDbFinder = new DefaultUserOrderDbFinder(connectionStringProvider, globalConfig);
            IUserOrderDbConnectionFactory userOrderDbConnectionFactory = new DefaultUserOrderDbConnectionFactory(dbConnectionFactory, userOrderDbFinder);
            //IOrderIdGenerator orderIdServices = new RedisOrderIdGenerator(redisConnectionWrapper);

            //基于数据库的订单ID生成器
            IOrderIdGenerator orderIdServices = new DbOrderIdGenerator(userOrderDbConnectionFactory);

            //排队服务
            IOrderSequenceServices orderSequenceServices = new RedisOrderSequenceServices(redisConnectionWrapper)
            {
                Logger = logger
            };

            //IOrderExpiredManager orderExpiredManager = new RedisOrderExpiredManager(redisConnectionWrapper, globalConfig);
            IOrderExpiredManager orderExpiredManager = new DbOrderExpiredManager(dbConnectionFactory,
                                                                                 connectionStringProvider,
                                                                                 globalConfig)
            {
                Logger = logger
            };

            //事件发布器
            IEventPublisher eventPublisher = new DefaultEventPublisher(messageManager, globalConfig);

            //预售服务
            IPresaleActivityServices presaleActivityServices = new RedisPresaleActivityServices(redisConnectionWrapper,
                                                                                                globalDbConnectionFactory,
                                                                                                globalConfig)
            {
                Logger = logger
            };

            //订单处理服务
            IOrderProgressServices orderProgressServices = new DefaultOrderProgressServices(userOrderDbConnectionFactory: userOrderDbConnectionFactory,
                                                                                            areaOrderDbConnectionFactory:null,
                                                                                            globalDbConnectionFactory: globalDbConnectionFactory,
                                                                                            orderIdGenerator: orderIdServices,
                                                                                            orderSequenceServices: orderSequenceServices,
                                                                                            orderExpiredManager: orderExpiredManager,
                                                                                            orderEventPublisher: eventPublisher,
                                                                                            presaleActivityServices: presaleActivityServices)
            {
                Logger = logger
            };

            //每个队列开启一个线程
            new string[] { "order.save.normal", "order.save.limit" }.AsParallel().ForAll(queueName =>
            {
                //开始消费消息
                messageManager.Consume<string>(queueName, message =>
               {
                   DateTime startTime = DateTime.Now;

                   //处理订单是否成功了
                   var result = orderProgressServices.SaveOrder(message.DeserializeJsonStringToObject<OrderDto>());

                   DateTime endTime = DateTime.Now;

                   Console.WriteLine("订单:{0}处理完毕-->耗时【{1}】毫秒",
                       result.Status == SaveOrderResultStatus.OK ? result.OrderId : result.Message,
                       (endTime - startTime).TotalMilliseconds);

               });
            });

            Console.ReadLine();
        }
    }
}
