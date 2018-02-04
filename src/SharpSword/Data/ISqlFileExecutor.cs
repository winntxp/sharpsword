/* *******************************************************
 * SharpSword zhangliang4629@163.com 9/22/2016 11:09:52 AM
 * ****************************************************************/
using System;

namespace SharpSword.Data
{
    /// <summary>
    /// 执行SQL文件里的SQL语句
    /// </summary>
    public interface ISqlFileExecutor
    {
        /// <summary>
        /// 根据提供的SQL文件路径，自动执行SQL里面的语句
        /// </summary>
        /// <param name="sqlFilePath"></param>
        void ExecuteSqlFile(string sqlFilePath);
    }
}
