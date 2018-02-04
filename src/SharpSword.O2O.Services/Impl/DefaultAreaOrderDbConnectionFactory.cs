/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/5/2017 12:44:51 PM
 * ****************************************************************/
using System.Data;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 订单区域维度数据库连接器
    /// </summary>
    public class DefaultAreaOrderDbConnectionFactory : IAreaOrderDbConnectionFactory, IPerLifetimeDependency
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IAreaOrderDbFinder _areaOrderDbFinder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        /// <param name="areaOrderDbFinder"></param>
        public DefaultAreaOrderDbConnectionFactory(IDbConnectionFactory dbConnectionFactory,
                                                   IAreaOrderDbFinder areaOrderDbFinder)
        {
            this._dbConnectionFactory = dbConnectionFactory;
            this._areaOrderDbFinder = areaOrderDbFinder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaIdSplitFactor"></param>
        /// <returns></returns>
        public IDbConnection Create(int areaIdSplitFactor)
        {
            return this._dbConnectionFactory.Create(this._areaOrderDbFinder.GetDbConnectionString(areaIdSplitFactor));
        }
    }
}
