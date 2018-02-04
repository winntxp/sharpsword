/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/17/2016 2:44:00 PM
 * ****************************************************************/

namespace SharpSword.Commands
{
    /// <summary>
    /// 执行命令返回code值
    /// </summary>
    public enum CommandReturnCodes
    {
        /// <summary>
        /// 执行命令成功
        /// </summary>
        Ok = 0,

        /// <summary>
        /// 执行命令失败
        /// </summary>
        Fail = 5,

        /// <summary>
        /// 需要进行重试
        /// </summary>
        Retry = 240
    }
}
