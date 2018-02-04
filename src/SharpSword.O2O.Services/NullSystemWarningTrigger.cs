/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/14/2017 4:31:08 PM
 * ****************************************************************/
using System;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 我们实现下空实现异常报警器
    /// </summary>
    public class NullSystemWarningTrigger : ISystemWarningTrigger
    {
        /// <summary>
        /// 
        /// </summary>
        private static ISystemWarningTrigger _instance = new NullSystemWarningTrigger();

        /// <summary>
        /// 
        /// </summary>
        public static ISystemWarningTrigger Instance => _instance;

        /// <summary>
        /// 
        /// </summary>
        private NullSystemWarningTrigger() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="waningMessage"></param>
        /// <param name="exception"></param>
        public void Warning(object source, string waningMessage, Exception exception = null) { }
    }
}
