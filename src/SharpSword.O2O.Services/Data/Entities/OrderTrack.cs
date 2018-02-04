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
    public class OrderTrack : Entity
    {

        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public int? AdminId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AdminName { get; set; }

        ///<summary>
        /// 用户类型（1、会员，2、门店超级管理员，3、门店管理员）
        ///</summary>
        public int? AdminRole { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TheName { get; set; }

        ///<summary>
        /// 订单状态（1、待付款，2、待提货/已付款，3、交易完成/已提货，4、交易关闭）
        ///</summary>
        public int? StateId { get; set; }

        ///<summary>
        /// 在用户端是否显示(0:不显示;1:显示)
        ///</summary>
        public int? IsDisplayUser { get; set; }

        ///<summary>
        /// 订单状态名称
        ///</summary>
        public string StateName { get; set; }
    }

}
