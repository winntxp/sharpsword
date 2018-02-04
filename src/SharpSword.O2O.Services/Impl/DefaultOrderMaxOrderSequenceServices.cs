/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/15/2017 2:20:25 PM
 * ****************************************************************/
using Dapper;
using System.Linq;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 我们从所有用户维度读取最大订单编号
    /// </summary>
    public class DefaultOrderMaxOrderSequenceServices : IOrderMaxOrderSequenceServices, IPerLifetimeDependency
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IDbConnectionStringProvider _dbConnectionStringProvider;
        private readonly GlobalConfig _globalConfig;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        /// <param name="dbConnectionStringProvider"></param>
        /// <param name="globalConfig"></param>
        public DefaultOrderMaxOrderSequenceServices(IDbConnectionFactory dbConnectionFactory,
                                                    IDbConnectionStringProvider dbConnectionStringProvider,
                                                    GlobalConfig globalConfig)
        {
            this._dbConnectionFactory = dbConnectionFactory;
            this._dbConnectionStringProvider = dbConnectionStringProvider;
            this._globalConfig = globalConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetMaxOrderId()
        {
            //获取所有用户维度分库连接信息
            var dbConnectionStrings = _dbConnectionStringProvider.GetDbConnectionStringsByStartsWith(this._globalConfig.UserOrderDbSplitPrefix);

            //并行计算出每个库的最大订单编号(并行计算不要超过 512)
            return dbConnectionStrings.AsParallel().WithDegreeOfParallelism(dbConnectionStrings.Count()).Select(k =>
            {
                return this._dbConnectionFactory.Create(k.ConnectionString)
                                                .Query<long>(@"SELECT ISNULL(MAX([ID]),-1) FROM [OrderIdSevice]")
                                                .FirstOrDefault();
            }).Max().ToString();
        }
    }
}
