/* **************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/27/2016 1:58:26 PM
 * **************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// GUID创建器
    /// </summary>
    public interface IGuidGenerator
    {
        /// <summary>
        /// 获取一个新的GUID
        /// </summary>
        /// <returns></returns>
        Guid Create();
    }
}
