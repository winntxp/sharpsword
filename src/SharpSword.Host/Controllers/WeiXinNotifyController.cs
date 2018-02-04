/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/14/2016 1:45:18 PM
 * ****************************************************************/
using SharpSword.OAuth;
using System.Web.Mvc;

namespace SharpSword.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class WeiXinController : MvcControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public WeiXinController()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Notify(string echostr)
        {
            return this.Content(echostr);
        }
    }
}
