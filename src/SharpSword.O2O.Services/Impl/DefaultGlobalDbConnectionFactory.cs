/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/6/2017 9:40:30 AM
 * ****************************************************************/
using System.Data;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultGlobalDbConnectionFactory : IGlobalDbConnectionFactory, IPerLifetimeDependency
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IGlobalDbFinder _globalDbFinder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        /// <param name="globalDbFinder"></param>
        public DefaultGlobalDbConnectionFactory(IDbConnectionFactory dbConnectionFactory,
                                                IGlobalDbFinder globalDbFinder)
        {
            this._dbConnectionFactory = dbConnectionFactory;
            this._globalDbFinder = globalDbFinder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IDbConnection Create()
        {
            return this._dbConnectionFactory.Create(this._globalDbFinder.GetDbConnectionString());
        }
    }
}
