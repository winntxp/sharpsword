/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.OAuth
{
    /// <summary>
    /// 平台应用提供者
    /// </summary>
    public interface IAppSourceProvider
    {
        /// <summary>
        /// 获取待注册的APP信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<App> GetApps();
    }
}
