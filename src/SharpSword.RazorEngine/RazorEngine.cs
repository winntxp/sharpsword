/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/17/2016 1:14:04 PM
 * ****************************************************************/
using SharpSword.ViewEngine;
using RazorEngine;
using RazorEngine.Templating;
using System.IO;

namespace SharpSword.RazorEngine
{
    /// <summary>
    /// 
    /// </summary>
    public class RazorEngine : IViewEngine
    {
        /// <summary>
        /// 
        /// </summary>
        public string SupportedExtension
        {
            get
            {
                return ".cshtml";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewPath"></param>
        /// <param name="parameters"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public string CompileByViewPath(string viewPath, IViewParameterCollection parameters, System.Text.Encoding encode)
        {
            return this.CompileByViewSource(File.ReadAllText(viewPath), parameters, encode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewSource"></param>
        /// <param name="parameters"></param>
        /// <param name="encode"></param>
        /// <returns></returns>
        public string CompileByViewSource(string viewSource, IViewParameterCollection parameters, System.Text.Encoding encode)
        {
            return Engine.Razor.RunCompile(viewSource, "TempKey_".With(MD5.Encrypt(viewSource)), null, parameters);
        }
    }
}

