/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 14:47:19
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.Domain.Entitys
{
    /// <summary>
    /// 查询分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable]
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index start 0</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount">为了查询效率，第一次查询后，将总记录数保存到url参数，下次的时候无需再次查询统计总计</param>
        public PagedList(IQueryable<T> source, int pageIndex, int pageSize, int? totalCount = null)
        {
            int total = totalCount.HasValue && totalCount.Value > 0 ? totalCount.Value : source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;
            if (total % pageSize > 0)
            {
                TotalPages++;
            }
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="source">source</param>
        /// <param name="pageIndex">Page index start 0</param>
        /// <param name="pageSize">Page size</param>
        /// <param name="totalCount"></param>
        public PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int? totalCount)
        {
            TotalCount = totalCount.HasValue && totalCount.Value > 0 ? totalCount.Value : source.Count();
            TotalPages = TotalCount / pageSize;
            if (TotalCount % pageSize > 0)
            {
                TotalPages++;
            }
            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; private set; }

        /// <summary>
        /// 每页显示条数
        /// </summary>
        public int PageSize { get; private set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalCount { get; private set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; private set; }

        /// <summary>
        /// 是否有上一页
        /// </summary>
        public bool HasPreviousPage
        {
            get { return (PageIndex > 0); }
        }

        /// <summary>
        /// 是否有下一页
        /// </summary>
        public bool HasNextPage
        {
            get { return (PageIndex + 1 < TotalPages); }
        }
    }
}