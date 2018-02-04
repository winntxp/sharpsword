/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 泛型日志记录器空实现
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class GenericNullLogger<T> : ILogger<T> where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        private static readonly ILogger<T> _instance = new GenericNullLogger<T>();

        /// <summary>
        /// 注意此公开的构造函数必须存在，为了实现泛型注入
        /// </summary>
        public GenericNullLogger() { }

        /// <summary>
        /// 
        /// </summary>
        public static ILogger<T> Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public bool IsEnabled(LogLevel level)
        {
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
        }
    }
}