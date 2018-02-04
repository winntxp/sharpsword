/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/29/2016 1:59:56 PM
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 分页入参，并且需要带用户信息和排序信息
    /// </summary>
    [Serializable]
    public abstract class PageListRequestDtoBaseWithUserAndSortBy<TSort> : PageListRequestDtoBaseWithUser, ISortedRequestDto<TSort>
    {
        /// <summary>
        /// 排序字段
        /// </summary>
        public TSort SortBy { get; set; }

    }
}
