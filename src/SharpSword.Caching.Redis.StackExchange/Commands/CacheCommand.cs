/* *******************************************************
 * SharpSword zhangliang4629@163.com 11/25/2016 10:40:50 AM
 * ****************************************************************/
using SharpSword.Commands;
using SharpSword.Domain.Entitys;

namespace SharpSword.Caching.Redis.StackExchange.Commands
{
    /// <summary>
    /// 
    /// </summary>
    public class CacheCommand : CommandHandlerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ICacheManager _cacheManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheManager"></param>
        public CacheCommand(ICacheManager cacheManager)
        {
            this._cacheManager = cacheManager;
        }

        /// <summary>
        /// 
        /// </summary>
        [CommandName("cache info")]
        [CommandHelp("cache info 获取Redis服务器信息")]
        public void GetServerInformation()
        {
            var redisCacheManager = this._cacheManager as RedisCacheManager;
            if (redisCacheManager.IsNull())
            {
                this.Context.Output.WriteLine(L("缓存 {0} 不支持获取服务器信息", this._cacheManager.ToString()));
                return;
            }

            var serverInformations = redisCacheManager.GetServerInformation();

            foreach (var item in serverInformations)
            {
                this.Context.Output.WriteLine("{0}\t:\t{1}".With(item.Key, item.Value));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pattern">匹配模式</param>
        [CommandName("cache keys")]
        [CommandHelp("cache keys [pattern] [pagesize] [pageindex] 获取Redis缓存键集合 [pattern] 参数示例：cache keys * 100 1 表示：查询所有缓存键，每页显示100条记录，显示第一页")]
        public void GetAllKeys(string pattern, int pageSize = 100, int pageIndex = 1)
        {
            var redisCacheManager = this._cacheManager as RedisCacheManager;
            if (redisCacheManager.IsNull())
            {
                this.Context.Output.WriteLine(L("缓存 {0} 不支持获取缓存键", this._cacheManager.ToString()));
                return;
            }

            var keys = redisCacheManager.GetKeys(pattern);

            IPagedList<string> pagedList = new PagedList<string>(keys, pageIndex - 1, pageSize, null);

            this.Context.Output.WriteLine(L("总缓存键数：{0}\t\t总页数：{1}\t当前页：{2}", pagedList.TotalCount, pagedList.TotalPages, pagedList.PageIndex + 1));
            this.Context.Output.WriteLine("--------------------------------------------------------------------");

            foreach (var item in pagedList)
            {
                this.Context.Output.WriteLine("{0}".With(item));
            }
        }
    }
}
