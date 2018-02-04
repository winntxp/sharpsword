/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 7/6/2016 4:05:37 PM
 * ****************************************************************/

namespace SharpSword.SDK
{
    /// <summary>
    /// 空实现，我们什么都不做
    /// </summary>
    internal class NullApiLogger : IApiClientLogger
    {
        /// <summary>
        /// 
        /// </summary>
        private static NullApiLogger _instance = new NullApiLogger();

        /// <summary>
        /// 
        /// </summary>
        public static IApiClientLogger Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private NullApiLogger() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Error(string message)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Info(string message)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public void Warn(string message)
        {

        }
    }
}
