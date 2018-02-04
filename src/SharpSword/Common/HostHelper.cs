/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/25 13:23:07
 * ****************************************************************/
using System;
using System.IO;
using System.Web;
using System.Web.Hosting;

namespace SharpSword
{
    /// <summary>
    /// 主机帮助
    /// </summary>
    public class HostHelper
    {
        /// <summary>
        /// 获取当前运行环境的bin文件夹物理路径
        /// </summary>
        /// <returns>获取当前运行环境的bin文件夹物理路径，如："c:\inetpub\wwwroot\bin"</returns>
        public static string GetBinDirectory()
        {
            return HostingEnvironment.IsHosted ? HttpRuntime.BinDirectory : AppDomain.CurrentDomain.BaseDirectory;
        }

        /// <summary>
        /// 映射虚拟路径到实际物理路径
        /// </summary>
        /// <param name="path">路径，如： "~/SharpSword/reademe.txt"，或者:E:\SharpSword\reademe.txt </param>
        /// <returns>返回物理路径，如： "c:\inetpub\wwwroot\bin"</returns>
        public static string MapPath(string path)
        {
            //我们将含有如：E:\SharpSword\readme.txt样式的路径，直接返回
            if (path.Contains(@":\"))
            {
                return path;
            }

            //比如站点
            if (HostingEnvironment.IsHosted)
            {
                return HostingEnvironment.MapPath(path);
            }

            //比如win程序
            string baseDirectory = GetBinDirectory();
            path = path.Replace("~/", "").TrimStart('/').Replace('/', '\\');
            return Path.Combine(baseDirectory, path);
        }

        /// <summary>
        /// 尝试重写下web.config时间，用于重启应用程序域
        /// </summary>
        /// <returns></returns>
        public static bool TryWriteWebConfig()
        {
            try
            {
                File.SetLastWriteTimeUtc(MapPath("~/web.config"), DateTime.UtcNow);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 尝试重写下Global.asax时间，用于重启应用程序域
        /// </summary>
        /// <returns></returns>
        public static bool TryWriteGlobalAsax()
        {
            try
            {
                File.SetLastWriteTimeUtc(MapPath("~/global.asax"), DateTime.UtcNow);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
