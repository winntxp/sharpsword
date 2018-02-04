/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 12:18:52 PM
 * ****************************************************************/
using SharpSword.O2O.Data.Entities;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 用户相关接口，注意实现里，需要进行缓存策略
    /// </summary>
    public interface IUserServices
    {
        /// <summary>
        /// 获取会员信息
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        AspnetUser GetUser(long userId);
    }
}