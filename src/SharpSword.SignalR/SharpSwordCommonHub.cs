/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/30/2015 9:09:00 AM
 * ****************************************************************/
using Microsoft.AspNet.SignalR;
using SharpSword.RealTime;
using SharpSword.Runtime;
using System;
using System.Threading.Tasks;

namespace SharpSword.SignalR
{
    /// <summary>
    /// 
    /// </summary>
    public class SharpSwordCommonHub : Hub
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IOnlineClientManager _onlineClientManager;

        /// <summary>
        /// 日志管理器
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 当前登录用户接口
        /// </summary>
        public ISession Session { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="onlineClientManager">当前在线用户管理器</param>
        public SharpSwordCommonHub(IOnlineClientManager onlineClientManager)
        {
            _onlineClientManager = onlineClientManager;
            Logger = NullLogger.Instance;
            Session = NullSession.Instance;
        }

        /// <summary>
        /// 当有新用户连接触发
        /// </summary>
        /// <returns></returns>
        public async override Task OnConnected()
        {
            await base.OnConnected();

            var client = new OnlineClient(connectionId: Context.ConnectionId,
                                          ipAddress: GetIpAddressOfClient(),
                                          userId: Session.UserId.IsNullOrEmptyForDefault(() => Context.ConnectionId, key => key));

            _onlineClientManager.Add(client);

            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.Debug("客户端已连接: " + client);
            }
        }

        /// <summary>
        /// 重新连接
        /// </summary>
        /// <returns></returns>
        public async override Task OnReconnected()
        {
            await base.OnReconnected();

            var client = this._onlineClientManager.GetByConnectionId(this.Context.ConnectionId);
            if (client != null)
            {
                await Task.FromResult(0);
            }

            var onlineClient = new OnlineClient(connectionId: Context.ConnectionId,
                              ipAddress: GetIpAddressOfClient(),
                              userId: Session.UserId.IsNullOrEmptyForDefault(() => Context.ConnectionId, key => key));

            _onlineClientManager.Add(onlineClient);

            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.Debug("客户端已重新连接: " + onlineClient);
            }
        }

        /// <summary>
        /// 当用户断开连接触发
        /// </summary>
        /// <param name="stopCalled"></param>
        /// <returns></returns>
        public async override Task OnDisconnected(bool stopCalled)
        {
            await base.OnDisconnected(stopCalled);

            if (Logger.IsEnabled(LogLevel.Debug))
            {
                Logger.Debug("客户端已关闭连接: " + Context.ConnectionId);
            }

            try
            {
                _onlineClientManager.Remove(Context.ConnectionId);
            }
            catch (Exception ex)
            {
                Logger.Warning(ex.ToString(), ex);
            }
        }

        /// <summary>
        /// 获取客户端IP地址
        /// </summary>
        /// <returns></returns>
        private string GetIpAddressOfClient()
        {
            try
            {
                return Context.Request.Environment["server.RemoteIpAddress"].ToString();
            }
            catch (Exception ex)
            {
                Logger.Error("未能获取客户端ip地址! ConnectionId: " + Context.ConnectionId);
                Logger.Error(ex.Message, ex);
                return "";
            }
        }

        /// <summary>
        /// 自定义释放下资源
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
