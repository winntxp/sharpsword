<!--
 *************************************************************
 * SharpSword zhangliang@sharpsword.com.cn <%:DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss.fff") %>
 * ***********************************************************
-->

<%@ Page Language="C#" %>

<%@ Assembly Name="SharpSword" %>
<%@ Assembly Name="Autofac" %>
<%@ Assembly Name="SharpSword.SdkBuilder.CSharp" %>
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
    var requestDto = this.RequestContext.RequestDto as BuildSdkAction.BuildSdkActionRequestDto;

    //输出的数据
    var responseDto = this.ActionResult.Data as BuildSdkAction.BuildSdkActionResponseDto;

    //命名空间，需要输出不同的命名空间，请修改下面命名空间变量
    var @namespace = responseDto.SdkNamespace;

    //全部接口
    var actions = responseDto.ActionSelector.GetActionDescriptors().OrderBy(o => o.ActionName);
%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>API接口文档生成器-基于框架SharpSword：<%:ApiVersion.Version %></title>
    <style type="text/css">
        html {
            height: 100%;
            margin: 0px;
        }

        body {
            height: 100%;
            width: 100%;
            background: #ffffff;
            color: #333;
            font: normal 12px/1.5 'Arial', 'SimSun', 'Tahoma', 'Helvetica', 'sans-serif','Consolas';
            text-align: left;
            overflow-x: hidden;
        }

        .header {
            padding: 0px;
            background: #C00;
            width: 100%;
            margin: 0px;
            border-bottom: 0px solid #E3E6EB;
            height: 40px;
            font-size: 18px;
            font-weight: bold;
            border-bottom: 1px solid #ccc;
            line-height: 40px;
            color: #ffffff;
            position: fixed;
            left: 0px;
            top: 0px;
        }

            .header .lft {
                margin-left: 470px;
            }

            .header .rgt {
                position: fixed;
                right: 0px;
                top: 0px;
                padding-right: 10px;
                font-size: 12px;
            }

        .left {
            background: #305c9a;
            position: absolute;
            left: 0px;
            top: 0px;
            z-index: 2;
            width: 450px;
            height: auto;
        }

            .left .top {
                height: 40px;
                line-height: 40px;
                background: #205081;
                position: fixed;
                left: 0;
                top: 0px;
                z-index: 3;
                width: 450px;
                text-align: left;
                font-weight: bold;
                color: #ffffff;
            }

            .left ul {
                margin-top: 50px;
            }

                .left ul li span {
                    font: normal 9px/1.0 'Consolas', 'Arial', 'SimSun', 'Tahoma', 'Helvetica', 'sans-serif';
                    color: red;
                }

        .right {
            width: 99%;
            overflow: hidden;
            padding-top: 40px;
        }

        .right-content {
            margin-left: 460px;
        }

            .right-content b {
                font-size: 18px;
            }

        a {
            text-decoration: none;
            color: #ffffff;
            outline: 0;
            cursor: pointer;
        }

            a:hover {
                color: #C00;
                text-decoration: underline;
            }

        .header .rgt a:hover {
            color: #305c9a;
            text-decoration: underline;
        }

        .table {
            border-collapse: collapse;
        }

            .table td, th {
                padding-left: 10px;
            }

            .table td {
                border-bottom: 1px solid #ccc;
            }
    </style>
    <script type="text/javascript" src="/GetResource?resourceName=jquery-1.9.1.min.js,jquery.poshytip.js"></script>
    <link rel="stylesheet" href="/GetResource?resourceName=tip-yellow.css" type="text/css" />
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
<body style="font-family: Consolas;">
    <div class="header">
        <div class="lft">API接口文档/SDK代码&nbsp;&nbsp;<span style="font-size: 9px;">Powered by：V<%:ApiVersion.Version %></span> </div>
        <div class="rgt">
            <a target="_blank" href="/CSharpDownSdk">SDK开发包(.NET)</a>&nbsp;|&nbsp;
            <a target="_blank" href="/CSharpDownSource">SDK源码(.NET)</a>&nbsp;|&nbsp;
            <a target="_blank">SDK开发包(JAVA，IOS，PHP)</a>&nbsp;|&nbsp
            <a target="_blank" href="/DocBuilder">下载API接口文档</a>&nbsp;|&nbsp;
        </div>
    </div>
    <div class="left">
        <div class="top"><span style="margin-left: 28px; font-size: 13px;">HOST:<%:this.RequestContext.HttpContext.Request.LocalAddr() %> API Numbers：<%:actions.Count(o => o.CanPackageToSdk) %></span></div>
        <ul>
            <%foreach (var item in actions)
                {
                    if (!item.CanPackageToSdk)
                    {
                        continue;
                    }
            %>
            <li style="color: #ffffff;">
                <a class="tooltip" data-actionname="<%:item.ActionName %>" data-version="<%:item.Version %>" href="<%:"/Api?ActionName=API.BuildSDK&Format=view&data=" + this.RequestContext.HttpContext.Server.UrlEncode("{actionname:\"" + item.ActionName + "\",Version:\""+ item.Version +"\"}") %>" title="程序集：<%:item.ActionType.Assembly.GetName().Name %>，处理类：<%:item.ActionType.FullName %>"><%:item.ActionName %>&nbsp;(v<%:item.Version %>)<span style="color: red;"><%if (item.RequiredUserIdAndUserName)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    { %>(U)<%} %></span><%if (!item.CanPackageToSdk)
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            { %><span style="color: red" title="不打包到SDK库">⊙</span><%} %></a>
            </li>
            <%} %>
        </ul>
    </div>
    <div class="right">
        <div class="right-content">
            <%if (this.ActionResult.Info == "OK")
                { %>
            <div style="width: 100%">
                <b>调用地址：</b>
                <table style="width: 100%; background: #f4f4f4; line-height: 30px;" class="table">
                    <tr style="background: #808080; color: #ffffff;">
                        <th style="width: 200px;">环境</th>
                        <th>HTTP请求地址</th>
                        <th>请求方式</th>
                        <td style="text-align:center">测试</td>
                    </tr>
                    <tr>
                        <td>开发环境</td>
                        <td>http://<%:this.RequestContext.HttpContext.Request.HttpHost() %>/api</td>
                        <td><%:responseDto.ActionDescriptor.HttpMethod %></td>
                        <td style="text-align:center">
                             <b style="font-size: 14px;">
                                 <a style="color: #000; font-size: 12px;" target="_blank" href="/Api?ActionName=<%:requestDto.ActionName %>&format=View&data={version:<%:requestDto.Version %>}">text/html</a>
                                 &nbsp;|&nbsp;<a style="color: #000; font-size: 12px;" target="_blank" href="/Api?ActionName=<%:requestDto.ActionName %>&format=JSON&data={}">application/json</a>
                                 &nbsp;|&nbsp;<a style="color: #000; font-size: 12px;" target="_blank" href="/Api?ActionName=<%:requestDto.ActionName %>&format=XML&data={}">application/xml</a></b>
                        </td>
                    </tr>
                </table>
                <br />
                <b>接口说明</b>(<b style="font-size: 14px;">
                    <a style="color: #000; font-size: 12px;" target="_blank" href="/Api?ActionName=Api.Doc&format=View&data=<%:RequestContext.HttpContext.Server.UrlEncode("{"+"version:\"{0}\",actionname:\"{1}\"".With(requestDto.Version,requestDto.ActionName)+"}") %>">接口传输文档
                    </a></b>)
                <table>
                    <tr>
                        <td><%:responseDto.ActionDescriptor.Description %></td>
                    </tr>
                </table>
                <br />
                <b>请求参数：</b>
                <table style="width: 100%; background: #f4f4f4; line-height: 30px;" class="table">
                    <tr style="background: #808080; color: #ffffff;">
                        <th style="width: 100px;">名称</th>
                        <th style="width: 100px;">类型</th>
                        <th style="width: 80px;">是否必须</th>
                        <th>描述</th>
                        <th>示例</th>
                    </tr>
                    <tr>
                        <td>ActionName</td>
                        <td>string</td>
                        <td>是</td>
                        <td>接口名称</td>
                        <td><%:requestDto.ActionName %> </td>
                    </tr>
                    <tr>
                        <td>Format</td>
                        <td>string</td>
                        <td>否</td>
                        <td>返回的数据格式:JSON/XML/VIEW</td>
                        <td>JSON</td>
                    </tr>
                    <tr>
                        <td>AppKey</td>
                        <td>string</td>
                        <td>否</td>
                        <td>用于认证调用方身份信息</td>
                        <td>980203</td>
                    </tr>
                    <tr>
                        <td>Version</td>
                        <td>string</td>
                        <td>否</td>
                        <td>接口版本</td>
                        <td><%:responseDto.ActionDescriptor.Version %></td>

                    </tr>
                    <tr>
                        <td>TimeStamp</td>
                        <td>string</td>
                        <td>否</td>
                        <td>时间戳，客户端请求时间</td>
                        <td><%:DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") %></td>
                    </tr>
                    <tr>
                        <td>RequestId</td>
                        <td>string</td>
                        <td>否</td>
                        <td>客户端请求ID，建议每次请求使用不同值，可以使用GUID，便于接口调用链跟踪</td>
                        <td><%:System.Guid.NewGuid().ToString("N").ToUpper() %></td>
                    </tr>
                    <tr>
                        <td>Data</td>
                        <td>string</td>
                        <td>是</td>
                        <td>上送数据对象JSON数据</td>
                        <td>
                            <pre style="width: 100%; font-family: Consolas; color: red; line-height: 20px;" id="requestDtoJson"><%:responseDto.RequestJson %></pre>

                        </td>
                    </tr>
                    <tr>
                        <td>Sign</td>
                        <td>string</td>
                        <td>否</td>
                        <td>上送数据签名</td>
                        <td>根据约定进行数据签名</td>
                    </tr>
                </table>
                <br />
                <b>输出格式：</b>
                <table style="width: 100%; background: #f4f4f4; line-height: 30px;" class="table">
                    <tr style="background: #808080; color: #ffffff;">
                        <th>JSON</th>
                        <th>XML</th>
                    </tr>
                    <tr>
                        <td>
                            <pre style="width: 100%; font-family: Consolas; line-height: 20px;" id="responseJosn"><%:responseDto.ResponseJson.FormatJsonString() %></pre>
                        </td>
                        <td>
                            <pre style="width: 100%; font-family: Consolas; margin-left: 10px; line-height: 20px;" id="responseDtoXml"><%: RequestContext.HttpContext.Server.HtmlEncode(responseDto.ResponseXml) %></pre>
                        </td>
                    </tr>
                </table>
            </div>
            <div style="width: 100%; margin-top: 20px;">
                <table style="width: 100%; background: #f4f4f4; line-height: 30px;" class="table">
                    <tr style="background: #808080; color: #ffffff;">
                        <th>C#客户端调用代码</th>
                    </tr>
                    <tr>
                        <td>
                            <b style="color: rgb(146, 39, 143);">Request: </b>
                            <hr />
                            <pre style="font-family: Consolas; line-height: 18px; font-size: 11px;"><%:RequestContext.HttpContext.Server.HtmlEncode(responseDto.RequestSource)%> </pre>
                            <hr />
                            <div style="width: 100%">
                                <b style="color: rgb(146, 39, 143);">Response:</b>
                                <hr />
                                <pre style="font-family: Consolas; line-height: 18px; font-size: 11px;"><%:RequestContext.HttpContext.Server.HtmlEncode(responseDto.ResponseSource)%> </pre>
                            </div>
                        </td>
                    </tr>
                    </table>
                    <%}
                        else
                        { %>
                    <%:this.ActionResult.Info %>
                    <%} %>
            </div>
        </div>
    </div>

</body>
</html>



<script type="text/javascript">
    $("a.tooltip").poshytip({
        className: 'tip-yellow',
        bgImageFrameSize: 11,
        content: function (updateCallback) {
            $.ajax({
                type: "post",
                url: "/Api",
                data: "ActionName=API.Descriptor&Format=View&Data={ActionName:\"" + $(this).attr("data-ActionName") + "\",Version:\"" + $(this).attr("data-Version") + "\"}",
                dataType: "html",
                cache: false,
                success: function (data) {
                    updateCallback(data);
                },
                error: function (e, textStatus, errorThrown) {
                    console.log(textStatus + ":" + errorThrown);
                }
            });

            return 'Loading...';
        }
    });
</script>
