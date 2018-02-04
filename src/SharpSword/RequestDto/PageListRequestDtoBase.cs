/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 03/07/2016 14:48:17 PM
 * ****************************************************************/
using System;
using System.ComponentModel.DataAnnotations;

namespace SharpSword
{
    /// <summary>
    /// 列表页请求参数基类，基于列表页的请求DTO，请继承此基类（非强制性）
    /// 此分页请求基类，默认上送的PageSize=10,PageIndex=1，在实现类里可以
    /// 重写BeforeValid()方法来更改框架默认设置的值
    /// </summary>
    [Serializable]
    public abstract class PageListRequestDtoBase : RequestDtoBase, IPageListRequestDto
    {
        /// <summary>
        /// 页码
        /// </summary>
        [GreaterThanOrEqual(1)]
        public int PageIndex { get; set; }

        /// <summary>
        /// 页容量(从框架级别来限制下，防止正式环境里请求过大数据)
        /// </summary>
        [Range(1, 90000)]
        public int PageSize { get; set; }
    }
}
