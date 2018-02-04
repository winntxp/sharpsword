/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/12 10:33:05
 * ****************************************************************/
using SharpSword.Timing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SharpSword.WebApi
{
    /// <summary>
    /// C#客户端SDK生成器
    /// </summary>
    public class CSharpSdkCodeGenerator : SdkCodeGeneratorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDocResourceManager"></param>
        protected CSharpSdkCodeGenerator(ActionDocResourceManager actionDocResourceManager) : base(actionDocResourceManager) { }

        /// <summary>
        /// 转换属性输出类型
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, string> CreateTypeMapping()
        {
            Dictionary<string, string> typeMap = new Dictionary<string, string>
            {
                {"Byte", "byte"},
                {"Int16", "short"},
                {"Int32", "int"},
                {"Int32[]", "int[]"},
                {"Int64", "long"},
                {"Int64[]", "long[]"},
                {"Char", "char"},
                {"Single", "float"},
                {"Double", "double"},
                {"Double[]", "double[]"},
                {"Decimal", "decimal"},
                {"Decimal[]", "decimal[]"},
                {"Object", "object"},
                {"Object[]", "object[]"},
                {"String", "string"},
                {"String[]", "string[]"},
                {"Boolean", "bool"},
                {"Boolean[]", "bool[]"},
                {"Int32?", "int?"},
                {"Int64?", "long?"},
                {"Double?", "double?"},
                {"Single?", "float?"},
                {"Decimal?", "decimal?"},
                {"Boolean?", "bool?"},
                {"Icollection<String>", "ICollection<string>"},
                {"IList<String>", "IList<string>"},
                {"List<String>", "List<string>"},
                {"IList<Int32>", "IList<int>"},
                {"List<Int32>", "List<int>"},
                {"IList<Int64>", "IList<long>"},
                {"List<Int64>", "List<long>"},
                {"IEnumerable<String>", "IEnumerable<string>"}
            };
            return typeMap;
        }

        /// <summary>
        /// 创建using代码块
        /// </summary>
        /// <param name="actionDescriptor">接口描述对象</param>
        /// <param name="codeBlockStringBuilder"></param>
        /// <returns></returns>
        private void CreateUsingBlock(IActionDescriptor actionDescriptor, StringBuilder codeBlockStringBuilder)
        {
            codeBlockStringBuilder.Append("/******************************************************************");
            codeBlockStringBuilder.Append(Environment.NewLine);
            codeBlockStringBuilder.Append("* SharpSword System Auto-Generation At {0}".With(Clock.Now.ToString("yyyy-MM-dd HH:mm:ss")));
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
        }

        /// <summary>
        /// 接口名称
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        private string GetActionName(IActionDescriptor actionDescriptor)
        {
            return actionDescriptor.ActionName.Replace(".", "");
        }

        /// <summary>
        /// 生成C#请求对象类
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public override KeyValuePair<string, string> GeneratorRequest(IActionDescriptor actionDescriptor)
        {
            //请求类名称
            var requestFileName = "{0}Request".With(this.GetActionName(actionDescriptor));

            //上送参数对象
            Type requestDtoType = actionDescriptor.RequestDtoType;

            //保存生成类源文件
            StringBuilder sourceStringBuilder = new StringBuilder();

            //using
            this.CreateUsingBlock(actionDescriptor, sourceStringBuilder);

            //命名空间
            sourceStringBuilder.AppendFormat("namespace {0}.Request ", "@namespace");
            sourceStringBuilder.Append(Environment.NewLine);
            sourceStringBuilder.Append("{");
            sourceStringBuilder.Append(Environment.NewLine);

            //根节点注释文档
            sourceStringBuilder.Append("\t/// <summary>");
            sourceStringBuilder.Append(Environment.NewLine);
            foreach (string item in this.ActionDocResourceManager.GetDescriptionLines(actionDescriptor.ActionType.FullName))
            {
                sourceStringBuilder.Append("\t/// " + item.Trim());
                sourceStringBuilder.Append(Environment.NewLine);
            }
            sourceStringBuilder.Append("\t/// </summary>");
            sourceStringBuilder.Append(Environment.NewLine);

            //根节点类
            sourceStringBuilder.Append("\tpublic class {0}".With(requestFileName));
            sourceStringBuilder.Append(Environment.NewLine);
            sourceStringBuilder.Append("\t{");
            sourceStringBuilder.Append(Environment.NewLine);

            //根节点属性
            var properties = requestDtoType.GetPropertiesInfo();
            foreach (var propertie in properties)
            {
                sourceStringBuilder.Append("\t\t/// <summary>");
                sourceStringBuilder.Append(Environment.NewLine);
                foreach (string item in this.ActionDocResourceManager.GetDescriptionLines("{0}.{1}".With(requestDtoType.FullName, propertie.Name)))
                {
                    sourceStringBuilder.Append("\t\t/// " + item.Trim());
                    sourceStringBuilder.Append(Environment.NewLine);
                    sourceStringBuilder.Append("\t\t/// " + this.IsRequired(propertie));
                    sourceStringBuilder.Append(Environment.NewLine);
                }
                sourceStringBuilder.Append("\t\t/// </summary>");
                sourceStringBuilder.Append(Environment.NewLine);
                sourceStringBuilder.Append("\t\tpublic {0} {1} ".With(this.ConvertPropertyType(propertie.PropertyType), propertie.Name)).Append("{ get; set; }");
                sourceStringBuilder.Append(Environment.NewLine);
                sourceStringBuilder.Append(Environment.NewLine);
            }

            //重写接口获取方法
            sourceStringBuilder.Append("\t\t/// <summary>");
            sourceStringBuilder.Append(Environment.NewLine);
            sourceStringBuilder.Append("\t\t/// 调用接口名称，{0}".With(actionDescriptor.ActionName));
            sourceStringBuilder.Append(Environment.NewLine);
            sourceStringBuilder.Append("\t\t/// </summary>");
            sourceStringBuilder.Append(Environment.NewLine);
            sourceStringBuilder.Append("\t\t/// <returns></returns>");
            sourceStringBuilder.Append(Environment.NewLine);
            sourceStringBuilder.Append("\t\tpublic override string GetApiName()");
            sourceStringBuilder.Append(Environment.NewLine);
            sourceStringBuilder.Append("\t\t{");
            sourceStringBuilder.Append(Environment.NewLine);
            //输出GetApiName返回ActionName
            sourceStringBuilder.Append("\t\t\treturn \"{0}\";".With(actionDescriptor.ActionName));
            sourceStringBuilder.Append(Environment.NewLine);
            sourceStringBuilder.Append("\t\t}");
            sourceStringBuilder.Append(Environment.NewLine);

            //重写参数生成JSON方法
            sourceStringBuilder.Append(Environment.NewLine);
            sourceStringBuilder.Append("\t\t/// <summary>");
            sourceStringBuilder.Append(Environment.NewLine);
            sourceStringBuilder.Append("\t\t/// 请求参数json化");
            sourceStringBuilder.Append(Environment.NewLine);
            sourceStringBuilder.Append("\t\t/// <returns>Request Json string</returns>");
            sourceStringBuilder.Append(Environment.NewLine);
            sourceStringBuilder.Append("\t\tpublic override string GetRequestJsonData()");
            sourceStringBuilder.Append(Environment.NewLine);
            sourceStringBuilder.Append("\t\t{");
            sourceStringBuilder.Append(Environment.NewLine);

            //输出GetRequestJsonData返回参数
            IList<string> propertiesx = new List<string>();
            requestDtoType.GetProperties().ToList().ForEach(p =>
            {
                propertiesx.Add("this.{0}".With(p.Name));
            });
            sourceStringBuilder.Append("\t\t\treturn new { ");
            //参数有太多就换行，方便代码查看
            if (propertiesx.Count <= 5)
            {
                sourceStringBuilder.Append(string.Join(",", propertiesx.ToArray()));
            }
            else
            {
                sourceStringBuilder.Append(string.Join(",{0}\t\t\t\t".With(Environment.NewLine), propertiesx.ToArray()));
            }
            sourceStringBuilder.Append(" }.ToJson();");
            sourceStringBuilder.Append(Environment.NewLine);
            sourceStringBuilder.Append("\t\t}");
            sourceStringBuilder.Append(Environment.NewLine);

            //获取类所有的复杂属性对象类
            List<Type> complexTypes = new List<Type>();
            this.GetComplexObjTypes(requestDtoType, complexTypes);

            //生成所有属性复杂类（内部类）
            foreach (var complexType in complexTypes)
            {
                //是枚举类型
                if (complexType.BaseType == typeof(Enum))
                {
                    sourceStringBuilder.Append(Environment.NewLine);
                    sourceStringBuilder.Append("\t\tpublic enum {0}".With(complexType.Name));
                    sourceStringBuilder.Append(Environment.NewLine);
                    sourceStringBuilder.Append("\t\t{");
                    sourceStringBuilder.Append(Environment.NewLine);
                    IList<string> enumValues = new List<string>();
                    foreach (var item in Enum.GetValues(complexType))
                    {
                        enumValues.Add("\t\t\t{0}={1}".With(item.ToString(), (int)item));
                    }
                    sourceStringBuilder.Append(string.Join("," + Environment.NewLine, enumValues.ToArray()));
                    sourceStringBuilder.Append(Environment.NewLine);
                    sourceStringBuilder.Append("\t\t}");
                    sourceStringBuilder.Append(Environment.NewLine);
                }
                else //省略掉了结构体
                {
                    sourceStringBuilder.Append(Environment.NewLine);
                    sourceStringBuilder.Append("\t\tpubclic {0}".With(complexType.Name));
                    sourceStringBuilder.Append(Environment.NewLine);
                    sourceStringBuilder.Append("\t\t{");
                    sourceStringBuilder.Append(Environment.NewLine);
                    properties = complexType.GetPropertiesInfo();

                    //复杂类属性
                    foreach (var propertie in properties)
                    {
                        sourceStringBuilder.Append("\t\t\t/// <summary>");
                        sourceStringBuilder.Append(Environment.NewLine);
                        foreach (string item in this.ActionDocResourceManager.GetDescriptionLines("{0}.{1}".With(complexType.FullName, propertie.Name)))
                        {
                            sourceStringBuilder.Append("\t\t\t/// " + item.Trim());
                            sourceStringBuilder.Append(Environment.NewLine);
                            sourceStringBuilder.Append("\t\t/// " + this.IsRequired(propertie));
                            sourceStringBuilder.Append(Environment.NewLine);
                        }
                        sourceStringBuilder.Append("\t\t\t/// </summary>");
                        sourceStringBuilder.Append(Environment.NewLine);
                        sourceStringBuilder.Append(Environment.NewLine);
                        sourceStringBuilder.Append("\t\t\tpublic {0} {1} ".With(this.ConvertPropertyType(propertie.PropertyType), propertie.Name)).Append("{ get; set; }");
                        sourceStringBuilder.Append(Environment.NewLine);
                    }

                    sourceStringBuilder.Append("\t\t}");
                    sourceStringBuilder.Append(Environment.NewLine);
                }
            }

            //包含下上面的复杂属性内部类
            sourceStringBuilder.Append("\t}");
            sourceStringBuilder.Append(Environment.NewLine);
            sourceStringBuilder.Append("}");

            //返回上送的请求对象类
            return new KeyValuePair<string, string>(requestFileName, sourceStringBuilder.ToString());
        }

        /// <summary>
        /// 生成C#返回对象类
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public override KeyValuePair<string, string> GeneratorResponse(IActionDescriptor actionDescriptor)
        {
            //下送数据类名称
            var responseFileName = "{0}Resp".With(this.GetActionName(actionDescriptor));

            //下送数据对象
            Type responseDtoType = actionDescriptor.ResponseDtoType;

            //直接返回空源文件(空的源文件不会生成实际物理文件)
            if (actionDescriptor.ResponseDtoType == typeof(NullResponseDto))
            {
                return new KeyValuePair<string, string>(responseFileName, "");
            }

            //返回数据
            return new KeyValuePair<string, string>(responseFileName, "");
        }

        /// <summary>
        /// 语言
        /// </summary>
        public override string Language
        {
            get { return "CSharp"; }
        }
    }
}

