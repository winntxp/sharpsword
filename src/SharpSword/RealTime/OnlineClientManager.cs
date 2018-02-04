/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/30/2015 9:09:00 AM
 * ****************************************************************/
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace SharpSword.RealTime
{
    /// <summary>
    /// 客户端在线连接管理器实现(内存版本实现，需要注册成单例模式)
    /// 注意：由于是内存版本实现，因此当我们将站点设置WEB园数量设置>1获取我们后端部署多套应用程序进行负载均衡的时候，此类将无法正常工作，需要注意
    /// 这时我们需要将连接管理移到第三方存储模块，比如：RDIS或者数据库等
    /// </summary>
    public class OnlineClientManager : IOnlineClientManager, ISingletonDependency
    {
        /// <summary>
        /// 保存在线客户端信息，KEY为：连接ID
        /// </summary>
        private readonly ConcurrentDictionary<string, IOnlineClient> _clients;

        /// <summary>
        /// 
        /// </summary>
        public OnlineClientManager()
        {
            _clients = new ConcurrentDictionary<string, IOnlineClient>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        public void Add(IOnlineClient client)
        {
            _clients[client.ConnectionId] = client;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public bool Remove(IOnlineClient client)
        {
            return Remove(client.ConnectionId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public bool Remove(string connectionId)
        {
            IOnlineClient client;
            return _clients.TryRemove(connectionId, out client);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        public IOnlineClient GetByConnectionId(string connectionId)
        {
            return _clients.GetValueOrDefault(connectionId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IOnlineClient GetByUserId(string userId)
        {
            return GetAllClients().FirstOrDefault(c => c.UserId == userId);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IReadOnlyList<IOnlineClient> GetAllClients()
        {
            return _clients.Values.ToImmutableList();
        }
    }
}