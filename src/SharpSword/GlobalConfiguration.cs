/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/13 15:08:29
 * ****************************************************************/
using System;
using System.Collections.Concurrent;

namespace SharpSword
{
    /// <summary>
    /// 系统框架配置类
    /// </summary>
    public class GlobalConfiguration
    {
        /// <summary>
        /// 构造一个默认的自定义数据记录容器(方便其他扩展存储全局数据)
        /// </summary>
        private readonly ConcurrentDictionary<string, object> _moduleAdditionDatas = new ConcurrentDictionary<string, object>();

        /// <summary>
        /// 
        /// </summary>
        private static GlobalConfiguration _instance = new GlobalConfiguration();

        /// <summary>
        /// 获取系统配置全局唯一实例
        /// </summary>
        public static GlobalConfiguration Instance
        {
            get { return _instance; }
        }

        /// <summary>
        /// 构造函数已经设置参数值
        /// </summary>
        private GlobalConfiguration()
        {
            this.ServerName = string.Format("sharpsword-server-{0}", Guid.NewGuid().ToString("N"));
            this.ValidUserIdAndUserNameFun = (requestDto) => !requestDto.IsNull() &&
                                                             !requestDto.UserId.IsNullOrEmpty() &&
                                                             !requestDto.UserName.IsNullOrEmpty();
        }

        /// <summary>
        /// 服务器名称，使用分布式的时候，这个比较有用，设置服务器名称，便于调试发现那台服务器出现问题
        /// 默认名称为：sharpsword-server-{Guid}
        /// </summary>
        public string ServerName { get; set; }

        /// <summary>
        /// 验证UserId和用户名称是否合法(因为框架系统无法判断当前接口系统的用户ID和用户名称如何判断才算合法的，所以需要外部来指定一个委托判断)
        /// </summary>
        public Func<IRequiredUser, bool> ValidUserIdAndUserNameFun { get; set; }

        /// <summary>
        /// 当前接口站点服务器地址比如：www.domain.com，不带http和任何/字符。
        /// </summary>
        public string HttpHost { get; set; }

        /// <summary>
        /// 用于保存其他全局数据，比如扩展插件保存数据等（全局）
        /// </summary>
        internal ConcurrentDictionary<string, object> Properties => this._moduleAdditionDatas;
    }
}
