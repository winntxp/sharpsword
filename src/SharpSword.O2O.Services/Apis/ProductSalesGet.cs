/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/28/2017 3:47:21 PM
 * ****************************************************************/
using SharpSword.O2O.Services;
using SharpSword.WebApi;
using System.Collections.Generic;
using System.ComponentModel;

namespace SharpSword.O2O.Apis
{
    /// <summary>
    /// 活动商品销量查询(我们将此接口做个全局缓存1分钟)
    /// </summary>
    [ActionName("Order.Product.Sales.Get"), ResponseFormat(ResponseFormat.JSON | ResponseFormat.XML)]
    [Description("活动商品销量查询")]
    public class ProductSalesGet : ActionBase<ProductSalesGet.ProductSalesGetRequest, ProductSalesGet.ProductSalesGetResponse>
    {
        /// <summary>
        /// 上送参数对象
        /// </summary>
        public class ProductSalesGetRequest : RequestDtoBase
        {
            /// <summary>
            /// 活动ID
            /// </summary>
            public long PresaleActivityId { get; set; }

            /// <summary>
            /// 商品ID
            /// </summary>
            public int ProductId { get; set; }

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
        /// 下送参数对象
        /// </summary>
        public class ProductSalesGetResponse
        {
            /// <summary>
            /// 已经销售量
            /// </summary>
            public long SaleQuantity { get; set; }

            /// <summary>
            /// 商品预售数量（限量）
            /// </summary>
            public long PresaleQuantity { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly IPresaleActivityServices _presaleServices;

        /// <summary>
        /// ctor
        /// </summary>
        public ProductSalesGet(IPresaleActivityServices presaleServices)
        {
            this._presaleServices = presaleServices;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<ProductSalesGetResponse> Execute()
        {
            //获取销售数量（从缓存里获取）
            var saleQuantity = this._presaleServices
                                            .GetPresaleProductSaleQuantity(this.RequestDto.PresaleActivityId,
                                                                           this.RequestDto.ProductId);

            //获取预售商品信息
            var presaleProduct = this._presaleServices
                                            .GetPresaleProduct(this.RequestDto.PresaleActivityId,
                                                               this.RequestDto.ProductId);

            if (presaleProduct.IsNull())
            {
                return this.ErrorActionResult("活动商品不存在");
            }

            //如果预售商品设置了限购，并且出现了超卖，我们前端还是现实限购数量
            if (presaleProduct.PresaleQuantity.HasValue && presaleProduct.PresaleQuantity > 0
                                                        && saleQuantity > presaleProduct.PresaleQuantity.Value)
            {
                saleQuantity = (long)presaleProduct.PresaleQuantity.Value;
            }

            //返回已经售出数量
            return this.SuccessActionResult(new ProductSalesGetResponse()
            {
                SaleQuantity = saleQuantity,
                PresaleQuantity = (long)(presaleProduct.PresaleQuantity ?? 0)
            });
        }
    }
}
