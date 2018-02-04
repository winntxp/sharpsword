/* *******************************************************
 * SharpSword zhangliang4629@163.com 11/29/2016 1:59:56 PM
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 分页入参，并且需要带用户信息
    /// </summary>
    [Serializable]
    public abstract class PageListRequestDtoBaseWithUser : PageListRequestDtoBase, IRequiredUser
    {
        /// <summary>
        /// 用户编号
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 用户名称
        /// </summary>
        public string UserName { get; set; }
    }
}
