/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/6/8 13:56:08
 * ****************************************************************/

namespace SharpSword.WebApi.Actions
{
    /// <summary>
    /// 提供作业任务访问，保持站点活跃
    /// </summary>
    [ActionName("Api.KeepAlive"), DisablePackageSdk, DisableDataSignatureTransmission]
    [AllowAnonymous, Route("api/keepalive")]
    public class KeepAliveAction : ActionBase<KeepAliveAction.KeepAliveActionRequestDto, string>
    {
        /// <summary>
        /// 上送的参数对象
        /// </summary>
        public class KeepAliveActionRequestDto : RequestDtoBase { }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<string> Execute()
        {
            return this.SuccessActionResult("I am ok");
        }
    }
}
