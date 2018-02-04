/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/13/2016 9:46:33 AM
 * ****************************************************************/
using System.Text;

namespace SharpSword.ViewEngine
{
    /// <summary>
    /// 视图引擎管理器扩展类
    /// </summary>
    public static class IViewEngineManagerExtensions
    {
        /// <summary>
        /// 根据视图路径编译视图
        /// </summary>
        /// <param name="viewEngineManager">视图引擎管理器</param>
        /// <param name="viewPath">视图文件路径，比如：g:\\temp\t.aspx 或者 ~/temp/t.aspx</param>
        /// <param name="encode">视图文件编码</param>
        /// <param name="parameters">视图模板参数</param>
        /// <returns>返回编译执行后的视图</returns>
        public static string CompileByViewPath(this IViewEngineManager viewEngineManager, string viewPath, Encoding encode, params ViewParameter[] parameters)
        {
            encode.CheckNullThrowArgumentNullException(nameof(encode));
            viewPath.CheckNullThrowArgumentNullException(nameof(viewPath));
            //构造参数集合
            var viewParameters = new ViewParameterCollection(parameters ?? new ViewParameter[] { });
            return viewEngineManager.CompileByViewPath(viewPath, viewParameters, encode);
        }

        /// <summary>
        /// 根据视图路径编译视图,注意：默认编码为UTF-8
        /// </summary>
        /// <param name="viewEngineManager">视图引擎管理器</param>
        /// <param name="viewPath">视图文件路径，比如：g:\\temp\t.aspx 或者 ~/temp/t.aspx</param>
        /// <param name="parameters">视图模板参数</param>
        /// <returns>返回编译执行后的视图</returns>
        public static string CompileByViewPath(this IViewEngineManager viewEngineManager, string viewPath, params ViewParameter[] parameters)
        {
            return viewEngineManager.CompileByViewPath(viewPath, Encoding.UTF8, parameters);
        }

        /// <summary>
        /// 根据视图源代码编译视图
        /// </summary>
        /// <param name="viewEngineManager">视图引擎管理器</param>
        /// <param name="viewSource">视图模板源代码</param>
        /// <param name="encode">视图文件编码</param>
        /// <param name="parameters">视图模板参数</param>
        /// <returns>返回编译执行后的视图</returns>
        public static string CompileByViewSource(this IViewEngineManager viewEngineManager, string viewSource, Encoding encode, params ViewParameter[] parameters)
        {
            encode.CheckNullThrowArgumentNullException(nameof(encode));
            viewSource.CheckNullThrowArgumentNullException(nameof(viewSource));
            //构造参数集合
            var viewParameters = new ViewParameterCollection(parameters ?? new ViewParameter[] { });
            return viewEngineManager.CompileByViewSource(viewSource, viewParameters, encode);
        }

        /// <summary>
        /// 根据视图源代码编译视图
        /// </summary>
        /// <param name="viewEngineManager">视图引擎管理器</param>
        /// <param name="viewSource">视图模板源代码</param>
        /// <param name="parameters">视图模板参数</param>
        /// <returns>返回编译执行后的视图</returns>
        public static string CompileByViewSource(this IViewEngineManager viewEngineManager, string viewSource, params ViewParameter[] parameters)
        {
            return viewEngineManager.CompileByViewSource(viewSource, Encoding.UTF8, parameters);
        }
    }
}
