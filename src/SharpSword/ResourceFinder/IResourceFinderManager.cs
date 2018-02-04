/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/5/27 14:13:02
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword
{
    /// <summary>
    /// 资源查找过滤器
    /// </summary>
    public interface IResourceFinderManager
    {
        /// <summary>
        /// 系统框架注册的所有资源查找器
        /// </summary>
        IEnumerable<IResourceFinder> ResourceFinders { get; }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="resourceName">资源名称（部分路径或者全部路径，如：sys.png 或者 ServiceCenter.Api.Core.Resource.sys.png）</param>
        /// <returns>实现的时候，如果为找到指定的资源，请返回null，而不要返回string.empty(这样会与空的资源文件产生语义冲突)</returns>
        string GetResource(string resourceName);

        /// <summary>
        /// 获取所有的资源集合
        /// </summary>
        /// <returns></returns>
        IEnumerable<KeyValuePair<string, string>> GetResources();

        /// <summary>
        /// 获取所有的资源键名称信息
        /// </summary>
        /// <returns></returns>
        IEnumerable<string> GetResourceNames();
    }
}
