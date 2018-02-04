/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/
using SharpSword.Domain.Entitys;
using System;

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Order : Entity
    {
        ///<summary>
        /// 订单编号
        ///</summary>
        public string OrderId { get; set; }

        ///<summary>
        /// 订单状态（1、待付款，2、待提货/已付款，3、交易完成/已提货，4、交易关闭）
        ///</summary>
        public int OrderStatus { get; set; }

        ///<summary>
        /// 用户编号
        ///</summary>
        public long UserId { get; set; }

        ///<summary>
        /// 用户名称
        ///</summary>
        public string UserName { get; set; }

        ///<summary>
        /// 微信昵称
        ///</summary>
        public string WechatNickname { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string WechatImage { get; set; }

        ///<summary>
        /// 收货人
        ///</summary>
        public string ShipTo { get; set; }

        ///<summary>
        /// 手机号码
        ///</summary>
        public string CellPhone { get; set; }

        ///<summary>
        /// 地址
        ///</summary>
        public string Address { get; set; }

        ///<summary>
        /// 配送区域ID（OperationArea.OperationAreaID）
        ///</summary>
        public int? OperationAreaId { get; set; }

        ///<summary>
        /// 区域名称
        ///</summary>
        public string OperationAreaName { get; set; }

        ///<summary>
        /// 总提成
        ///</summary>
        public decimal? TotalCommission { get; set; }

        ///<summary>
        /// 订单商品金额
        ///</summary>
        public decimal? Amount { get; set; }

        ///<summary>
        /// 订单总价（最终支付价格）
        ///</summary>
        public decimal? OrderTotal { get; set; }

        ///<summary>
        /// IP地址
        ///</summary>
        public string Ip { get; set; }

        ///<summary>
        /// 下单时间
        ///</summary>
        public DateTime OrderDate { get; set; }

        ///<summary>
        /// 付款时间
        ///</summary>
        public DateTime? PayDate { get; set; }

        ///<summary>
        /// 提货时间
        ///</summary>
        public DateTime? DeliveryTime { get; set; }

        ///<summary>
        /// 完成时间
        ///</summary>
        public DateTime? FinishDate { get; set; }

        ///<summary>
        /// 门店编号
        ///</summary>
        public int? SupplierId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string SupplierNo { get; set; }

        ///<summary>
        /// 门店名称
        ///</summary>
        public string SupplierName { get; set; }

        ///<summary>
        /// 是否显示（1、显示，0、不显示），会员端是否显示此订单，会员删除订单时将此值修改成0
        ///</summary>
        public bool? IsShow { get; set; }

        ///<summary>
        /// 客户端来源(1、android；2、IOS；3、微信；0、未知)
        ///</summary>
        public int? ClientType { get; set; }

        ///<summary>
        /// 支付方式(1、货到付款；2、线上支付)
        ///</summary>
        public int? PayType { get; set; }

        ///<summary>
        /// 支付完成标识(1、未支付完成；2、已支付完成； 3、支付中； 4、已付定金)
        ///</summary>
        public int? IsHasPay { get; set; }

        ///<summary>
        /// 支付方式名称
        ///</summary>
        public string PaymentType { get; set; }

        ///<summary>
        /// 是否为预售订单（1、是，0、否）
        ///</summary>
        public int IsPresale { get; set; }

        ///<summary>
        /// 预售活动ID号
        ///</summary>
        public long? PresaleActivityId { get; set; }

        ///<summary>
        /// 备注
        ///</summary>
        public string Remark { get; set; }

        ///<summary>
        /// 路线ID
        ///</summary>
        public int? LineId { get; set; }

        ///<summary>
        /// 路线名称
        ///</summary>
        public string LineName { get; set; }

        ///<summary>
        /// 配送员ID
        ///</summary>
        public int? DistributionClerkId { get; set; }

        ///<summary>
        /// 配送员姓名
        ///</summary>
        public string DistributionClerkName { get; set; }

        ///<summary>
        /// 代客下单（1、是，0、否）
        ///</summary>
        public int? IsValetOrder { get; set; }

        ///<summary>
        /// 会员是否显示此订单
        ///</summary>
        public int? MemberIsShow { get; set; }

        ///<summary>
        /// 门店是否显示此订单
        ///</summary>
        public int? SupplierIsShow { get; set; }

        ///<summary>
        /// 提货单号
        ///</summary>
        public int? BillOfLading { get; set; }

        ///<summary>
        /// 公司总提成
        ///</summary>
        public decimal? CompanyCommission { get; set; }

        ///<summary>
        /// 线路顺序（V2.2+）
        ///</summary>
        public int? LineSort { get; set; } // LineSort

        /// <summary>
        /// 
        /// </summary>
        public Order()
        {
            IsShow = true;
            ClientType = 3;
            IsPresale = 1;
            IsValetOrder = 0;
            MemberIsShow = 1;
            SupplierIsShow = 1;
        }
    }

}
