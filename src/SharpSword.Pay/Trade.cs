/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword.Pay
{
    /// <summary>
    /// 根据第三方支付平台POST过来的信息解析出通用的交易信息
    /// </summary>
    [Serializable]
    public class Trade : IEqualityComparer<Trade>, IEquatable<Trade>
    {
        /// <summary>
        /// 
        /// </summary>
        private IDictionary<string, object> _properties;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tradeId">本地交易号</param>
        public Trade(string tradeId)
        {
            tradeId.CheckNullThrowArgumentNullException(nameof(tradeId));
            this.TradeId = tradeId;
            this._properties = new Dictionary<string, object>();
        }

        /// <summary>
        /// 本地交易编号
        /// </summary>
        public string TradeId { get; private set; }

        /// <summary>
        /// 返回的支付总金额(单位为:分)，一般情况下，第三方平台会回送实际支付的金额，我们再程序里最好对支付金额再次判断，防止支付异常以及安全威胁
        /// </summary>
        public decimal? TotleFee { get; set; }

        /// <summary>
        /// 支付平台的交易编号，即：支付成功后，在第三方平台保存的第三方交易编号（或者支付编号）
        /// </summary>
        public string OuterTradeId { get; set; }

        /// <summary>
        /// 用于保存支付平台回送的其他一些信息
        /// </summary>
        public IDictionary<string, object> Properties => this._properties;

        /// <summary>
        /// <![CDATA[实现IEqualityComparer<Trade>.Equals]]>
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(Trade x, Trade y)
        {
            if (Object.ReferenceEquals(x, y))
            {
                return true;
            }
            if (Object.ReferenceEquals(x, null) || object.ReferenceEquals(y, null))
            {
                return false;
            }
            return x.TradeId == y.TradeId;
        }

        /// <summary>
        /// <![CDATA[实现IEqualityComparer<Trade>.GetHashCode]]>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode(Trade obj)
        {
            if (object.ReferenceEquals(obj, null))
            {
                return 0;
            }
            return obj.TradeId.GetHashCode();
        }

        /// <summary>
        /// 判断2个Trade是否相等，我们仅仅判断TradeId是否相等
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Trade other)
        {
            return null != other && this.TradeId == other.TradeId;
        }
    }
}
