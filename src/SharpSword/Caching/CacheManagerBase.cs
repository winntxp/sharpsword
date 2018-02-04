/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/23/2016 10:21:57 AM
 * ****************************************************************/

namespace SharpSword.Caching
{
    /// <summary>
    /// 外部缓存实现类继承此抽象基类
    /// </summary>
    public abstract class CacheManagerBase : ICacheManager
    {
        /// <summary>
        /// 
        /// </summary>
        public CacheManagerBase()
        {
            //this.EventBus = NullEventBus.Instance;
        }

        #region ICacheManager

        /// <summary>
        /// 
        /// </summary>
        void ICacheManager.Clear()
        {
            this.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T ICacheManager.Get<T>(string key)
        {
            return this.Get<T>(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool ICacheManager.IsSet(string key)
        {
            return this.IsSet(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        void ICacheManager.Remove(string key)
        {
            this.Remove(key);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        void ICacheManager.RemoveByPattern(string pattern)
        {
            this.RemoveByPattern(pattern);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        void ICacheManager.Set(string key, object data, int cacheTime)
        {
            this.Set(key, data, cacheTime);
        }

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public abstract void Clear();

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract T Get<T>(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract bool IsSet(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        public abstract void Remove(string key);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern"></param>
        public abstract void RemoveByPattern(string pattern);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="cacheTime"></param>
        public abstract void Set(string key, object data, int cacheTime);
    }
}
