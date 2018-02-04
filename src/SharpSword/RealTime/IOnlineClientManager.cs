/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/30/2015 9:09:00 AM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.RealTime
{
    /// <summary>
    /// 实时通讯客户端管理器，用于管理在线用户
    /// </summary>
    public interface IOnlineClientManager
    {
        /// <summary>
        /// 添加一个连接的客户端
        /// </summary>
        /// <param name="client"></param>
        void Add(IOnlineClient client);

        /// <summary>
        /// 移除一个连接的客户端
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        bool Remove(IOnlineClient client);

        /// <summary>
        /// 根据连接ID移除连接客户端
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        bool Remove(string connectionId);

        /// <summary>
        /// 根据连接ID获取一个客户端信息
        /// </summary>
        /// <param name="connectionId"></param>
        /// <returns></returns>
        IOnlineClient GetByConnectionId(string connectionId);

        /// <summary>
        /// 根据连接的用户获取客户端信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IOnlineClient GetByUserId(string userId);

        /// <summary>
        /// 获取所有的连接客户端
        /// </summary>
        /// <returns></returns>
        IReadOnlyList<IOnlineClient> GetAllClients();
    }
}