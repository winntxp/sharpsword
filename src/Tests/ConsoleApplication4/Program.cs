using SharpSword.O2O.Services;
using SharpSword.O2O.Services.Impl;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ConsoleApplication4
{
    class Program
    {
        static void Main(string[] args)
        {

            //系统设置
            var globalConfig = new GlobalConfig();

            //每个队列开启一个线程
            new int[] { 1,2,3,4,67,8}.AsParallel().WithDegreeOfParallelism(6).ForAll(i=>
            {
                IMessageManager messageManager = new RabbitMqMessageManager(new OrderMessageManagerConfig()
                {
                    HostName = "192.168.6.149",
                    UserName = "frxs",
                    Password = "frxs@123",
                    Port = 5672
                }, globalConfig);


                //事件发布器
                IEventPublisher eventPublisher = new DefaultEventPublisher(messageManager, globalConfig);

                for (int x = 0; x < 100000; x++)
                {
                    eventPublisher.OrderCreated(new SharpSword.O2O.Services.Events.OrderCreated()
                    {
                        OrderId = System.Guid.NewGuid().ToString(),
                    });

                    Console.WriteLine(x);

                }
            });


            Console.ReadLine();
        }
    }
}
