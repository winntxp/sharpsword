/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.Pay.AliPay
{
    /// <summary>
    /// 支付宝支付请求数据处理
    /// </summary>
    public class AlipayRequestHandler : PayRequestHandlerBase<AlipayConfig>
    {
        /// <summary>
        /// 
        /// </summary>
        public string OutTradeNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TotalFee { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Paymethod { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DefaultBank { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string AntiPhishingKey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExterInvokeIp { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExtraCommonParam { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string BuyerEmail { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RoyaltyType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string RoyaltyParameters { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ShowUrl { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public AlipayRequestHandler(AlipayConfig config) : base(config) { }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IDictionary<string, string> GetArguments()
        {
            SortedDictionary<string, string> Arguments = new SortedDictionary<string, string>();
            Arguments.Add("service", "create_direct_pay_by_user");
            Arguments.Add("payment_type", "1");
            Arguments.Add("partner", this.PayConfig.Partner);
            Arguments.Add("seller_email", this.PayConfig.SellerEmail);
            Arguments.Add("return_url", this.PayConfig.ReturnUrl);
            Arguments.Add("notify_url", this.PayConfig.NotifyUrl);
            Arguments.Add("_input_charset", "UTF-8");
            Arguments.Add("show_url", this.ShowUrl);
            Arguments.Add("out_trade_no", this.OutTradeNo);
            Arguments.Add("subject", this.Subject);
            Arguments.Add("body", this.Body);
            Arguments.Add("total_fee", this.TotalFee);
            Arguments.Add("paymethod", this.Paymethod);
            Arguments.Add("defaultbank", this.DefaultBank);
            Arguments.Add("anti_phishing_key", this.AntiPhishingKey);
            Arguments.Add("exter_invoke_ip", this.ExterInvokeIp);
            Arguments.Add("extra_common_param", this.ExtraCommonParam);
            Arguments.Add("buyer_email", this.BuyerEmail);
            Arguments.Add("royalty_type", this.RoyaltyType);
            Arguments.Add("royalty_parameters", this.RoyaltyParameters);

            return Arguments;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override HttpMethod GetHttpMethod()
        {
            return  HttpMethod.POST;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string GetGetwayUrl()
        {
            return "https://.......";
        }

    }
}
