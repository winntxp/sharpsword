/******************************************************************
 * SharpSword ftanghnust@gmail.com 2016/2/18 9:19:42
 * ****************************************************************/
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using SharpSword.Timing;
using SharpSword.WebApi;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace SharpSword.SdkBuilder.CSharp.Actions
{
    /// <summary>
    /// 生成客户端SDK类文件
    /// </summary>
    [ActionName("API.BuildSdk"), Serializable, DisablePackageSdk, AllowAnonymous]
    [EnableRecordApiLog(true), DisableDataSignatureTransmission]
    public class BuildSdkAction : ActionBase<BuildSdkAction.BuildSdkActionRequestDto, BuildSdkAction.BuildSdkActionResponseDto>
    {
        /// <summary>
        /// 上送的参数
        /// </summary>
        public class BuildSdkActionRequestDto : RequestDtoBase
        {
            /// <summary>
            /// 需要构建客户端访问代码的批量接口名称
            /// </summary>
            [Display(Name = "接口名称")]
            public string ActionName { get; set; }

            /// <summary>
            /// 接口版本
            /// </summary>
            public string Version { get; set; }

            /// <summary>
            /// 保存类型；输入值为：source(源代码),dll(SDK包)
            /// </summary>
            [Display(Name = "保存类型")]
            public string SaveType { get; set; }

            /// <summary>
            /// 修改下上送的参数值
            /// </summary>
            public override void BeforeValid()
            {
                //如果存在保存类型就将字符串转换成小写
                if (!this.SaveType.IsNullOrEmpty())
                {
                    this.SaveType = this.SaveType.ToLower();
                }
            }
        }

        /// <summary>
        /// 下送输出对象
        /// </summary>
        public class BuildSdkActionResponseDto
        {
            /// <summary>
            /// 当前接口查找对象(正式环境里请不要这样使用，而应该全部定义为无行为的对象类)
            /// </summary>
            public IActionSelector ActionSelector { get; set; }

            /// <summary>
            /// SDK源码
            /// </summary>
            public string SDKSource { get; set; }

            /// <summary>
            /// SDK请求源代码
            /// </summary>
            public string RequestSource { get; set; }

            /// <summary>
            /// SDK输出源代码
            /// </summary>
            public string ResponseSource { get; set; }

            /// <summary>
            /// SDK命名空间
            /// </summary>
            public string SdkNamespace { get; set; }

            /// <summary>
            /// API接口框架版本
            /// </summary>
            public string Version { get; set; }

            /// <summary>
            /// 接口描述信息
            /// </summary>
            public IActionDescriptor ActionDescriptor { get; set; }

            /// <summary>
            /// 上送的DATA数据JSON格式
            /// </summary>
            public string RequestJson { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string ResponseJson { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public string ResponseXml { get; set; }
        }

        /// <summary>
        /// 接口查找器
        /// </summary>
        private readonly IActionSelector _actionSelector;
        private readonly ActionDocResourceManager _actionDocResourceManager;
        private readonly SdkBuilderConfig _sdkBuilderConfig;
        private readonly IApiDocBuilder _apiDocBuilder;
        private readonly IMediaTypeFormatterFactory _mediaTypeFormatterFactory;
        /// <summary>
        /// 跳过属性集合
        /// </summary>
        private readonly string[] _skipPropertys = new string[] { };// new string[] { "UserId", "UserName" };

        /// <summary>
        /// 接口初始化
        /// </summary>
        /// <param name="actionSelector">接口查找器，系统框架会自动赋值</param>
        /// <param name="apiPluginManager">接口扩展插件管理器</param>
        /// <param name="sdkBuilderConfig"></param>
        public BuildSdkAction(IActionSelector actionSelector, ActionDocResourceManager actionDocResourceManager, IApiDocBuilder apiDocBuilder, IMediaTypeFormatterFactory mediaTypeFormatterFactory, SdkBuilderConfig sdkBuilderConfig)
        {
            this._actionSelector = actionSelector;
            this._actionDocResourceManager = actionDocResourceManager;
            this._apiDocBuilder = apiDocBuilder;
            this._sdkBuilderConfig = sdkBuilderConfig;
            this._mediaTypeFormatterFactory = mediaTypeFormatterFactory;
        }

        /// <summary>
        /// 获取SDK基架类库文件信息
        /// </summary>
        /// <param name="sdkZipFilePath">SDK基架包</param>
        /// <param name="namespace">命名空间</param>
        /// <returns>key为：文件名称，value为：源代码</returns>
        private IList<KeyValuePair<string, string>> GetSdkBaseSources(string sdkZipFilePath, string @namespace)
        {
            //基架文件不存在
            if (!File.Exists(sdkZipFilePath))
            {
                return new List<KeyValuePair<string, string>>();
            }
            //用于保存SDK基架源文件,一个类一个字符串对象
            List<KeyValuePair<string, string>> sources = new List<KeyValuePair<string, string>>();
            using (ZipInputStream zipInputStream = new ZipInputStream(File.OpenRead(sdkZipFilePath)))
            {
                ZipEntry theEntry;
                while ((theEntry = zipInputStream.GetNextEntry()) != null)
                {
                    //获取文件名称
                    string fileName = Path.GetFileName(theEntry.Name);

                    //文件名称存在，并且是CS文件
                    if (fileName.IsNullOrEmpty() || !fileName.EndsWith(".cs", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }

                    //读取文件
                    Stream streamWriter = new MemoryStream();
                    byte[] data = new byte[2048];
                    while (true)
                    {
                        var size = zipInputStream.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            streamWriter.Write(data, 0, size);
                        }
                        else
                        {
                            streamWriter.Position = 0;
                            using (StreamReader streamReader = new StreamReader(streamWriter))
                            {
                                string source = streamReader.ReadToEnd().Replace("namespace SharpSword.SDK", "namespace {0}".With(@namespace));
                                sources.Add(new KeyValuePair<string, string>(fileName, source));
                            }
                            break;
                        }
                    }
                }
            }

            return sources.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private IList<KeyValuePair<string, string>> GetSdkApiExtensions()
        {
            var actions = (from item in this._actionSelector.GetActionDescriptors()
                .Where(o => o.CanPackageToSdk)
                           group item by item.ActionName into g
                           select g.OrderByDescending(o => o.Version).FirstOrDefault()).OrderBy(x => x.ActionName).ToList();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("/******************************************************************");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("* SharpSword System Auto-Generation At {0}".With(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("* *****************************************************************/");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.AppendFormat("using {0}.Request;", this._sdkBuilderConfig.SdkNamespace);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.AppendFormat("using {0}.Resp; ", this._sdkBuilderConfig.SdkNamespace);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("namespace {0} ".With(this._sdkBuilderConfig.SdkNamespace));
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("{");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("\t/// <summary>");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("\t/// Total APIS:【{0}】，CreateTime：{1}".With(actions.Count, Clock.Now));
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("\t/// </summary>");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("\tpublic static class IServerExtensions ");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("\t{ ");
            foreach (var action in actions)
            {
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("\t\t/// <summary>");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("\t\t/// {0}，{1}".With(action.ActionName, action.Description));
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("\t\t/// </summary>");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("\t\t/// <param name=\"server\">{0}.IApiServer</param>".With(this._sdkBuilderConfig.SdkNamespace));
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("\t\t/// <param name=\"request\">inherit from RequestBase{T}</param>");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("\t\t/// <param name=\"requestId\">请求编号（跟踪调用链），一般来说，不需要手工指定此值，客户端SDK会自动生成调用链请求ID，并且将调用深度自动附加到RequestId后面，比如：XDSDSFS.0->XDSDSFS.1</param>");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("\t\t/// <param name=\"cacheOptions\">本地缓存器设置</param>");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("\t\t/// <returns></returns>");
                stringBuilder.Append(Environment.NewLine);
                if (action.ResponseDtoType == typeof(NullResponseDto))
                {
                    stringBuilder.AppendFormat("\t\tpublic static ResponseBase {0}(this IApiServer server, {0}Request request, string requestId = null, CacheOptions cacheOptions = null)", action.ActionName.Replace(".", ""));
                }
                else
                {
                    stringBuilder.AppendFormat("\t\tpublic static {0}Resp {0}(this IApiServer server, {0}Request request, string requestId = null, CacheOptions cacheOptions = null)", action.ActionName.Replace(".", ""));
                }
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("\t\t{");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.AppendFormat("\t\t\treturn server.ApiClient.Execute(request, requestId, cacheOptions);");
                stringBuilder.Append(Environment.NewLine);
                stringBuilder.Append("\t\t}");
            }

            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("\t}");
            stringBuilder.Append(Environment.NewLine);
            stringBuilder.Append("}");


            List<KeyValuePair<string, string>> sources = new List<KeyValuePair<string, string>>();
            sources.Add(new KeyValuePair<string, string>("IServerExtensions.cs", stringBuilder.ToString()));
            return sources;
        }

        /// <summary>
        /// 创建using代码块
        /// </summary>
        /// <param name="actionDescriptor">接口描述对象</param>
        /// <returns></returns>
        private StringBuilder CreateUsingBlock(IActionDescriptor actionDescriptor)
        {
            StringBuilder codeBlockStringBuilder = new StringBuilder();
            codeBlockStringBuilder.Append("/**********************************************************");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("* SharpSword System Auto-Generation At {0}".With(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")));
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("* *********************************************************");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("* Assembly:{0}".With(actionDescriptor.ActionType.Assembly.GetName().Name));
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("* *********************************************************");
            codeBlockStringBuilder.Append(Environment.NewLine);
            foreach (var item in actionDescriptor.GetAttributes().OrderBy(o => o.Key))
            {
                codeBlockStringBuilder.Append("* {0}:{1}".With(item.Key, item.Value));
                codeBlockStringBuilder.Append(Environment.NewLine);
            }
            codeBlockStringBuilder.Append("* *******************************************************/");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("using System;");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("using System.Collections;");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("using System.Collections.Generic;");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("using System.Linq;");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("using System.Text;");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append(Environment.NewLine);
            return codeBlockStringBuilder;
        }

        /// <summary>
        /// 创建命名空间代码块
        /// </summary>
        /// <param name="namespace">命名空间</param>
        /// <param name="dtoType"></param>
        /// <returns></returns>
        private StringBuilder CreateNamespaceBlock(string @namespace, DtoTypeEnum dtoType)
        {
            StringBuilder codeBlockStringBuilder = new StringBuilder();
            if (dtoType == DtoTypeEnum.Request)
            {
                codeBlockStringBuilder.AppendFormat("namespace {0}.Request", @namespace);
            }
            else
            {
                codeBlockStringBuilder.AppendFormat("namespace {0}.Resp", @namespace);
            }
            return codeBlockStringBuilder;
        }

        /// <summary>
        /// 获取自定义复杂类型里的所有复杂类（方便构造出来后进行内部类的输出）
        /// </summary>
        /// <param name="objType">任意对象</param>
        /// <param name="results">返回对象内部所有的复杂类（遍历整个对象树）</param>
        private void GetComplexObjTypes(Type objType, IList<Type> results)
        {
            //复杂对象才去遍历
            if (this.IsComplexType(objType))
            {
                //如果是集合的话，输出特殊处理
                if (new Type[] { typeof(IEnumerable<>), typeof(ICollection<>), typeof(IList<>), typeof(List<>) }.Any(t => t.Name == objType.Name))
                {
                    objType = objType.GetGenericArguments()[0];
                    if (!results.Contains(objType))
                    {
                        results.Add(objType);
                    }
                }

                //遍历对象属性树
                foreach (var p in objType.GetProperties())
                {
                    //基元类型，直接返回
                    if (this.IsPrimitive(p.PropertyType))
                    {
                        continue;
                    }
                    //可空类型
                    else if (typeof(Nullable<>).Name == p.PropertyType.Name)
                    {
                        //基类型
                        var baseType = p.PropertyType.GetGenericArguments()[0].BaseType;

                        //当前类型
                        var genericArgumentType = p.PropertyType.GetGenericArguments()[0];

                        //可空类型是枚举或者是结构体（是枚举需要输出枚举类）
                        if (baseType == typeof(System.Enum)) //|| baseType == typeof(System.ValueType))
                        {
                            if (!results.Contains(genericArgumentType))
                            {
                                results.Add(genericArgumentType);
                            }
                        }

                        //否则继续下一个属性
                        continue;
                    }
                    //集合类型
                    else if (new Type[] { typeof(IEnumerable<>), typeof(ICollection<>), typeof(IList<>), typeof(List<>) }.Any(t => t.Name == p.PropertyType.Name) || p.PropertyType.IsArray)
                    {
                        //集合类参数
                        var genericArgument = p.PropertyType.IsArray ? p.PropertyType.GetElementType() : p.PropertyType.GetGenericArguments()[0];

                        //集合里的对象是否是复杂自定义对象
                        if (!this.IsPrimitive(genericArgument))
                        {
                            //添加到复杂对象集合
                            if (!results.Contains(genericArgument))
                            {
                                //添加到集合
                                results.Add(genericArgument);

                                //继续进行对象树访问
                                this.GetComplexObjTypes(genericArgument, results);
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                    //复杂自定义对象
                    else
                    {
                        //添加到复杂对象集合
                        if (!results.Contains(p.PropertyType))
                        {
                            results.Add(p.PropertyType);

                            //继续进行对象树访问
                            this.GetComplexObjTypes(p.PropertyType, results);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 是否是基元类型
        /// </summary>
        /// <param name="type">任意数据类型</param>
        /// <returns></returns>
        private bool IsPrimitive(Type type)
        {
            if (type.IsPrimitive || new Type[] { typeof(string), typeof(DateTime), typeof(decimal), typeof(TimeSpan), typeof(DateTimeOffset), typeof(Guid) }.Any(t => t == type))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 判断某个对象是否是复杂类
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private bool IsComplexType(Type type)
        {
            if (this.IsPrimitive(type))
            {
                return false;
            }
            else if (typeof(Nullable<>).Name == type.Name)
            {
                return false;
            }
            else if (new Type[] { typeof(IEnumerable<>), typeof(ICollection<>), typeof(IList<>), typeof(List<>) }.Any(t => t.Name == type.Name) || type.IsArray)
            {
                var genericArgument = type.IsArray ? type.GetElementType() : type.GetGenericArguments()[0];
                if (this.IsPrimitive(genericArgument))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 生成指定复杂类字符串
        /// </summary>
        /// <param name="type"></param>
        /// <param name="otherName"></param>
        /// <returns></returns>
        private string CreateComplexClass(Type type, string otherName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("/// <summary>");
            sb.Append(Environment.NewLine);
            foreach (string item in this._actionDocResourceManager.GetDescription(type.FullName).Split(Environment.NewLine.ToCharArray()))
            {
                sb.Append("/// " + item.Trim());
                sb.Append(Environment.NewLine);
            }
            sb.Append("/// </summary>");
            sb.Append(Environment.NewLine);

            //非枚举类型
            if (type.BaseType != typeof(Enum))
            {
                sb.Append("public class {0}".With(string.IsNullOrEmpty(otherName) ? type.Name : otherName));
                sb.Append(Environment.NewLine);
                sb.Append("{");
                sb.Append(Environment.NewLine);
                //type.GetProperties().Where(o => !this.skipPropertys.Any(str => str == o.Name)).ToList().ForEach(t =>
                type.GetProperties().ToList().ForEach(t =>
                 {
                     sb.Append("\t/// <summary>");
                     sb.Append(Environment.NewLine);
                     foreach (string item in this._actionDocResourceManager.GetDescription(string.Format("{0}.{1}", type.FullName, t.Name)).Split(Environment.NewLine.ToCharArray()))
                     {
                         sb.Append("\t/// " + item.Trim());
                         sb.Append(Environment.NewLine);
                     }
                     sb.Append("\t/// </summary>");
                     sb.Append(Environment.NewLine);
                     sb.Append("\tpublic {0} {1}".With(ConvertPropertyType(t.PropertyType), t.Name).Append(" { get; set; }"));
                     sb.Append(Environment.NewLine);
                 });
                sb.Append("}");
            }
            else
            {
                //是枚举类型
                sb.Append("public enum {0}".With(string.IsNullOrEmpty(otherName) ? type.Name : otherName));
                sb.Append(Environment.NewLine);
                sb.Append("{");
                sb.Append(Environment.NewLine);
                IList<string> enumValues = new List<string>();
                foreach (var item in Enum.GetValues(type))
                {
                    StringBuilder enumStringBulder = new StringBuilder();
                    enumStringBulder.Append(Environment.NewLine);
                    enumStringBulder.Append("\t/// <summary>");
                    enumStringBulder.Append(Environment.NewLine);
                    foreach (string x in this._actionDocResourceManager.GetDescription(string.Format("{0}.{1}", type.FullName, item.ToString())).Split(Environment.NewLine.ToCharArray()))
                    {
                        enumStringBulder.Append("\t/// " + x.Trim());
                        enumStringBulder.Append(Environment.NewLine);
                    }
                    enumStringBulder.Append("\t/// </summary>");
                    enumStringBulder.Append(Environment.NewLine);
                    enumStringBulder.Append("\t{0}={1}".With(item.ToString(), (int)item));
                    enumValues.Append(enumStringBulder.ToString());
                }

                sb.Append(string.Join("," + Environment.NewLine, enumValues.ToArray()));
                sb.Append(Environment.NewLine);
                sb.Append("}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// 生成主类字符串
        /// </summary>
        /// <param name="actionDescription"></param>
        /// <param name="dtoType"></param>
        /// <returns></returns>
        private string CreateMainClass(ActionDescriptor actionDescription, DtoTypeEnum dtoType)
        {
            StringBuilder sb = new StringBuilder();
            //接口名称
            string actionName = actionDescription.ActionName.Replace(".", "");
            sb.Append("/// <summary>");
            sb.Append(Environment.NewLine);
            sb.Append("/// Description:{0}".With(actionDescription.Description));
            sb.Append(Environment.NewLine);
            sb.Append("/// CreateTime:{0}".With(Clock.Now));
            sb.Append(Environment.NewLine);
            //foreach (string item in this._actionDocResourceManager.GetDescription(actionDescription.ActionType.FullName).Split(Environment.NewLine.ToCharArray()))
            //{
            //    sb.Append("/// " + item.Trim());
            //    sb.Append(Environment.NewLine);
            //}
            sb.Append("/// </summary>");
            sb.Append(Environment.NewLine);
            if (dtoType == DtoTypeEnum.Request)
            {
                //如果下送参数不为NullResponseDto
                if (actionDescription.ResponseDtoType != typeof(NullResponseDto))
                {
                    sb.Append("public class {0}Request : RequestBase<Resp.{0}Resp> ".With(actionName));
                }
                else
                {
                    sb.Append("public class {0}Request : RequestBase<ResponseBase> ".With(actionName));
                }
                sb.Append(Environment.NewLine);
                sb.Append("{");
                sb.Append(Environment.NewLine);
                sb.Append("\t/// <summary>");
                sb.Append(Environment.NewLine);
                sb.Append("\t/// 接口版本号，如：1.0");
                sb.Append(Environment.NewLine);
                sb.Append("\t/// </summary>");
                sb.Append(Environment.NewLine);
                sb.Append("\tprivate string _version = string.Empty;");
                sb.Append(Environment.NewLine);
                sb.Append("\t/// <summary>");
                sb.Append(Environment.NewLine);
                sb.Append("\t/// {0}".With(actionDescription.ActionName));
                sb.Append(Environment.NewLine);
                sb.Append("\t/// </summary>");
                sb.Append(Environment.NewLine);
                sb.Append("\t/// <param name=\"version\">API接口版本号，如：1.0。默认为空，不指定版本号，API接口自动选择同名接口最高版本提供服务</param>");
                sb.Append(Environment.NewLine);
                sb.Append("\tpublic {0}Request(string version = \"\")".With(actionName));
                sb.Append(Environment.NewLine);
                sb.Append("\t{");
                sb.Append(Environment.NewLine);
                sb.Append("\t\tthis._version = version; ");
                sb.Append(Environment.NewLine);
                sb.Append("\t}");
                sb.Append(Environment.NewLine);
            }
            else
            {
                sb.Append("[Serializable]");
                sb.Append(Environment.NewLine);
                sb.Append("public class {0}Resp : ResponseBase ".With(actionName));
                sb.Append(Environment.NewLine);
                sb.Append("{");
                sb.Append(Environment.NewLine);
                if (!this.IsComplexType(actionDescription.ResponseDtoType))    //非复杂类型
                {
                    sb.Append("\t/// <summary>");
                    sb.Append(Environment.NewLine);
                    sb.Append("\t///  ");
                    sb.Append(Environment.NewLine);
                    sb.Append("\t/// <summary>");
                    sb.Append(Environment.NewLine);
                    sb.Append("\tpublic " + ConvertPropertyType(actionDescription.ResponseDtoType) + " Data { get; set; }");
                    sb.Append(Environment.NewLine);
                }
                else
                {
                    sb.Append("\t/// <summary>");
                    sb.Append(Environment.NewLine);
                    sb.Append("\t///   ");
                    sb.Append(Environment.NewLine);
                    sb.Append("\t/// <summary>");
                    sb.Append(Environment.NewLine);

                    //如果是集合的话，输出特殊处理
                    if (new Type[] { typeof(IEnumerable<>), typeof(ICollection<>), typeof(IList<>), typeof(List<>) }.Any(t => t.Name == actionDescription.ResponseDtoType.Name))
                    {
                        sb.Append("\tpublic {0}<{1}RespData>".With(actionDescription.ResponseDtoType.Name.Substring(0, actionDescription.ResponseDtoType.Name.Length - 2), actionName) + " Data { get; set; }");
                    }
                    else
                    {
                        sb.Append("\tpublic {0}RespData".With(actionName) + " Data { get; set; }");
                    }

                    sb.Append(Environment.NewLine);
                }
            }

            //输出主类所有属性
            Type type = null;
            if (dtoType == DtoTypeEnum.Request)
            {
                type = actionDescription.RequestDtoType;

                type.GetProperties().Where(o => !this._skipPropertys.Any(str => str == o.Name)).ToList().ForEach(t =>
                {
                    sb.Append("\t/// <summary>");
                    sb.Append(Environment.NewLine);
                    sb.Append("\t/// 是否必填【" + this.IsRequired(t) + "】");
                    sb.Append(Environment.NewLine);
                    //基类
                    if (type.BaseType != null && type.BaseType != typeof(object))
                    {
                        foreach (string item in this._actionDocResourceManager.GetDescription(string.Format("{0}.{1}", type.BaseType, t.Name)).Split(Environment.NewLine.ToCharArray()))
                        {
                            if (!item.IsNullOrEmpty())
                            {
                                sb.Append("\t/// " + item.Trim());
                                sb.Append(Environment.NewLine);
                            }
                        }
                    }
                    foreach (string item in this._actionDocResourceManager.GetDescription(string.Format("{0}.{1}", type.FullName, t.Name)).Split(Environment.NewLine.ToCharArray()))
                    {
                        if (!item.IsNullOrEmpty())
                        {
                            sb.Append("\t/// " + item.Trim());
                            sb.Append(Environment.NewLine);
                        }
                    }
                    sb.Append("\t/// </summary>");
                    sb.Append(Environment.NewLine);
                    sb.Append("\tpublic {0} {1}".With(ConvertPropertyType(t.PropertyType), t.Name).Append(" { get; set; }"));
                    sb.Append(Environment.NewLine);
                });

                //重写版本号
                sb.Append(this.CreateVersionBlock(actionDescription));

                //输出GetApiName返回ActionName
                sb.Append(CreateApiNameMethodBlock(actionDescription));

                //输出GetRequestJsonData返回参数
                sb.Append(CreateRequestJsonDataMethodBlock(type));
            }

            sb.Append("}");

            return sb.ToString();
        }

        /// <summary>
        /// 是否是必填参数
        /// </summary>
        /// <param name="propertyInfo"></param>
        /// <returns></returns>
        public bool IsRequired(System.Reflection.PropertyInfo propertyInfo)
        {
            return propertyInfo.GetCustomAttributes<RequiredAttribute>().Any();
        }

        /// <summary>
        /// 格式化类字符串
        /// </summary>
        /// <param name="clsStr"></param>
        /// <param name="formatStr"></param>
        /// <returns></returns>
        private string FormatStr(string clsStr, string formatStr)
        {
            string result = string.Empty;
            string[] tempArray = clsStr.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            foreach (var s in tempArray)
            {
                result = result + formatStr + s + Environment.NewLine;
            }
            return result;
        }

        /// <summary>
        /// 重写版本号
        /// </summary>
        /// <param name="actionDescription"></param>
        /// <returns></returns>
        private StringBuilder CreateVersionBlock(ActionDescriptor actionDescription)
        {
            //接口名称
            string actionName = actionDescription.ActionName;
            StringBuilder codeBlockStringBuilder = new StringBuilder();
            codeBlockStringBuilder.Append("\t/// <summary>");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\t/// 重写接口{0}版本，默认不指定版本号".With(actionName));
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\t/// </summary>");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\t/// <returns></returns>");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\tpublic override string GetVersion()");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\t{");
            codeBlockStringBuilder.Append(Environment.NewLine);
            //输出GetApiName返回ActionName
            codeBlockStringBuilder.Append("\t\treturn this._version;");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\t}");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append(Environment.NewLine);
            return codeBlockStringBuilder;
        }

        /// <summary>
        /// 创建重写获取接口名称代码块
        /// </summary>
        /// <param name="actionDescription">接口对象描述信息</param>
        /// <returns></returns>
        private StringBuilder CreateApiNameMethodBlock(ActionDescriptor actionDescription)
        {
            //接口名称
            string actionName = actionDescription.ActionName;
            StringBuilder codeBlockStringBuilder = new StringBuilder();
            codeBlockStringBuilder.Append("\t/// <summary>");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\t/// 调用接口名称，{0}".With(actionName));
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\t/// </summary>");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\t/// <returns></returns>");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\tpublic override string GetApiName()");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\t{");
            codeBlockStringBuilder.Append(Environment.NewLine);
            //输出GetApiName返回ActionName
            codeBlockStringBuilder.Append("\t\treturn \"{0}\";".With(actionName));
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\t}");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append(Environment.NewLine);
            return codeBlockStringBuilder;
        }

        /// <summary>
        /// 获取重写获取对象JSON对象方法代码块
        /// </summary>
        /// <param name="requestDtoType">上送数据对象</param>
        /// <returns></returns>
        private StringBuilder CreateRequestJsonDataMethodBlock(Type requestDtoType)
        {
            StringBuilder codeBlockStringBuilder = new StringBuilder();
            codeBlockStringBuilder.Append("\t/// <summary>");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\t/// 请求参数json化");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\t/// </summary>");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\t/// <returns></returns>");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\tpublic override string GetRequestJsonData()");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\t{");
            codeBlockStringBuilder.Append(Environment.NewLine);

            //输出GetRequestJsonData返回参数
            //IList<string> properties = new List<string>();
            //requestDtoType.GetProperties().ToList().ForEach(p =>
            //{
            //    properties.Add("this.{0}".With(p.Name));
            //});

            //codeBlockStringBuilder.Append("\t\treturn new { ");
            ////参数有太多就换行，方便代码查看
            //if (properties.Count <= 5)
            //{
            //    codeBlockStringBuilder.Append(string.Join(",", properties.ToArray()));
            //}
            //else
            //{
            //    codeBlockStringBuilder.Append(string.Join(",{0}\t\t\t".With(Environment.NewLine), properties.ToArray()));
            //}
            //codeBlockStringBuilder.Append(" }.ToJson();");

            codeBlockStringBuilder.Append("\t\treturn this.ToJson();");

            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("\t}");

            return codeBlockStringBuilder;

        }

        /// <summary>
        /// 类文件字符串请放置于Data属性里面
        /// </summary>
        /// <returns></returns>
        public override ActionResult<BuildSdkActionResponseDto> Execute()
        {
            //命名空间
            string @sdkNamespace = this._sdkBuilderConfig.SdkNamespace;

            //返回对象
            var resultData = new BuildSdkActionResponseDto()
            {
                ActionSelector = this._actionSelector,
                SdkNamespace = @sdkNamespace,
                Version = ApiVersion.Version,
                SDKSource = string.Empty,
                RequestSource = string.Empty,
                ResponseSource = string.Empty
            };

            //存放上送接口类字符串
            IList<KeyValuePair<string, string>> request = new List<KeyValuePair<string, string>>();

            //存放下送接口类字符串
            IList<KeyValuePair<string, string>> response = new List<KeyValuePair<string, string>>();

            //保存扩展
            IList<KeyValuePair<string, string>> apiExtensions = new List<KeyValuePair<string, string>>();

            IList<string> actions = new List<string>();
            if (!this.RequestDto.ActionName.IsNullOrEmpty())
            {
                actions = new List<string>() { this.RequestDto.ActionName };
            }
            else
            {
                //同名接口只生产最新的接口SDK文档
                actions = (from item in this._actionSelector.GetActionDescriptors()
                               .Where(o => o.CanPackageToSdk)
                           group item by item.ActionName into g
                           select g.OrderByDescending(o => o.Version).FirstOrDefault().ActionName).ToList();
            }

            //获取扩展
            apiExtensions = this.GetSdkApiExtensions();

            //获取请求和输出信息
            foreach (string actionName in actions)
            {
                //直接从缓存里读取接口信息
                var actionDescription = this._actionSelector.GetActionDescriptor(actionName, this.RequestDto.Version);

                //未找到对应的接口
                if (actionDescription.IsNull())
                {
                    return this.ErrorActionResult("未找到接口：{0}".With(actionName), resultData);
                }

                try
                {
                    this.GetRequestSources(request, actionDescription);
                }
                catch { }

                try
                {
                    this.GetResponseSources(response, actionDescription);
                }
                catch (Exception exc)
                {

                }

                resultData.ActionDescriptor = actionDescription;
                resultData.RequestJson = "{}";
                resultData.ResponseJson = "{}";
                resultData.ResponseXml = "";
                //下送的数据
                try
                {
                    var actionResult = new ActionResult()
                    {
                        Data = this._apiDocBuilder.CreateInstance(actionDescription.ResponseDtoType),
                        Flag = ActionResultFlag.SUCCESS,
                        Info = "OK"
                    };


                    resultData.RequestJson = this._apiDocBuilder.CreateInstance(actionDescription.RequestDtoType).Serialize2FormatJosn();
                    resultData.ResponseJson = this._mediaTypeFormatterFactory.Create(ResponseFormat.JSON).SerializedActionResultToString(this.RequestContext, actionResult);
                    resultData.ResponseXml = this._mediaTypeFormatterFactory.Create(ResponseFormat.XML).SerializedActionResultToString(this.RequestContext, actionResult);
                }
                catch { }
            }



            string outputStr = "";
            foreach (var req in request)
            {
                outputStr = outputStr + Environment.NewLine + req.Value;
                resultData.RequestSource += Environment.NewLine + req.Value;
            }
            foreach (var res in response)
            {
                outputStr = outputStr + Environment.NewLine + res.Value;
                resultData.ResponseSource += Environment.NewLine + res.Value;
            }
            resultData.SDKSource = outputStr;

            if (!string.IsNullOrEmpty(this.RequestDto.SaveType))
            {
                //获取基架源代码路径
                var sdkBaseFile = this.RequestContext.HttpContext.Server.MapPath("/App_Data/sdk.zip");

                //获取SDK基架源代码
                var sdkBasesources = this.GetSdkBaseSources(sdkBaseFile, @sdkNamespace);

                //生成dll
                if (this.RequestDto.SaveType.ToLower() == "dll")
                {
                    //生成存放DLL的目录
                    var dllSaveDirectory = this.RequestContext.HttpContext.Server.MapPath("/App_Data/sdkdll");
                    if (!Directory.Exists(dllSaveDirectory))
                    {
                        Directory.CreateDirectory(dllSaveDirectory);
                    }

                    //sdk-dll名
                    string dllname = "{0}.dll".With(@sdkNamespace);

                    //bin目录
                    string binDirectoryPath = this.RequestContext.HttpContext.Server.MapPath("/bin");

                    //输出dll保存地址
                    string dllSavePath = Path.Combine(dllSaveDirectory, dllname);

                    //保存DLL注释文件XML路径
                    string docSavePath = Path.Combine(dllSaveDirectory, "{0}.XML".With(@sdkNamespace));

                    //获取代码编译器
                    CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
                    CompilerParameters compilerparams = new CompilerParameters();
                    //指定编译选项，是类库、并且生成XML注释文档
                    compilerparams.CompilerOptions = "/target:library /optimize /doc:{0}".With(docSavePath);
                    compilerparams.GenerateInMemory = false;
                    compilerparams.GenerateExecutable = false;
                    compilerparams.IncludeDebugInformation = false;
                    compilerparams.TreatWarningsAsErrors = false;
                    compilerparams.OutputAssembly = dllSavePath;

                    //加载默认系统dll文件  
                    string[] systemDlls = new string[] { "System.dll", "System.Core.dll", "System.Xml.Linq.dll", "System.Configuration.dll", "System.Data.DataSetExtensions.dll", "Microsoft.CSharp.dll", "System.Data.dll", "System.Xml.dll", "System.Web.dll" };
                    systemDlls.ToList().ForEach(assembly =>
                    {
                        compilerparams.ReferencedAssemblies.Add(assembly);
                    });

                    //获取bin目录下面的所有dll文件 排除当前生成dll
                    Directory.GetFiles(binDirectoryPath, "*.dll", SearchOption.TopDirectoryOnly).Select(path => Path.GetFileName(path)).Where(t => t != dllname).ToList().ForEach(assembly =>
                    {
                        compilerparams.ReferencedAssemblies.Add(Path.Combine(binDirectoryPath, assembly));
                    });

                    IList<string> sourceClassStrings = new List<string>();
                    foreach (var sdk in sdkBasesources)
                    {
                        sourceClassStrings.Add(sdk.Value);
                    }
                    foreach (var req in request)
                    {
                        sourceClassStrings.Add(req.Value);
                    }
                    foreach (var res in response)
                    {
                        sourceClassStrings.Add(res.Value);
                    }
                    foreach (var res in apiExtensions)
                    {
                        sourceClassStrings.Add(res.Value);
                    }

                    //编译源代码
                    var compilerResults = provider.CompileAssemblyFromSource(compilerparams, sourceClassStrings.ToArray());

                    //编译源码出现错误
                    if (compilerResults.Errors.HasErrors)
                    {
                        IList<string> errors = new List<string>();
                        foreach (CompilerError error in compilerResults.Errors)
                        {
                            errors.Add(String.Format("Error on line {0}: {1}", error.Line, error.ErrorText));
                        }
                        return this.ErrorActionResult("编译源文件错误；错误详情：\r\n{0}".With(string.Join("\r\n", errors.ToArray())), resultData);
                    }

                    this.RequestContext.HttpContext.Response.ContentType = "application/octet-stream";
                    this.RequestContext.HttpContext.Response.AddHeader("Connection", "Keep-Alive");
                    this.RequestContext.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename={0}".With(dllname));
                    this.RequestContext.HttpContext.Response.WriteFile(dllSavePath);

                }
                else if (this.RequestDto.SaveType.ToLower() == "source")
                {
                    //包名
                    string zipname = "{0}.zip".With(DateTime.Now.ToString("yyyyMMddhhmmss"));

                    //打包输出
                    var sdkurl = this.RequestContext.HttpContext.Server.MapPath("/App_Data/sdksource/{0}/{1}".With(@sdkNamespace, zipname));

                    //生成文件临时目录
                    var tempPath = this.RequestContext.HttpContext.Server.MapPath("/App_Data/sdksource/Temp");
                    if (Directory.Exists(tempPath))
                    {
                        Directory.Delete(tempPath, true);
                    }
                    this.OutPutCSFile(request, DtoTypeEnum.Request, tempPath);
                    this.OutPutCSFile(response, DtoTypeEnum.Response, tempPath);
                    this.OutPutCSFile(sdkBasesources, DtoTypeEnum.SDK, tempPath);
                    this.OutPutCSFile(apiExtensions, DtoTypeEnum.Apis, tempPath);
                    this.OutPutCSProj(request, response, sdkBasesources, apiExtensions, tempPath);
                    if (this.Packing(tempPath, sdkurl, PackingScope.All))
                    {
                        Directory.Delete(tempPath, true);
                    }
                    this.RequestContext.HttpContext.Response.ContentType = "application/zip";
                    this.RequestContext.HttpContext.Response.AddHeader("Content-Disposition", "attachment; filename={0}.{1}".With(@sdkNamespace, zipname));
                    this.RequestContext.HttpContext.Response.WriteFile(sdkurl);
                    this.RequestContext.HttpContext.Response.End();
                }
            }

            return this.SuccessActionResult(resultData);
        }

        /// <summary>
        /// 生成Request上送类字符串数组
        /// </summary>
        /// <param name="request"></param>
        /// <param name="actionDescription"></param>
        private void GetRequestSources(IList<KeyValuePair<string, string>> request, ActionDescriptor actionDescription)
        {
            //接口名称
            string actionName = actionDescription.ActionName.Replace(".", "");

            //上送参数
            var requestDtoType = actionDescription.RequestDtoType;

            //输出类字符串集合结果
            Dictionary<string, string> requestDic = new Dictionary<string, string>();
            IList<Type> requestTypes = new List<Type>();

            //获取所有复杂类
            this.GetComplexObjTypes(requestDtoType, requestTypes);

            //生成主类
            requestDic.Add(requestDtoType.FullName, CreateMainClass(actionDescription, DtoTypeEnum.Request));

            //生成所有复杂类
            foreach (var t in requestTypes)
            {
                requestDic.Add(t.FullName, CreateComplexClass(t, ""));
            }

            string requestStr = "";
            foreach (var cls in requestDic)
            {
                //判断是否为主类
                if (cls.Key == requestDtoType.FullName)
                {
                    string tempStr = cls.Value.Trim().Substring(0, cls.Value.Trim().Length - 1);
                    requestStr = FormatStr(tempStr, "\t") + Environment.NewLine + requestStr;
                }
                else
                {
                    requestStr = requestStr + FormatStr(cls.Value.Trim(), "\t\t") + Environment.NewLine;
                }
            }
            requestStr = requestStr + "\t}";
            requestStr = requestStr + Environment.NewLine;
            request.Add(new KeyValuePair<string, string>("{0}Request.cs".With(actionName), CreateUsingBlock(actionDescription) + CreateNamespaceBlock(this._sdkBuilderConfig.SdkNamespace, DtoTypeEnum.Request).ToString() + Environment.NewLine +
                         "{" + Environment.NewLine +
                               requestStr +
                         "}"));
        }

        /// <summary>
        /// 生成Response下送类字符串数组
        /// </summary>
        /// <param name="response"></param>
        /// <param name="actionDescription"></param>
        private void GetResponseSources(IList<KeyValuePair<string, string>> response, ActionDescriptor actionDescription)
        {
            //接口名称
            string actionName = actionDescription.ActionName.Replace(".", "");
            //下送参数
            var respDtoType = actionDescription.ResponseDtoType;

            if (respDtoType != typeof(NullResponseDto))       //下送参数为NullResponseDto不必输出
            {
                IList<string> respList = new List<string>();   //输出类字符串集合结果
                IList<Type> respTypes = new List<Type>();

                if (this.IsComplexType(respDtoType))              //复杂对象添加
                {
                    if (new Type[] { typeof(IEnumerable<>), typeof(ICollection<>), typeof(IList<>), typeof(List<>) }.Any(t => t.Name == respDtoType.Name))
                    {
                        respTypes.Add(respDtoType.GetGenericArguments()[0]);
                    }
                    else
                    {
                        respTypes.Add(respDtoType);
                    }
                }

                this.GetComplexObjTypes(respDtoType, respTypes);     //获取所有复杂类
                respList.Add(CreateMainClass(actionDescription, DtoTypeEnum.Response));   //生成主类

                int count = 0;
                foreach (var t in respTypes)
                {
                    count = count + 1;
                    string othername = string.Empty;
                    if (count == 1)
                    {
                        othername = "{0}RespData".With(actionDescription.ActionName.Replace(".", ""));
                    }
                    respList.Add(CreateComplexClass(t, othername));        //生成所有复杂类
                }

                string respStr = "";
                int countCls = 0;
                foreach (var cls in respList)
                {
                    countCls = countCls + 1;
                    if (countCls == 1)     //判断是否为主类
                    {
                        string tempStr = cls.Trim().Substring(0, cls.Trim().Length - 1);
                        respStr = FormatStr(tempStr, "\t") + Environment.NewLine + respStr;
                    }
                    else
                    {
                        respStr = respStr + FormatStr(cls.Trim(), "\t\t") + Environment.NewLine;
                    }
                }

                respStr = respStr + "\t}";
                respStr = respStr + Environment.NewLine;

                response.Add(new KeyValuePair<string, string>("{0}Resp.cs".With(actionName), CreateUsingBlock(actionDescription) + CreateNamespaceBlock(this._sdkBuilderConfig.SdkNamespace, DtoTypeEnum.Response).ToString() + Environment.NewLine +
                         "{" + Environment.NewLine +
                               respStr +
                         "}"));
            }
        }

        /// <summary>
        /// 生成cs文件
        /// </summary>
        /// <param name="source"></param>
        /// <param name="dtoType"></param>
        /// <param name="path"></param>
        private void OutPutCSFile(IList<KeyValuePair<string, string>> source, DtoTypeEnum dtoType, string path)
        {
            string dirPath = string.Empty;
            if (dtoType == DtoTypeEnum.Request)
            {
                dirPath = "{0}/Request".With(path);
            }
            else if (dtoType == DtoTypeEnum.Response)
            {
                dirPath = "{0}/Resp".With(path);
            }
            else if (dtoType == DtoTypeEnum.Apis)
            {
                dirPath = "{0}/Apis".With(path);
            }
            else
            {
                dirPath = path;
            }
            if (!Directory.Exists(dirPath))
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
                directoryInfo.Create();
            }
            foreach (var o in source)
            {
                this.WriteFile("{0}/{1}".With(dirPath, o.Key), o.Value);
            }
        }

        /// <summary>
        /// 输出csproj文件
        /// </summary>
        /// <param name="request"></param>
        /// <param name="response"></param>
        /// <param name="sdkBasesources"></param>
        /// <param name="path"></param>
        private void OutPutCSProj(IList<KeyValuePair<string, string>> request, IList<KeyValuePair<string, string>> response, IList<KeyValuePair<string, string>> sdkBasesources, IList<KeyValuePair<string, string>> apisources, string path)
        {
            string @namespace = this._sdkBuilderConfig.SdkNamespace;
            StringBuilder csProjStringBuilder = new StringBuilder();
            csProjStringBuilder.Append("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
            csProjStringBuilder.Append(Environment.NewLine + "<Project ToolsVersion=\"4.0\" DefaultTargets=\"Build\" xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
            csProjStringBuilder.Append(Environment.NewLine + "<Import Project=\"$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props\" Condition=\"Exists('$(MSBuildExtensionsPath)\\$(MSBuildToolsVersion)\\Microsoft.Common.props')\" />");
            csProjStringBuilder.Append(Environment.NewLine + "<PropertyGroup>");
            csProjStringBuilder.Append(Environment.NewLine + "    <Configuration Condition=\" '$(Configuration)' == '' \">Debug</Configuration>");
            csProjStringBuilder.Append(Environment.NewLine + "    <Platform Condition=\" '$(Platform)' == '' \">AnyCPU</Platform>");
            csProjStringBuilder.Append(Environment.NewLine + "    <ProjectGuid>{851717B6-7F86-40A0-AC8A-C556DF9B2311}</ProjectGuid>");
            csProjStringBuilder.Append(Environment.NewLine + "    <OutputType>Library</OutputType>");
            csProjStringBuilder.Append(Environment.NewLine + "    <AppDesignerFolder>Properties</AppDesignerFolder>");
            csProjStringBuilder.Append(Environment.NewLine + "    <RootNamespace>{0}</RootNamespace>".With(@namespace));
            csProjStringBuilder.Append(Environment.NewLine + "    <AssemblyName>{0}</AssemblyName>".With(@namespace));
            csProjStringBuilder.Append(Environment.NewLine + "    <TargetFrameworkVersion>v4.5</TargetFrameworkVersion>");
            csProjStringBuilder.Append(Environment.NewLine + "    <FileAlignment>512</FileAlignment>");
            csProjStringBuilder.Append(Environment.NewLine + "</PropertyGroup>");
            csProjStringBuilder.Append(Environment.NewLine + "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Debug|AnyCPU' \">");
            csProjStringBuilder.Append(Environment.NewLine + "    <DebugSymbols>true</DebugSymbols>");
            csProjStringBuilder.Append(Environment.NewLine + "    <DebugType>full</DebugType>");
            csProjStringBuilder.Append(Environment.NewLine + "    <Optimize>false</Optimize>");
            csProjStringBuilder.Append(Environment.NewLine + "    <OutputPath>bin\\Debug\\</OutputPath>");
            csProjStringBuilder.Append(Environment.NewLine + "    <DefineConstants>DEBUG;TRACE</DefineConstants>");
            csProjStringBuilder.Append(Environment.NewLine + "    <ErrorReport>prompt</ErrorReport>");
            csProjStringBuilder.Append(Environment.NewLine + "    <WarningLevel>4</WarningLevel>");
            csProjStringBuilder.Append(Environment.NewLine + "</PropertyGroup>");
            csProjStringBuilder.Append(Environment.NewLine + "<PropertyGroup Condition=\" '$(Configuration)|$(Platform)' == 'Release|AnyCPU' \">");
            csProjStringBuilder.Append(Environment.NewLine + "    <DebugType>pdbonly</DebugType>");
            csProjStringBuilder.Append(Environment.NewLine + "   <Optimize>true</Optimize>");
            csProjStringBuilder.Append(Environment.NewLine + "    <OutputPath>bin\\Release\\</OutputPath>");
            csProjStringBuilder.Append(Environment.NewLine + "    <DefineConstants>TRACE</DefineConstants>");
            csProjStringBuilder.Append(Environment.NewLine + "    <ErrorReport>prompt</ErrorReport>");
            csProjStringBuilder.Append(Environment.NewLine + "    <WarningLevel>4</WarningLevel>");
            csProjStringBuilder.Append(Environment.NewLine + "</PropertyGroup>");
            csProjStringBuilder.Append(Environment.NewLine + "<ItemGroup>");
            csProjStringBuilder.Append(Environment.NewLine + "    <Reference Include=\"System\" />");
            csProjStringBuilder.Append(Environment.NewLine + "    <Reference Include=\"System.Core\" />");
            csProjStringBuilder.Append(Environment.NewLine + "    <Reference Include=\"System.Web\" />");
            csProjStringBuilder.Append(Environment.NewLine + "    <Reference Include=\"System.Xml.Linq\" />");
            csProjStringBuilder.Append(Environment.NewLine + "    <Reference Include=\"System.Configuration\" />");
            csProjStringBuilder.Append(Environment.NewLine + "    <Reference Include=\"System.Data.DataSetExtensions\" />");
            csProjStringBuilder.Append(Environment.NewLine + "    <Reference Include=\"Microsoft.CSharp\" />");
            csProjStringBuilder.Append(Environment.NewLine + "    <Reference Include=\"System.Data\" />");
            csProjStringBuilder.Append(Environment.NewLine + "    <Reference Include=\"System.Xml\" />");
            csProjStringBuilder.Append(Environment.NewLine + "</ItemGroup>");
            csProjStringBuilder.Append(Environment.NewLine + "<ItemGroup>");
            foreach (var req in request)
            {
                csProjStringBuilder.Append(Environment.NewLine + "  <Compile Include=\"Request\\{0}\" />".With(req.Key));
            }
            foreach (var res in response)
            {
                csProjStringBuilder.Append(Environment.NewLine + "  <Compile Include=\"Resp\\{0}\" />".With(res.Key));
            }
            foreach (var res in apisources)
            {
                csProjStringBuilder.Append(Environment.NewLine + "  <Compile Include=\"Apis\\{0}\" />".With(res.Key));
            }
            foreach (var sdk in sdkBasesources)
            {
                csProjStringBuilder.Append(Environment.NewLine + "  <Compile Include=\"{0}\" />".With(sdk.Key));
            }

            csProjStringBuilder.Append(Environment.NewLine + "</ItemGroup>");
            csProjStringBuilder.Append(Environment.NewLine + "<Import Project=\"$(MSBuildToolsPath)\\Microsoft.CSharp.targets\" />");
            csProjStringBuilder.Append(Environment.NewLine + "</Project>");
            this.WriteFile("{0}/{1}.csproj".With(path, @namespace), csProjStringBuilder.ToString());
        }

        /// <summary>
        /// 转换属性输出类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private string ConvertPropertyType(Type type)
        {
            string strOUT = string.Empty;
            //属性类型名称
            string strIN = string.Empty;
            //基元类型 
            if (this.IsPrimitive(type))
            {
                strIN = type.Name;
            }
            //可空类型
            else if (typeof(Nullable<>).Name == type.Name)
            {
                strIN = type.GetGenericArguments()[0].Name + "?";
            }
            //集合类型
            else if (new Type[] { typeof(IEnumerable<>), typeof(ICollection<>), typeof(IList<>), typeof(List<>) }.Any(t => t.Name == type.Name))   //集合类型
            {
                strIN = "{0}<{1}>".With(type.Name.Substring(0, type.Name.Length - 2), type.GetGenericArguments()[0].Name);
            }
            else
            {
                strIN = type.Name;
            }
            switch (strIN)
            {
                case "Byte":
                    strOUT = "byte";
                    break;
                case "Int16":
                    strOUT = "short";
                    break;
                case "Int32":
                    strOUT = "int";
                    break;
                case "Int32[]":
                    strOUT = "int[]";
                    break;
                case "Int64":
                    strOUT = "long";
                    break;
                case "Int64[]":
                    strOUT = "long[]";
                    break;
                case "Char":
                    strOUT = "char";
                    break;
                case "Single":
                    strOUT = "float";
                    break;
                case "Double":
                    strOUT = "double";
                    break;
                case "Double[]":
                    strOUT = "double[]";
                    break;
                case "Decimal":
                    strOUT = "decimal";
                    break;
                case "Decimal[]":
                    strOUT = "decimal[]";
                    break;
                case "Object":
                    strOUT = "object";
                    break;
                case "Object[]":
                    strOUT = "object[]";
                    break;
                case "String":
                    strOUT = "string";
                    break;
                case "String[]":
                    strOUT = "string[]";
                    break;
                case "Boolean":
                    strOUT = "bool";
                    break;
                case "Boolean[]":
                    strOUT = "bool[]";
                    break;
                case "Int32?":
                    strOUT = "int?";
                    break;
                case "Int64?":
                    strOUT = "long?";
                    break;
                case "Double?":
                    strOUT = "double?";
                    break;
                case "Single?":
                    strOUT = "float?";
                    break;
                case "Decimal?":
                    strOUT = "decimal?";
                    break;
                case "Boolean?":
                    strOUT = "bool?";
                    break;
                case "Icollection<String>":
                    strOUT = "ICollection<string>";
                    break;
                case "IList<String>":
                    strOUT = "IList<string>";
                    break;
                case "List<String>":
                    strOUT = "List<string>";
                    break;
                case "IList<Int32>":
                    strOUT = "IList<int>";
                    break;
                case "List<Int32>":
                    strOUT = "List<int>";
                    break;
                case "IList<Int64>":
                    strOUT = "IList<long>";
                    break;
                case "List<Int64>":
                    strOUT = "List<long>";
                    break;
                case "IEnumerable<String>":
                    strOUT = "IEnumerable<string>";
                    break;
                default:
                    strOUT = strIN;
                    break;
            }
            return strOUT;
        }

        /// <summary>
        /// Dto类型
        /// </summary>
        enum DtoTypeEnum
        {
            /// <summary>
            /// 上送
            /// </summary>
            Request = 0,
            /// <summary>
            /// 下送
            /// </summary>
            Response = 1,
            /// <summary>
            /// SDK
            /// </summary>
            SDK = 2,
            /// <summary>
            /// api扩展
            /// </summary>
            Apis
        }

        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="content"></param>
        private void WriteFile(string filename, string content)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            StreamWriter sw = new StreamWriter(fs, Encoding.Default);
            sw.Write(content);
            sw.Close();
        }

        /// <summary>
        /// 打包文件
        /// </summary>
        /// <param name="directoryPath">需要打包的目录</param>
        /// <param name="fileName">打包之后保存的文件名称，如D:\packing.zip</param>
        /// <param name="scope">打包的范围</param>
        /// <returns></returns>
        private bool Packing(string directoryPath, string fileName, PackingScope scope)
        {
            bool result = false;
            List<FileInfo> filesInfo = new List<FileInfo>();
            Crc32 crc = new Crc32();
            ZipOutputStream s = null;
            int i = 1;
            try
            {
                FileInfo filedd = new FileInfo(fileName);
                if (!Directory.Exists(filedd.Directory.FullName))
                {
                    Directory.CreateDirectory(filedd.Directory.FullName);
                }
                s = new ZipOutputStream(File.OpenWrite(fileName));
                s.SetLevel(9);

                DirectoryInfo mainDir = new DirectoryInfo(directoryPath);
                filesInfo = GetFileList(mainDir.FullName, scope);
                foreach (FileInfo file in filesInfo)
                {
                    using (FileStream fs = File.OpenRead(file.FullName))
                    {
                        byte[] buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        ZipEntry entry = new ZipEntry(ZipEntry.CleanName(file.FullName.Replace(mainDir.FullName + "\\", "")));
                        entry.DateTime = DateTime.Now;
                        entry.Comment = i.ToString();
                        entry.ZipFileIndex = i++;
                        entry.Size = fs.Length;
                        fs.Close();
                        crc.Reset();
                        crc.Update(buffer);
                        entry.Crc = crc.Value;
                        s.PutNextEntry(entry);
                        s.Write(buffer, 0, buffer.Length);
                    }
                }
                s.Finish();
                s.Close();

                result = true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                s.Close();
            }
            return result;
        }

        /// <summary>
        /// 获取指定范围内的待压缩文件列表
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <param name="scope"></param>
        /// <returns></returns>
        private List<FileInfo> GetFileList(string directoryPath, PackingScope scope)
        {
            List<FileInfo> filesInfo = new List<FileInfo>();
            DirectoryInfo dir = new DirectoryInfo(directoryPath);
            if (scope != PackingScope.Folder)
            {
                FileInfo[] files = dir.GetFiles();
                foreach (FileInfo fTemp in files)
                {
                    filesInfo.Add(fTemp);
                }
            }
            if (scope != PackingScope.File)
            {
                DirectoryInfo[] dirs = dir.GetDirectories();
                filesInfo.AddRange(dirs.SelectMany(dirTemp => GetFileList(dirTemp.FullName, PackingScope.All)));
            }
            return filesInfo;
        }

        /// <summary>
        /// 压缩范围
        /// </summary>
        enum PackingScope
        {
            Folder,
            File,
            All
        }
    }
}