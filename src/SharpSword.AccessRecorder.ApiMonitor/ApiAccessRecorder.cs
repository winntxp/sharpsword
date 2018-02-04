/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/1 10:22:47
 * ****************************************************************/
using SharpSword.Notifications;
using SharpSword.WebApi;

namespace SharpSword.AccessRecorder.ApiMonitor
{
    /// <summary>
    /// 用于测试记录访问接口
    /// </summary>
    public class ApiAccessRecorder : IApiAccessRecorder
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IRealTimeNotifier _realTimeNotifier;

        /// <summary>
        /// 使用日志记录器作为临时的接口访问记录器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 默认初始化空的日志接口
        /// </summary>
        public ApiAccessRecorder(IRealTimeNotifier realTimeNotifier)
        {
            this._realTimeNotifier = realTimeNotifier;
            this.Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public void Record(ApiAccessRecorderArgs args)
        {
            this._realTimeNotifier.SendNotificationsAsync(new ObjectNotificationData(args));
        }
    }
}