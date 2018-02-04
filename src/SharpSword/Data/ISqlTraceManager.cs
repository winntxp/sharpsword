/* *******************************************************
 * SharpSword zhangliang4629@163.com 12/5/2016 11:54:08 AM
 * ****************************************************************/

namespace SharpSword.Data
{
    /// <summary>
    /// SQL记录器
    /// </summary>
    public interface ISqlTraceManager
    {
        /// <summary>
        /// 记录SQL语句
        /// </summary>
        /// <param name="sql">执行的SQL语句</param>
        void Trace(string sql);
    }
}
