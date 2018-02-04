/* ****************************************************************
 * SharpSword zhangliang4629@163.com 12/6/2016 11:06:48 AM
 * ****************************************************************/
using System;

namespace SharpSword.Timing
{
    /// <summary>
    /// 给系统框架提供一个可扩展的时钟提供者（分布式的时候非常重要）
    /// </summary>
    public static class Clock
    {
        /// <summary>
        /// 
        /// </summary>
        private static IClockProvider _provider;

        /// <summary>
        /// 给初始化系统的时候，有机会修改时钟提供者
        /// </summary>
        public static IClockProvider Provider
        {
            get { return _provider; }
            set
            {
                if (value.IsNull())
                {
                    throw new SharpSwordCoreException("时钟提供者不能为null");
                }
                _provider = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        static Clock()
        {
            Provider = new LocalClockProvider();
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        public static DateTime Now
        {
            get { return Provider.Now; }
        }

        /// <summary>
        /// 将时间转换成本地时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime Normalize(DateTime dateTime)
        {
            return Provider.Normalize(dateTime);
        }
    }
}
