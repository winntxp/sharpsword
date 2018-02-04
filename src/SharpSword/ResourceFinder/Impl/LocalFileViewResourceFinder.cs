/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/16 9:20:51
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web;

namespace SharpSword.ResourceFinder.Impl
{
    /// <summary>
    /// 本地文件视图查找器
    /// </summary>
    public class LocalFileViewResourceFinder : ResourceFinderBase
    {
        /// <summary>
        /// 用于缓存所有系统框架的文本资源
        /// </summary>
        private static readonly Dictionary<string, string> CachedeLocalResources = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();
        private static bool _initializationed = false;
        private readonly HttpContextBase _httpContext;

        /// <summary>
        /// 视图文件保存的本地文件夹
        /// </summary>
        private const string ViewDirectory = "~/Views";

        /// <summary>
        /// 本地文件资源查找器
        /// </summary>
        /// <param name="httpContext">当前http请求上下文</param>
        public LocalFileViewResourceFinder(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
        }

        /// <summary>
        /// 获取所有查找器
        /// </summary>
        /// <returns></returns>
        public override IDictionary<string, string> GetResources()
        {
            //已经初始化了
            if (_initializationed)
            {
                return CachedeLocalResources;
            }

            using (new WriteLockDisposable(Locker))
            {
                //初始化了
                if (_initializationed)
                {
                    return CachedeLocalResources;
                }

                _initializationed = true;

                //获取物理路径
                string physicalPath = HostHelper.MapPath(ViewDirectory);

                //获取所有文件夹下面的文件
                if (Directory.Exists(physicalPath))
                {
                    //找出合法的后缀文件
                    var fiels = Directory.GetFiles(physicalPath, "*", SearchOption.AllDirectories)
                        .Where(fileName => this.SupportedFileExtensions
                                        .Any(ex => ex.Equals(Path.GetExtension(fileName), StringComparison.OrdinalIgnoreCase)))
                        .ToList();

                    //循环读取本地资源
                    foreach (var file in fiels)
                    {
                        //缓存里已经存在指定文件
                        if (CachedeLocalResources.ContainsKey(file))
                        {
                            continue;
                        }
                        //添加到缓存
                        using (StreamReader streamReader = new StreamReader(file))
                        {
                            CachedeLocalResources.Add(file, streamReader.ReadToEnd());
                        }
                    }
                }
            }

            //返回缓存本地资源文件
            return CachedeLocalResources;
        }

        /// <summary>
        /// 我们将本地文件系统设置为优先级比内嵌资源高，方便我们有相同的资源，优先使用本地文件
        /// </summary>
        public override int Priority { get { return 0; } }
    }
}
