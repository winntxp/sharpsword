/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:11:14 PM
 * ****************************************************************/
using SharpSword.Logging.Log4Net;
using SharpSword.O2O.Services;
using SharpSword.O2O.Services.Impl;
using System;
using System.Linq;

namespace SharpSword.O2O.OrderSync
{
    class Program
    {
        /// <summary>
        /// 异步消息写订单到区域维度
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            //全局配置
            var globalConfig = new GlobalConfig();

            //初始化消息队列管理器
            IMessageManager messageManager = new RabbitMqMessageManager(new OrderMessageManagerConfig()
            {
                HostName = "localhost",
                UserName = "read",
                Password = "123456"
            }, globalConfig)
            {
                Logger = new Log4NetLoggerFactory().CreateLogger(typeof(RabbitMqMessageManager))
            };

            //for (int i = 0; i < 100000; i++)
            //{
            //    var x = messageManager.Publish<string>(i.ToString(), "order.sync.0");
            //    Console.WriteLine(i.ToString() + x);
            //}

            //每个队列开启一个线程
            new string[] { "order.sync.0", "order.sync.1", "order.sync.2", "order.sync.3" }.AsParallel().ForAll(queueName =>
            {
                //开始消费消息
                messageManager.Consume<string>(queueName, message =>
               {
                   Console.WriteLine(message);
               });
            });

            Console.ReadLine();
        }
    }
}

