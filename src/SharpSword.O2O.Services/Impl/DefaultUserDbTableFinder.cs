/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:11:14 PM
 * ****************************************************************/

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultUserDbTableFinder : IUserDbTableFinder
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly GlobalConfig _globalConfig;

        /// <summary>
        /// 表后缀
        /// </summary>
        private const string TABLESUFFIX = "{0:00}";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="globalConfig"></param>
        public DefaultUserDbTableFinder(GlobalConfig globalConfig)
        {
            this._globalConfig = globalConfig;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual string GetTableSuffix(long userId)
        {
            return TABLESUFFIX.With(userId % 8);
        }
    }
}