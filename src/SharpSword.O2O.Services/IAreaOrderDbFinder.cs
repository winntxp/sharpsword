/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/30/2017 10:50:39 AM
 * ****************************************************************/

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 区域订单库分库策略
    /// </summary>
    public interface IAreaOrderDbFinder : IDbFinder
    {
        /// <summary>
        /// 获取数据库连接
        /// </summary>
        /// <param name="areaIdSplitFactor">区域编号拆分因子（后2位）</param>
        /// <returns></returns>
        string GetDbConnectionString(int areaIdSplitFactor);
    }
}
