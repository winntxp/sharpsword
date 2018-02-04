<!--
 *************************************************************
 * SharpSword zhangliang@sharpsword.com.cn <%:DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss.fff") %>
 * ***********************************************************
-->

<%@ Page Language="C#" %>

<%@ Assembly Name="SharpSword" %>
<%@ Assembly Name="SharpSword.SdkBuilder.CSharp" %>
<%@ Assembly Name="Autofac" %>
<%@ Import Namespace="SharpSword.WebApi" %>
<%@ Import Namespace="SharpSword.SdkBuilder.CSharp.Actions" %>
<%@ Import Namespace="SharpSword" %>
<%@ Import Namespace="System.Collections.Specialized" %>
<%@ Import Namespace="Autofac" %>
<%@ Import Namespace="Autofac.Core.Lifetime" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Linq" %>

<script runat="server">
    public RequestContext RequestContext;
    public ActionResult ActionResult;
</script>

<% 
    //上送参数
    var requestDto = this.RequestContext.RequestDto as ApiDocAction.ApiDocActionRequestDto;

    //输出的数据
    var responseDto = this.ActionResult.Data as ApiDocAction.ApiDocActionResponseDto;
%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>API接口文档接口描述-基于框架SharpSword：<%:ApiVersion.Version %></title>
    <style type="text/css">
        html { height: 100%; margin: 0px; }
        body { height: 100%; width: 100%; color: #333; font: normal 12px/1.5 'Arial', 'SimSun', 'Tahoma', 'Helvetica', 'sans-serif','Consolas'; text-align: left; overflow-x: hidden; }
        .left { text-align: right; padding-right: 10px; width: 100px; font-weight: bold; }
        .rgt { background: #f4f4f4; padding-left: 5px; }
        .title { padding-left: 5px; font-weight: bold; }
    </style>
    <script type="text/javascript" src="/GetResource?resourceName=jquery-1.9.1.min.js"></script>
    <script type="text/javascript">
        function format(txt, compress/*是否为压缩模式*/) {
            var indentChar = '    ';
            if (/^\s*$/.test(txt)) {
                alert('数据为空,无法格式化! ');
                return;
            }
            try { var data = eval('(' + txt + ')'); }
            catch (e) {
                alert('数据源语法错误,格式化失败! 错误信息: ' + e.description, 'err');
                return;
            };
            var draw = [], last = false, This = this, line = compress ? '' : '\n', nodeCount = 0, maxDepth = 0;

            var notify = function (name, value, isLast, indent/*缩进*/, formObj) {
                nodeCount++;/*节点计数*/
                for (var i = 0, tab = ''; i < indent; i++) tab += indentChar;/* 缩进HTML */
                tab = compress ? '' : tab;/*压缩模式忽略缩进*/
                maxDepth = ++indent;/*缩进递增并记录*/
                if (value && value.constructor == Array) {/*处理数组*/
                    draw.push(tab + (formObj ? ('"' + name + '":') : '') + '[' + line);/*缩进'[' 然后换行*/
                    for (var i = 0; i < value.length; i++)
                        notify(i, value[i], i == value.length - 1, indent, false);
                    draw.push(tab + ']' + (isLast ? line : (',' + line)));/*缩进']'换行,若非尾元素则添加逗号*/
                } else if (value && typeof value == 'object') {/*处理对象*/
                    draw.push(tab + (formObj ? ('"' + name + '":') : '') + '{' + line);/*缩进'{' 然后换行*/
                    var len = 0, i = 0;
                    for (var key in value) len++;
                    for (var key in value) notify(key, value[key], ++i == len, indent, true);
                    draw.push(tab + '}' + (isLast ? line : (',' + line)));/*缩进'}'换行,若非尾元素则添加逗号*/
                } else {
                    if (typeof value == 'string') value = '"' + value + '"';
                    draw.push(tab + (formObj ? ('"' + name + '":') : '') + value + (isLast ? '' : ',') + line);
                };
            };
            var isLast = true, indent = 0;
            notify('', data, isLast, indent, false);
            return draw.join('');
        }


    </script>
</head>
<body style="font-family: Consolas; padding: 0px; margin: 0px;">
    <table style="height: 30px; line-height: 30px; width: 100%; font-size: 12px; border-collapse: collapse; padding: 0px;">
        <tr>
            <td colspan="3" style="line-height: 30px; height: 30px; background: #C00; padding-left: 10px;"><b style="font-size: 14px; color: #ffffff;">ActionDescriptor</b></td>
        </tr>
        <tr>
            <td colspan="3" style="line-height: 18px; padding-left: 10px;">
                <table style="width: 100%; background: #ffffff; margin: 0px; width: 100%;">
                    <%foreach (var item in responseDto.ActionDescriptor.GetAttributes())
                      { %>
                    <tr>
                        <td class="left"><%:item.Key %>:</td>
                        <td class="rgt"><%:item.Value %></td>

                        <%} %>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 50%; padding: 10px;" valign="top">
                <table style="width: 100%; background: #f4f4f4; border-collapse: collapse;">
                    <tr>
                        <td style="background: #305c9a; font-weight: bold; font-size: 14px; color: #ffffff; padding: 5px;">Request</td>
                    </tr>
                    <tr>
                        <td style="line-height: 18px;" valign="top">
                            <table style="width: 100%; background: #ffffff; margin: 0px;">
                                <tr>
                                    <td class="left">ActionName:</td>
                                    <td class="rgt"><%:requestDto.ActionName %></td>
                                </tr>
                                <tr>
                                    <td class="left">Format:</td>
                                    <td class="rgt">XML/JSON/VIEW</td>
                                </tr>
                                <tr>
                                    <td class="left">AppKey:</td>
                                    <td class="rgt"></td>
                                </tr>
                                <tr>
                                    <td class="left">Version:</td>
                                    <td class="rgt"><%:requestDto.Version %></td>
                                </tr>
                                <tr>
                                    <td class="left">TimeStamp:</td>
                                    <td class="rgt"><%:DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                                </tr>
                                <tr>
                                    <td class="left">Sign:</td>
                                    <td class="rgt"><%:MD5.Encrypt(System.Guid.NewGuid().ToString()).ToUpper() %></td>
                                </tr>
                                <tr>
                                    <td class="left" valign="top">Data:</td>
                                    <td class="rgt">
                                        <pre style="width: 100%; font-family: Consolas" id="requestDtoJson"><%:responseDto.RequestDtoJson.FormatJsonString() %></pre>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <% foreach (var type in responseDto.RequestTypes)
                   { %>
                <table style="width: 100%; background: #ffffff; margin: 0px; width: 100%; border-collapse: collapse">
                    <tr>
                        <td colspan="4" style="background: #305c9a; font-weight: bold; font-size: 14px; color: #ffffff; padding: 5px;"><%:type.Key.Name %></td>
                    </tr>
                    <tr style="background: #f08080; font-weight: bold;">
                        <td style="width: 200px" class="title">MemberName</td>
                        <td style="width: 150px;" class="title">Type</td>
                        <td style="width: 100px;" class="title">Require</td>
                        <td class="title">Description</td>
                    </tr>
                    <% foreach (var v in type.Value)
                       { %>
                    <tr>
                        <td class="rgt"><%:v.MemberName %></td>
                        <td class="rgt"><%:v.DisplayType %></td>
                        <td class="rgt"><%:v.IsRequire %></td>
                        <td class="rgt"><%:v.Description %></td>
                    </tr>
                    <% } %>
                </table>
                <% } %>
            </td>
            <td style="width: 2px;">&nbsp;</td>
            <td style="width: 50%; padding: 10px;" valign="top">
                <table style="width: 100%; background: #f4f4f4; border-collapse: collapse;">
                    <tr>
                        <td style="background: #305c9a; font-weight: bold; font-size: 14px; color: #ffffff; padding: 5px;">Response
                            <div style="float: right; margin-right: 10px;"><a href="#JSON" style="color: #ffffff;">JSON</a>，<a href="#XML" style="color: #ffffff;">XML</a></div>
                        </td>
                    </tr>
                    <tr>
                        <td style="background: #f4f4f4; line-height: 18px;">
                            <table style="width: 100%; line-height: 18px; border-collapse: collapse">
                                <tr>
                                    <td style="background: #ff6a00; padding: 10px; font-size: 14px; font-weight: bold;"><a name="JSON">JSON</a></td>
                                </tr>
                                <tr>
                                    <td>
                                        <pre style="width: 100%; font-family: Consolas; margin-left: 10px;" id="responseDtoJson"><%:responseDto.ResponseDtoJson.FormatJsonString() %></pre>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width: 100%; line-height: 18px; border-collapse: collapse">
                                <tr>
                                    <td style="background: #ff6a00; padding: 10px; font-size: 14px; font-weight: bold;"><a name="XML">XML</a></td>
                                </tr>
                                <tr>
                                    <td>
                                        <pre style="width: 100%; font-family: Consolas; margin-left: 10px;" id="responseDtoXml"><%:responseDto.ResponseDtoXml.HtmlEncode() %></pre>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>

                <% foreach (var type in responseDto.ResponseTypes)
                   { %>
                <table style="width: 100%; background: #f4f4f4; border-collapse: collapse; padding: 5px;">
                    <tr>
                        <td colspan="4" style="background: #305c9a; font-weight: bold; font-size: 14px; color: #ffffff; padding: 5px;"><%:type.Key.Name %></td>
                    </tr>
                    <tr style="background: #f08080; font-weight: bold;">
                        <td style="width: 200px" class="title">MemberName</td>
                        <td style="width: 150px;" class="title">Type</td>
                        <td class="title">Description</td>
                    </tr>
                    <% foreach (var v in type.Value)
                       { %>
                    <tr>
                        <td class="rgt"><%:v.MemberName %></td>
                        <td class="rgt"><%:v.DisplayType %></td>
                        <td class="rgt"><%:v.Description %></td>
                    </tr>
                    <% } %>
                </table>
                <% } %>
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        //$("#requestDtoJson").html(format('<%:responseDto.RequestDtoJson %>').toString());
        //$("#responseDtoJson").html(format('<%:responseDto.ResponseDtoJson %>').toString());
    </script>
</body>
</html>
