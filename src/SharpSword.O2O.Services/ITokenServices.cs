/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:09:27 PM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// TOKEN生成器
    /// </summary>
    public interface ITokenServices
    {
        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        string Create();
    }
}
