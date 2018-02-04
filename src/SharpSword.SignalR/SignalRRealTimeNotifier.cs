/******************************************************************
* SharpSword zhangliang@sharpsword.com.cn 2015/11/25 11:48:48
* *****************************************************************/
using Microsoft.AspNet.SignalR;
using SharpSword.Notifications;
using SharpSword.RealTime;
using System;
using System.Threading.Tasks;

namespace SharpSword.SignalR
{
    /// <summary>
    /// 
    /// </summary>
    public class SignalRRealTimeNotifier : IRealTimeNotifier, ITransientDependency
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IOnlineClientManager _onlineClientManager;
        /// <summary>
        /// 
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private static IHubContext CommonHub
        {
            get
            {
                return GlobalHost.ConnectionManager.GetHubContext<SharpSwordCommonHub>();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onlineClientManager"></param>
        public SignalRRealTimeNotifier(IOnlineClientManager onlineClientManager)
        {
            _onlineClientManager = onlineClientManager;
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 实时推送消息给客户端
        /// </summary>
        /// <param name="userNotifications"></param>
        /// <returns></returns>
        public Task SendNotificationsAsync(UserNotification[] userNotifications)
        {
            foreach (var userNotification in userNotifications)
            {
                try
                {
                    var onlineClient = _onlineClientManager.GetByUserId(userNotification.UserId);
                    if (onlineClient == null)
                    {
                        continue;
                    }

                    var signalRClient = CommonHub.Clients.Client(onlineClient.ConnectionId);
                    if (signalRClient == null)
                    {
                        if (Logger.IsEnabled(LogLevel.Debug))
                        {
                            Logger.Debug("获取用户信息失败，用户编号： " + userNotification.UserId);
                        }
                        continue;
                    }

                    //远程调用客户端接受消息方法
                    signalRClient.getNotification(userNotification.Data.ToString());

                }
                catch (Exception ex)
                {
                    if (this.Logger.IsEnabled(LogLevel.Warning))
                    {
                        Logger.Warning("发送消息失败，用户Id: " + userNotification.UserId);
                        Logger.Warning(ex.ToString(), ex);
                    }
                }
            }

            return Task.FromResult(0);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="notificationData"></param>
        /// <returns></returns>
        public Task SendNotificationsAsync(NotificationData notificationData)
        {
            CommonHub.Clients.All.getNotification(notificationData.ToString());
            return Task.FromResult(0);
        }
    }
}
