/* ****************************************************************
 * SharpSword zhangliang4629@163.com 12/5/2016 12:18:01 PM
 * ****************************************************************/

namespace SharpSword.Data
{
    /// <summary>
    /// 默认SQL跟踪器，空实现
    /// </summary>
    public class NullSqlTraceManager : ISqlTraceManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static ISqlTraceManager _instance = new NullSqlTraceManager();

        /// <summary>
        /// 
        /// </summary>
        public static ISqlTraceManager Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 
        /// </summary>
        private NullSqlTraceManager() { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        public void Trace(string sql)
        {
            //throw new NotImplementedException();
        }
    }
}
