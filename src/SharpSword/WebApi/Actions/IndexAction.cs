/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/5/26 10:48:19
 * ****************************************************************/
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.WebApi.Actions
{
    /// <summary>
    /// 框架系统首页
    /// </summary>
    [ActionName("Api.Index"), DisablePackageSdk, AllowAnonymous]
    [DisableDataSignatureTransmission, EnableRecordApiLog(true), ResultCache(30)]
    [View("SharpSword.WebApi.Views.Api.Index.aspx")]
    public class IndexAction : ActionBase<IndexAction.IndexActionRequestDto, IndexAction.IndexActionResponseDto>
    {
        /// <summary>
        /// 上送的参数对象
        /// </summary>
        public class IndexActionRequestDto : RequestDtoBase { }

        /// <summary>
        /// 下送的数据
        /// </summary>
        public class IndexActionResponseDto
        {
            /// <summary>
            /// 接口插件
            /// </summary>
            public IEnumerable<IPluginDescriptor> ApiPluginDescriptors { get; set; }
            /// <summary>
            /// 接口描述对象
            /// </summary>
            public IEnumerable<ActionDescriptor> ActionDescriptors { get; set; }
        }

        /// <summary>
        /// 接口查找器
        /// </summary>
        private readonly IActionSelector _actionSelector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionSelector">接口查找器</param>
        public IndexAction(IActionSelector actionSelector)
        {
            this._actionSelector = actionSelector;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<IndexActionResponseDto> Execute()
        {
            var resp = new IndexActionResponseDto
            {
                ApiPluginDescriptors = PluginManager.GetApiPlugins().OrderByDescending(o => o.DisplayIndex).ThenBy(o => o.DisplayName),
                ActionDescriptors = this._actionSelector.GetActionDescriptors().Where(o => o.CanPackageToSdk).OrderBy(o => o.ActionName)
            };
            return this.SuccessActionResult(resp);
        }
    }
}
