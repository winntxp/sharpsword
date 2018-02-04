/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 11:31:23 AM
 * ****************************************************************/
using SharpSword.O2O.Data.Entities;
using System.Collections.Generic;

namespace SharpSword.O2O.Services.Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class OrderDto : Order
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<OrderItemDto> OrderItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IList<OrderTrack> OrderTracks { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OrderDto()
        {
            this.OrderItems = new List<OrderItemDto>();
            this.OrderTracks = new List<OrderTrack>();
        }
    }
}
