/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/5/27 14:15:25
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.ResourceFinder.Impl
{
    /// <summary>
    /// 资源查找器管理器
    /// </summary>
    public class DefaultResourceFinderManager : IResourceFinderManager
    {
        /// <summary>
        /// 
        /// </summary>
        private Lazy<IEnumerable<IResourceFinder>> lazyResourceFinders;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="resourceFinders">资源查找器</param>
        public DefaultResourceFinderManager(IEnumerable<IResourceFinder> resourceFinders)
        {
            resourceFinders.CheckNullThrowArgumentNullException(nameof(resourceFinders));
            lazyResourceFinders = new Lazy<IEnumerable<IResourceFinder>>(() => resourceFinders.OrderByDescending(o => o.Priority).ToArray());
        }

        /// <summary>
        /// 系统框架注册的所有资源查找器
        /// </summary>
        public IEnumerable<IResourceFinder> ResourceFinders
        {
            get { return this.lazyResourceFinders.Value; }
        }

        /// <summary>
        /// 获取所有资源查找器资源并集
        /// </summary>
        /// <param name="resourceFinders">资源查找器</param>
        /// <returns></returns>
        private static IEnumerable<KeyValuePair<string, string>> GetResources(IEnumerable<IResourceFinder> resourceFinders)
        {
            return resourceFinders.SelectMany(o => o.GetResources());
        }

        /// <summary>
        /// 获取资源
        /// </summary>
        /// <param name="resourceName">资源名称(可以是包含部分或者全部资源名称)</param>
        /// <returns>返回资源字符串，如果是图片，则返回base64字符串，如果不存在则返回null</returns>
        public string GetResource(string resourceName)
        {
            var resource = GetResources(this.ResourceFinders)
                    .AsParallel()
                    .FirstOrDefault(o => o.Key.EndsWith(resourceName, StringComparison.OrdinalIgnoreCase));

            //资源键和资源键不为空，直接返回
            if (!resource.IsNull() && !resource.Key.IsNullOrEmpty())
            {
                return resource.Value;
            }

            return null;
        }

        /// <summary>
        /// 获取所有资源
        /// </summary>
        /// <returns>获取资源的名值对集合（所有注册的资源查找器）</returns>
        public IEnumerable<KeyValuePair<string, string>> GetResources()
        {
            return GetResources(this.ResourceFinders);
        }

        /// <summary>
        /// 获取所有资源名称（只有注册的资源查找器查找出来的资源名称）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetResourceNames()
        {
            return this.GetResources().Select(o => o.Key).ToList();
        }

    }
}
