/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/12/2016 2:03:49 PM
 * ****************************************************************/
using System;
using System.ComponentModel;

namespace SharpSword.DynamicApi
{
    /// <summary>
    /// 动态API运行模式
    /// </summary>
    [Flags]
    public enum WorkMode
    {
        /// <summary>
        /// 自动运行模式，不会输出任何信息，接口会自动映射到当前应用程序域
        /// </summary>
        [Description("动态模式")]
        Dynamic = 1,

        /// <summary>
        /// 开发模式，此模式下，只会输出接口类，不会主动将接口加载到当前应用程序域
        /// </summary>
        [Description("开发模式")]
        Develop = 1 << 1
    }
}
