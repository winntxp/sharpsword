/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/14/2016 2:21:05 PM
 * ****************************************************************/
using SharpSword.Commands;
using System;
using System.Web;

namespace SharpSword.Host.Commands
{
    /// <summary>
    /// 系统服务器管理命令行，命令行处理器框架会自动进行IOC注册
    /// </summary>
    public class HostCommand : CommandHandlerBase
    {
        /// <summary>
        /// 
        /// </summary>
        private HttpContextBase _httpContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpContext"></param>
        public HostCommand(HttpContextBase httpContext)
        {
            this._httpContext = httpContext;
        }

        /// <summary>
        /// 重启用于程序
        /// </summary>
        [CommandName("host restart")]
        [CommandHelp("host restart \r\n\t 重启应用程序")]
        public void ReStart()
        {
            if (!this._httpContext.IsNull() && HostHelper.TryWriteWebConfig())
            {
                this.Context.Output.WriteLine("重启应用程序成功");
            }
            else
            {
                this.Context.Output.WriteLine("重启应用程序失败");
            }
        }

        /// <summary>
        /// 获取服务器配置信息
        /// </summary>
        [CommandName("host server")]
        [CommandHelp("host server \r\n\t 获取服务器配置信息")]
        public void GetServerInfo()
        {
            if (this._httpContext.IsNull())
            {
                this.Context.Output.WriteLine("获取服务器信息失败");
                return;
            }

            var server = new
            {
                //IIS版本
                IIS = this._httpContext.Request.ServerVariables["SERVER_SOFTWARE"].ToString(),
                //.NET版本信息
                NetInfo = ".NET版本：.NET CLR {0}.{1}.{2}.{3}".With(Environment.Version.Major,
                                                                    Environment.Version.Minor,
                                                                    Environment.Version.Build,
                                                                    Environment.Version.Revision),
                //操作系统
                OS = Environment.OSVersion.ToString(),
                //CPU个数
                CPUNumber = Environment.GetEnvironmentVariable("NUMBER_OF_PROCESSORS"),
                //CPU类型
                CpuInfo = Environment.GetEnvironmentVariable("PROCESSOR_IDENTIFIER"),
                //IIS服务器地址
                LocalAddr = this._httpContext.Request.LocalAddr(),
                //接口服务运行物理路径
                PhysicalPath = this._httpContext.Request.PhysicalApplicationPath
            };

            this.Context.Output.WriteLine("----------------server info-------------------");
            this.Context.Output.WriteLine("IIS版本：\t{0}".With(server.IIS));
            this.Context.Output.WriteLine(".NET版本：\t{0}".With(server.NetInfo));
            this.Context.Output.WriteLine("操作系统：\t{0}".With(server.OS));
            this.Context.Output.WriteLine("CPU类型：\t{0}".With(server.CpuInfo));
            this.Context.Output.WriteLine("CPU个数：\t{0}".With(server.CPUNumber));
            this.Context.Output.WriteLine("服务器地址：\t{0}".With(server.LocalAddr));
            this.Context.Output.WriteLine("物理路径：\t{0}".With(server.PhysicalPath));
        }
    }
}
