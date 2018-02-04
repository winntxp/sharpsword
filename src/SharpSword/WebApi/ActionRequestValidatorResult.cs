/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/1/16 9:08:54
 * ****************************************************************/
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口合法性调用校验结果对象
    /// </summary>
    [Serializable]
    public class ActionRequestValidatorResult
    {
        /// <summary>
        /// 表示成功
        /// </summary>
        public static ActionRequestValidatorResult Success
        {
            get
            {
                return new ActionRequestValidatorResult(new ActionResult() { Flag = ActionResultFlag.SUCCESS, Info = "OK" }, true);
            }
        }

        /// <summary>
        /// 初始化接口合法性对象
        /// </summary>
        /// <param name="actionResult">接口返回对象</param>
        /// <param name="isValid">是否验证成功</param>
        public ActionRequestValidatorResult(ActionResult actionResult, bool isValid)
        {
            this.ActionResult = actionResult;
            this.IsValid = isValid;
        }

        /// <summary>
        /// 返回的ActionResult对象，成功或者失败都会返回
        /// </summary>
        public ActionResult ActionResult { get; private set; }

        /// <summary>
        /// 接口调用合法性调用是否通过
        /// </summary>
        public bool IsValid { get; private set; }
    }
}
