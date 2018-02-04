/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System;
using System.ComponentModel;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口输出模式
    /// </summary>
    [Flags]
    public enum ResponseFormat
    {
        /// <summary>
        /// 序列化成XML格式
        /// </summary>
        [Description("XML")]
        XML = 1,

        /// <summary>
        /// 序列化成JSON格式
        /// </summary>
        [Description("JSON")]
        JSON = 2,

        /// <summary>
        /// 使用视图模板显示
        /// </summary>
        [Description("VIEW")]
        VIEW = 4
    }
}