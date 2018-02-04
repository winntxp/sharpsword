/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/28/2017 3:30:17 PM
 * ****************************************************************/
using SharpSword.O2O.Services;
using SharpSword.WebApi;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SharpSword.O2O.Apis
{
    /// <summary>
    /// 订单创建进度查询接口
    /// </summary>
    [ActionName("Order.Create.Progress"), ResponseFormat(ResponseFormat.JSON | ResponseFormat.XML)]
    [Description("订单创建进度查询接口")]
    public class OrderCreateProgress : ActionBase<OrderCreateProgress.OrderCreateProgressRequest, OrderProgress>
    {
        /// <summary>
        /// 上送参数对象
        /// </summary>
        public class OrderCreateProgressRequest : RequestDtoBase
        {
            /// <summary>
            /// 创建订单获取到的票据
            /// </summary>
            [Required, MaxLength(100)]
            public string Token { get; set; }

            /// <summary>
            /// 自定义校验上送参数
            /// </summary>
            /// <returns></returns>
            public override IEnumerable<DtoValidatorResultError> Valid()
            {
                return base.Valid();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly IOrderSequenceServices _orderSequenceServices;

        /// <summary>
        /// ctor
        /// </summary>
        public OrderCreateProgress(IOrderSequenceServices orderSequenceServices)
        {
            this._orderSequenceServices = orderSequenceServices;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<OrderProgress> Execute()
        {
            var orderProgress = this._orderSequenceServices.GetOrderProgress(this.RequestDto.Token);
            return this.SuccessActionResult(orderProgress);
        }
    }
}
