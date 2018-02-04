/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/2/17 15:41:12
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword
{
    /// <summary>
    /// 需要分页输出的地方（此类为建议类，实际使用中可以依据情况使用）
    /// 此类没有定义成泛型类是让在数据组装数据的时候方便
    /// </summary>
    [Serializable]
    public class PagerDataDto<T> : ResponseDtoBase
    {
        /// <summary>
        /// 页容量
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 总记录数
        /// </summary>
        public int TotalRecords { get; set; }

        /// <summary>
        /// 输出的集合数据，此处对象必须为一个集合类型的对象，比如：数组,列表
        /// </summary>
        public IEnumerable<T> Items { get; set; }
    }
}
