/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/26 11:59:37
 * ****************************************************************/
using StackExchange.Redis;
using System;
using System.Net;

namespace SharpSword.Caching.Redis.StackExchange
{
    /// <summary>
    /// 此连接包装器需要注册成单例模式，使用参考：
    /// https://docs.microsoft.com/zh-cn/azure/redis-cache/cache-dotnet-how-to-use-azure-redis-cache#working-with-caches
    /// </summary>
    public class RedisConnectionWrapper : IRedisConnectionWrapper
    {
        private readonly RedisCacheManagerConfig _config;
        private readonly Lazy<string> _connectionString;
        private volatile ConnectionMultiplexer _connection;
        private readonly object _lock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="config"></param>
        public RedisConnectionWrapper(RedisCacheManagerConfig config)
        {
            this._config = config;
            this._connectionString = new Lazy<string>(this.GetConnectionString);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            return _config.ConnectionString;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private ConnectionMultiplexer GetConnection()
        {
            if (!_connection.IsNull() && _connection.IsConnected)
            {
                return _connection;
            }

            lock (_lock)
            {
                if (!_connection.IsNull() && _connection.IsConnected)
                {
                    return _connection;
                }

                if (!_connection.IsNull() && !_connection.IsConnected)
                {
                    _connection.Dispose();
                }

                //创建连接复用管理器
                var config = ConfigurationOptions.Parse(_connectionString.Value);
                config.AbortOnConnectFail = false;
                _connection = ConnectionMultiplexer.Connect(config);
            }

            return _connection;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public IDatabase Database(int? db = null)
        {
            return this.GetConnection().GetDatabase(db ?? -1); //_settings.DefaultDb);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="endPoint"></param>
        /// <returns></returns>
        public IServer Server(EndPoint endPoint)
        {
            return this.GetConnection().GetServer(endPoint);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public EndPoint[] GetEndpoints()
        {
            return this.GetConnection().GetEndPoints();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="db"></param>
        public void FlushDb(int? db = null)
        {
            var endPoints = GetEndpoints();

            foreach (var endPoint in endPoints)
            {
                this.Server(endPoint).FlushDatabase(db ?? -1); //_settings.DefaultDb);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (_connection != null)
            {
                _connection.Dispose();
            }
        }
    }
}
