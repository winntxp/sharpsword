/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/30/2015 3:03:56 PM
 * ****************************************************************/
using System;

namespace SharpSword
{
    /// <summary>
    /// 系统默认的请求JSON反序列化对象;在无须上送DTO参数的时候，可以使用此类
    /// </summary>
    [Serializable]
    public class NullRequestDto : RequestDtoBase { }
}
