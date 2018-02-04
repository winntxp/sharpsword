/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/27/2016 11:45:19 AM
 * ****************************************************************/
using System.Threading.Tasks;

namespace SharpSword.Net.SMS
{
    /// <summary>
    /// 
    /// </summary>
    // 短信发送空实现，系统默认注册
    public class NullSMSSender : SMSSenderBase
    {
        /// <summary>
        /// 
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static NullSMSSender _instance = new NullSMSSender();

        /// <summary>
        /// 
        /// </summary>
        public ISMSSender Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 
        /// </summary>
        public NullSMSSender()
        {
            this.Logger = GenericNullLogger<NullSMSSender>.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="to"></param>
        /// <param name="body"></param>
        /// <returns></returns>
        public override Task<SendResult> SendAsync(string to, string body)
        {
            if (this.Logger.IsEnabled(LogLevel.Debug))
            {
                this.Logger.Debug("sms sended，to:{0}，body:{1}".With(to, body));
            }
            return Task.FromResult(new SendResult(ResultStatus.SUCCESS, "OK"));
        }
    }
}
