/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/20 8:55:42
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 当前调用接口的用户信息
    /// </summary>
    public class UserIdentity
    {
        /// <summary>
        /// 
        /// </summary>
        public UserIdentity() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userName"></param>
        public UserIdentity(string userId, string userName)
        {
            this.UserId = userId;
            this.UserName = userName;
        }

        /// <summary>
        /// 当前请求操作用户ID
        /// </summary>
        public string UserId { get; private set; }

        /// <summary>
        /// 当前请求操作用户名称
        /// </summary>
        public string UserName { get; private set; }
    }
}
