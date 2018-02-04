/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/27/2016 1:59:39 PM
 * *******************************************************/
using SharpSword.Timing;
using System;

namespace SharpSword.GuidGenerator.Impl
{
    /// <summary>
    /// 创建一个连续的GUID
    /// </summary>
    public class SequentialGuidGenerator : IGuidGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        public static SequentialGuidGenerator Instance { get { return _instance; } }

        /// <summary>
        /// 
        /// </summary>
        private static readonly SequentialGuidGenerator _instance = new SequentialGuidGenerator();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Guid Create()
        {
            var guidArray = Guid.NewGuid().ToByteArray();

            var baseDate = new DateTime(1900, 1, 1);
            var now = Clock.Now;

            // Get the days and milliseconds which will be used to build the byte string 
            var days = new TimeSpan(now.Ticks - baseDate.Ticks);
            var msecs = now.TimeOfDay;

            // Convert to a byte array 
            // Note that SQL Server is accurate to 1/300th of a millisecond so we divide by 3.333333 
            var daysArray = BitConverter.GetBytes(days.Days);
            var msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            // Reverse the bytes to match SQL Servers ordering 
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            // Copy the bytes into the guid 
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);

            return new Guid(guidArray);
        }
    }
}
