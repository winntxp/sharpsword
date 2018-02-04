/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 默认的空的记录器
    /// </summary>
    public class NullLogger : ILogger
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly ILogger _instance = new NullLogger();

        /// <summary>
        /// 
        /// </summary>
        private NullLogger() { }

        /// <summary>
        /// 
        /// </summary>
        public static ILogger Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 日志记录级别
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel level)
        {
            return false;
        }

        /// <summary>
        /// 输出日志
        /// </summary>
        /// <param name="level"></param>
        /// <param name="exception"></param>
        /// <param name="format"></param>
        /// <param name="args"></param>
        public void Log(LogLevel level, Exception exception, string format, params object[] args)
        {
        }
    }
}