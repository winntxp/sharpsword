/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/6 8:37:50
 * ****************************************************************/
using SharpSword.WebApi;

namespace SharpSword.Tools.Actions
{
    /// <summary>
    /// 调试接口工具接口插件
    /// </summary>
    [ActionName("Api.TestTool")]
    [DisablePackageSdk, EnableRecordApiLog(true), AllowAnonymous, DisableDataSignatureTransmission, ResultCache(0)]
    public class ApiTestToolAction : ActionBase<ApiTestToolAction.ApiTestToolActionRequestDto, NullResponseDto>
    {
        /// <summary>
        /// 上送的参数对象
        /// </summary>
        public class ApiTestToolActionRequestDto : RequestDtoBase
        {
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<NullResponseDto> Execute()
        {
            return this.SuccessActionResult();
        }
    }
}
