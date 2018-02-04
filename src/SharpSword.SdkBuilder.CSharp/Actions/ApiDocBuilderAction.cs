/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/20 13:26:25
 * ****************************************************************/
using ICSharpCode.SharpZipLib.Core;
using ICSharpCode.SharpZipLib.Zip;
using SharpSword.WebApi;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace SharpSword.SdkBuilder.CSharp.Actions
{
    /// <summary>
    /// 生成API文档接口，此项目同时也示例，接口可以直接进行调用；返回值为HTML接口文档下载地址
    /// </summary>
    [ActionName("Api.Doc.Builder"), DisablePackageSdk, EnableRecordApiLog(true), AllowAnonymous, DisableDataSignatureTransmission]
    public class ApiDocBuilderAction : ActionBase<NullRequestDto, string>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IActionSelector _actionSelector;
        private readonly IApiDocBuilder _simulation;
        private readonly IMediaTypeFormatterFactory _mediaTypeFormatterFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionSelector">接口搜索器</param>
        /// <param name="simulation">接口仿真数据输出器</param>
        /// <param name="mediaTypeFormatterFactory">格式化器工厂</param>
        public ApiDocBuilderAction(
            IActionSelector actionSelector,
            IApiDocBuilder simulation,
            IMediaTypeFormatterFactory mediaTypeFormatterFactory)
        {
            this._actionSelector = actionSelector;
            this._simulation = simulation;
            this._mediaTypeFormatterFactory = mediaTypeFormatterFactory;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<string> Execute()
        {
            //获取所有的接口信息
            var actionDescriptors = this._actionSelector.GetActionDescriptors().Where(o => o.CanPackageToSdk && !o.IsObsolete).ToList();

            //调用输出器接口
            const string actionName = "API.Doc";
            //zip保存地址
            const string zipSaveFile = "~/App_Data/apidoc/apidoc.zip";
            //保存文件夹
            const string htmlSaveDirectory = "~/App_Data/apidoc/temp";

            //保存物理路径
            var physicalHtmlSaveDirectory = this.RequestContext.HttpContext.Server.MapPath(htmlSaveDirectory);
            var physicalZipSaveFile = this.RequestContext.HttpContext.Server.MapPath(zipSaveFile);

            //删除临时文件夹
            if (Directory.Exists(physicalHtmlSaveDirectory))
            {
                Directory.Delete(physicalHtmlSaveDirectory, true);
            }
            if (File.Exists(physicalZipSaveFile))
            {
                File.Delete(physicalZipSaveFile);
            }

            //生成临时文件夹
            Directory.CreateDirectory(physicalHtmlSaveDirectory);

            //循环所有接口
            foreach (var actionDescriptor in actionDescriptors)
            {
                //上送业务参数对象
                var requestDto = new ApiDocAction.ApiDocActionRequestDto()
                {
                    ActionName = actionDescriptor.ActionName,
                    Version = actionDescriptor.Version
                };

                //原始请求参数
                var requestParams = new RequestParams()
                {
                    ActionName = actionName,
                    Data = requestDto.Serialize2Josn(),
                    Format = "View",
                    AppKey = "",
                    Sign = "",
                    TimeStamp = DateTime.Now.ToString(CultureInfo.InvariantCulture),
                    Version = ""
                };

                //构造请求上下文
                var requestContext = new RequestContext(
                            httpContext: this.RequestContext.HttpContext,
                            globalConfiguration: GlobalConfiguration.Instance,
                            requestDto: requestDto,
                            actionDescriptor: this._actionSelector.GetActionDescriptor(actionName, ""),
                            rawRequestParams: requestParams,
                            decryptedRequestParams: requestParams.MapTo<RequestParams>());

                //获取生成接口
                IAction apiDocAction = new ApiDocAction(this._actionSelector, this._simulation, this._mediaTypeFormatterFactory);
                apiDocAction.RequestDto = requestDto;
                apiDocAction.RequestContext = requestContext;
                apiDocAction.ActionDescriptor = (ActionDescriptor)requestContext.ActionDescriptor;

                try
                {
                    //执行文档生成器接口（当成服务使用）
                    var actionResult = apiDocAction.Execute();

                    //格式化器，默认使用view
                    var mediaTypeFormatter = this._mediaTypeFormatterFactory.Create(ResponseFormat.VIEW);

                    //格式化后的数据
                    var serializedActionResultToString = mediaTypeFormatter.SerializedActionResultToString(requestContext, new ActionResult()
                    {
                        Data = actionResult.Data,
                        Flag = ActionResultFlag.SUCCESS,
                        Info = "OK"
                    });

                    //替换掉内部的JS引用
                    serializedActionResultToString = serializedActionResultToString
                        .Replace("/GetResource?resourceName=jquery-1.9.1.min.js",
                            "http://apps.bdimg.com/libs/jquery/1.6.4/jquery.min.js");

                    //输出到文件
                    //HostingEnvironment.MapPath
                    using (var streamWriter = new StreamWriter(this.RequestContext.HttpContext.Server.MapPath("{0}/{1}.{2}.html"
                                .With(htmlSaveDirectory, actionDescriptor.ActionName, actionDescriptor.Version))))
                    {
                        streamWriter.WriteLine(serializedActionResultToString);
                    }
                }
                catch (Exception ex)
                {
                    this.Logger.Error("接口名称：{0}，错误:{1}".With(apiDocAction.RequestDto.Serialize2Josn(), ex.StackTrace));
                }
            }

            //打包文档
            using (var zipOutputStream = new ZipOutputStream(File.OpenWrite(physicalZipSaveFile)))
            {
                byte[] buffer = new byte[4096];
                zipOutputStream.SetLevel(9);
                foreach (var file in Directory.GetFiles(physicalHtmlSaveDirectory))
                {
                    var entry = new ZipEntry(Path.GetFileName(file));
                    zipOutputStream.PutNextEntry(entry);
                    using (var fileStream = File.OpenRead(file))
                    {
                        StreamUtils.Copy(fileStream, zipOutputStream, buffer);
                    }
                }
            }

            //输出到客户端
            try
            {
                this.RequestContext.HttpContext.Response.ContentType = "application/zip";
                this.RequestContext.HttpContext.Response.AddHeader("Content-Disposition",
                    "attachment; filename=apidoc_{0}.zip".With(DateTime.Now.ToString("yyMMddHHmmss")));
                this.RequestContext.HttpContext.Response.WriteFile(physicalZipSaveFile);
                this.RequestContext.HttpContext.Response.End();
            }
            catch (Exception)
            {
                // ignored
            }

            //返回
            return new ActionResult<string>()
            {
                Flag = ActionResultFlag.SUCCESS,
                Info = "批量生成API接口文档成功",
                Data = zipSaveFile
            };

        }
    }
}
