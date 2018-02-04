/* ****************************************************************
 * SharpSword zhangliang4629@163.com 11/29/2016 1:59:56 PM
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 分页入参，并且需要带用户信息
    /// </summary>
    [Serializable]
    public abstract class PageListRequestDtoBaseWithUserAndPrimaryKey<TPrimaryKey> : PageListRequestDtoBaseWithUser, IRequiredPrimaryKey<TPrimaryKey>
    {
        /// <summary>
        /// 查询主键信息
        /// </summary>
        public TPrimaryKey Id { get; set; }
    }
}
