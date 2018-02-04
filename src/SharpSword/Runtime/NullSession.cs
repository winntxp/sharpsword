/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/27/2016 4:27:49 PM
 * ****************************************************************/

namespace SharpSword.Runtime
{
    /// <summary>
    /// 当前用户的空实现
    /// </summary>
    public class NullSession : SessionBase
    {
        /// <summary>
        /// Singleton instance.
        /// </summary>
        public static NullSession Instance { get { return SingletonInstance; } }

        /// <summary>
        /// 
        /// </summary>
        private static readonly NullSession SingletonInstance = new NullSession();

        /// <summary>
        /// 
        /// </summary>
        public override string UserId
        {
            get { return null; }
        }

        /// <summary>
        /// 
        /// </summary>
        public override string UserName
        {
            get { return null; }
        }
    }
}
