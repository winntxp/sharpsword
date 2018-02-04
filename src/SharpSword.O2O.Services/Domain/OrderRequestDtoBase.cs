/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 11:31:23 AM
 * ****************************************************************/
using System.ComponentModel.DataAnnotations;

namespace SharpSword.O2O.Services.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class OrderRequestDtoBase : RequestDtoBase
    {
        /// <summary>
        /// 订单编号
        /// </summary>
        [Required, MaxLength(50)]
        public string OrderId { get; set; }
    }
}