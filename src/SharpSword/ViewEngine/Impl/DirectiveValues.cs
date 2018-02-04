/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/12 9:44:46
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.ViewEngine.Impl
{
    /// <summary>
    /// 代码块对象
    /// </summary>
    internal class DirectiveValues : Dictionary<string, string>
    {
        /// <summary>
        /// 
        /// </summary>
        public string Directive;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="directive"></param>
        public DirectiveValues(string directive)
        {
            Directive = directive;
        }
    }
}

