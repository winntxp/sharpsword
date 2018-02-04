/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/
using System;

namespace SharpSword.Pay
{
    /// <summary>
    /// 支付结果处理返回对象
    /// </summary>
    [Serializable]
    public class VerifyDataResult
    {
        /// <summary>
        /// 对于成功状态，我们使用静态，防止重复创建对象
        /// </summary>
        private static VerifyDataResult _instanceOk = new VerifyDataResult(VerifyDataResultStatus.OK);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status">支出处理结果</param>
        /// <param name="exc">支付处理失败异常，只有在处理失败的情况下存在，处理成功的情况下为null</param>
        /// <param name="message">支付处理消息（成功返回OK，失败返回失败错误消息）</param>
        public VerifyDataResult(VerifyDataResultStatus status, Exception exc = null, string message = "OK")
        {
            this.Status = status;
            this.Exception = exc;
            this.Message = message ?? string.Empty;
        }

        /// <summary>
        /// 支出处理结果
        /// </summary>
        public VerifyDataResultStatus Status { get; private set; }

        /// <summary>
        /// 支付处理消息（成功返回OK，失败返回失败错误消息）
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 支付处理失败异常，只有在处理失败的情况下存在，处理成功的情况下为null
        /// </summary>
        public Exception Exception { get; private set; }

        /// <summary>
        /// 方便返回支付成功对象
        /// </summary>
        public static VerifyDataResult OK
        {
            get { return _instanceOk; }
        }

        /// <summary>
        /// 支付出现异常错误
        /// </summary>
        /// <param name="exc">错误异常</param>
        /// <param name="errorMessage">错误消息</param>
        /// <returns></returns>
        public static VerifyDataResult Fail(Exception exc, string errorMessage)
        {
            return new VerifyDataResult(VerifyDataResultStatus.FAIL, exc, errorMessage);
        }
    }
}
