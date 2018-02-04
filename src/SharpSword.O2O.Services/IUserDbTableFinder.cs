/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/30/2017 10:50:39 AM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 用户表选择器
    /// </summary>
    public interface IUserDbTableFinder : IDbTableFinder
    {
        /// <summary>
        /// 获取数据表名称
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        string GetTableSuffix(long userId);
    }
}