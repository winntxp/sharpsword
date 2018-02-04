/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/12 9:45:02
 * ****************************************************************/
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace SharpSword.ViewEngine.Impl
{
    /// <summary>
    /// 接口框架视图引擎；使用webform语法
    /// </summary>
    public class DefaultViewEngine : IViewEngine
    {
        /// <summary>
        /// 编译源码的语言
        /// </summary>
        private readonly string _language = "C#";

        /// <summary>
        /// 编译源文件需要的系统框架dll
        /// </summary>
        private readonly string[] _systemDlls = new string[] { "System.dll", "System.Core.dll", "System.Web.dll", "System.Linq.dll" };

        /// <summary>
        /// 缓存视图文件便于后的程序集，提高执行性能
        /// </summary>
        private static readonly IDictionary<string, KeyValuePair<SectionCollection, Assembly>>
            CacheAssemblys = new Dictionary<string, KeyValuePair<SectionCollection, Assembly>>();

        /// <summary>
        /// 默认使用C#语言编译器
        /// </summary>
        public DefaultViewEngine() : this(Language.CSharp) { }

        /// <summary>
        /// 指定VIEW视图使用的语言，C#或者VB
        /// </summary>
        /// <param name="language">编译器语言</param>
        private DefaultViewEngine(Language language)
        {
            this._language = language.ToString();
        }

        /// <summary>
        /// 编译视图也需要引入的第三方dll程序集
        /// </summary>
        public string[] Assemblies { get; set; }

        /// <summary>
        /// 编译视图需要引入的命名空间
        /// </summary>
        public string[] Namespaces { get; set; }

        /// <summary>
        /// 支持的后缀
        /// </summary>
        public string SupportedExtension
        {
            get
            {
                return ".aspx";
            }
        }

        /// <summary>
        /// 除掉空行
        /// </summary>
        /// <param name="streamReader"></param>
        /// <returns></returns>
        private string RemoveEmptyLine(TextReader streamReader)
        {
            var lines = new List<string>();
            string line = null;
            do
            {
                line = streamReader.ReadLine();
                if (!line.IsNullOrEmpty())
                {
                    lines.Add(line);
                }
            }
            while (!line.IsNull());

            //为了能够增加空行，我们定义一个空行标签 <!--NewLine-->
            return Regex.Replace(string.Join(Environment.NewLine, lines.ToArray()), "<!--NewLine-->",
                                        string.Empty, RegexOptions.IgnoreCase);

        }

        /// <summary>
        /// 根据视图文件原始文件内容，编译视图文件到程序集
        /// </summary>
        /// <param name="viewSource">视图文件源码</param>
        /// <param name="assemblies">引用的程序集集合</param>
        /// <param name="namespaces">需要添加的命名空间集合</param>
        /// <param name="parameters">视图定义的需要输入的参数集合</param>
        /// <param name="response">将执行后的视图保存到数据流</param>
        /// <returns>the comiled assembly</returns>
        private void CompileByViewSource(string viewSource, string[] assemblies, string[] namespaces, IViewParameterCollection parameters, StreamWriter response)
        {
            //源码为空，直接抛出异常
            viewSource.CheckNullThrowArgumentNullException(nameof(viewSource));

            //计算源文件的MD5值，具有相同MD5值的源文件，必须会有相同的编译结果；
            //仅仅不同的是，属性参数的不同
            var viewSourceMd5 = MD5.Encrypt(viewSource);

            //检测缓存系统里是否存在已经编译的视图模板文件
            if (CacheAssemblys.ContainsKey(viewSourceMd5))
            {
                var item = CacheAssemblys[viewSourceMd5];
                //直接执行已经编译的视图文件
                item.Key.Process(item.Value, parameters, response);
                return;
            }

            //引用的程序集
            this.Assemblies = assemblies;

            //引用的命名空间
            this.Namespaces = namespaces;

            //分析视图原文件
            var sections = ViewParser.ParsePage(viewSource, string.Empty);

            //获取代码编译器
            CodeDomProvider provider = CodeDomProvider.CreateProvider(this._language);
            CompilerParameters compilerparams = new CompilerParameters
            {
                GenerateInMemory = true,
                GenerateExecutable = false,
                IncludeDebugInformation = false
            };
            //system dll
            foreach (var sysDll in this._systemDlls)
                compilerparams.ReferencedAssemblies.Add(sysDll);
            //setup references assemblies
            if (!this.Assemblies.IsNull())
            {
                foreach (string assembly in this.Assemblies)
                {
                    compilerparams.ReferencedAssemblies.Add(assembly);
                }
            }

            //引入编译需要的第三方DLL
            foreach (Section section in sections)
            {
                if (section.Type == SectionType.Directive)
                {
                    if (section.Values.Directive.Equals("assembly", StringComparison.OrdinalIgnoreCase))
                    {
                        string assembly;
                        if (section.Values.TryGetValue("name", out assembly))
                        {
                            compilerparams.ReferencedAssemblies.Add(Path.Combine(HostHelper.GetBinDirectory(), assembly + ".dll"));
                        }
                    }
                }
            }

            //获取源代码
            string sourceClassString = sections.ExtractSource(namespaces);

            //源代码为空，直接返回
            if (sourceClassString.Length == 0)
            {
                return;
            }

            //编译源代码
            var compilerResults = provider.CompileAssemblyFromSource(compilerparams, new string[] { sourceClassString });

            //编译错误，直接将错误信息输出
            if (compilerResults.Errors.HasErrors)
            {
                //先输出下原始的视图源文件
                response.WriteLine(sourceClassString);

                //将错误信息输出到源文件最后
                foreach (CompilerError error in compilerResults.Errors)
                {
                    response.WriteLine("Error on line {0}: {1}", error.Line, error.ErrorText);
                }
            }
            //编译成功，执行编译结果
            else
            {
                //压入到缓存，方便下次快速访问，无需再次编译（多次编译在执行很多次的情况下，内存会暴增，因为前面编译的程序集，并没有释放）
                if (!CacheAssemblys.ContainsKey(viewSourceMd5))
                    CacheAssemblys.Add(viewSourceMd5, new KeyValuePair<SectionCollection, Assembly>(sections, compilerResults.CompiledAssembly));
                //执行一次编译
                sections.Process(compilerResults.CompiledAssembly, parameters, response);
            }
        }

        /// <summary>
        /// 根据提供的视图文件路径，编译视图文件到程序集
        /// </summary>
        /// <param name="viewPath">视图文件路径，请输入绝对路径比如：g:\\temp\t.aspx</param>
        /// <param name="assemblies">视图引用类型需要用到的程序集</param>
        /// <param name="namespaces">视图引用类型需要用到的命名空间（如果类全部是完整的输入，不需要命名空间，但是需要引用所属的程序集）</param>
        /// <param name="parameters">视图对外公开的参数（即输入模型对象）</param>
        /// <param name="response">将视图执行结果输出到流</param>
        private void CompileByViewPath(string viewPath, string[] assemblies, string[] namespaces, IViewParameterCollection parameters, StreamWriter response)
        {
            //路径不能为空
            if (viewPath.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(viewPath));
            }

            //指定的视图文件不存在
            if (!File.Exists(viewPath))
            {
                throw new Exception("视图文件：{0} 未找到".With(viewPath));
            }

            //读取视图文件
            var viewSource = File.ReadAllText(viewPath);

            //编译源文件，生成对应的程序集
            this.CompileByViewSource(viewSource, assemblies, namespaces, parameters, response);
        }

        /// <summary>
        /// 编译视图文件并执行视图
        /// </summary>
        /// <param name="viewPath">视图文件路径，比如：g:\\temp\t.aspx 或者 ~/temp/t.aspx</param>
        /// <param name="parameters">视图定义的参数集合</param>
        /// <param name="encode">视图文件文件编码</param>
        /// <returns>返回视图执行结果字符串</returns>
        public string CompileByViewPath(string viewPath, IViewParameterCollection parameters, Encoding encode)
        {
            encode.CheckNullThrowArgumentNullException(nameof(encode));

            MemoryStream memoryStream = new MemoryStream();
            StreamWriter response = new StreamWriter(memoryStream, encode) { AutoFlush = true };
            this.CompileByViewPath(HostHelper.MapPath(viewPath), null, null, parameters, response);
            memoryStream.Position = 0;
            StreamReader streamReader = new StreamReader(memoryStream, encode);
            var responseString = this.RemoveEmptyLine(streamReader);
            streamReader.Dispose();
            return responseString;
        }

        /// <summary>
        /// 编译视图文件并执行视图
        /// </summary>
        /// <param name="viewSource">视图文件源码</param>
        /// <param name="parameters">视图定义的参数集合</param>
        /// <param name="encode">视图文件文件编码</param>
        /// <returns>编译视图源代码，并将视图执行结果返回</returns>
        public string CompileByViewSource(string viewSource, IViewParameterCollection parameters, Encoding encode)
        {
            encode.CheckNullThrowArgumentNullException(nameof(encode));

            MemoryStream memoryStream = new MemoryStream();
            StreamWriter response = new StreamWriter(memoryStream, encode) { AutoFlush = true };
            this.CompileByViewSource(viewSource, null, null, parameters, response);
            memoryStream.Position = 0;
            StreamReader streamReader = new StreamReader(memoryStream, encode);
            var responseString = this.RemoveEmptyLine(streamReader);

            streamReader.Dispose();

            return responseString;
        }
    }
}
