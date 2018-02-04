/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 12:43:56
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口明确指定使用默认的压缩方式
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DeflateCompressAttribute : Attribute { }
}
