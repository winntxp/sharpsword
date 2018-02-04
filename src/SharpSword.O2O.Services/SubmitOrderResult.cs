/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 12:18:52 PM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 提交订单处理结果
    /// </summary>
    public class SubmitOrderResult
    {
        /// <summary>
        /// 
        /// </summary>
        public SubmitOrderResult() : this(true, "OK", null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submitStatus"></param>
        /// <param name="message"></param>
        /// <param name="orderProgress"></param>
        public SubmitOrderResult(bool submitStatus = true, string message = "OK", OrderProgress orderProgress = null)
        {
            this.SubmitStatus = submitStatus;
            this.Message = message;
            this.OrderProgress = orderProgress;
        }

        /// <summary>
        /// 是否提交成功（只有提交成功了，才会返回OrderProgress属性信息）
        /// </summary>
        public bool SubmitStatus { get; set; }

        /// <summary>
        /// 返回消息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 订单状态(只有SubmitStatus==true的时候才会返回)
        /// </summary>
        public OrderProgress OrderProgress { get; set; }
    }
}
