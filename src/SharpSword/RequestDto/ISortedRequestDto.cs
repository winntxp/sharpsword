/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/18/2016 2:03:20 PM
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// 用于排序参数
    /// 现实情况下，最好少用直接字符串进行传入，我们可以使用枚举或者数字来代码排序类型
    /// 这样防止业务开发的时候，开发人员不小心直接拼凑SQL语句等待，给程序带来不安全隐患
    /// </summary>
    public interface ISortedRequestDto<TSort>
    {
        /// <summary>
        /// Examples:
        /// "Name"
        /// "Name DESC"
        /// "Name ASC, Age DESC"
        /// 或者按照数字，比如：1或者N
        /// </summary>
        TSort SortBy { get; set; }
    }
}
