/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/24/2017 2:59:31 PM
 * ****************************************************************/
using SharpSword.O2O.Services;
using SharpSword.O2O.Services.Domain;
using SharpSword.WebApi;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpSword.O2O.Apis
{
    /// <summary>
    /// 订单创建接口
    /// </summary>
    [ActionName("Order.Get"), ResponseFormat(ResponseFormat.JSON | ResponseFormat.XML)]
    [Description("获取订单明细")]
    public class OrderGet : ActionBase<OrderGet.OrderGetRequest, OrderDto>
    {
        /// <summary>
        /// 上送参数对象
        /// </summary>
        public class OrderGetRequest : RequestDtoBase
        {
            /// <summary>
            /// 订单编号
            /// </summary>
            [Required, MaxLength(50)]
            public string OrderId { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly IOrderServices _orderServices;

        /// <summary>
        /// ctor
        /// </summary>
        public OrderGet(IOrderServices orderServices)
        {
            this._orderServices = orderServices;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<OrderDto> Execute()
        {
            var result = this._orderServices.GetOrder(this.RequestDto.OrderId);
            return this.SuccessActionResult(result);
        }
    }
}
