/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/6/2016 11:13:18 AM
 * ****************************************************************/
using System;

namespace SharpSword.Timing
{
    /// <summary>
    /// 定义一个事件间隔
    /// </summary>
    public interface IDateTimeRange
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        DateTime EndTime { get; set; }
    }
}
