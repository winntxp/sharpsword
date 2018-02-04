/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/24 12:39:27
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// 请求参数业务逻辑验证返回错误信息对象
    /// </summary>
    public class DtoValidatorResultError
    {
        /// <summary>
        /// 请求参数业务逻辑验证返回错误信息对象
        /// </summary>
        /// <param name="memberName">参数名称</param>
        /// <param name="errorMessage">错误消息</param>
        public DtoValidatorResultError(string memberName, string errorMessage)
        {
            this.MemberName = memberName;
            this.ErrorMessage = errorMessage;
        }

        /// <summary>
        /// 默认错误信息为:参数错误
        /// </summary>
        /// <param name="memberName">参数名称</param>
        public DtoValidatorResultError(string memberName)
            : this(memberName, "参数校验不通过")
        {
        }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string MemberName { get; private set; }

        /// <summary>
        /// 错误消息
        /// </summary>
        public string ErrorMessage { get; private set; }
    }
}
