/* ****************************************************************
 * SharpSword zhangliang4629@sharpsword.com.cn 12/6/2016 9:48:36 AM
 * ****************************************************************/

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 创建用户审计
    /// </summary>
    public interface ICreationAudited : IHasCreationTime
    {
        /// <summary>
        /// 创建用户编号
        /// </summary>
        string CreatorUserId { get; set; }

        /// <summary>
        /// 创建用户名称
        /// </summary>
        string CreatorUserName { get; set; }
    }
}
