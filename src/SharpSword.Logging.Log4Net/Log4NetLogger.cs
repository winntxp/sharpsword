/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System;
using log4net;

namespace SharpSword.Logging.Log4Net
{
    /// <summary>
    /// Log4net日志实现，对log4net进行适配
    /// </summary>
    public class Log4NetLogger : ILogger
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILog _logger;

        /// <summary>
        /// Log4net日志实现，对log4net进行适配
        /// </summary>
        /// <param name="logger"></param>
        public Log4NetLogger(ILog logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void Error(Exception exception, string format, params object[] args)
        {
            _logger.ErrorFormat(format, args);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Debug:
                    return this._logger.IsDebugEnabled;
                case LogLevel.Information:
                    return this._logger.IsInfoEnabled;
                case LogLevel.Warning:
                    return this._logger.IsWarnEnabled;
                case LogLevel.Error:
                    return this._logger.IsErrorEnabled;
                case LogLevel.Fatal:
                    return this._logger.IsFatalEnabled;
            }
            return false;
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
            //日志消息
            string message = args == null ? format : string.Format(format, args);

            //等级
            switch (level)
            {
                case LogLevel.Debug:
                    this._logger.Debug(message, exception);
                    break;
                case LogLevel.Information:
                    this._logger.Info(message, exception);
                    break;
                case LogLevel.Warning:
                    this._logger.Warn(message, exception);
                    break;
                case LogLevel.Error:
                    this._logger.Error(message, exception);
                    break;
                case LogLevel.Fatal:
                    this._logger.Fatal(message, exception);
                    break;
            }
        }
    }
}
