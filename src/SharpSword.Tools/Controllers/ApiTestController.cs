/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/29/2015 4:34:48 PM
 * ****************************************************************/
using SharpSword.WebApi;
using SharpSword.WebApi.Host;
using System;
using System.Linq;
using System.Web.Mvc;

namespace SharpSword.Tools.Controllers
{
    /// <summary>
    /// API接口入口类
    /// </summary>
    public class ApiTestController : ApiControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IActionSelector _actionSelector;
        private readonly IApiDocBuilder _apiDocBuilder;

        /// <summary>
        /// API入口处理程序
        /// </summary>
        /// <param name="actionSelector">接口查找器</param>
        public ApiTestController(IActionSelector actionSelector, IApiDocBuilder apiDocBuilder)
        {
            this.ValidateRequest = false;
            this._actionSelector = actionSelector;
            this._apiDocBuilder = apiDocBuilder;
        }

        /// <summary>
        /// 测试工具首页
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult ApiTool()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new
            {
                ActionName = "API.TestTool",
                Format = "VIEW",
                Data = new { }.Serialize2Josn()
            });
        }

        /// <summary>
        /// 搜索接口集合 /logs/ActionsGet?query=x
        /// </summary>
        /// <param name="query">接口关键词</param>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult ActionsGet(string query)
        {
            //所有接口
            var actions = this._actionSelector.GetActionDescriptors()
                                              .Where(a => a.CanPackageToSdk && (query.IsNullOrEmpty() || a.ActionName.Contains(query, StringComparison.OrdinalIgnoreCase))).ToList();

            //返回JSON字符串
            return this.Json(new
            {
                suggestions = from item in actions
                              select new
                              {
                                  value = item.ActionName,
                                  data = item.ActionName
                              }
            }, JsonRequestBehavior.AllowGet);

        }


        /// <summary>
        /// 获取接口上送参数JSON数据模型
        /// </summary>
        /// <param name="actionName"></param>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult GetRequestDto(string actionName)
        {
            var actionDescriptor = this._actionSelector.GetActionDescriptors().FirstOrDefault(o => o.ActionName == actionName);
            if (actionDescriptor.IsNull())
            {
                return this.Content("{}");
            }

            return this.Content(this._apiDocBuilder.CreateInstance(actionDescriptor.RequestDtoType).Serialize2Josn().FormatJsonString());
        }
    }
}