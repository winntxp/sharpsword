/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/
using SharpSword.Configuration;

namespace SharpSword.Pay
{
    /// <summary>
    /// 支付平台配置抽象基类，我们抽象出来，编译后期扩展
    /// </summary>
    public abstract class PayConfigBase : IPayConfig, ISetting
    {
        /// <summary>
        /// 支付平台支付成功后，通过HTTP提交到本地支付成功接收接口URL（通常此接口对用户端是不可见的）
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 支付平台支付成功后，调整到本地的URL地址（通常此地址是用于显示支付成功信息，给用户看见）
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
