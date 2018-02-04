/* *******************************************************
 * SharpSword zhangliang4629@163.com 8/15/2016 10:39:11 AM
 * ****************************************************************/
using System;
using log4net;
using log4net.Config;

namespace SharpSword.Logging.Log4Net
{
    /// <summary>
    /// 泛型日志记录器
    /// </summary>
    public class Log4NetLogger_1<TServiceType> : ILogger<TServiceType> where TServiceType : class
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// 
        /// </summary>
        static Log4NetLogger_1()
        {
            XmlConfigurator.Configure();
        }

        /// <summary>
        /// 
        /// </summary>
        public Log4NetLogger_1()
        {
            //创建日志记录器
            this._logger = new Log4NetLogger(LogManager.GetLogger(typeof(TServiceType)));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel level)
        {
            return this._logger.IsEnabled(level);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <param name="exception"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void Log(LogLevel level, Exception exception, string format, params object[] args)
        {
            this._logger.Log(level, exception, format, args);
        }
    }
}
