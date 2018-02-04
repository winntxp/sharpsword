/* *******************************************************
 * SharpSword zhangliang4629@163.com 11/22/2016 2:23:57 PM
 * ****************************************************************/
using System;

namespace SharpSword.Localization
{
    /// <summary>
    /// 用于我们本地委托，或者我们可以直接在框架里使用此接口来翻译语言包
    /// </summary>
    public interface ITextFormatter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text">待格式化的数据如：{0}错误信息{1}</param>
        /// <param name="args">格式化参数集合</param>
        /// <returns></returns>
        LocalizedString Get(string text, params object[] args);
    }
}
