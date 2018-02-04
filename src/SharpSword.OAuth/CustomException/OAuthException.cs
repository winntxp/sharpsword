/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;

namespace SharpSword.OAuth
{
    /// <summary>
    /// 表示Etp返回的错误信息
    /// </summary>
    public sealed class OAuthException : Exception
    {
        /// <summary>
        /// 创建EtpException实例。
        /// </summary>
        private OAuthException() { }

        /// <summary>
        /// 获取主错误码。
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// 获取主错误描述。
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// 获取子错误码。
        /// </summary>
        public string SubCode { get; private set; }

        /// <summary>
        /// 获取子错误码描述。
        /// </summary>
        public string SubDescription { get; private set; }

        /// <summary>
        /// 获取错误类型。
        /// </summary>
        public ErrorType ErrorType { get; private set; }

        /// <summary>
        /// 获取一个值，该值指示错误发生后是否可重试。
        /// </summary>
        public bool Retriable { get; private set; }

        /// <summary>
        ///获取异常信息。
        ///异常消息格式为：子错误描述（错误码：主错误码，子错误码）。
		/// </summary>
		public override string Message
        {
            get { return string.Format("{0}(错误码:{1},{2})", SubDescription, Code, SubCode); }
        }

        /// <summary>
        /// 创建一个EtpException实例，该实例表示调用Api时发生了业务级异常。
        /// </summary>
        internal static OAuthException CreateBusinessException()
        {
            OAuthException etpException = new OAuthException();
            //设置业务级异常
            etpException.ErrorType = ErrorType.BusinessError;
            return etpException;
        }

        /// <summary>
        /// 创建一个EtpException实例，该实例表示调用Api时发生了应用级异常。
        /// </summary>
        internal static OAuthException CreateApplicationException()
        {
            OAuthException etpException = new OAuthException();
            //设置应用级异常
            etpException.ErrorType = ErrorType.ApplicationError;
            return etpException;
        }

        /// <summary>
        /// 创建一个EtpException实例，该实例表示调用Api时发生了平台级异常。
        /// </summary>
        /// <param name="retriable">指示调用方是否可以重新尝试调用。</param>
        internal static OAuthException CreatePlatformException(bool retriable)
        {
            OAuthException etpException = new OAuthException();
            //设置是否重试
            etpException.Retriable = retriable;
            //设置平台及异常
            etpException.ErrorType = ErrorType.PlatformError;
            return etpException;
        }

        /// <summary>
        /// 设置主错误信息。
        /// </summary>
        /// <param name="code">主错误码。</param>
        /// <param name="description">主错误描述。</param>
        internal void SetError(string code, string description)
        {
            //设置主错误码
            this.Code = code;
            //设置主错误信息
            this.Description = description;
        }

        /// <summary>
        /// 设置子错误信息。
        /// </summary>
        /// <param name="subCode">子错误码。</param>
        /// <param name="subDescription">子错误描述。</param>
        internal void SetSubError(string subCode, string subDescription)
        {
            //设置子错误码
            this.SubCode = subCode;
            //设置子错误信息
            this.SubDescription = subDescription;
        }
    }

}