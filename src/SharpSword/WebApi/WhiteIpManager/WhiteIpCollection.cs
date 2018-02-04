/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/15 18:57:46
 * ****************************************************************/
using System.Collections.ObjectModel;
using System.Linq;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 白名单系统配置表
    /// </summary>
    public class WhiteIpCollection : Collection<string>
    {
        /// <summary>
        /// 添加一批白名单
        /// </summary>
        /// <param name="ips">白名单</param>
        public void Add(params string[] ips)
        {
            if (ips.IsNull())
            {
                return;
            }
            foreach (var ip in ips.Where(ip => !this.Contains(ip)))
            {
                base.Add(ip);
            }
        }

        /// <summary>
        /// 删除一批白名单
        /// </summary>
        /// <param name="ips">白名单</param>
        public void Remove(params string[] ips)
        {
            if (ips.IsNull())
            {
                return;
            }
            foreach (var item in ips)
            {
                base.Remove(item);
            }
        }

        /// <summary>
        /// 检测指定IP是否有权限访问接口系统
        /// </summary>
        /// <param name="ip">待检测IP地址</param>
        /// <returns>IP地址是否在白名单里</returns>
        public bool IsValid(string ip)
        {
            //设置了白名单，需要判断是否在定义的白名单里面
            return 0 == this.Count || this.Contains(ip);
        }
    }
}
