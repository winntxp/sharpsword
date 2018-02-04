/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/12 9:44:30
 * ****************************************************************/
using System.Text;
using System.Text.RegularExpressions;

namespace SharpSword.ViewEngine.Impl
{
    #region defines the aspx template parser

    /// <summary>
    /// the Parser class parses the aspx page and converts it into compilable code
    /// currently the parser supports the following Page Directives:
    /// @Page - defines the type of page (cs or vb code)
    /// @Assembly - used to include (external) assemblies
    /// @Import - used to include namespaces
    /// note that currently added script declarations must be written in the 
    /// same language as the page (no mixed vb and cs allowed)
    /// </summary>
    internal class ViewParser
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="viewPath"></param>
        /// <returns></returns>
        public static SectionCollection ParsePage(string page, string viewPath)
        {
            //maches <script> sections, returns result in 'code' variable 
            //<script[^>]*runat[\s]*=[\s]*"?Server"?[^>]*>(?<code>[^<]*)</script>
            //matches code blocks <% %>
            //<%{1}([^%])*%>

            //先处理包含文件
            page = new IncludeParser().Parse(page, viewPath);
            SectionCollection list = new SectionCollection();

            //处理<%=x%>这样的属性输出(直接进行替换操作，替换成:Response.Write方法)
            page = ParseText0(page);

            //处理引用dll
            page = ParseDirectives(list, page);

            //处理属性，方法，代码块
            page = ParseDeclarations(list, page);

            //处理<%%>代码块
            ParseText(list, page.Trim());
            //返回转化后的结果
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string StripQuotes(string text)
        {
            return text.Trim("\" ".ToCharArray());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="list"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private static string ParseDirectives(SectionCollection list, string page)
        {
            // finds directives
            //<%\s*@\s*(?<directive>[\w]*)[^%]*%>
            //<%\s*@\s*(?<directive>[\w]*)\s*(?<p1>[\w]*)\s*=(?<v1>[^\s%]*)[^%]*%>

            // parses the directive and up to 4 name/value pairs
            //<%\s*@\s*(?<directive>[\w]*)\s*((?<p1>[\w]*)\s*=(?<v1>[^\s%]*))?\s*((?<p2>[\w]*)\s*=(?<v2>[^\s%]*))?\s*((?<p3>[\w]*)\s*=(?<v3>[^\s%]*))?\s*((?<p4>[\w]*)\s*=(?<v4>[^\s%]*))?[^%]*%>

            StringBuilder result = new StringBuilder();
            //Regex reg =new Regex(@"<%\s*@\s*(?<directive>[\w]*)[^%]*%>", RegexOptions.IgnoreCase);
            Regex reg = new Regex(@"<%\s*@\s*(?<directive>[\w]*)\s*((?<p1>[\w]*)\s*=(?<v1>[^\s%]*))?\s*((?<p2>[\w]*)\s*=(?<v2>[^\s%]*))?\s*((?<p3>[\w]*)\s*=(?<v3>[^\s%]*))?\s*((?<p4>[\w]*)\s*=(?<v4>[^\s%]*))?[^%]*%>", RegexOptions.IgnoreCase);
            MatchCollection mc = reg.Matches(page);

            int index = 0;
            foreach (Match m in mc)
            {
                result.Append(page.Substring(index, m.Index - index));
                DirectiveValues dvs = new DirectiveValues(m.Groups["directive"].Value.ToLower());
                if (m.Groups["p1"].Value != string.Empty)
                    dvs.Add(m.Groups["p1"].Value.ToLower(), StripQuotes(m.Groups["v1"].Value));
                if (m.Groups["p2"].Value != string.Empty)
                    dvs.Add(m.Groups["p2"].Value.ToLower(), StripQuotes(m.Groups["v2"].Value));
                if (m.Groups["p3"].Value != string.Empty)
                    dvs.Add(m.Groups["p3"].Value.ToLower(), StripQuotes(m.Groups["v3"].Value));
                if (m.Groups["p4"].Value != string.Empty)
                    dvs.Add(m.Groups["p4"].Value.ToLower(), StripQuotes(m.Groups["v4"].Value));

                Section s = new Section(list.Count, page.Substring(m.Index, m.Length), SectionType.Directive, dvs);
                list.Add(s);
                index = m.Index + m.Length;
            }
            result.Append(page.Substring(index));

            return result.ToString();
        }

        /// <summary>
        /// <![CDATA[
        /// <script charset="UTF-8" runat="server">*</script>
        /// ]]>
        /// </summary>
        /// <param name="list"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private static string ParseDeclarations(SectionCollection list, string page)
        {

            StringBuilder result = new StringBuilder();
            Regex reg1 = new Regex("<script[^>]*runat[\\s]*=[\\s]*\"?server\"?[^>]*>", RegexOptions.IgnoreCase);
            Regex reg2 = new Regex("</\\s*script\\s*>", RegexOptions.IgnoreCase);

            int index = 0;
            while (index < page.Length)
            {

                // try find start of block
                Match m1 = reg1.Match(page, index);
                if (!m1.Success) break; // exit loop here

                //start <script> found, now find end.
                Match m2 = reg2.Match(page, m1.Index + m1.Length);
                if (!m2.Success) break; // exit loop here

                result.Append(page.Substring(index, m1.Index - index));

                string block = page.Substring(m1.Index + m1.Length, m2.Index - (m1.Index + m1.Length));
                Section s = new Section(list.Count, block, SectionType.Declaration);
                list.Add(s);

                index = m2.Index + m2.Length;
            }

            result.Append(page.Substring(index));

            return result.ToString();
        }

        /// <summary>
        /// 处理属性，方法，代码块
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        private static string ParseText0(string page)
        {
            page = page.Trim();
            var matches = Regex.Matches(page, @"<%\s*=\s*(.*?)%>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var matches1 = Regex.Matches(page, @"<%\s*:\s*(.*?)%>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            foreach (Match match in matches)
            {
                page = page.Replace(match.Groups[0].Value, "<%Response.Write(" + match.Groups[1].Value.Trim() + ");%>");
            }
            foreach (Match match in matches1)
            {
                page = page.Replace(match.Groups[0].Value, "<%Response.Write(" + match.Groups[1].Value.Trim() + ");%>");
            }
            return page;
        }

        /// <summary>
        /// <![CDATA[
        /// 处理<%%>代码块
        /// ]]>
        /// </summary>
        /// <param name="list"></param>
        /// <param name="page"></param>
        private static void ParseText(SectionCollection list, string page)
        {
            //文档为空
            if (page.Length <= 0)
            {
                return;
            }

            int index = 0;
            while (index < page.Length)
            {

                int k = page.IndexOf("<%", index);

                if (k < 0) break;

                if (k > 0)
                    list.Add(new Section(list.Count, page.Substring(index, k - index), SectionType.Text));

                index = k;

                int j = page.IndexOf("%>", index);

                if (j <= index) break;

                list.Add(new Section(list.Count, page.Substring(index + 2, j - (index + 2)), SectionType.Code));

                index = j + 2;
            }

            list.Add(new Section(list.Count, page.Substring(index), SectionType.Text));
        }

    }

    #endregion defines the aspx template parser
}

