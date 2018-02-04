/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 17:05:48
 * ****************************************************************/
using SharpSword.Configuration;
using SharpSword.Configuration.WebConfig;
using System;

namespace SharpSword.MQ.ActiveMQ
{
    /// <summary>
    /// 消息队列实现配置
    /// </summary>
    [ConfigurationSectionName("sharpsword.module.mq.activemq"), Serializable, FailReturnDefault]
    public class ActiveMQConfig : ConfigurationSectionHandlerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ActiveMQConfig() { }

        /// <summary>
        /// 连接字符串
        /// </summary>
        public string ProviderURI { get; set; } = "tcp://localhost:61616";

        /// <summary>
        /// 
        /// </summary>
        public string ActiveMQTopic { get; set; } = "ActiveMQ";

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
