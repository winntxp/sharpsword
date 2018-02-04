/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/
using System;

namespace SharpSword.Pay.AliPay
{
    /// <summary>
    /// 支付宝支付参数
    /// </summary>
    [Serializable]
    public class AlipayConfig : PayConfigBase
    {
        /// <summary>
        /// 支付宝商家APP编号
        /// </summary>
        public string Partner { get; set; }

        /// <summary>
        /// 数据签名KEY
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 卖家支付宝EMAIL账户
        /// </summary>
        public string SellerEmail { get; set; }
    }
}
