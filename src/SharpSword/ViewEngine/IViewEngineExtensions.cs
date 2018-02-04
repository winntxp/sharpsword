/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/17/2016 12:50:53 PM
 * ****************************************************************/
using System.Text;

namespace SharpSword.ViewEngine
{
    /// <summary>
    /// 
    /// </summary>
    public static class IViewEngineExtensions
    {
        /// <summary>
        /// 编译视图文件并执行视图，默认使用UTF-8编译
        /// </summary>
        /// <param name="apiViewEngine">接口框架视图引擎接口</param>
        /// <param name="viewPath">视图文件路径，请输入绝对路径比如：g:\\temp\t.aspx</param>
        /// <param name="parameters">视图定义的参数集合</param>
        /// <returns>返回视图执行结果字符串</returns>
        public static string CompileByViewPath(this IViewEngine apiViewEngine, string viewPath, ViewParameterCollection parameters)
        {
            parameters.CheckNullThrowArgumentNullException(nameof(parameters));
            return apiViewEngine.CompileByViewPath(viewPath, parameters, Encoding.UTF8);
        }

        /// <summary>
        /// 编译视图文件并执行视图，默认使用UTF-8编译
        /// </summary>
        /// <param name="apiViewEngine">接口框架视图引擎接口</param>
        /// <param name="viewPath">视图文件路径，请输入绝对路径比如：g:\\temp\t.aspx</param>
        /// <param name="parameters">视图定义的参数集合</param>
        /// <returns></returns>
        public static string CompileByViewPath(this IViewEngine apiViewEngine, string viewPath, params ViewParameter[] parameters)
        {
            //构造参数集合
            ViewParameterCollection viewParameters = new ViewParameterCollection();

            //指定了参数
            if (!parameters.IsNull() && parameters.Length > 0)
            {
                viewParameters.AddRange(parameters);
            }

            //便于并执行原文件
            return apiViewEngine.CompileByViewPath(viewPath, viewParameters);
        }

        /// <summary>
        /// 编译视图文件并执行视图，默认使用UTF-8编译
        /// </summary>
        /// <param name="apiViewEngine">接口框架视图引擎接口</param>
        /// <param name="viewSource">视图文件源码</param>
        /// <param name="parameters">视图定义的参数集合</param>
        /// <returns>编译视图源代码，并将视图执行结果返回</returns>
        public static string CompileByViewSource(this IViewEngine apiViewEngine, string viewSource, ViewParameterCollection parameters)
        {
            parameters.CheckNullThrowArgumentNullException(nameof(parameters));
            return apiViewEngine.CompileByViewSource(viewSource, parameters, Encoding.UTF8);
        }

        /// <summary>
        /// 编译视图文件并执行视图，默认使用UTF-8编译
        /// </summary>
        /// <param name="apiViewEngine">接口框架视图引擎接口</param>
        /// <param name="viewSource">视图文件源码</param>
        /// <param name="parameters">视图定义的参数集合</param>
        /// <returns></returns>
        public static string CompileByViewSource(this IViewEngine apiViewEngine, string viewSource, params ViewParameter[] parameters)
        {
            //构造参数集合
            ViewParameterCollection viewParameters = new ViewParameterCollection();

            //指定了参数
            if (!parameters.IsNull() && parameters.Length > 0)
            {
                viewParameters.AddRange(parameters);
            }

            return apiViewEngine.CompileByViewSource(viewSource, viewParameters);
        }
    }
}
