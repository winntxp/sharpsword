/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/29/2015 4:34:48 PM
 * ****************************************************************/
using SharpSword.Data;
using SharpSword.DtoGenerator.ViewModels;
using SharpSword.EntityFramework;
using SharpSword.ViewEngine;
using SharpSword.WebApi.Host;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web.Mvc;

namespace SharpSword.DtoGenerator.Host
{
    /// <summary>
    /// 入口类
    /// </summary>
    public class DtoGeneratorController : ApiControllerBase
    {
        /// <summary>
        /// 模板名称
        /// </summary>
        private const string TempletName = "DtoGeneratorTemplet.aspx";
        private readonly IResourceFinderManager _resourceFinderManager;
        private readonly IDbContext _dbContext;
        private readonly IViewEngineManager _viewEngineManager;
        private readonly DtoGeneratorConfig _dtoGeneratorConfig;

        /// <summary>
        /// API入口处理程序
        /// </summary>
        /// <param name="resourceFinderManager"></param>
        /// <param name="dbContext"></param>
        /// <param name="viewEngineManager"></param>
        /// <param name="dtoGeneratorConfig"></param>
        public DtoGeneratorController(IResourceFinderManager resourceFinderManager,
                                      DtoGeneratorDbContext dbContext,
                                      IViewEngineManager viewEngineManager,
                                      DtoGeneratorConfig dtoGeneratorConfig)
        {
            resourceFinderManager.CheckNullThrowArgumentNullException(nameof(resourceFinderManager));
            dbContext.CheckNullThrowArgumentNullException(nameof(dbContext));
            viewEngineManager.CheckNullThrowArgumentNullException(nameof(viewEngineManager));
            dtoGeneratorConfig.CheckNullThrowArgumentNullException(nameof(dtoGeneratorConfig));

            this.ValidateRequest = false;
            this._resourceFinderManager = resourceFinderManager;
            this._dbContext = dbContext;
            this._viewEngineManager = viewEngineManager;
            this._dtoGeneratorConfig = dtoGeneratorConfig;
        }

        /// <summary>
        /// 保存生成的源文件到本地磁盘
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="source"></param>
        private void SaveDtoSource(DtoViewModel dto, string source)
        {
            var httpServerUtility = this.HttpContext.Server;

            //不指定就不保存
            if (!this._dtoGeneratorConfig.SourceSaveDirectory.IsNullOrEmpty())
            {
                //创建目录
                var dllSaveDirectory = httpServerUtility.MapPath(this._dtoGeneratorConfig.SourceSaveDirectory);
                if (!Directory.Exists(dllSaveDirectory))
                {
                    Directory.CreateDirectory(dllSaveDirectory);
                }
                using (var streamWriter = new StreamWriter(httpServerUtility.MapPath("{0}/{1}.cs".
                    With(this._dtoGeneratorConfig.SourceSaveDirectory, dto.ClassName))))
                {
                    streamWriter.WriteLine(source);
                }
            }
        }

        /// <summary>
        /// DTO生成器界面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult DtoGenerator()
        {
            return this.RedirectToRoute(RoutePublisher.WebApiRouteName, new
            {
                ActionName = "API.DtoGenerator",
                Format = "VIEW",
                Data = new { }.Serialize2Josn()
            });
        }

        /// <summary>
        /// 请求生成DTO源文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult DtoGenerator(DtoViewModel dto)
        {
            try
            {
                //视图源文件
                var viewSource = this._resourceFinderManager.GetResource(TempletName);

                //数据操作上下文
                var dbContext = (this._dbContext as DbContextBase);
                if (dbContext.IsNull())
                {
                    throw new SharpSwordCoreException("参数:dbContext转型错误，必须继承：DbContextBase");
                }

                //创建动态类型
                // ReSharper disable once PossibleNullReferenceException
                var dynamicType = dbContext.CreateDynamicType(dto.SQL, new object[] { });
                dto.DtoType = dynamicType;

                //命名空间
                if (dto.Namespace.IsNullOrEmpty())
                {
                    dto.Namespace = this.GetType().Assembly.GetName().Name;
                }

                //多个空格转换成1个
                dto.SQL = Regex.Replace(dto.SQL, "\\s{1,}", " ");

                //类名称
                if (dto.ClassName.IsNullOrEmpty())
                {
                    dto.ClassName = "DTO_{0}".With(MD5.Encrypt(dto.SQL).ToUpper());
                }

                //继承
                dto.Inherit = dto.Inherit.IsNullOrEmpty() ? "" : " : {0}".With(dto.Inherit);

                //属性字段
                foreach (var property in dto.DtoType.GetPropertiesInfo())
                {
                    dto.Properties.Add(new DtoProperty(property.Name, property.PropertyType.GetTypeName(), property.PropertyType.GetFCLTypeName()));
                }

                //编译并执行视图
                var dtoSource = this._viewEngineManager.CompileByViewSource(viewSource, new ViewParameter("DTO", dto));

                //保存文件
                this.SaveDtoSource(dto, dtoSource);

                //源码格式化，关键词着色（c#）
                var csharpFormat = new CodeFormatter.CSharpFormat() { EmbedStyleSheet = false };
                return Content(csharpFormat.FormatCode(dtoSource));
            }
            catch (Exception exc)
            {
                //记录下错误日志
                this.Logger.Error(exc);

                //返回错误消息
                return Content(exc.Message.HtmlEncode());
            }
        }

        /// <summary>
        /// 下载内部默认的模板，让开发可以自己修改
        /// </summary>
        /// <returns></returns>
        public ActionResult TempleteDown()
        {
            //内嵌的默认资源完整路径
            var resourceFullName = "{0}.Views.{1}".With(this.GetType().Assembly.GetName().Name, TempletName);

            //视图源文件
            var viewSource = this._resourceFinderManager.GetResource(resourceFullName);

            //输出到客户端
            return this.File(viewSource.GetBytes(), MimeTypes.ApplicationOctetStream, TempletName);

        }
    }
}