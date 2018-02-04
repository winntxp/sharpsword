/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 14:53:46
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 白名单集合；一旦定义了白名单，那么只能在白名单里面的IP地址才能访问，如果未定义，那么全部IP都可以访问
    /// 一般配置在Global.asax文件里，应用程序启动的时候就加载，在运行时，最好不要添加白名单，可能会涉及到并发问题
    /// </summary>
    public class WhiteIpManager
    {
        /// <summary>
        /// 将白名单保存在静态全局缓存里
        /// </summary>
        private static readonly WhiteIpCollection Instance = new WhiteIpCollection();

        /// <summary>
        /// 返回接口配置表
        /// </summary>
        public static WhiteIpCollection Ips
        {
            get
            {
                return Instance;
            }
        }
    }
}