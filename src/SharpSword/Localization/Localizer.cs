/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/28 21:44:40
 * ****************************************************************/
using SharpSword.Localization;

namespace SharpSword
{
    /// <summary>
    /// 语言包读取委托(框架约定只要服务类定义了 public Localizer L{get;set;} 这样属性，系统框架会自动进行注入具体本地化器)
    /// </summary>
    /// <param name="text">语言包键(key)</param>
    /// <param name="args">参数值集合</param>
    /// <returns>返回指定键的语言包</returns>
    public delegate LocalizedString Localizer(string text, params object[] args);
}
