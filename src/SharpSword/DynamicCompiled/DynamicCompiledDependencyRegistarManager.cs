/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/29 12:36:24
 * ****************************************************************/
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SharpSword
{
    /// <summary>
    /// 动态编译外部实现IDynamicCompiledDependencyRegistar接口的文件，并且注册
    /// 外部实现的类文件，请不要带有参数的构造函数
    /// </summary>
    internal class DynamicCompiledDependencyRegistarManager
    {
        /// <summary>
        /// 动态编译外部类
        /// </summary>
        /// <param name="sourceFilePath">
        /// 待动态编译的.net类库文件
        /// 编译文件需要引用的dll名称（dll文件在host情况下，请放置于bin目录，其他防止于根目录即可）
        /// 系统已经默认注册了当前程序域bin目录所在文件夹的所有dll
        /// </param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">参数sourceFilePath为null</exception>
        /// <exception cref="SharpSwordCoreException">文件不存在或者编译错误</exception>
        public static void Registar(string sourceFilePath)
        {
            //未指定文件直接抛出异常
            if (sourceFilePath.IsNullOrEmpty())
            {
                throw new ArgumentNullException(nameof(sourceFilePath));
            }

            //指定文件不存在，直接抛出异常
            if (!File.Exists(sourceFilePath))
            {
                throw new SharpSwordCoreException("文件：{0} 不存在，请检查显示目录是否存在此文件，此文件为接口配置文件".With(sourceFilePath));
            }

            //读取源码(按照UTF-8编码读取)
            string sourceClassString;
            using (StreamReader streamReader = new StreamReader(sourceFilePath, Encoding.UTF8))
            {
                sourceClassString = streamReader.ReadToEnd();
            }

            //源文件无内容，直接跳过了，不编译
            if (sourceClassString.IsNullOrEmpty())
            {
                //throw new ApiException("源文件：{0}无内容".With(sourceFilePath));
                return;
            }

            //获取代码编译器
            CodeDomProvider provider = CodeDomProvider.CreateProvider("C#");
            CompilerParameters compilerparams = new CompilerParameters
            {
                GenerateInMemory = true,
                GenerateExecutable = false,
                IncludeDebugInformation = false
            };

            //默认加载系统dll文件
            string[] systemDlls = { "mscorlib.dll",
                                    "System.dll",
                                    "System.Core.dll",
                                    "System.Web.dll",
                                    "System.Linq.dll",
                                    "System.Reflection.dll"
                                   };
            systemDlls.ToList().ForEach(assembly =>
            {
                compilerparams.ReferencedAssemblies.Add(assembly);
            });

            //获取bin文件夹路径
            string binDirectoryPath = HostHelper.GetBinDirectory();

            //获取bin目录下面的所有dll文件
            Directory.GetFiles(binDirectoryPath, "*.dll", SearchOption.TopDirectoryOnly)
                     .Select(Path.GetFileName)
                     .ToList()
                     .ForEach(assembly =>
                     {
                         compilerparams.ReferencedAssemblies.Add(Path.Combine(binDirectoryPath, assembly));
                     });

            //编译源代码
            var compilerResults = provider.CompileAssemblyFromSource(compilerparams, new string[] { sourceClassString });

            //编译错误，直接将错误信息输出
            if (compilerResults.Errors.HasErrors)
            {
                IList<string> errors = (from CompilerError error
                                        in compilerResults.Errors
                                        select String.Format("Error on line {0}: {1}", error.Line, error.ErrorText)).ToList();

                throw new SharpSwordCoreException("编译源文件：{0} 错误；错误详情：\r\n{1}"
                    .With(sourceFilePath, string.Join("\r\n", errors.ToArray())));
            }

            //查找编译后的程序集，找到所有实现了IDynamicCompiledDependencyRegistar接口的类，然后批量注册
            compilerResults.CompiledAssembly.GetTypes()
                           .Where(type => type.IsPublic && !type.IsAbstract &&
                                          typeof(IDynamicCompiledDependencyRegistar).IsAssignableFrom(type))
                           .ToList().ForEach(type =>
                           {
                               //批量注册
                               ((IDynamicCompiledDependencyRegistar)Activator.CreateInstance(type)).Register(GlobalConfiguration.Instance);
                           });
        }
    }
}
