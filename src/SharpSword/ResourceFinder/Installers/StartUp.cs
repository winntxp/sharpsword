/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2015 2:29:27 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.ResourceFinder.Installers
{
    /// <summary>
    /// 我们启动的时候，先预热下资源，将资源先读取到我们缓存
    /// </summary>
    internal class StartUp : StartUpBase
    {
        /// <summary>
        /// 
        /// </summary>
        private IEnumerable<IResourceFinder> _resourceFinders;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceFinders"></param>
        public StartUp(IEnumerable<IResourceFinder> resourceFinders)
        {
            this._resourceFinders = resourceFinders ?? new List<IResourceFinder>();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Init()
        {
            foreach (var resourceFinder in _resourceFinders)
            {
                resourceFinder.GetResources();
            }
        }
    }
}
