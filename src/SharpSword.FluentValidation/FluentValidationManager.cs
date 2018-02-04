/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/21/2016 10:09:38 AM
 * ****************************************************************/

namespace SharpSword.FluentValidation
{
    /// <summary>
    /// 配置管理器
    /// </summary>
    internal class FluentValidationManager
    {
        /// <summary>
        /// 
        /// </summary>
        private static FluentValidationCollection _instance = new FluentValidationCollection();

        /// <summary>
        /// 返回所有的验证配置信息
        /// </summary>
        public static FluentValidationCollection Configs => _instance;
    }
}
