/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/29/2015 4:34:48 PM
 * ****************************************************************/
using SharpSword.WebApi;
using SharpSword.WebApi.Host;

namespace SharpSword.TaskManagement.Host
{
    /// <summary>
    /// API接口入口类
    /// </summary>
    public class TaskManagementController : ApiControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IActionSelector _actionSelector;

        /// <summary>
        /// API入口处理程序
        /// </summary>
        /// <param name="actionSelector">接口查找器</param>
        public TaskManagementController(IActionSelector actionSelector)
        {
            this.ValidateRequest = false;
            this._actionSelector = actionSelector;
        }

        /// <summary>
        /// 跳转到正确的接口请求
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult TaskManager()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new
            {
                ActionName = "API.TaskManager",
                Format = "VIEW",
                Data = new { }.Serialize2Josn()
            });
        }
    }
}