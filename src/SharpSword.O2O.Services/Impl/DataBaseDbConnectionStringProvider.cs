/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/20/2017 5:03:17 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.O2O.Services.Impl
{
    /// <summary>
    /// 基于数据库的数据库连接字符串连接获取
    /// </summary>
    public class DataBaseDbConnectionStringProvider : WebConfigDbConnectionStringProvider
    {
        private static IList<ConnectionStringSetting> ConnectionStringSettings = new List<ConnectionStringSetting>();
        private static object Locker = new object();
        private static bool _initializationed = false;
        private readonly IGlobalDbConnectionFactory _globalDbConnectionFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="globalDbConnectionFactory"></param>
        public DataBaseDbConnectionStringProvider(IGlobalDbConnectionFactory globalDbConnectionFactory)
        {
            this._globalDbConnectionFactory = globalDbConnectionFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<ConnectionStringSetting> GetDbConnectionStrings()
        {
            if (_initializationed)
            {
                return ConnectionStringSettings;
            }

            lock (Locker)
            {
                if (_initializationed)
                {
                    return ConnectionStringSettings;
                }

                _initializationed = true;

                //从数据库获取连接字符串数据

                return ConnectionStringSettings;
            }
        }
    }
}
