/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 空的默认日志创建工厂类
    /// </summary>
    public class NullLoggerFactory : ILoggerFactory
    {
        /// <summary>
        /// 创建日志
        /// </summary>
        /// <param name="type">日志产生的依附类，用于表示日志是哪个类型产生的</param>
        /// <returns></returns>
        public ILogger CreateLogger(Type type)
        {
            return NullLogger.Instance;
        }
    }
}