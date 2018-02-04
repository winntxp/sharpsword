/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/5/2017 12:42:43 PM
 * ****************************************************************/
using System.Data;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 此接口是为方便编程定义的接口，用户根据订单编号获取区域订单分库策略
    /// </summary>
    public interface IAreaOrderDbConnectionFactory
    {
        /// <summary>
        /// 根据订单编号创建数据库连接
        /// </summary>
        /// <param name="areaIdSplitFactor">区域编号拆分因子</param>
        /// <returns></returns>
        IDbConnection Create(int areaIdSplitFactor);
    }
}
