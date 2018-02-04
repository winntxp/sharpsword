/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/4/2017 8:58:32 AM
 * ****************************************************************/
using System.Linq;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultGlobalDbFinder : IGlobalDbFinder
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDbConnectionStringProvider _dbConnectionStringProvider;
        private readonly GlobalConfig _config;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnectionStringProvider"></param>
        /// <param name="config"></param>
        public DefaultGlobalDbFinder(IDbConnectionStringProvider dbConnectionStringProvider,
                                     GlobalConfig config)
        {
            this._dbConnectionStringProvider = dbConnectionStringProvider;
            this._config = config;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string GetDbConnectionString()
        {
            var dbConnectionString = this._dbConnectionStringProvider.GetDbConnectionStringsByStartsWith(this._config.GlobalDbSplitPrefix).FirstOrDefault();

            if (dbConnectionString.IsNull())
            {
                throw new SharpSwordCoreException("请确保有以{0}开头的字符串连接配置".With(this._config.GlobalDbSplitPrefix));
            }
            return dbConnectionString.ConnectionString;
        }
    }
}
