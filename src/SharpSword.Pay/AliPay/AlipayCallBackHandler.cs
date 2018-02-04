/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/

namespace SharpSword.Pay.AliPay
{
    /// <summary>
    /// 支付宝回调数据处理
    /// </summary>
    public class AlipayCallBackHandler : PayedCallBackHandlerBase<AlipayConfig>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public AlipayCallBackHandler(AlipayConfig config) : base(config) { }

        /// <summary>
        /// 验证第三方平台POST过来的支付参数
        /// </summary>
        /// <param name="context"></param>
        protected override VerifyDataResult VerifyData(PayCallBackContext context)
        {
            return VerifyDataResult.OK;
        }

        /// <summary>
        /// 获取支付成功后，反馈给第三方平台的成功消息
        /// </summary>
        /// <param name="context"></param>
        /// <param name="trade"></param>
        /// <returns></returns>
        protected override string GetSuccessFeedBackMessage(PayCallBackContext context, Trade trade)
        {
            return "OK";
        }

        /// <summary>
        /// 根据POST过来的数据抽象出交易信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected override Trade GetTrade(PayCallBackContext context)
        {
            string tradeId = context.Arguments["out_trade_no"];
            var trade = new Trade(tradeId);
            trade.TotleFee = context.Arguments["total_fee"].As<decimal>();
            trade.OuterTradeId = context.Arguments["trade_no"];
            return trade;
        }

    }
}
