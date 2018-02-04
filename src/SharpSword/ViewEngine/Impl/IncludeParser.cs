/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/12 9:44:21
 * ****************************************************************/
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace SharpSword.ViewEngine.Impl
{
    /// <summary>
    /// <![CDATA[
    /// 表示 #Include 命令解释器。
    /// 格式：<!--#include file="include/header_css_js.html"-->
    /// 支持： ../../x/x.html， /x/x.html ， x/x.html
    /// ]]>
    /// </summary>
    internal class IncludeParser
    {
        /// <summary>
        /// 
        /// </summary>
        private int _nestedCount;
        //最大包含嵌套
        private int _maxNestedCount = 50;
        private string _templatePath;
        private string _applicationStartPath;

        /// <summary>
        /// 处置包含文档。
        /// </summary>
        /// <param name="viewSourceString">包含模板代码的字符串。</param>
        /// <param name="viewPath">处置包含命令时要使用的基准路径（为空的话，将不会处理视图里的包含文件）</param>
        public string Parse(string viewSourceString, string viewPath)
        {
            //视图文件为空就直接返回空
            if (String.IsNullOrEmpty(viewSourceString))
            {
                return String.Empty;
            }
            //没有设置模板路径，直接返回
            if (string.IsNullOrWhiteSpace(viewPath))
            {
                return viewSourceString;
            }
            this._templatePath = viewPath;
            this._applicationStartPath = AppDomain.CurrentDomain.BaseDirectory;
            return _ProcessSSIElement(viewSourceString, viewPath);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="viewSourceString"></param>
        /// <param name="viewPath"></param>
        /// <returns></returns>
        private string _ProcessSSIElement(string viewSourceString, string viewPath)
        {

            //只支持10层嵌套
            if (_nestedCount > this._maxNestedCount)
            {
                return viewSourceString;
            }

            var matches = Regex.Matches(viewSourceString, @"<!--#include\s{1,}file=""([^""]+)""\s*-->", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (Match match in matches)
            {
                // C:\WEB\Root
                string templateFullDir = Path.GetDirectoryName(viewPath);
                //当前抽取出来的路径洗洗
                var filePath = match.Groups[1].Value;
                //正确的物理路径
                string tempaltePath;
                // ../../../xx.html 
                var m = Regex.Match(filePath, "^((\\.\\./){1,})(.*)$");
                if (m.Success)
                {
                    int i = m.Groups[1].Length / 3;
                    string templateDir = templateFullDir;
                    for (int n = 0; n < i; n++)
                    {
                        templateDir = templateDir.Substring(0, templateDir.LastIndexOf("\\"));
                    }
                    tempaltePath = Path.Combine(templateDir, m.Groups[3].Value);
                }
                else
                {
                    // /xx/xx.html
                    if (filePath.StartsWith("/"))
                    {
                        tempaltePath = Path.Combine(this._applicationStartPath, filePath.TrimStart(new char[] { '/' }));
                    }
                    // xx/xx.html
                    else
                    {
                        tempaltePath = Path.Combine(templateFullDir, filePath);
                    }
                }
                var file = new FileInfo(tempaltePath);
                if (file.Exists == false)
                {
                    continue;
                }
                var subTemplate = File.ReadAllText(file.FullName).Trim();
                subTemplate = _ProcessSSIElement(subTemplate, tempaltePath);
                viewSourceString = viewSourceString.Replace(match.Groups[0].Value, subTemplate);
            }

            _nestedCount++;

            return viewSourceString;
        }
    }
}

