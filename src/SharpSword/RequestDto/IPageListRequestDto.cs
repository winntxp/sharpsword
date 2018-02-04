/* *******************************************************
 * SharpSword zhangliang4629@163.com 10/18/2016 9:59:23 AM
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// 获取分页请求参数
    /// </summary>
    public interface IPageListRequestDto
    {
        /// <summary>
        /// 页码
        /// </summary>
        int PageIndex { get; set; }

        /// <summary>
        /// 页容量
        /// </summary>
        int PageSize { get; set; }
    }
}
