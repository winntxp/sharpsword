/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 日志记录器创建工厂
    /// </summary>
    public interface ILoggerFactory
    {
        /// <summary>
        /// 创建日志记录器
        /// </summary>
        /// <param name="type">任意的类型；不会影响到ILogger的创建；仅仅作为日志记录异常类</param>
        /// <returns></returns>
        ILogger CreateLogger(Type type);
    }
}