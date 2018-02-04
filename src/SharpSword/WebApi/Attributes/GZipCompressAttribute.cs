/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/24 12:43:56
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口明确指定使用GZip方式压缩数据(系统会根据HTTP头来确定是否使用)
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class GZipCompressAttribute : Attribute { }
}
