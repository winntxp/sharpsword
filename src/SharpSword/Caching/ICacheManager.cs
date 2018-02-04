/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/13 16:59:44
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// 系统框架缓存器接口；在实际使用中请引入：ICacheManager.Extensions扩展来进行使用
    /// 外部实现此接口，请继承CacheManagerBase抽象基类来实现
    /// </summary>
    public interface ICacheManager
    {
        /// <summary>
        /// 根据缓存键获取缓存实体对象
        /// </summary>
        /// <typeparam name="T">明确的缓存对象</typeparam>
        /// <param name="key">缓存键</param>
        /// <returns>指定缓存类型的对象</returns>
        T Get<T>(string key);

        /// <summary>
        /// 设置缓存；注意这里设置缓存，是否先删除缓存还是不删除已有缓存；请在具体实现里做
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <param name="data">缓存数据</param>
        /// <param name="cacheTime">缓存过期时间,单位为：分钟</param>
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// 根据缓存键判断是否已经有缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns>Result</returns>
        bool IsSet(string key); 

        /// <summary>
        /// 根据缓存键删除对应缓存
        /// </summary>
        /// <param name="key">缓存键</param>
        void Remove(string key);

        /// <summary>
        /// 根据正则表达式来删除缓存
        /// </summary>
        /// <param name="pattern">正则表达式匹配模式</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// 清空全部的缓存键
        /// </summary>
        void Clear();
    }
}
