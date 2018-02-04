/* *******************************************************
 * SharpSword zhangliang4629@sharpsword.com.cn 12/6/2016 11:06:12 AM
 * ****************************************************************/
using System;

namespace SharpSword.Timing
{
    /// <summary>
    /// 提供一个基于框架的标准时钟接口
    /// </summary>
    public interface IClockProvider
    {
        /// <summary>
        /// 获取当前时间
        /// </summary>
        DateTime Now { get; }

        /// <summary>
        /// 将事件转换成当前指定的时区时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        DateTime Normalize(DateTime dateTime);
    }
}
