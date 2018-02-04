/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/7 8:44:06
 * ****************************************************************/
using System.Reflection;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口框架版本
    /// </summary>
    public class ApiVersion
    {
        /// <summary>
        /// 当前框架版本
        /// </summary>
        private static string _version = Assembly.GetExecutingAssembly().GetName().Version.ToString();

        /// <summary>
        /// 获取当前程序集版本
        /// </summary>
        public static string Version
        {
            get
            {
                return _version;
            }
        }
    }
}
