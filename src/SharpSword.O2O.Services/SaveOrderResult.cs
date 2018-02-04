/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:24:54 PM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 订单处理结果
    /// </summary>
    public class SaveOrderResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="status">处理是否成功</param>
        /// <param name="message">处理结果消息</param>
        public SaveOrderResult(string token, SaveOrderResultStatus status, string message)
        {
            this.Token = token;
            this.Status = status;
            this.Message = message;
        }

        /// <summary>
        /// 处理是否成功
        /// </summary>
        public SaveOrderResultStatus Status { get; private set; }

        /// <summary>
        /// 处理结果消息
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 成功后返回订单编号
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// 提交订单的票据
        /// </summary>
        public string Token { get; set; }
    }
}