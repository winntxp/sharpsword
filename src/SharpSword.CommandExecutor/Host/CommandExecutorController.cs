/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/29/2015 4:34:48 PM
 * ****************************************************************/
using SharpSword.CommandExecutor.Parameters;
using SharpSword.Commands;
using SharpSword.WebApi.Host;
using System;
using System.Web.Mvc;

namespace SharpSword.CommandExecutor.Host
{
    /// <summary>
    /// 入口类
    /// </summary>
    public class CommandExecutorController : ApiControllerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IResourceFinderManager _resourceFinderManager;
        private readonly ICommandManager _commandManager;
        private readonly ICommandParametersParser _commandParametersParser;

        /// <summary>
        /// API入口处理程序
        /// </summary>
        /// <param name="resourceFinderManager"></param>
        /// <param name="commandManager"></param>
        /// <param name="commandParametersParser"></param>
        public CommandExecutorController(IResourceFinderManager resourceFinderManager,
                                         ICommandManager commandManager,
                                         ICommandParametersParser commandParametersParser)
        {
            resourceFinderManager.CheckNullThrowArgumentNullException("resourceFinderManager");
            this.ValidateRequest = false;
            this._resourceFinderManager = resourceFinderManager;
            this._commandParametersParser = commandParametersParser;
            this._commandManager = commandManager;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new
            {
                ActionName = "API.CommandExecutor",
                Format = "VIEW",
                Data = new { }.Serialize2Josn()
            });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public void Execute(string command)
        {
            if (command.IsNullOrEmpty())
            {
                this.Response.Output.WriteLine(L("请输入命令行或者输入 help 获取帮助"));
                return;
            }

            //输入参数
            var args = command.Trim().Split(new char[] { ' ', ' ' }, StringSplitOptions.RemoveEmptyEntries);

            //整理输入参数
            var commandParameters = this._commandParametersParser.Parse(args);

            //执行命令行
            try
            {
                this._commandManager.Execute(new Commands.CommandParameters()
                {
                    Output = this.Response.Output,
                    Arguments = commandParameters.Arguments,
                    Switches = commandParameters.Switches,
                    Input = null
                });
            }
            catch (Exception exc)
            {
                this.Response.Output.WriteLine(exc.Message);
            }
        }
    }
}