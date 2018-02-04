/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/

namespace SharpSword.Pay
{
    /// <summary>
    /// 所有支付接口配置需要实现此接口
    /// </summary>
    public interface IPayConfig
    {
        /// <summary>
        /// 第三方平台支付成功后，将消息通知到本地的接口地址
        /// </summary>
        string NotifyUrl { get; set; }

        /// <summary>
        /// 第三方平台支付成功后，直接将支付接口跳转到本地的地址
        /// </summary>
        string ReturnUrl { get; set; }
    }
}
