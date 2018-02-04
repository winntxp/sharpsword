/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/18 17:44:00
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 禁止进行加解密传输，加上此特性后，接口将不会走IApiSecurity接口流程
    /// 上送数据和下送数据都直接走原始方式
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class DisableDataSignatureTransmissionAttribute : Attribute
    {
    }
}
