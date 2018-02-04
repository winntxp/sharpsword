/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/6/8 13:59:45
 * ****************************************************************/
using SharpSword.Tasks;
using System;
using System.Net.Http;

namespace SharpSword.WebApi.Tasks
{
    /// <summary>
    /// 保持站点激活作业任务(此任务定期后请求一次当前运行的站点指定的接口，防止IIS站点应用程序池关闭)
    /// </summary>
    public class KeepAliveTask : IBackgroundTask
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly ILogger _logger;
        private readonly GlobalConfiguration _globalConfiguration;
        private readonly IMachineNameProvider _machineNameProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger">日志接口</param>
        /// <param name="globalConfiguration">系统配置</param>
        /// <param name="machineNameProvider">当前站点允许实例</param>
        public KeepAliveTask(ILogger<KeepAliveTask> logger,
                             GlobalConfiguration globalConfiguration,
                             IMachineNameProvider machineNameProvider)
        {
            this._logger = logger ?? GenericNullLogger<KeepAliveTask>.Instance;
            this._globalConfiguration = globalConfiguration;
            this._machineNameProvider = machineNameProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="taskExecuteContext"></param>
        public void Execute(TaskExecuteContext taskExecuteContext)
        {
            //未设置站点域名，直接返回，不执行
            if (this._globalConfiguration.HttpHost.IsNullOrEmpty())
            {
                if (this._logger.IsEnabled(LogLevel.Warning))
                {
                    this._logger.Warning("全局配置文件GlobalConfiguration.HttpHost属性未配置");
                }
                return;
            }

            //访问一次远程站点
            using (var httpClient = new HttpClient())
            {
                //请求的URL
                var keepAliveUrl = "http://{0}/api/keepalive?format=json"
                        .With(this._globalConfiguration.HttpHost);

                try
                {
                    //请求一次
                    var resp = httpClient.GetStringAsync(keepAliveUrl).Result;

                    //将返回数据记录到日志
                    if (this._logger.IsEnabled(LogLevel.Information))
                    {
                        this._logger.Information("运行实例：{0}\r\n作业任务：{1}\r\n返回消息：{2}"
                        .With(this._machineNameProvider.GetMachineName(), taskExecuteContext.TaskScheduler.Name, resp));
                    }
                }
                catch (Exception ex)
                {
                    this._logger.Error(ex, "心跳作业任务请求失败，请求URL:{0}".With(keepAliveUrl));
                }
            }
        }
    }
}
