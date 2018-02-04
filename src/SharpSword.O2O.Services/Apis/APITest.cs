/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/16/2017 9:42:44 AM
 * ****************************************************************/
using System.Collections.Generic;
using System.ComponentModel;
using SharpSword.WebApi;

namespace SharpSword.O2O.Services.Apis
{
    /// <summary>
    /// 
    /// </summary>
    [ActionName("APITest"), ResponseFormat(ResponseFormat.JSON | ResponseFormat.XML), AllowAnonymous]
    [Description("APITest")]
    public class APITest : ActionBase<APITest.APITestRequestDto, object>
    {
        /// <summary>
        /// 上送参数对象
        /// </summary>
        public class APITestRequestDto : RequestDtoBase
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
        public class APITestResponseDto : ResponseDtoBase
        {

        }

        /// <summary>
        /// 
        /// </summary>
        private readonly IOrderMaxOrderSequenceServices _orderMaxOrderIdServices;

        /// <summary>
        /// ctor
        /// </summary>
        public APITest(IOrderMaxOrderSequenceServices orderMaxOrderIdServices)
        {
            this._orderMaxOrderIdServices = orderMaxOrderIdServices;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<object> Execute()
        {

            var x = this._orderMaxOrderIdServices.GetMaxOrderId();

            return this.SuccessActionResult(x);
        }

    }
}
