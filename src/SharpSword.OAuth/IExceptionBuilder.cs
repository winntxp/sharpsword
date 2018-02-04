/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/

namespace SharpSword.OAuth
{
    /// <summary>
    /// Exception建造者接口。
    /// </summary>
    public interface IExceptionBuilder
    {
        /// <summary>
        /// 创建一个Exception实例，该实例封装返回的错误消息。
        /// </summary>
        /// <param name="code">主错误码。</param>
        /// <param name="description">主错误描述。</param>
        /// <param name="subCode">子错误码。</param>
        /// <param name="subDescription">子错误描述。</param>
        OAuthException Create(string code, string description, string subCode = "", string subDescription = "");
    }

}