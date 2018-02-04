/* *******************************************************
 * SharpSword zhangliang4629@163.com 11/29/2016 1:59:56 PM
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 分页入参，并且带查询关键词
    /// </summary>
    [Serializable]
    public abstract class PageListRequestDtoBaseWithSearch : PageListRequestDtoBase, ISearchRequestDto
    {
        /// <summary>
        /// 查询关键词
        /// </summary>
        public string Q { get; set; }

    }
}
