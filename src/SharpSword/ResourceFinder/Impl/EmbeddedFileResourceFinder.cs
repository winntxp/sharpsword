/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/29 11:17:51
 * ****************************************************************/
using SharpSword.WebApi;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharpSword.ResourceFinder.Impl
{
    /// <summary>
    /// 模块内嵌视图查找器
    /// </summary>
    public class EmbeddedFileResourceFinder : ResourceFinderBase
    {
        /// <summary>
        /// 用于缓存所有程序集内嵌资源文件信息
        /// key:视图路径，value:视图源代码
        /// </summary>
        private static readonly Dictionary<string, string> CachedManifestResources = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        private static bool _initializationed = false;
        private static readonly object Locker = new object();
        private readonly IActionSelector _actionSelector;

        /// <summary>
        /// 默认的内嵌视图查找器
        /// </summary>
        /// <param name="actionSelector">接口查找器</param>
        public EmbeddedFileResourceFinder(IActionSelector actionSelector)
        {
            actionSelector.CheckNullThrowArgumentNullException(nameof(actionSelector));
            this._actionSelector = actionSelector;
        }

        /// <summary>
        /// 内嵌图片扩展名
        /// </summary>
        private readonly string[] _supportedLogoImageExtensions = new string[] { ".jpg", ".png", ".gif", ".ico" };

        /// <summary>
        /// 允许读取内嵌的图片信息
        /// </summary>
        protected override string[] SupportedFileExtensions
        {
            get
            {
                return base.SupportedFileExtensions.Concat(this._supportedLogoImageExtensions).ToArray();
            }
        }

        /// <summary>
        /// 获取所有程序集的内嵌视图文件；惰性加载，第一次获取的时候加载
        /// </summary>
        /// <returns></returns>
        public override IDictionary<string, string> GetResources()
        {
            //还未初始化
            if (_initializationed)
            {
                return CachedManifestResources;
            }
            lock (Locker)
            {
                if (_initializationed)
                {
                    return CachedManifestResources;
                }

                //已经初始化了标志
                _initializationed = true;

                //获取所有接口信息
                var actions = this._actionSelector.GetActionDescriptors();

                //获取所有程序集(搜索所有的接口并且使用接口所在程序集来进行归组)
                var assemblys = (from action in actions group action by action.ActionType.Assembly into g select g.Key).ToList();

                //连接下插件所在程序集
                assemblys = assemblys.Concat(PluginManager.GetApiPlugins().Select(o => o.GetType().Assembly)).ToList();

                //筛选排除重复
                assemblys = (assemblys.GroupBy(assembly => assembly).Select(g => g.Key)).OrderBy(o => o.FullName).ToList();

                //循环接口所有程序集
                foreach (var assembly in assemblys)
                {
                    //获取程序集内嵌资源名称
                    var resourceNames = assembly.GetManifestResourceNames();

                    //循环内嵌资源
                    foreach (var resourceName in resourceNames)
                    {
                        //已经存在缓存
                        if (CachedManifestResources.ContainsKey(resourceName))
                        {
                            continue;
                        }

                        //循环指定的扩展名，不在扩展名之中的，不加载到视图缓存集合
                        foreach (var fileExtension in this.SupportedFileExtensions)
                        {
                            //视图文件必须以指定扩展名结尾
                            if (!resourceName.EndsWith(fileExtension, StringComparison.OrdinalIgnoreCase))
                            {
                                continue;
                            }

                            //读取接口所在程序集内嵌资源文件
                            var resourceStream = assembly.GetManifestResourceStream(resourceName);

                            //读取的资源项目是否为null
                            if (resourceStream.IsNull())
                            {
                                continue;
                            }

                            //图片资源(图片都是一些小图片)
                            if (this._supportedLogoImageExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
                            {
                                var resourceStreamBytes = new byte[resourceStream.Length];
                                resourceStream.Read(resourceStreamBytes, 0, (int)resourceStream.Length);
                                //转换成base64保存
                                var viewSource = resourceStreamBytes.ToBase64();
                                //将原文件添加到缓存
                                CachedManifestResources.Add(resourceName, viewSource);
                                resourceStream.Close();
                            }
                            else
                            {
                                //读取内嵌资源文件
                                using (var streamReader = new StreamReader(resourceStream))
                                {
                                    //获取全部源文件
                                    var viewSource = streamReader.ReadToEnd();
                                    //将原文件添加到缓存
                                    CachedManifestResources.Add(resourceName, viewSource);
                                }
                            }
                        }
                    }
                }
            }

            //返回所有视图
            return CachedManifestResources;
        }
    }
}
