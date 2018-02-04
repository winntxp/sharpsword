/* *******************************************************
 * SharpSword zhangliang4629@163.com 11/22/2016 2:30:33 PM
 * ****************************************************************/

namespace SharpSword.Localization
{
    /// <summary>
    /// 将制定的字符串翻译成系统设置的区域语言
    /// </summary>
    public interface ILocalizedStringManager : IDependency
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="cultureName"></param>
        /// <returns></returns>
        string GetLocalizedString(string text, string cultureName);
    }
}
