<!--
 *************************************************************
 * SharpSword zhangliang@sharpsword.com.cn <%:DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss.fff") %>
 * ***********************************************************
-->

<%@ Page Language="C#" %>

<%@ Assembly Name="SharpSword" %>
<%@ Assembly Name="SharpSword.CommandExecutor" %>
<%@ Assembly Name="Autofac" %>
<%@ Import Namespace="SharpSword" %>
<%@ Import Namespace="SharpSword.CommandExecutor" %>
<%@ Import Namespace="SharpSword.CommandExecutor.Actions" %>
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

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Command命令行执行器，基于框架SharpSword：<%:ApiVersion.Version %></title>
    <script type="text/javascript" src="/GetResource?resourceName=jquery-1.9.1.min.js"></script>
    <style type="text/css">
        html { height: 100%; margin: 0px; }
        body { height: 100%; width: 100%; background: #000; color: #ffffff; font: normal 12px/1.5 'Arial', 'SimSun', 'Tahoma', 'Helvetica', 'sans-serif','Consolas'; text-align: left; overflow-x: hidden; }
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
        .rem { color:#666666; }
        .kwrd { color:blue;  }
        .csharpcode {
            line-height: 18px;
            font-size: 12px;
            font-family: Consolas;
        }
        #content{font-family: courier new,courier,monospace}
    </style>
    <link rel="stylesheet" type="text/css" href="/GetResource?resourceName=buttons.css" />
</head>
<body style="font-family: Consolas; padding: 0px; margin: 0px;">
    <table style="height: 30px; line-height: 30px; width: 100%; margin: 0px; background: #305c9a; color: #ffffff; font-size: 14px; font-family: 微软雅黑; padding: 0px; border-collapse: collapse;">
        <tr>
            <td style="padding: 0px; font-weight: bold; line-height: 30px;">Command命令行执行器</td>
            <td style="text-align: right; padding-right: 20px; width: 60px; background: #c00;">
                <a href="/" style="color: #ffffff; font-weight: bold;">返回</a>
            </td>
        </tr>
    </table>
    <form action="/DtoGenerator" id="DtoGenerator" method="post" target="_blank" style="margin: 0px; width: 100%;">
        <table style="height: 30px; line-height: 30px; width: 100%; margin: 20px auto 10px auto;">
            <tr>
                <td class="l">Command命令</td>
                <td>
                    <textarea name="command" id="command" style="height: 100px; background: #000; color: #ffffff; height: 100px;" placeholder="可以输入：help 命令查看系统支持的命令行"></textarea>
                </td>
                <td class="l"></td>
            </tr>
            <tr>
                <td class="l"></td>
                <td style="padding: 10px 0px 10px 0px;">
                    <input type="submit" value="执行命令行" class="button icon add" style="font-weight: bold;" />
                </td>
                <td class="l"></td>
            </tr>
            <tr id="dtoCsharpCode" style="background: #000; color: #ffffff;">
                <td></td>
                <td>
                    <pre id="content"></pre>
                </td>
                <td class="l"></td>
            </tr>
        </table>
        <script type="text/javascript">

            $("#DtoGenerator").submit(function (e) {
                $("#content").html("命令行处理中，请稍等......")
                $("#dtoCsharpCode").hide();
                $.ajax({
                    cache: true,
                    type: "POST",
                    url: "/CommandExecutor/Execute",
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
