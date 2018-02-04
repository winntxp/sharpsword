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
    public abstract class PageListRequestDtoBaseWithUserAndSearchKey : PageListRequestDtoBaseWithUser, ISearchRequestDto
    {
        /// <summary>
        /// 查询关键词
        /// </summary>
        public string Q { get; set; }
    }
}
