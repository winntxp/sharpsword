/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/12/2017 1:51:17 PM
 * ****************************************************************/
using Dapper;
using SharpSword.O2O.Services.Domain;
using System.Linq;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultUserIdGenerator : IUserIdGenerator
    {
        /// <summary>
        /// 
        /// </summary>
        private IGlobalDbConnectionFactory _globalDbConnectionFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="globalDbConnectionFactory"></param>
        public DefaultUserIdGenerator(IGlobalDbConnectionFactory globalDbConnectionFactory)
        {
            this._globalDbConnectionFactory = globalDbConnectionFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public long Create(UserIdGeneratorCreateUserInfo user)
        {
            return this._globalDbConnectionFactory.Create()
                                                  .Query<long>(@"DECLARE @ID AS BIGINT; UPDATE UserIds SET @ID=[Id]=[Id]+1; SELECT @ID AS [ID]")
                                                  .FirstOrDefault();
        }
    }
}
