/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/24/2017 2:59:31 PM
 * ****************************************************************/
using SharpSword.O2O.Services;
using SharpSword.O2O.Services.Domain;
using SharpSword.WebApi;
using System.ComponentModel;

namespace SharpSword.O2O.Apis
{
    /// <summary>
    /// 订单创建接口
    /// </summary>
    [ActionName("Order.Create"), ResponseFormat(ResponseFormat.JSON | ResponseFormat.XML)]
    [Description("订单创建接口")]
    public class OrderCreate : ActionBase<OrderCreate.OrderCreateRequest, SubmitOrderResult>
    {
        /// <summary>
        /// 上送参数对象
        /// </summary>
        public class OrderCreateRequest : OrderCreateRequestDto, IRequiredUser { }

        /// <summary>
        /// 
        /// </summary>
        private readonly IOrderSubmitServices _orderServices;

        /// <summary>
        /// ctor
        /// </summary>
        public OrderCreate(IOrderSubmitServices orderServices)
        {
            this._orderServices = orderServices;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<SubmitOrderResult> Execute()
        {
            var result = this._orderServices.SubmitOrder(this.RequestDto);
            return this.SuccessActionResult(result);
        }
    }
}
