<!--
 *************************************************************
 * SharpSword zhangliang@sharpsword.com.cn <%:DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss.fff") %>
 * ***********************************************************
-->

<%@ Page Language="C#" %>

<%@ Assembly Name="SharpSword" %>
<%@ Assembly Name="SharpSword.DtoGenerator" %>
<%@ Assembly Name="Autofac" %>
<%@ Import Namespace="SharpSword" %>
<%@ Import Namespace="SharpSword.DtoGenerator" %>
<%@ Import Namespace="SharpSword.DtoGenerator.Actions" %>
<%@ Import Namespace="System.Collections.Specialized" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="SharpSword.WebApi" %>

<script runat="server">
    public RequestContext RequestContext;
    public ActionResult ActionResult;
</script>

<%
    var responseDto = this.ActionResult.Data as DtoGeneratorAction.DtoGeneratorActionResponseDto;
%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>DTO生成器插件，基于接口框架：<%:ApiVersion.Version %></title>
    <script type="text/javascript" src="/GetResource?resourceName=jquery-1.9.1.min.js"></script>
    <style type="text/css">
        html { height: 100%; margin: 0px; }
        body { height: 100%; width: 100%; background: #f4f4f4; color: #333; font: normal 12px/1.5 'Arial', 'SimSun', 'Tahoma', 'Helvetica', 'sans-serif','Consolas'; text-align: left; overflow-x: hidden; }
        b { font-size: 12px; }
        .l { text-align: right; width: 100px; padding: 5px 10px 5px 0px; font-weight: bold; font-size: 14px; }
        input, select, textarea { box-sizing: border-box; width: 99%; }
        input, select, textarea, fieldset { border-collapse: separate; -webkit-border-radius: 5px; -moz-border-radius: 5px; border-radius: 5px; /* W3C syntax */ }
        input, select, textarea { padding: 0.5em 0.5em; border: 1px solid #d4d4d4; margin: 0; text-decoration: none; text-shadow: 1px 1px 0 #fff; font: 12px/normal '宋体'; color: #333; white-space: pre-wrap; cursor: pointer; outline: none; background-color: #FFFFFF; background-image: -webkit-gradient(linear, 0 0, 0 100%, from(#f4f4f4), to(#FFFFFF)); background-image: -moz-linear-gradient(#f4f4f4, #FFFFFF); background-image: -o-linear-gradient(#f4f4f4, #ececec); background-image: linear-gradient(#f4f4f4, #FFFFFF); -webkit-background-clip: padding; -moz-background-clip: padding; -o-background-clip: padding-box; /*background-clip: padding-box;*/ /* commented out due to Opera 11.10 bug */ -webkit-border-radius: 0.2em; -moz-border-radius: 0.2em; border-radius: 0.2em; /* IE hacks */ zoom: 0; *display: inline; }
        input:focus, textarea:focus { border-color: rgba(82, 168, 236, 0.8); outline: 0; outline: thin dotted; -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(82,168,236,.6); -moz-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(82,168,236,.6); box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(82,168,236,.6); }
        input.yellow { background: url(); background-color: #FAFFBD; }
        select.yellow { background: url(); background-color: #FAFFBD; }
        textarea.yellow { background: url(); background-color: #FAFFBD; }
        input[type="checkbox"] { border: 0px; padding: 0px; }
        input.datetime { width: 75px; }
        select, textarea, input[type="text"] { font-family: 微软雅黑; font-size: 14px; }
        .rem { color: #666666; }
        .kwrd { color: blue; }
        .csharpcode { line-height: 18px; font-size: 12px; font-family: Consolas; }
    </style>
    <link rel="stylesheet" type="text/css" href="/GetResource?resourceName=buttons.css" />
</head>
<body style="font-family: Consolas; padding: 0px; margin: 0px">
    <table style="height: 30px; line-height: 30px; width: 100%; margin: 0px; background: #305c9a; color: #ffffff; font-size: 14px; font-family: 微软雅黑; padding: 0px; border-collapse: collapse;">
        <tr>
            <td style="padding: 0px; font-weight: bold; line-height: 30px;">DTO生成器插件</td>
            <td style="text-align:center; padding-right: 20px; width: 100px; background:#ff006e;">
                <a href="/DtoGenerator/TempleteDown" style="color: #ffffff; font-weight: bold;" title="下载模板后，将模板放置在站点~Views目录下即可，然后根据自己的需求修改模板。">下载模板</a></td>
            <td style="text-align: right; padding-right: 20px; width: 60px; background: #c00;">
                <a href="/" style="color: #ffffff; font-weight: bold;">返回</a>
            </td>
        </tr>
    </table>
    <form action="/DtoGenerator" id="DtoGenerator" method="post" target="_blank" style="margin: 0px; width: 100%;">
        <table style="height: 30px; line-height: 30px; width: 100%; margin: 10px auto 10px auto;">
            <tr>
                <td style="width: 160px;" class="l">Connection:</td>
                <td>ConnectionString:<b><%:responseDto.ConnectionString %></b>&nbsp;&nbsp;ProviderName:<b><%:responseDto.Connection %></b>
                </td>
            </tr>
            <tr>
                <td style="width: 100px;" class="l">Namespace:</td>
                <td>
                    <input name="Namespace" id="Namespace" type="text" placeholder="命名空间" />
                </td>
            </tr>
            <tr>
                <td class="l">ClassName:</td>
                <td>
                    <input name="ClassName" value="" type="text" placeholder="DTO类名" />
                </td>
            </tr>
            <tr>
                <td class="l">Inherit:</td>
                <td>
                    <input name="Inherit" value="" type="text" placeholder="继承类或者实现的接口" />
                </td>
            </tr>
            <tr>
                <td class="l">SQL/SP:</td>
                <td>
                    <textarea name="SQL" style="height: 100px" class="yellow"></textarea>
                    <div style="color: #666666">不同数据库只要查询语句是合法的就可以（比如：如果MSSql支持普通查询语句和存储过程）</div>
                </td>
            </tr>
            <tr>
                <td class="l"></td>
                <td style="padding: 10px 0px 10px 0px;">
                    <input type="submit" value="生成SQL查询列对应的DTO对象" class="button icon add" style="font-weight: bold;" />

                </td>
            </tr>
            <%if (!responseDto.Config.SourceSaveDirectory.IsNullOrEmpty())
                { %>
            <tr>
                <td style="width: 160px;" class="l">文件保存路径:</td>
                <td>
                    <%:this.RequestContext.HttpContext.Server.MapPath(responseDto.Config.SourceSaveDirectory) %>
                </td>
            </tr>
            <%} %>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr id="dtoCsharpCode">
                <td></td>
                <td id="content"></td>
            </tr>
        </table>
        <script type="text/javascript">

            $("#DtoGenerator").submit(function (e) {
                $("#content").html("生成中......")
                $("#dtoCsharpCode").hide();
                $.ajax({
                    cache: true,
                    type: "POST",
                    url: "/DtoGenerator",
                    data: $('#DtoGenerator').serialize(),
                    async: false,
                    error: function (request) {
                        alert("Connection error");
                    },
                    success: function (data) {
                        $("#content").html(data);
                        $("#dtoCsharpCode").show(1000)
                    }
                });
                return false;
            });
        </script>
    </form>
</body>
</html>
