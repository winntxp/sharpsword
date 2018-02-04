/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:16:33 PM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 订单处理进度
    /// </summary>
    public class OrderProgress
    {
        /// <summary>
        /// 当前订单创建状态：排队中(Queuing) , 创建成功(OrderCreateSuccess)/创建失败(OrderCreateFail) 
        /// 其实这里的status设置set属性应该定义成private但是考虑到需要序列化和反序列化，我们定义为公开类型
        /// </summary>
        public OrderProgressStatus Status { get; set; }

        /// <summary>
        /// 排队中.会返回此值
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 如果订单状态正在：排队中会返回此字段
        /// </summary>
        public long? Rank { get; set; }

        /// <summary>
        /// 如果创建成果状态，将会返回订单ID信息(排队中和创建失败此字段不会返回)
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 状态描述（如：排队中...,创建订单成功，创建订单失败，返回错误消息：比如：库存不足）
        /// </summary>
        public string Description { get; set; }
    }
}