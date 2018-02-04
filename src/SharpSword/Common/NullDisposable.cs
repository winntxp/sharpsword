/* ****************************************************************
 * SharpSword zhangliang4629@163.com 10/3/2016 4:51:20 PM
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class NullDisposable : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        public static NullDisposable Instance
        {
            get { return SingletonInstance; }
        }

        /// <summary>
        /// 
        /// </summary>
        private static readonly NullDisposable SingletonInstance = new NullDisposable();

        /// <summary>
        /// 
        /// </summary>
        private NullDisposable() { }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose() { }
    }
}
