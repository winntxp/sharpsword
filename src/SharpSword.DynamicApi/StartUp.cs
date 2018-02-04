/* ****************************************************************
 * SharpSword zhangliang4629@163.com 9/7/2016 11:25:19 AM
 * ****************************************************************/
using Autofac;
using SharpSword.Environments;
using SharpSword.ViewEngine;
using SharpSword.WebApi;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharpSword.DynamicApi
{
    /// <summary>
    /// 启动的时候先预热下，将所有合法的待生成的API的方法查找下
    /// </summary>
    public class StartUp : IStartUp
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDynamicApiSelector _dynamicApiSelector;
        private readonly ILogger _logger;
        private readonly IActionSelector _actionSelector;
        private readonly ITypeFinder _typeFinder;
        private readonly IMachineNameProvider _machineNameProvider;
        private readonly IResourceFinderManager _resourceFinderManager;
        private readonly GlobalConfiguration _globalConfiguration;
        private readonly IViewEngineManager _viewEngineManager;
        private readonly DynamicApiConfig _dynamicApiConfig;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dynamicApiSelector">动态接口方法查找器</param>
        /// <param name="actionSelector">API接口查找器</param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="logger">日志记录器</param>
        /// <param name="machineNameProvider">当前实例信息提供器</param>
        /// <param name="resourceFinderManager">资源查找器</param>
        /// <param name="viewEngineManager">视图引擎管理器</param>
        /// <param name="globalConfiguration">动态API接口配置信息</param>
        /// <param name="dynamicApiConfig"></param>
        public StartUp(
            IDynamicApiSelector dynamicApiSelector,
            IActionSelector actionSelector,
            ITypeFinder typeFinder,
            ILogger<StartUp> logger,
            IMachineNameProvider machineNameProvider,
            IResourceFinderManager resourceFinderManager,
            IViewEngineManager viewEngineManager,
            GlobalConfiguration globalConfiguration,
            DynamicApiConfig dynamicApiConfig)
        {
            this._dynamicApiSelector = dynamicApiSelector;
            this._actionSelector = actionSelector;
            this._typeFinder = typeFinder;
            this._logger = logger ?? GenericNullLogger<StartUp>.Instance;
            this._machineNameProvider = machineNameProvider;
            this._resourceFinderManager = resourceFinderManager;
            this._globalConfiguration = globalConfiguration;
            this._viewEngineManager = viewEngineManager;
            this._dynamicApiConfig = dynamicApiConfig;
        }

        /// <summary>
        /// 初始化(整个什么周期只执行一次，在IOC容器注册完成后进行)
        /// </summary>
        public void Init()
        {
            try
            {
                this.Start();
            }
            catch (Exception exc)
            {
                this._logger.Error(exc);
            }
        }

        /// <summary>
        /// 自动将业务层指定的方法输出为接口
        /// </summary>
        private void Start()
        {
            //临时保存源代码 Key:源代码文件名称，Value:源代码
            IList<KeyValuePair<string, string>> sourceClassStrings = new List<KeyValuePair<string, string>>();

            //生成类的模板信息，当然我们可以直接使用:DynamicApi.Generator.aspx ，但是为了防止冲突，我们使用完整限制路径
            var t = this._resourceFinderManager.GetResource("{0}.Views.DynamicApi.Generator.aspx".With(this.GetType().Assembly.GetName().Name));

            //循环当前注册的所有模型
            foreach (var item in this._dynamicApiSelector.GetDynamicApiDescriptors(m => true))
            {
                var serializedActionResultToString = this._viewEngineManager.CompileByViewSource(t,
                                        new ViewParameter("ActionResult", new ActionResult() { Data = item }),
                                        new ViewParameter("DynamicApiConfig", this._dynamicApiConfig));
                //接口保存的文件名称
                string actionFileName = "{0}.Action.cs".With(item.ActionName);

                //添加到集合
                sourceClassStrings.Add(new KeyValuePair<string, string>(actionFileName, serializedActionResultToString));
            }

            //没有找到动态API接口，直接返回
            if (!sourceClassStrings.Any())
            {
                return;
            }

            //开发模式，保存到本地磁盘
            if ((_dynamicApiConfig.WorkMode & WorkMode.Develop) == WorkMode.Develop)
            {
                //创建文件夹
                this.CreateDynamicApiDirectory();

                //保存原代码
                this.SaveSourceToDisk(sourceClassStrings);
            }

            //工作默认为自动映射
            if ((_dynamicApiConfig.WorkMode & WorkMode.Dynamic) == WorkMode.Dynamic)
            {
                this.CompileAssemblyFromSource(sourceClassStrings.ToArray());
            }
        }

        /// <summary>
        /// 创建文件夹，如果不存在就创建(当一个站点运行在一个服务器，但是配置了多个WEB-frame的时候，可能会出现错误)
        /// </summary>
        private void CreateDynamicApiDirectory()
        {
            //生成存放DLL的目录
            var dllSaveDirectory = HostHelper.MapPath(_dynamicApiConfig.DynamicDirectory);
            if (!Directory.Exists(dllSaveDirectory))
            {
                Directory.CreateDirectory(dllSaveDirectory);
            }
            //存在文件夹，就先清空文件
            else
            {
                var files = Directory.GetFiles(dllSaveDirectory, "*");
                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
        }

        /// <summary>
        /// 保存源代码到本地磁盘 
        /// </summary>
        /// <param name="sources">文件名-源代码</param>
        private void SaveSourceToDisk(IEnumerable<KeyValuePair<string, string>> sources)
        {
            //循环保存源代码到本地磁盘
            foreach (var source in sources)
            {
                using (var streamWriter = new StreamWriter(HostHelper.MapPath("{0}/{1}".With(_dynamicApiConfig.DynamicDirectory, source.Key))))
                {
                    streamWriter.WriteLine(source.Value);
                }
            }
        }

        /// <summary>
        /// 编译源代码到DLL
        /// </summary>
        /// <param name="sources">待编译的源代码</param>
        private void CompileAssemblyFromSource(IEnumerable<KeyValuePair<string, string>> sources)
        {
            //生成存放DLL的目录
            var dllSaveDirectory = HostHelper.MapPath(_dynamicApiConfig.DynamicDirectory);

            //dll名
            string dllname = "{0}.dll".With(_dynamicApiConfig.ActionNameSpace);

            //bin目录
            string binDirectoryPath = HostHelper.MapPath("~/bin");

            //输出dll保存地址
            //string assemblySavePath = Path.Combine(dllSaveDirectory, dllname);

            //保存DLL注释文件XML路径
            //string docSavePath = Path.Combine(dllSaveDirectory, "{0}.XML".With(this._dynamicApiConfig.ActionNameSpace));

            //获取代码编译器
            CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
            CompilerParameters compilerparams = new CompilerParameters();
            //指定编译选项，是类库、并且生成XML注释文档
            compilerparams.CompilerOptions = "/target:library /optimize";
            //compilerparams.CompilerOptions = "/target:library /optimize /doc:{0}".With(docSavePath);
            compilerparams.GenerateInMemory = true;
            compilerparams.GenerateExecutable = false;
            //compilerparams.IncludeDebugInformation = true;
            compilerparams.IncludeDebugInformation = false;
            compilerparams.TreatWarningsAsErrors = false;
            //compilerparams.OutputAssembly = assemblySavePath;

            //加载默认系统dll文件  
            string[] systemDlls = new string[] { "System.dll",
                                                 "System.Core.dll",
                                                 "System.Xml.Linq.dll",
                                                 "System.Configuration.dll",
                                                 "System.Data.DataSetExtensions.dll",
                                                 "Microsoft.CSharp.dll",
                                                 "System.Data.dll",
                                                 "System.Xml.dll",
                                                 "System.Web.dll" };

            systemDlls.ToList().ForEach(assembly =>
            {
                compilerparams.ReferencedAssemblies.Add(assembly);
            });

            //获取bin目录下面的所有dll文件 排除当前生成dll
            Directory.GetFiles(binDirectoryPath, "*.dll", SearchOption.TopDirectoryOnly)
                     .Select(path => Path.GetFileName(path))
                     .Where(t => t != dllname).ToList()
                     .ForEach(assembly =>
                     {
                         compilerparams.ReferencedAssemblies.Add(Path.Combine(binDirectoryPath, assembly));
                     });

            //编译源代码
            var compilerResults = provider.CompileAssemblyFromSource(compilerparams, sources.Select(o => o.Value).ToArray());

            //编译源码出现错误
            if (compilerResults.Errors.HasErrors)
            {
                IList<string> errors = new List<string>();
                foreach (CompilerError error in compilerResults.Errors)
                {
                    errors.Add(String.Format("Error on line {0}: {1}", error.Line, error.ErrorText));
                }

                //记录下错误日志
                this._logger.Error(errors.JoinToString(Environment.NewLine));

                //出错直接返回
                return;
            }

            // 获取动态的程序集
            // var dynamicAssembly = Assembly.LoadFile(assemblySavePath);

            // 加载程序集到当前域
            // AppDomain.CurrentDomain.Load(dynamicAssembly.GetName());

            //重新刷新API接口缓存器
            this._actionSelector.Reset();

            //由于执行的先后原因，我们再次刷新下所有API接口IOC注册
            var containerBuilder = new ContainerBuilder();
            //框架自动搜索程序集，注册所有实现了IAction接口的类；
            this._typeFinder.FindClassesOfType(typeof(IAction), new[] { compilerResults.CompiledAssembly })
                            .Where(type => type.IsAssignableToActionBase()).ToList().ForEach(type =>
                            {
                                containerBuilder.RegisterType(type).PropertiesAutowired().InstancePerLifetimeScope();
                            });
            containerBuilder.Update(ServicesContainer.Current.Container);

            //记录下日志
            if (this._logger.IsEnabled(LogLevel.Information))
            {
                this._logger.Information("动态API生成成功，程序集：{0}，当前HOST工作进程：{1}"
                       .With(compilerResults.CompiledAssembly.FullName, this._machineNameProvider.GetMachineName()));
            }
        }

        /// <summary>
        /// 设置启动次序
        /// </summary>
        public int Priority { get { return 0; } }
    }
}
