/* *******************************************************
 * SharpSword zhangliang4629@163.com 11/22/2016 12:35:45 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.CommandExecutor.Parameters
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandLineParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="commandLine"></param>
        /// <returns></returns>
        IEnumerable<string> Parse(string commandLine);
    }
}
