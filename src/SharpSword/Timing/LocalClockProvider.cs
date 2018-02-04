/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/6/2016 11:07:18 AM
 * ****************************************************************/
using System;

namespace SharpSword.Timing
{
    /// <summary>
    /// 系统框架默认本地时钟提供者
    /// </summary>
    public class LocalClockProvider : IClockProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime Now
        {
            get { return DateTime.Now; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public DateTime Normalize(DateTime dateTime)
        {
            if (dateTime.Kind == DateTimeKind.Unspecified)
            {
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
            }
            if (dateTime.Kind == DateTimeKind.Utc)
            {
                return dateTime.ToLocalTime();
            }
            return dateTime;
        }
    }
}
