/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.OAuth
{
    public class NullAppSourceProvider : IAppSourceProvider
    {
        public IEnumerable<App> GetApps()
        {
            return new List<App>();
        }
    }
}
