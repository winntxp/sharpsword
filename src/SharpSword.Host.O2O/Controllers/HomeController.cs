/* *******************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/14/2016 1:45:18 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.UI;

namespace SharpSword.Host.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : MvcControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IResourceFinderManager _resourceFinderManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="resourceFinderManager"></param>
        public HomeController(IResourceFinderManager resourceFinderManager)
        {
            this._resourceFinderManager = resourceFinderManager;
        }

        /// <summary>
        /// 
        /// </summary>
        [OutputCache(Duration = 600, Location = OutputCacheLocation.ServerAndClient)]
        public ActionResult Index()
        {
            var readme = this._resourceFinderManager.GetResource("SharpSword.txt");
            List<string> lines = new List<string>();
            using (StringReader stringReader = new StringReader(readme))
            {
                while (true)
                {
                    string line = stringReader.ReadLine();
                    if (line.IsNull())
                    {
                        break;
                    }

                    if (Regex.IsMatch(line.Trim(), "(^[0-9]{1,3}\\.[0-9]{1,3}.*)"))
                    {
                        lines.Add("<b style='font-size:14px;'>{0}</b>".With(line));
                        continue;
                    }

                    if (Regex.IsMatch(line.Trim(), "(^[0-9]{1,3}.*)|(^#.*)"))
                    {
                        lines.Add("<b>{0}</b>".With(line));
                        continue;
                    }

                    lines.Add(line);
                }
            }

            //获取框架程序集信息
            var assembly = typeof(IResourceFinderManager).Assembly;
            this.ViewBag.Readme = lines.JoinToString(Environment.NewLine);
            this.ViewBag.Logo = this._resourceFinderManager.GetResource("SystemPlugin.png");
            this.ViewBag.Version = assembly.GetName().Version.ToString();
            this.ViewBag.Title = assembly.GetCustomAttributes(typeof(AssemblyTitleAttribute), false)
                                         .Cast<AssemblyTitleAttribute>()
                                         .FirstOrDefault().Title;
            return View();
        }
    }
}
