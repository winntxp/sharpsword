/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/1 10:47:45
 * ****************************************************************/
using SharpSword.Timing;
using System;
using System.ComponentModel;

namespace SharpSword.WebApi.Actions
{
    /// <summary>
    /// 获取服务器时间；返回一个字符串时间数据，格式为：yyyy-MM-dd HH:mm:ss.fff
    /// </summary>
    [ActionName("API.ServerTime.Get"), AllowAnonymous, DisableDataSignatureTransmission]
    [ResponseFormat(ResponseFormat.JSON | ResponseFormat.XML)]
    [Description("获取服务器时间；返回一个字符串时间数据，格式为：yyyy-MM-dd HH:mm:ss.fff")]
    public class ServerTimeGetAction : ActionBase<NullRequestDto, string>
    {
        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<string> Execute()
        {
            return this.SuccessActionResult(Clock.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }
    }
}
