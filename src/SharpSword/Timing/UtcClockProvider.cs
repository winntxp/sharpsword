/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/6/2016 11:12:29 AM
 * ****************************************************************/
using System;

namespace SharpSword.Timing
{
    /// <summary>
    /// 
    /// </summary>
    public class UtcClockProvider : IClockProvider
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime Now
        {
            get { return DateTime.UtcNow; }
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
                return DateTime.SpecifyKind(dateTime, DateTimeKind.Utc);
            }

            if (dateTime.Kind == DateTimeKind.Local)
            {
                return dateTime.ToUniversalTime();
            }

            return dateTime;
        }
    }
}
