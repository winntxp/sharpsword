/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/5/27 10:11:03
 * ****************************************************************/
using SharpSword.WebApi;
using System.ComponentModel.DataAnnotations;

namespace SharpSword.SdkBuilder.CSharp.Actions
{
    /// <summary>
    /// 接口描述对象获取
    /// </summary>
    [ActionName("Api.Descriptor"), AllowAnonymous, DisablePackageSdk, DisableDataSignatureTransmission]
    [EnableRecordApiLog(true), EnableAjaxRequest, ResultCache(5)]
    //[View("SharpSword.WebApi.Views.Api.Descriptor.aspx")]
    public class ApiDescriptorAction :
        ActionBase<ApiDescriptorAction.ApiDescriptorActionRequestDto, ApiDescriptorAction.ApiDescriptorActionResponseDto>
    {
        /// <summary>
        /// 上送的参数对象
        /// </summary>
        public class ApiDescriptorActionRequestDto : RequestDtoBase
        {
            /// <summary>
            /// 接口名称
            /// </summary>
            [Required]
            public string ActionName { get; set; }

            /// <summary>
            /// 版本号
            /// </summary>
            [Required]
            public string Version { get; set; }
        }

        /// <summary>
        /// 下送的数据
        /// </summary>
        public class ApiDescriptorActionResponseDto
        {
            /// <summary>
            /// 
            /// </summary>
            public ActionDescriptor ActionDescriptor { get; set; }
        }

        /// <summary>
        /// 接口查找器
        /// </summary>
        private readonly IActionSelector _actionSelector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionSelector">接口查找器</param>
        public ApiDescriptorAction(IActionSelector actionSelector)
        {
            this._actionSelector = actionSelector;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<ApiDescriptorActionResponseDto> Execute()
        {
            return this.SuccessActionResult(new ApiDescriptorActionResponseDto()
            {
                ActionDescriptor =
                    this._actionSelector.GetActionDescriptor(this.RequestDto.ActionName, this.RequestDto.Version)
            });
        }

    }
}
