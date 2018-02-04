/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/22/2016 11:43:14 AM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.CommandExecutor.Parameters
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICommandParametersParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        CommandParameters Parse(IEnumerable<string> args);
    }
}
