/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/13/2016 9:06:47 AM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SharpSword.ViewEngine.Impl
{
    /// <summary>
    /// 默认视图引擎管理器
    /// </summary>
    public class DefaultViewEngineManager : IViewEngineManager
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger<DefaultViewEngineManager> Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewEngines">视图引擎集合</param>
        public DefaultViewEngineManager(IEnumerable<IViewEngine> viewEngines)
        {
            viewEngines.CheckNullThrowArgumentNullException(nameof(viewEngines));
            this.ViewEngines = viewEngines;
            this.Logger = GenericNullLogger<DefaultViewEngineManager>.Instance;
        }

        /// <summary>
        /// 获取当前系统注册的所有视图引擎
        /// </summary>
        public IEnumerable<IViewEngine> ViewEngines { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewPath">视图文件路径，请输入绝对路径比如：g:\\temp\t.aspx</param>
        /// <param name="parameters">视图模板参数</param>
        /// <param name="encode">视图编码格式</param>
        /// <returns></returns>
        public string CompileByViewPath(string viewPath, IViewParameterCollection parameters, Encoding encode)
        {
            //获取指定文件扩展名
            var fileExtension = Path.GetExtension(viewPath);

            //根据模板文件后缀来筛选合适的编译引擎
            var viewEngine = this.ViewEngines.FirstOrDefault(o => o.SupportedExtension.Equals(fileExtension, StringComparison.OrdinalIgnoreCase));
            if (viewEngine.IsNull())
            {
                throw new SharpSwordCoreException(Resource.CoreResource.DefaultViewEngineManager_IViewEngine_NotFound0.With(fileExtension));
            }

            //返回编译后的视图
            return viewEngine.CompileByViewPath(viewPath, parameters, encode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewSource">视图模板源代码</param>
        /// <param name="parameters">视图模板参数</param>
        /// <param name="encode">视图编码格式</param>
        /// <returns></returns>
        public string CompileByViewSource(string viewSource, IViewParameterCollection parameters, Encoding encode)
        {
            //随机抽取一个视图引擎
            var viewEngine = this.ViewEngines.FirstOrDefault();
            if (viewEngine.IsNull())
            {
                throw new SharpSwordCoreException(Resource.CoreResource.DefaultViewEngineManager_IViewEngine_NotFound1);
            }
            //返回编译执行后的视图
            return viewEngine.CompileByViewSource(viewSource, parameters, encode);
        }
    }
}
