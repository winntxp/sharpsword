/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/9/2016 4:39:25 PM
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 
    /// </summary>
    public static class DayOfWeekExtensions
    {
        /// <summary>
        /// 当前是否是周六，周天
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static bool IsWeekend(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek.In(DayOfWeek.Saturday, DayOfWeek.Sunday);
        }

        /// <summary>
        /// 是否是工作天（星期一到星期五）
        /// </summary>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        public static bool IsWeekday(this DayOfWeek dayOfWeek)
        {
            return dayOfWeek.In(DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday);
        }
    }
}
