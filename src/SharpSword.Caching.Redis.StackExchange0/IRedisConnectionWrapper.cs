/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/26 11:59:37
 * ****************************************************************/
using StackExchange.Redis;
using System;
using System.Net;

namespace SharpSword.Caching.Redis.StackExchange
{
    /// <summary>
    /// Redis connection wrapper
    /// </summary>
    public interface IRedisConnectionWrapper : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        IDatabase Database(int? db = null);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        IServer Server(EndPoint endPoint);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        EndPoint[] GetEndpoints();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        void FlushDb(int? db = null);
    }
}
