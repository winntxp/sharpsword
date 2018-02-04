/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/24 9:31:54
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// RequestDto如果继承了此接口，那么就要校验请求包Data里是否包含了userId和UserName参数，
    /// 系统框架在自动校验的时候，会进行用户编号和用户名称的校验(系统框架给予此接口特殊待遇)
    /// </summary>
    public interface IRequiredUser
    {
        /// <summary>
        /// 当前操作用户ID
        /// </summary>
        string UserId { get; set; }

        /// <summary>
        /// 当前操作用户名称
        /// </summary>
        string UserName { get; set; }
    }
}
