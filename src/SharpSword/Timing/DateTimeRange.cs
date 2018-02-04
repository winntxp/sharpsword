/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/6/2016 11:13:39 AM
 * ****************************************************************/
using System;

namespace SharpSword.Timing
{
    /// <summary>
    /// 2个时间段操作类
    /// </summary>
    [Serializable]
    public sealed class DateTimeRange : IDateTimeRange
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }

        /// <summary>
        /// 当前时间
        /// </summary>
        private static DateTime Now { get { return Clock.Now; } }

        /// <summary>
        /// 
        /// </summary>
        public DateTimeRange() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startTime">开始时间</param>
        public DateTimeRange(DateTime startTime)
        {
            this.StartTime = startTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        public DateTimeRange(DateTime startTime, DateTime endTime)
        {
            this.StartTime = startTime;
            this.EndTime = endTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTimeRange"></param>
        public DateTimeRange(IDateTimeRange dateTimeRange)
        {
            this.StartTime = dateTimeRange.StartTime;
            this.EndTime = dateTimeRange.EndTime;
        }

        /// <summary>
        /// 昨天时间段
        /// </summary>
        public static DateTimeRange Yesterday
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.Date.AddDays(-1), now.Date.AddMilliseconds(-1));
            }
        }

        /// <summary>
        /// 今天时间段
        /// </summary>
        public static DateTimeRange Today
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.Date, now.Date.AddDays(1).AddMilliseconds(-1));
            }
        }

        /// <summary>
        /// 明天时间段
        /// </summary>
        public static DateTimeRange Tomorrow
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.Date.AddDays(1), now.Date.AddDays(2).AddMilliseconds(-1));
            }
        }

        /// <summary>
        /// 上一个月时间段
        /// </summary>
        public static DateTimeRange LastMonth
        {
            get
            {
                var now = Now;
                var startTime = now.Date.AddDays(-now.Day + 1).AddMonths(-1);
                var endTime = startTime.AddMonths(1).AddMilliseconds(-1);
                return new DateTimeRange(startTime, endTime);
            }
        }

        /// <summary>
        /// 当前月时间段
        /// </summary>
        public static DateTimeRange ThisMonth
        {
            get
            {
                var now = Now;
                var startTime = now.Date.AddDays(-now.Day + 1);
                var endTime = startTime.AddMonths(1).AddMilliseconds(-1);
                return new DateTimeRange(startTime, endTime);
            }
        }

        /// <summary>
        /// 下一个月时间段
        /// </summary>
        public static DateTimeRange NextMonth
        {
            get
            {
                var now = Now;
                var startTime = now.Date.AddDays(-now.Day + 1).AddMonths(1);
                var endTime = startTime.AddMonths(1).AddMilliseconds(-1);
                return new DateTimeRange(startTime, endTime);
            }
        }

        /// <summary>
        /// 上一年时间段
        /// </summary>
        public static DateTimeRange LastYear
        {
            get
            {
                var now = Now;
                return new DateTimeRange(new DateTime(now.Year - 1, 1, 1), new DateTime(now.Year, 1, 1).AddMilliseconds(-1));
            }
        }

        /// <summary>
        /// 当前年时间段
        /// </summary>
        public static DateTimeRange ThisYear
        {
            get
            {
                var now = Now;
                return new DateTimeRange(new DateTime(now.Year, 1, 1), new DateTime(now.Year + 1, 1, 1).AddMilliseconds(-1));
            }
        }

        /// <summary>
        /// 下一年时间段
        /// </summary>
        public static DateTimeRange NextYear
        {
            get
            {
                var now = Now;
                return new DateTimeRange(new DateTime(now.Year + 1, 1, 1), new DateTime(now.Year + 2, 1, 1).AddMilliseconds(-1));
            }
        }

        /// <summary>
        /// 最近30天内时间段（含今天）
        /// </summary>
        public static DateTimeRange Last30Days
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.AddDays(-30), now);
            }
        }

        /// <summary>
        /// 最近30天内时间段，不包含今天
        /// </summary>
        public static DateTimeRange Last30DaysExceptToday
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.Date.AddDays(-30), now.Date.AddMilliseconds(-1));
            }
        }

        /// <summary>
        /// 最近7天内时间段，含今天
        /// </summary>
        public static DateTimeRange Last7Days
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.AddDays(-7), now);
            }
        }

        /// <summary>
        /// 最近7天内时间段，不包含今天
        /// </summary>
        public static DateTimeRange Last7DaysExceptToday
        {
            get
            {
                var now = Now;
                return new DateTimeRange(now.Date.AddDays(-7), now.Date.AddMilliseconds(-1));
            }
        }

        /// <summary>
        /// 最近N天的时间区间，包含当前时间
        /// </summary>
        /// <param name="days">最近多少天</param>
        /// <returns></returns>
        public static DateTimeRange LastDays(int days)
        {
            var now = Now;
            return new DateTimeRange(now.AddDays(-days), now);
        }

        /// <summary>
        /// 最近N天的时间区间，排除今天
        /// </summary>
        /// <param name="days">最近多少天</param>
        /// <returns></returns>
        public static DateTimeRange LastDaysExceptToday(int days)
        {
            var now = Now;
            return new DateTimeRange(now.Date.AddDays(-days), now.Date.AddMilliseconds(-1));
        }

        /// <summary>
        /// 重写了ToString()，返回当前时间段的，开始，结束字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return string.Format("[{0} - {1}]", StartTime, EndTime);
        }
    }
}
