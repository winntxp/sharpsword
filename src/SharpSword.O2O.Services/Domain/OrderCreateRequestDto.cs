/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/24/2017 1:08:41 PM
 * ****************************************************************/
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SharpSword.O2O.Services.Domain
{
    /// <summary>
    /// 创建订单入参
    /// </summary>
    public class OrderCreateRequestDto : RequestDtoBase
    {
        /// <summary>
        /// 会员ID
        /// </summary>
        [Required]
        public string UserId { get; set; }

        /// <summary>
        /// 会员名称
        /// </summary>
        [MaxLength(50)]
        public string UserName { get; set; }

        /// <summary>
        /// 微信昵称
        /// </summary>
        [MaxLength(50)]
        public string WechatNickname { get; set; }

        /// <summary>
        /// 微信头像
        /// </summary>
        public string WechatImage { get; set; }

        /// <summary>
        /// 门店编号，提货门店编号
        /// </summary>
        public int StoreId { get; set; }

        /// <summary>
        /// 订单总价（最终支付价格）
        /// </summary>
        public decimal PayMent { get; set; }

        /// <summary>
        /// 是否代客下单
        /// </summary>
        public int? IsValetOrder { get; set; }

        /// <summary>
        /// 收货人(也有可能是待客下单，客户姓名)
        /// </summary>
        [MaxLength(50)]
        public string ReceiverName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string ReceiverPhone { get; set; }

        /// <summary>
        /// 订单详情
        /// </summary>
        public List<OrderCreateDetail> Details { get; set; }

        /// <summary>
        /// 自定义校验
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<DtoValidatorResultError> Valid()
        {
            var dtoValidatorResultErrors = base.Valid().ToList();

            //订单明细
            if (this.Details.IsNull() || this.Details.Count == 0)
            {
                dtoValidatorResultErrors.Add(new DtoValidatorResultError("Details", "商品信息不能为空"));
                return dtoValidatorResultErrors;
            }

            foreach (var item in this.Details)
            {
                if (item.Quantity <= 0)
                {
                    dtoValidatorResultErrors.Add(new DtoValidatorResultError("Details.Quantity", "商品数量必须大于0"));
                    break;
                }
            }

            //检测用户ID是否正确
            if (!this.UserId.Is<long>())
            {
                dtoValidatorResultErrors.Add(new DtoValidatorResultError("UserId", "用户编号必须为整数"));
            }

            return dtoValidatorResultErrors;
        }
    }

}
