/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using log4net;
using log4net.Config;
using System;

namespace SharpSword.Logging.Log4Net
{
    /// <summary>
    /// Log4net日志创建器
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
        /// Log4net日志创建器
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public ILogger CreateLogger(Type type)
        {
            //创建日志记录器
            return new Log4NetLogger(LogManager.GetLogger(type));
        }
    }
}
