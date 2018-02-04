/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/21/2017 3:13:48 PM
 * ****************************************************************/
using SharpSword.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SharpSword.O2O.Services.Apis
{
    /// <summary>
    /// 
    /// </summary>
    [ActionName("Order.Delete"), ResponseFormat(ResponseFormat.JSON | ResponseFormat.XML)]
    [Description("删除订单")]
    public class OrderDelete : ActionBase<OrderDelete.OrderDeleteRequestDto, OrderDelete.OrderDeleteResponseDto>
    {
        /// <summary>
        /// 上送参数对象
        /// </summary>
        public class OrderDeleteRequestDto : RequestDtoBase
        {
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
        /// 下送数据对象
        /// </summary>
        public class OrderDeleteResponseDto : ResponseDtoBase
        {

        }

        /// <summary>
        /// ctor
        /// </summary>
        public OrderDelete()
        {

        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<OrderDeleteResponseDto> Execute()
        {
            throw new NotImplementedException();
        }

    }
}
