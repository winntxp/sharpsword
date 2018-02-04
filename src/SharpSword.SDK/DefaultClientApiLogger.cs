/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/2/2015 8:32:16 PM
 * ****************************************************************/
using System;
using System.IO;
using System.Diagnostics;

namespace SharpSword.SDK
{
    /// <summary>
    /// 默认日志打点
    /// </summary>
    public class DefaultClientApiLogger : IApiClientLogger
    {
        /// <summary>
        /// 
        /// </summary>
        public const string LogDir = "App_Data/Logs/Sdk";
        public const string LogFileName = "{0}.log";
        public const string DatetimeFormat = "yyyy-MM-dd HH:mm:ss";

        /// <summary>
        /// 
        /// </summary>
        static DefaultClientApiLogger()
        {
            try
            {
                Directory.CreateDirectory(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LogDir));
            }
            catch
            {
                // ignored
            }
            string logFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                LogDir + "/" + string.Format(LogFileName, DateTime.Now.ToString("yyyyMMdd")));
            Trace.Listeners.Add(new TextWriterTraceListener(logFilePath));
            Trace.AutoFlush = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {
            Trace.WriteLine(message, DateTime.Now.ToString(DatetimeFormat) + " ERROR");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string message)
        {
            Trace.WriteLine(message, DateTime.Now.ToString(DatetimeFormat) + " WARN");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {
            Trace.WriteLine(message, DateTime.Now.ToString(DatetimeFormat) + " INFO");
        }
    }
}
