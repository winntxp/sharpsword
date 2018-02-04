/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/12/2017 11:41:36 AM
 * ****************************************************************/
using SharpSword.O2O.Services.Domain;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 用户ID生成器接口
    /// </summary>
    public interface IUserIdGenerator
    {
        /// <summary>
        /// 创建用户编号
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        long Create(UserIdGeneratorCreateUserInfo user);
    }
}
