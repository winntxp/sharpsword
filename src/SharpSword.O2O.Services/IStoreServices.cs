/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 12:18:52 PM
 * ****************************************************************/
using SharpSword.O2O.Services.Domain;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 门店服务接口
    /// </summary>
    public interface IStoreServices
    {
        /// <summary>
        /// 获取门店信息，需要进行缓存处理，当缓存不存在的时候，需要从存储介质获取信息
        /// </summary>
        /// <param name="storeId">门店ID</param>
        /// <returns>如果找不到或者系统出现异常，不会抛出异常，会返回null：系统调用的时候，需要判断下是否为null</returns>
        StoreProfileDto GetStore(long storeId);
    }
}
