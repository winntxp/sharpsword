/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/9 8:41:11
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 系统默认的Action查找器
    /// </summary>
    public class DefaultActionSelector : IActionSelector
    {
        /// <summary>
        /// 缓存所有的action接口，便于快速检索方法，提高接口检索速度;忽略大小写
        /// </summary>
        private static readonly IList<ActionDescriptor> CachedActionDescriptors = new List<ActionDescriptor>();
        /// <summary>
        /// 用于保存对应的接口名称和版本，方便快速的判断接口是否存在
        /// </summary>
        private static readonly IList<string> CachedActionNames = new List<string>();
        private static bool _initializationed = false;
        private static readonly object Locker = new object();
        /// <summary>
        /// 
        /// </summary>
        private readonly ITypeFinder _typeFinder;
        private readonly GlobalConfiguration _globalConfiguration;
        private readonly ApiConfiguration _apiConfig;

        /// <summary>
        /// 系统默认的Action查找器
        /// </summary>
        /// <param name="typeFinder">类型查找接口</param>
        /// <param name="globalConfiguration">系统框架参数</param>
        /// <param name="apiConfig"></param>
        public DefaultActionSelector(ITypeFinder typeFinder, GlobalConfiguration globalConfiguration, ApiConfiguration apiConfig)
        {
            typeFinder.CheckNullThrowArgumentNullException(nameof(typeFinder));
            globalConfiguration.CheckNullThrowArgumentNullException(nameof(globalConfiguration));
            apiConfig.CheckNullThrowArgumentNullException(nameof(apiConfig));
            this._typeFinder = typeFinder;
            this._globalConfiguration = globalConfiguration;
            this._apiConfig = apiConfig;
        }

        /// <summary>
        /// 获取所有的合法接口
        /// </summary>
        /// <returns>返回所有合法的接口ActionDescriptor集合</returns>
        /// <param name="skipSystemActions">是否跳过系统框架接口</param>
        public virtual IEnumerable<ActionDescriptor> GetActionDescriptors(bool skipSystemActions = false)
        {
            //还未初始化
            if (_initializationed)
            {
                return CachedActionDescriptors;
            }

            lock (Locker)
            {
                if (_initializationed)
                {
                    return CachedActionDescriptors;
                }

                //已经初始化了标志
                _initializationed = true;

                //筛选接口
                this._typeFinder.FindClassesOfType(typeof(IAction))
                    //必须要继承自ActionBase抽象基类，将过期的接口直接忽略掉
                    .Where(type => type.IsAssignableToActionBase()).OrderBy(x => x.Name).ToList().ForEach(type =>
                    {
                        //创建一个接口描述对象
                        var actionDescriptor = new ReflectedActionDescriptor(type).GetActionDescriptor();

                        //已经注销
                        if (actionDescriptor.IsObsolete)
                        {
                            return;
                        }

                        //接口名称
                        string actionName = actionDescriptor.ActionName;

                        //查询缓存里是否存在了
                        var existsActionDescriptor = CachedActionDescriptors.FirstOrDefault(x =>
                                            x.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase)
                                            &&
                                            x.Version == actionDescriptor.Version);

                        //不存在就添加到缓存
                        if (existsActionDescriptor.IsNull())
                        {
                            //添加到全局接口描述缓存
                            CachedActionDescriptors.Add(actionDescriptor);

                            //添加到接口名称缓存，方便快速判断是否存在接口以及指定的版本
                            CachedActionNames.Add("{0}${1}".With(actionName, actionDescriptor.Version));
                        }
                        else
                        {
                            //还原下操作
                            _initializationed = false;
                            CachedActionDescriptors.Clear();
                            CachedActionNames.Clear();
                            //已经存在了键（实现类配置了相同的接口名称）；直接抛出异常，便于在开发期就发现问题
                            throw new SharpSwordCoreException(Resource.CoreResource.DefaultActionSelector_FoundMoreThenOneActionName
                                .With(actionName, type.FullName, existsActionDescriptor.ActionType.FullName, actionDescriptor.Version));
                        }
                    });
            }

            //返回接口集合
            return CachedActionDescriptors;
        }

        /// <summary>
        /// 根据接口名称获取接口信息
        /// </summary>
        /// <param name="actionName">接口名称，忽略大小写</param>
        /// <returns>返回指定接口名称下面的所有接口版本</returns>
        public IEnumerable<ActionDescriptor> GetActionDescriptors(string actionName)
        {
            return this.GetActionDescriptors().Where(o => o.ActionName.Equals(actionName, StringComparison.OrdinalIgnoreCase)).ToList();
        }

        /// <summary>
        /// 根据接口名称获取接口描述对象
        /// </summary>
        /// <param name="actionName">接口名称</param>
        /// <param name="version">接口版本，版本设置为空或者null，框架将会使用同名接口版本号最大的接口</param>
        /// <returns>如果未找到则返回null</returns>
        public ActionDescriptor GetActionDescriptor(string actionName, string version)
        {
            //接口不存在直接返回null
            if (actionName.IsNullOrEmpty())
            {
                return null;
            }

            //获取接口所有版本
            var actionDescriptors = this.GetActionDescriptors(actionName);

            //为找到任何版本信息
            if (actionDescriptors.IsNull() || actionDescriptors.IsEmpty())
            {
                return null;
            }

            //未指定版本号，直接获取指定接口最新的版本
            if (version.IsNullOrEmpty())
            {
                return this.GetHighestActionDescriptor(actionDescriptors);
            }

            //指定了版本号，就直接查找执行版本即可
            var actionDescriptor = actionDescriptors.FirstOrDefault(o => o.Version == version);

            //指定版本未找到且设置了搜索最高版本的配置，就获取最高版本
            if (actionDescriptor.IsNull() && this._apiConfig.DefaultActionVersionFailToHighestActionVersion)
            {
                actionDescriptor = this.GetHighestActionDescriptor(actionDescriptors);
            }

            //返回接口描述对象
            return actionDescriptor;
        }

        /// <summary>
        /// 从接口描述集合里获取到最新版本
        /// </summary>
        /// <param name="actionDescriptors">接口描述对象集合</param>
        /// <returns>返回指定接口描述集合里版本最高的接口描述对象</returns>
        private ActionDescriptor GetHighestActionDescriptor(IEnumerable<ActionDescriptor> actionDescriptors)
        {
            return actionDescriptors.OrderByDescending(o => o.Version).FirstOrDefault();
        }

        /// <summary>
        /// 重新清空下接口缓存
        /// </summary>
        public void Reset()
        {
            CachedActionDescriptors.Clear();
            _initializationed = false;
            this.GetActionDescriptors();
        }
    }
}