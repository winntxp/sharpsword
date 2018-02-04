/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/7/2016 5:33:06 PM
 * ****************************************************************/
using SharpSword.Data;
using SharpSword.Notifications;

namespace SharpSword.Host.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class LogSqlTraceManager : ISqlTraceManager
    {
        /// <summary>
        /// 
        /// </summary>
        private ILogger _logger;
        private IRealTimeNotifier _realTimeNotifier;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="realTimeNotifier"></param>
        public LogSqlTraceManager(ILogger logger, IRealTimeNotifier realTimeNotifier)
        {
            this._logger = logger;
            this._realTimeNotifier = realTimeNotifier;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        public void Trace(string sql)
        {
            this._logger.Information(sql);

            //推送到监控
            this._realTimeNotifier.SendNotificationsAsync(new MessageNotificationData(sql));
        }
    }
}
