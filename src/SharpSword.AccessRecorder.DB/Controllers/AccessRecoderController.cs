/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/31 10:19:09
 * ****************************************************************/
using SharpSword.Timing;
using SharpSword.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SharpSword.AccessRecorder.DB.Controllers
{
    /// <summary>
    /// 插件默认的路由控制器;所有的插件都使用下面方式来进行跳转
    /// </summary>
    public class AccessRecoderController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IActionSelector _actionSelector;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionSelector">接口查找器</param>
        public AccessRecoderController(IActionSelector actionSelector)
        {
            this._actionSelector = actionSelector;
        }

        /// <summary>
        /// 获取待查询字符串
        /// </summary>
        /// <param name="dict"></param>
        /// <returns></returns>
        private string GetSignString(IDictionary<string, string> dict)
        {
            return string.Join("&", (from item in dict select "{0}={1}".With(item.Key, item.Value)).ToArray());
        }

        /// <summary>
        /// 跳转到哪里
        /// </summary>
        /// <param name="data">上送的data数据对象json字符串</param>
        /// <param name="actionName"></param>
        /// <param name="appSecret"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        private string RedirectTo(string actionName, string format, string data, string appSecret)
        {
            //数据签名密钥
            string _appSecret = appSecret.IsNullOrEmpty() ? Guid.NewGuid().ToString() : appSecret;

            //排序字段
            SortedDictionary<string, string> @params = new SortedDictionary<string, string>
            {
                {"actionname", actionName},
                {"format", format},
                {"data", "{0}".With(data)},
                {"timestamp", Clock.Now.ToString()},
                {"appkey", "901023"}
            };
            //@params.Add("version", "1.0");

            //数据签名
            @params.Add("sign", MD5.Encrypt("{0}{1}{0}".With(_appSecret, this.GetSignString(@params))).ToUpper());

            //返回跳转数据
            return "/Api?{0}".With(this.GetSignString(@params));
        }

        /// <summary>
        /// 请使用： /Logs 来访问接口访问日志插件
        /// </summary>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult Logs()
        {
            return Redirect(this.RedirectTo("API.Logs.List", "View", new { }.Serialize2Josn(), string.Empty));
        }

        /// <summary>
        /// <![CDATA[
        /// 搜索 /logs/Search?apiname=x&ip=x
        /// ]]>
        /// </summary>
        /// <param name="apiname">接口名称</param>
        /// <param name="ip">ip地址</param>
        /// <param name="usedTime">接口执行花费的毫秒数</param>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult Search(string apiname, string ip, int? usedTime)
        {
            return Redirect(this.RedirectTo("API.Logs.List", "View", new
            {
                apiname = apiname,
                ip = ip,
                UsedTime = usedTime
            }.Serialize2Josn(), string.Empty));
        }

        /// <summary>
        /// 搜索接口集合 /logs/ActionsGet?query=x
        /// </summary>
        /// <param name="query">接口关键词</param>
        /// <returns></returns>
        public System.Web.Mvc.ActionResult ActionsGet(string query)
        {
            //所有接口
            var actions = this._actionSelector.GetActionDescriptors();

            if (!query.IsNullOrEmpty())
            {
                actions = actions.Where(o => o.ActionName.ToLower().Contains(query.ToLower()));
            }

            //返回JSON字符串
            return this.Content(new
            {
                suggestions = from item in actions
                              select new
                              {
                                  value = item.ActionName,
                                  data = item.ActionName
                              }
            }.Serialize2Josn());

        }
    }
}
