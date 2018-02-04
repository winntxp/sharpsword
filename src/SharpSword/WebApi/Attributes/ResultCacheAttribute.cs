/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/19 12:45:11
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 缓存特性；单位分钟，当接口使用此特性后，将会在入口进行缓存全局拦截ActionResult
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public class ResultCacheAttribute : Attribute
    {
        /// <summary>
        /// 缓存默认的时间
        /// </summary>
        private const int DefaultCacheTime = 30 * 24 * 60;

        /// <summary>
        /// 接口缓存特性
        /// </summary>
        /// <param name="prefix">缓存前缀（一般管理一组相关联的缓存体系，方便联动删除）</param>
        /// <param name="cacheTime">缓存时间，单位：分钟，默认30天</param>
        /// <param name="ignoreUserIdAndUserName">
        /// 根据上送参数对象生成缓存键的时候是否忽略上送用户ID和用户名称
        /// 为什么要设置此开关，因为当同一业务的时候，如果客户端上送了用户ID和用户名称，那么针对同一接口将会生成2个缓存副本，
        /// 这不是我们所期望的，我们期望同一业务的查询（上送参数一致），不同的人看到的都是相同的缓存
        /// 默认是忽略掉当前操作用户业务参数
        /// </param>
        public ResultCacheAttribute(string prefix, int cacheTime = DefaultCacheTime, bool ignoreUserIdAndUserName = true)
        {
            this.Prefix = prefix;
            this.CacheTime = cacheTime;
            this.IgnoreUserIdAndUserName = ignoreUserIdAndUserName;
        }

        /// <summary>
        /// 接口缓存特性
        /// </summary>
        /// <param name="cacheTime">缓存时间，单位：分钟</param>
        /// <param name="ignoreUserIdAndUserName">根据上送参数对象生成缓存键的时候是否忽略上送用户ID和用户名称</param>
        public ResultCacheAttribute(int cacheTime, bool ignoreUserIdAndUserName)
               : this(prefix: string.Empty, cacheTime: cacheTime, ignoreUserIdAndUserName: ignoreUserIdAndUserName)
        {
        }

        /// <summary>
        /// 缓存特性；单位分钟，当接口使用此特性后，将会在入口进行缓存全局拦截ActionResult
        /// 请注意，添加修改接口不要设置缓存特性，要不Execute()方法将无法执行
        /// </summary>
        /// <param name="cacheTime">缓存时间，单位：分钟</param>
        public ResultCacheAttribute(int cacheTime)
            : this(prefix: string.Empty, cacheTime: cacheTime, ignoreUserIdAndUserName: true)
        {
        }

        /// <summary>
        /// 接口缓存特性
        /// </summary>
        /// <param name="ignoreUserIdAndUserName">忽略掉特定用户信息</param>
        public ResultCacheAttribute(bool ignoreUserIdAndUserName) :
            this(prefix: string.Empty, cacheTime: DefaultCacheTime, ignoreUserIdAndUserName: ignoreUserIdAndUserName)
        {
        }

        /// <summary>
        /// 缓存时间，单位：分钟
        /// </summary>
        public int CacheTime { get; private set; }

        /// <summary>
        /// 缓存前缀，配合缓存接口：ICacheManager.RemoveByPattern(string pattern)方法使用
        /// 也即一组相关的业务接口，可以定义相同的缓存前缀，这样在进行缓存操作的时候，可以批量同时移除相关业务缓存
        /// </summary>
        public string Prefix { get; private set; }

        /// <summary>
        /// 是否忽略上送业务参数里的用户身份信息（进行全局缓存，如果不忽略就是单独用户缓存）
        /// 根据上送参数对象生成缓存键的时候是否忽略上送用户ID和用户名称
        /// 为什么要设置此开关，因为当同一业务的时候，如果客户端上送了用户ID和用户名称，那么针对同一接口将会生成2个缓存副本，
        /// 这不是我们所期望的，我们期望同一业务的查询（上送参数一致），不同的人看到的都是相同的缓存(比如获取通用的列表数据)
        /// 默认是忽略掉当前操作用户业务参数
        /// </summary>
        public bool IgnoreUserIdAndUserName { get; private set; }
    }
}
