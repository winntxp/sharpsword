using Memcached.ClientLibrary;
/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/19 16:12:25
 * ****************************************************************/
using System;
using System.Globalization;

namespace SharpSword.Caching.Memcached
{
    /// <summary>
    /// API框架接口缓存实现类Mamcached
    /// 服务器运行文件：platformV3.0\Platform\packages\memcached-win32x64\x64
    /// 客户端SDK文件：platformV3.0\Platform\packages\Memcached.ClientLibrary.1.0\lib\net20
    /// </summary>
    public class MemcachedManager : ICacheManager
    {
        /// <summary>
        /// 
        /// </summary>
        private string _time = DateTime.Now.ToString(CultureInfo.InvariantCulture);
        private const string PoolName = "First";

        /// <summary>
        /// 
        /// </summary>
        private MemcachedClient _memcachedClient;

        /// <summary>
        /// API框架接口缓存实现类Mamcached
        /// </summary>
        /// <param name="config">配置文件</param>
        public MemcachedManager(MemcachedManagerConfig config)
        {
            SockIOPool pool = SockIOPool.GetInstance(PoolName);
            pool.InitConnections = 3;
            pool.MinConnections = 3;
            pool.MaxConnections = 5;
            pool.SocketConnectTimeout = 1000;
            pool.SocketTimeout = 3000;
            pool.MaintenanceSleep = 30;
            pool.Failover = true;
            pool.Nagle = false;
            //设置Memcached Server 
            pool.SetServers(config.Servers);
            pool.Initialize();
            _memcachedClient = new MemcachedClient
            {
                PoolName = PoolName,
                EnableCompression = true
            };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            var data = _memcachedClient.Get(key);
            if (data is T)
            {
                return (T)data;
            }
            return default(T);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        public void Set(string key, object data, int cacheTime)
        {
            if (null == data)
            {
                return;
            }
            _memcachedClient.Set(key, data, DateTime.Now.AddMinutes(cacheTime));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool IsSet(string key)
        {
            return _memcachedClient.KeyExists(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            _memcachedClient.Delete(key);
        }

        /// <summary>
        /// 不支持
        /// </summary>
        /// <param name="pattern"></param>
        public void RemoveByPattern(string pattern)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public void Clear()
        {
            _memcachedClient.FlushAll();
        }
    }
}