/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/7/2016 2:22:24 PM
 * ****************************************************************/
using Autofac;
using SharpSword.WebApi;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SharpSword.DynamicApi
{
    /// <summary>
    /// 检索出待自动生成接口的业务代码
    /// </summary>
    [DisablePackageSdk, DisableDataSignatureTransmission, AllowAnonymous]
    internal class CollectionAction : ActionBase<NullRequestDto, IEnumerable<DynamicApiDescriptor>>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IDynamicApiSelector _dynamicApiSelector;
        private readonly IMediaTypeFormatterFactory _mediaTypeFormatterFactory;
        private readonly IActionSelector _actionSelector;
        private readonly ITypeFinder _typeFinder;
        private readonly DynamicApiConfig _dynamicApiConfig;
        private readonly IMachineNameProvider _machineNameProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dynamicApiSelector">动态API方法查找器</param>
        /// <param name="mediaTypeFormatterFactory">内容输出格式化器创建器</param>
        /// <param name="actionSelector">API接口查找器</param>
        /// <param name="typeFinder">类型查找器</param>
        /// <param name="dynamicApiConfig">动态API接口配置信息</param>
        /// <param name="machineNameProvider">当前实例信息提供器</param>
        public CollectionAction(
            IDynamicApiSelector dynamicApiSelector,
            IMediaTypeFormatterFactory mediaTypeFormatterFactory,
            IActionSelector actionSelector,
            ITypeFinder typeFinder,
            DynamicApiConfig dynamicApiConfig,
            IMachineNameProvider machineNameProvider)
        {
            this._dynamicApiSelector = dynamicApiSelector;
            this._mediaTypeFormatterFactory = mediaTypeFormatterFactory;
            this._actionSelector = actionSelector;
            this._typeFinder = typeFinder;
            this._dynamicApiConfig = dynamicApiConfig;
            this._machineNameProvider = machineNameProvider;
        }

        /// <summary>
        /// 返回所有业务层指定需要映射的接口信息
        /// </summary>
        /// <returns></returns>
        public override ActionResult<IEnumerable<DynamicApiDescriptor>> Execute()
        {
            return this.SuccessActionResult(this._dynamicApiSelector.GetDynamicApiDescriptors(m => true));
        }

        /// <summary>
        /// 自动将业务层指定的方法输出为接口
        /// </summary>
        /// <param name="actionExecutedContext">直接执行完毕后触发的方法执行上下文</param>
        protected override void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            //请求的内部接口
            var actionName = "DynamicApi.Generator";

            //临时保存源代码 Key:源代码文件名称，Value:源代码
            IList<KeyValuePair<string, string>> sourceClassStrings = new List<KeyValuePair<string, string>>();

            //循环当前注册的所有模型
            foreach (var item in (IEnumerable<DynamicApiDescriptor>)actionExecutedContext.Result.Data)
            {
                //原始请求参数
                var requestParams = new RequestParams()
                {
                    ActionName = actionName,
                    Data = "{}",
                    Format = "View",
                };

                //构造请求上下文
                var requestContext = new RequestContext(
                            httpContext: this.RequestContext.HttpContext,
                            globalConfiguration: GlobalConfiguration.Instance,
                            requestDto: null,
                            actionDescriptor: this.RequestContext.ActionDescriptor,
                            rawRequestParams: requestParams,
                            decryptedRequestParams: requestParams.MapTo<RequestParams>());

                //格式化器，默认使用view
                var mediaTypeFormatter = this._mediaTypeFormatterFactory.Create(ResponseFormat.VIEW);

                //接口源代码
                var serializedActionResultToString = mediaTypeFormatter.SerializedActionResultToString(requestContext, new ActionResult()
                {
                    Data = item,
                    Flag = ActionResultFlag.SUCCESS,
                    Info = "OK"
                });

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
            if ((this._dynamicApiConfig.WorkMode & WorkMode.Develop) == WorkMode.Develop)
            {
                //创建文件夹
                this.CreateDynamicApiDirectory();

                //保存原代码
                this.SaveSourceToDisk(sourceClassStrings);
            }

            //工作默认为自动映射
            if ((this._dynamicApiConfig.WorkMode & WorkMode.Dynamic) == WorkMode.Dynamic)
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
            var dllSaveDirectory = this.RequestContext.HttpContext.Server
                                       .MapPath(this._dynamicApiConfig.DynamicDirectory);
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
                using (var streamWriter = new StreamWriter(this.RequestContext.HttpContext.Server
                    .MapPath("{0}/{1}".With(this._dynamicApiConfig.DynamicDirectory, source.Key))))
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
            //动态API配置文件
            var httpServerUtility = this.RequestContext.HttpContext.Server;

            //生成存放DLL的目录
            var dllSaveDirectory = httpServerUtility.MapPath(this._dynamicApiConfig.DynamicDirectory);

            //dll名
            string dllname = "{0}.dll".With(this._dynamicApiConfig.ActionNameSpace);

            //bin目录
            string binDirectoryPath = httpServerUtility.MapPath("~/bin");

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
                this.Logger.Error(errors.JoinToString(Environment.NewLine));

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
            if (this.Logger.IsEnabled(LogLevel.Information))
            {
                this.Logger.Information("动态API生成成功，程序集：{0}，当前HOST工作进程：{1}"
                       .With(compilerResults.CompiledAssembly.FullName, this._machineNameProvider.GetMachineName()));
            }
        }
    }
}
