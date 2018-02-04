/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 序列化输出器激活器
    /// </summary>
    public interface IMediaTypeFormatterFactory
    {
        /// <summary>
        /// 根据格式化枚举类型，创建对应的输出器
        /// </summary>
        /// <param name="format">输出格式化枚举</param>
        /// <returns>根据指定格式返回指定格式对应的序列化器</returns>
        IMediaTypeFormatter Create(ResponseFormat format);
    }
}