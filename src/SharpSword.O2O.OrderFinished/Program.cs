/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:11:14 PM
 * ****************************************************************/
using SharpSword.Caching.Redis.StackExchange;
using SharpSword.Logging.Log4Net;
using SharpSword.O2O.Services;
using SharpSword.O2O.Services.Domain;
using SharpSword.O2O.Services.Impl;
using System;

namespace SharpSword.O2O.OrderFinished
{
    class Program
    {
        /// <summary>
        /// 定时自动完成N天未自动确认收货的订单
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //全局配置
            var globalConfig = new GlobalConfig();

            IRedisConnectionWrapper redisConnectionWrapper = new RedisConnectionWrapper(new RedisCacheManagerConfig()
            {
                ConnectionString = "192.168.6.233:6388,allowAdmin=true"
            });
            IMessageManager messageManager = new RabbitMqMessageManager(new OrderMessageManagerConfig()
            {
                HostName = "192.168.8.149",
                UserName = "frxs",
                Password = "frxs@123"
            }, globalConfig)
            {
                Logger = new Log4NetLoggerFactory().CreateLogger(typeof(RabbitMqMessageManager))
            };

            IDbConnectionStringProvider connectionStringProvider = new WebConfigDbConnectionStringProvider();
            IDbConnectionFactory dbConnectionFactory = new MSSQLDbConnectionFactory();
            IUserOrderDbFinder userOrderDbFinder = new DefaultUserOrderDbFinder(connectionStringProvider, globalConfig);
            IGlobalDbFinder globalDbFinder = new DefaultGlobalDbFinder(connectionStringProvider, globalConfig);
            IGlobalDbConnectionFactory globalDbConnectionFactory = new DefaultGlobalDbConnectionFactory(dbConnectionFactory, globalDbFinder);
            IUserOrderDbConnectionFactory userOrderDbConnectionFactory = new DefaultUserOrderDbConnectionFactory(dbConnectionFactory, userOrderDbFinder);
            IEventPublisher eventPublisher = new DefaultEventPublisher(messageManager, globalConfig);
            IPresaleActivityServices presaleActivityServices = new RedisPresaleActivityServices(redisConnectionWrapper,
                                                                                                globalDbConnectionFactory,
                                                                                                globalConfig);
            //IOrderExpiredManager orderExpiredManager = new RedisOrderExpiredManager(redisConnectionWrapper, globalConfig);
            //IOrderFinishedManager orderFinishedManager = new RedisOrderFinishedManager(redisConnectionWrapper, globalConfig);
            IOrderExpiredManager orderExpiredManager = new DbOrderExpiredManager(dbConnectionFactory, connectionStringProvider, globalConfig);
            IOrderFinishedManager orderFinishedManager = new DbOrderFinishedManager(dbConnectionFactory, connectionStringProvider, globalConfig);
            IOrderServices orderServices = new DefaultOrderServices(userOrderDbConnectionFactory,
                                                                    eventPublisher,null,null,
                                                                    presaleActivityServices,
                                                                    orderExpiredManager,
                                                                    orderFinishedManager,
                                                                    globalConfig);
            orderExpiredManager.Job((orderId, ticks) =>
            {
                Console.WriteLine("{0}-{1}", orderId, ticks);

                //自动完成作业
                orderServices.FinishOrder(new FinishOrderRequestDto()
                {
                    OrderId = orderId
                });
            });
        }
    }
}
