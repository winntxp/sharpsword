/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/5/2017 12:46:43 PM
 * ****************************************************************/
using System.Data;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultUserDbConnectionFactory : IUserDbConnectionFactory, IPerLifetimeDependency
    {
        private readonly IDbConnectionFactory _dbConnectionFactory;
        private readonly IUserDbFinder _userDbFinder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbConnectionFactory"></param>
        /// <param name="userDbFinder"></param>
        public DefaultUserDbConnectionFactory(IDbConnectionFactory dbConnectionFactory,
                                              IUserDbFinder userDbFinder)
        {
            this._dbConnectionFactory = dbConnectionFactory;
            this._userDbFinder = userDbFinder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IDbConnection Create(long userId)
        {
            return this._dbConnectionFactory.Create(this._userDbFinder.GetDbConnectionString(userId));
        }
    }
}
