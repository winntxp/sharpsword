/* *******************************************************
 * SharpSword zhangliang4629@163.com 10/18/2016 9:59:23 AM
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// 分页模糊查询关键词
    /// </summary>
    public interface ISearchRequestDto
    {
        /// <summary>
        /// 查询关键词
        /// </summary>
        string Q { get; set; }
    }
}
