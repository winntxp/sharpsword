/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/5/2017 12:45:58 PM
 * ****************************************************************/
using System.Data;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 订单用户维度数据库连接器
    /// </summary>
    public class DefaultUserOrderDbConnectionFactory : IUserOrderDbConnectionFactory, IPerLifetimeDependency
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IUserOrderDbFinder _areaOrderDbFinder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        /// <param name="userOrderDbFinder"></param>
        public DefaultUserOrderDbConnectionFactory(IDbConnectionFactory dbConnectionFactory,
                                                   IUserOrderDbFinder userOrderDbFinder)
        {
            this._dbConnectionFactory = dbConnectionFactory;
            this._areaOrderDbFinder = userOrderDbFinder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userIdSplitFactor"></param>
        /// <returns></returns>
        public IDbConnection Create(int userIdSplitFactor)
        {
            return this._dbConnectionFactory.Create(this._areaOrderDbFinder.GetDbConnectionString(userIdSplitFactor));
        }
    }
}
