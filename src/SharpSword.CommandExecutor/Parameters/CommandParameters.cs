/* *******************************************************
 * SharpSword zhangliang4629@163.com 11/22/2016 11:42:22 AM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.CommandExecutor.Parameters
{
    /// <summary>
    /// 
    /// </summary>
    public class CommandParameters
    {
        /// <summary>
        /// 
        /// </summary>
        public IList<string> Arguments { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public IDictionary<string, string> Switches { get; set; }
    }
}
