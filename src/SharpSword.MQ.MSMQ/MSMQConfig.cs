/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 17:05:48
 * ****************************************************************/
using SharpSword.Configuration.WebConfig;
using System;

namespace SharpSword.MQ.MSMQ
{
    /// <summary>
    /// MSMQ消息队列实现配置
    /// </summary>
    [ConfigurationSectionName("sharpsword.module.mq.msmq"), Serializable]
    public class MSMQConfig : ConfigurationSectionHandlerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public MSMQConfig() { }

        /// <summary>
        /// 连接字符串，如： .\\private$\\TEST
        /// </summary>
        public string ConnectionString { get; set; }

    }
}
