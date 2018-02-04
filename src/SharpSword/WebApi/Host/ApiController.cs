/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/29/2015 4:34:48 PM
 * ****************************************************************/
using System.Web.Mvc;

namespace SharpSword.WebApi.Host
{
    /// <summary>
    /// API接口入口类
    /// </summary>
    public class ApiController : ApiControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IActionRequestHander _actionRequestHander;

        /// <summary>
        /// API入口处理程序
        /// </summary>
        /// <param name="actionRequestHander">API入口处理程序</param>
        public ApiController(IActionRequestHander actionRequestHander)
        {
            this._actionRequestHander = actionRequestHander;
            this.ValidateRequest = false;
        }

        /// <summary>
        /// API接口处理入口
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public System.Web.Mvc.ActionResult RequestHander()
        {
            return new ActionRequestHanderActionResult(() => { this._actionRequestHander.Execute(); }, this.Logger);
        }

        /// <summary>
        /// 提交首页，直接转到API处理入口 /index
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult Index()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new
            {
                ActionName = "API.Index",
                Format = "VIEW",
                Data = new { }.Serialize2Josn()
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult Events()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new
            {
                ActionName = "API.Events",
                Format = "VIEW",
                Data = new { }.Serialize2Josn()
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult SqlUsed()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new
            {
                ActionName = "API.SqlUsed",
                Format = "VIEW",
                Data = new { }.Serialize2Josn()
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult TransUsed()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new
            {
                ActionName = "API.TransUsed",
                Format = "VIEW",
                Data = new { }.Serialize2Josn()
            });
        }

        /// <summary>
        /// 接口帮助文档接口 /help
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult Help()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new
            {
                ActionName = "Api.Help",
                Format = "XML",
                Data = new { }.Serialize2Josn()
            });
        }

        /// <summary>
        /// API接口帮助信息XML展示
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult HelpXml()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new
            {
                ActionName = "Api.Help",
                Format = "XML",
                Data = new { }.Serialize2Josn()
            });
        }

        /// <summary>
        /// API接口帮助信息JSON展示
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult HelpJson()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new
            {
                ActionName = "Api.Help",
                Format = "JSON",
                Data = new { }.Serialize2Josn()
            });
        }

        /// <summary>
        /// API接口帮助信息View展示
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult HelpView()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new
            {
                ActionName = "Api.Help",
                Format = "VIEW",
                Data = new { }.Serialize2Josn()
            });
        }
    }
}