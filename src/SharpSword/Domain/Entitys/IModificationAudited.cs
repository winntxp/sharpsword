/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/6/2016 9:49:34 AM
 * ****************************************************************/

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModificationAudited : IHasModificationTime
    {
        /// <summary>
        /// 修改用户编号
        /// </summary>
        string LastModifyUserId { get; set; }

        /// <summary>
        /// 修改用户名称
        /// </summary>
        string LastModifyUserName { get; set; }
    }
}
