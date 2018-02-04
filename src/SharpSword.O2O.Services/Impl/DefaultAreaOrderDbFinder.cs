/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:11:14 PM
 * ****************************************************************/
using System;
using System.Linq;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 默认区域订单分库管理器
    /// </summary>
    public class DefaultAreaOrderDbFinder : IAreaOrderDbFinder
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDbConnectionStringProvider _dbConnectionStringProvider;
        private readonly GlobalConfig _globalConfig;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnectionStringProvider"></param>
        /// <param name="globalConfig"></param>
        public DefaultAreaOrderDbFinder(IDbConnectionStringProvider dbConnectionStringProvider,
                                        GlobalConfig globalConfig)
        {
            this._dbConnectionStringProvider = dbConnectionStringProvider;
            this._globalConfig = globalConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaIdSplitFactor"></param>
        /// <returns></returns>
        public virtual string GetDbConnectionString(int areaIdSplitFactor)
        {
            //所有匹配到的数据库连接
            var dbConnectionStrings = this._dbConnectionStringProvider.GetDbConnectionStringsByStartsWith(this._globalConfig.AreaOrderDbSplitPrefix).ToList();

            //有多少个数据库
            if (dbConnectionStrings.Count == 0)
            {
                throw new SharpSwordCoreException("未找到任何订单区域维度分库策略连接字符串，连接字符串名称命名方式为：{0}[模值]"
                                                                            .With(this._globalConfig.AreaOrderDbSplitPrefix));
            }

            //拆分成多少个库
            int dbNumber = dbConnectionStrings.Count;

            //连接名称
            string dbName = "{0}{1}".With(this._globalConfig.AreaOrderDbSplitPrefix, areaIdSplitFactor % dbNumber);

            //找到分库策略
            var connectionStringSettings = dbConnectionStrings.FirstOrDefault(x => x.Name.Equals(dbName, StringComparison.OrdinalIgnoreCase));

            //对应的分库找不到对应的数据库连接字符串
            if (connectionStringSettings.IsNull())
            {
                throw new SharpSwordCoreException("未找到对应的{0}数据库连接配置信息".With(dbName));
            }

            //返回对应的分库连接字符串
            return connectionStringSettings.ConnectionString;
        }
    }
}