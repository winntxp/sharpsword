/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/12 10:20:43
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace SharpSword.ViewEngine.Impl
{
    /// <summary>
    /// 根据视图文件分析器获取到所有的代码块，文本常量进行源代码生成
    /// </summary>
    internal class SectionCollection : List<Section>
    {
        /// <summary>
        /// 换行符
        /// </summary>
        private readonly string _newLine = System.Environment.NewLine;

        /// <summary>
        /// 生成源代码
        /// </summary>
        /// <param name="namespaces">需要用到的命名空间</param>
        /// <returns>返回视图文件格式化后的类源文件</returns>
        public string ExtractSource(string[] namespaces)
        {
            //保存所有源文件行
            StringBuilder lines = new StringBuilder();

            //默认先加入一些命名空间
            lines.AppendFormat("using System;{0}", this._newLine);
            lines.AppendFormat("using System.Text;{0}", this._newLine);
            lines.AppendFormat("using System.Collections.Generic;{0}", this._newLine);

            //生成源代码的时候另外增加的命名空间引用
            if (null != namespaces && namespaces.Length > 0)
            {
                foreach (string ns in namespaces)
                {
                    lines.AppendFormat("using {0};{1}", ns, this._newLine);
                }
            }

            //添加引用命名空间
            foreach (Section line in this)
            {
                if (line.Type != SectionType.Directive)
                {
                    continue;
                }
                if (line.Values.Directive.ToLower() == "import")
                {
                    string ns;
                    if (line.Values.TryGetValue("namespace", out ns))
                    {
                        lines.AppendFormat("using {0};{1}", ns, this._newLine);
                    }
                }
            }

            //定义源代码生成的类
            lines.Append("public class Page : " + typeof(ViewEnginePageBase).FullName + " {" + this._newLine);

            //定义视图执行后需要将结果输出到的数据流对象
            //lines.AppendFormat("  public System.IO.StreamWriter Response;{0}", this._newLine);

            //添加文本常量定义
            foreach (Section line in this)
            {
                if (line.Type == SectionType.Text)
                {
                    lines.AppendFormat("  public string SectionText{0};{1}", line.Index, this._newLine);
                }
            }

            //输出视图定义的接口模型对象参数
            foreach (Section line in this)
            {
                if (line.Type == SectionType.Declaration)
                {
                    lines.Append("  " + line.Text.Trim() + this._newLine);
                }
            }

            //添加构造函数
            lines.Append("  public Page() { }" + this._newLine);

            //添加方法执行入口
            lines.Append("  public void RenderPage() {" + this._newLine);

            //开始
            lines.Append("base.BeginRenderPage(); " + this._newLine);

            //视图具体执行的代码
            foreach (Section line in this)
            {
                //文本常量直接输出
                if (line.Type == SectionType.Text)
                {
                    lines.AppendFormat("     Response.Write(SectionText{0});{1}", line.Index, this._newLine);
                }
                //代码块就直接输出代码块
                else if (line.Type == SectionType.Code)
                {
                    if (line.Text.Trim().StartsWith("@"))
                    {
                        continue;
                    }
                    lines.Append("     " + line.Text.Trim() + this._newLine);
                }
            }

            //结束
            lines.Append("base.EndRenderPage(); " + this._newLine);

            lines.Append("   }" + this._newLine);
            lines.Append(" }" + this._newLine);

            //输出构造完成的源代码
            return lines.ToString();
        }

        /// <summary>
        /// 编译执行源代码
        /// </summary>
        /// <param name="assembly">编译完成的源代码所属的程序集</param>
        /// <param name="parameters">视图参数的输入(可以为null)</param>
        /// <param name="response">将视图执行结果写到流</param>
        public void Process(Assembly assembly, IViewParameterCollection parameters, StreamWriter response)
        {
            //程序集为null，直接返回
            if (assembly == null) return;

            try
            {
                //从程序集里创建预定义的视图类对象
                dynamic instance = assembly.CreateInstance("Page");

                //获取对象类型
                Type typepage = instance.GetType();

                //定义写入流对象
                typepage.InvokeMember("Response", BindingFlags.SetProperty, null, instance, new object[] { response });

                //对文本常量进行赋值
                foreach (Section section in this)
                {
                    if (section.Type == SectionType.Text)
                    {
                        typepage.InvokeMember("SectionText" + section.Index, BindingFlags.SetField, null, instance, new object[] { section.Text });//trim
                    }
                }

                //对视图公开参数进行赋值
                if (parameters != null)
                {
                    foreach (ViewParameter parameter in parameters)
                    {
                        try
                        {
                            typepage.InvokeMember(parameter.Name, BindingFlags.SetField, null, instance, new object[] { parameter.Value });
                        }
                        catch (MissingFieldException)
                        {
                            //variable was not declared in template but continue anyway
                        }
                    }
                }

                //直接动态调用
                instance.RenderPage();

                //执行预定义的RenderPage方法，开始执行视图引擎
                //typepage.InvokeMember("RenderPage", BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod, null, instance, null);
            }
            catch (Exception ex)
            {
                response.WriteLine();
                var inner = ex;
                while (inner != null)
                {
                    response.WriteLine(inner.Message);
                    inner = inner.InnerException;
                }
                response.WriteLine(ex.StackTrace);
            }
        }
    }
}
