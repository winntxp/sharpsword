/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using log4net;
using log4net.Config;
using System;

namespace SharpSword.Logging.Log4Net
{
    /// <summary>
    /// Log4net��־������
    /// </summary>
    public class Log4NetLoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        static Log4NetLoggerFactory()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// Log4net��־������
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ILogger CreateLogger(Type type)
        {
            //������־��¼��
            return new Log4NetLogger(LogManager.GetLogger(type));
        }
    }
}
