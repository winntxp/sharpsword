/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/30/2017 10:50:39 AM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 全局库数据库查找器
    /// </summary>
    public interface IGlobalDbFinder : IDbFinder
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <returns></returns>
        string GetDbConnectionString();
    }
}
