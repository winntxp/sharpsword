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
    /// �ͻ����������ӹ�����ʵ��(�ڴ�汾ʵ�֣���Ҫע��ɵ���ģʽ)
    /// ע�⣺�������ڴ�汾ʵ�֣���˵����ǽ�վ������WEB԰��������>1��ȡ���Ǻ�˲������Ӧ�ó�����и��ؾ����ʱ�򣬴��ཫ�޷�������������Ҫע��
    /// ��ʱ������Ҫ�����ӹ����Ƶ��������洢ģ�飬���磺RDIS�������ݿ��
    /// </summary>
    public class OnlineClientManager : IOnlineClientManager, ISingletonDependency
    {
        /// <summary>
        /// �������߿ͻ�����Ϣ��KEYΪ������ID
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