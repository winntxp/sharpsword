/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 日志记录器
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 是否启用日志记录器(针对某一级别的)
        /// </summary>
        /// <param name="level">日志记录等级，需要自己在实现类里实现严格按照等级记录，还是按照最低等级记录等等</param>
        /// <returns></returns>
        bool IsEnabled(LogLevel level);

        /// <summary>
        /// 记录日志
        /// </summary>
        /// <param name="level">记录等级</param>
        /// <param name="exception">错误异常</param>
        /// <param name="format">格式化字符串，如：服务器错误{0}......</param>
        /// <param name="args">格式化字符参数值</param>
        void Log(LogLevel level, Exception exception, string format, params object[] args);
    }
}
