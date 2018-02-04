/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/22/2016 3:26:33 PM
 * ****************************************************************/
using SharpSword.Commands;
using SharpSword.Serializers;
using System;

namespace SharpSword.Caching.Commands
{
    /// <summary>
    /// 缓存模块提供的命令行
    /// </summary>
    public class CacheCommand : CommandHandlerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ICacheManager _cacheManager;
        private readonly IJsonSerializer _josnSerializer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cacheManager"></param>
        /// <param name="josnSerializer"></param>
        public CacheCommand(ICacheManager cacheManager, IJsonSerializer josnSerializer)
        {
            this._cacheManager = cacheManager;
            this._josnSerializer = josnSerializer;
            this.Model = "a";
        }

        /// <summary>
        /// 清空所有缓存
        /// </summary>
        [CommandName("cache clear")]
        [CommandHelp("cache clear \r\n\t 清空缓存服务器所有缓存")]
        public void Clear()
        {
            this._cacheManager.Clear();
            this.Context.Output.WriteLine(L("清空缓存操作完成"));
        }

        /// <summary>
        /// 移除缓存键 匹配模式 a:精确匹配 p:正则匹配
        /// </summary>
        [CommandSwitch]
        public string Model { get; set; }

        /// <summary>
        /// 删除指定缓存
        /// </summary>
        /// <param name="cacheKey"></param>
        [CommandName("cache remove")]
        [CommandHelp("cache remove [cachekey] /model:[p|a] \r\n\t 通过缓存键删除缓存，参数，p:代表模糊匹配删除，a:代表精确匹配删除")]
        [CommandSwitches("Model")]
        public void Remove(string cacheKey)
        {
            if (this.Model.Equals("p", StringComparison.OrdinalIgnoreCase))
            {
                this._cacheManager.RemoveByPattern(cacheKey);
            }
            else
            {
                this._cacheManager.Remove(cacheKey);
            }
            this.Context.Output.WriteLine(L("删除缓存键 {0} 成功", cacheKey));
        }

        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="cachekey"></param>
        /// <returns></returns>
        [CommandHelp("cache get [cachekey] \r\n\t 通过缓存键获取缓存")]
        [CommandName("cache get")]
        public void Get(string cachekey)
        {
            var obj = this._cacheManager.Get<object>(cachekey);
            if (obj.IsNull())
            {
                this.Context.Output.WriteLine(L("缓存键 {0} 不存在", cachekey));
                return;
            }
            this.Context.Output.WriteLine(this._josnSerializer.Serialize(obj));
        }
    }
}
