/* ****************************************************************
 * SharpSword zhangliang4629@163.com 11/22/2016 1:53:32 PM
 * ****************************************************************/

namespace SharpSword.Localization
{
    /// <summary>
    /// 默认的本地化转换
    /// </summary>
    public static class NullLocalizer
    {
        /// <summary>
        /// 
        /// </summary>
        static readonly Localizer _instance;

        /// <summary>
        /// 我们直接创建一个返回系统框架以及业务定义的格式化字符串，不做任何本地翻译
        /// </summary>
        static NullLocalizer()
        {
            _instance = (format, args) => new LocalizedString((args.IsNull() || args.Length == 0) ? format : string.Format(format, args));
        }

        /// <summary>
        /// 
        /// </summary>
        public static Localizer Instance { get { return _instance; } }
    }
}
