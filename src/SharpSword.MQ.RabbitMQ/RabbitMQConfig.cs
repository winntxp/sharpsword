/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 17:05:48
 * ****************************************************************/
using SharpSword.Configuration;
using SharpSword.Configuration.WebConfig;
using System;

namespace SharpSword.MQ.RabbitMQ
{
    /// <summary>
    /// 消息队列实现配置
    /// </summary>
    [ConfigurationSectionName("sharpsword.module.mq.rabbitmq"), Serializable, FailReturnDefault]
    public class RabbitMQConfig : ConfigurationSectionHandlerBase
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
        /// 队列名称
        /// </summary>
        public string QueueName { get; set; } = "SharpSword";

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
