/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/29 13:55:47
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 禁止打包成SDK访问;当接口加上此特性后，自动打包SDK的时候，将不会打包
    /// 一般在开发接口插件的时候可以使用此特性来包装不会将插件接口打包
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = false)]
    public sealed class DisablePackageSdkAttribute : Attribute
    {
    }
}
