/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/29/2015 4:34:48 PM
 * ****************************************************************/
using SharpSword.Host;
using SharpSword.WebApi.Host;
using System.Web.Mvc;

namespace SharpSword.SdkBuilder.CSharp.Host
{
    /// <summary>
    /// API接口入口类
    /// </summary>
    public class SdkBuilderCSharpController : ApiControllerBase
    {
        /// <summary>
        /// API入口处理程序
        /// </summary>
        public SdkBuilderCSharpController()
        {
            this.ValidateRequest = false;
        }

        /// <summary>
        /// 接口文档生成器
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult DocBuilder()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new { ActionName = "Api.Doc.Builder", Format = "VIEW", Data = "" });
        }

        /// <summary>
        /// 下载SDK(C#)
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult CSharpDownSdk()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new
            {
                ActionName = "API.BuildSDK",
                Format = "VIEW",
                Data = new { SaveType = "dll" }.Serialize2Josn()
            });
        }

        /// <summary>
        /// 下载源码
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult CSharpDownSource()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new
            {
                ActionName = "API.BuildSDK",
                Format = "VIEW",
                Data = new { SaveType = "source" }.Serialize2Josn()
            });
        }

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult CSharpSdkBuilder()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new
            {
                ActionName = "API.BuildSdk",
                Format = "VIEW",
                Data = new { Actionname = "API.BuildSdk", Version = "" }.Serialize2Josn()
            });
        }
    }
}