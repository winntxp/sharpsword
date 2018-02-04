/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/18 19:22:39
 * ****************************************************************/

namespace SharpSword.SDK
{
    /// <summary>
    /// 本地SDK缓存设置参数
    /// 是否启用本地缓存，默认为false，默认缓存时间60分钟
    /// 如果需要启用本地缓存，请设置属性FromLocalCache=true
    /// </summary>
    public sealed class CacheOptions
    {
        /// <summary>
        /// 创建下全局静态，防止多次重复创建此默认对象
        /// </summary>
        private static CacheOptions _instance = new CacheOptions(false, 0);

        /// <summary>
        /// 获取默认缓存设置(不启用缓存)
        /// </summary>
        public static CacheOptions Default { get { return _instance; } }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="useLocalCache">是否启用本地SDK客户端缓存</param>
        /// <param name="cacheTime">缓存时间(单位分钟)</param>
        public CacheOptions(bool useLocalCache, int cacheTime)
        {
            //默认不启用本地SDK缓存
            this.UseLocalCache = useLocalCache;
            //默认的缓存时间60分钟
            this.CacheTime = cacheTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="useLocalCache">是否启用本地SDK客户端缓存</param>
        public CacheOptions(bool useLocalCache) : this(useLocalCache, 60) { }

        /// <summary>
        /// 是否启用本地缓存，默认false
        /// </summary>
        public bool UseLocalCache { get; private set; }

        /// <summary>
        /// 缓存时间，缓存时间单位为分钟(系统默认60分钟)
        /// </summary>
        public int CacheTime { get; private set; }
    }
}
