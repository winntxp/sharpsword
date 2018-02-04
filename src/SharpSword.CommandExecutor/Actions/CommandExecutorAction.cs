/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/6 8:37:50
 * ****************************************************************/
using SharpSword.WebApi;

namespace SharpSword.CommandExecutor.Actions
{
    /// <summary>
    /// DTO生成器插件
    /// </summary>
    [ActionName("API.CommandExecutor")]
    [DisablePackageSdk, EnableRecordApiLog(true), DisableDataSignatureTransmission, AllowAnonymous, ResultCache(5)]
    public class CommandExecutorAction : ActionBase<NullRequestDto, object>
    {
        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<object> Execute()
        {
            return this.SuccessActionResult();
        }
    }
}
