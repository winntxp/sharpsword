<!--
 *************************************************************
 * SharpSword zhangliang@sharpsword.com.cn <%:DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss.fff") %>
 * ***********************************************************
-->
<%@ Page Language="C#" %>
<%@ Assembly Name="Autofac" %>
<%@ Assembly Name="SharpSword" %>
<%@ Assembly Name="SharpSword.Tools" %>
<%@ Import Namespace="SharpSword" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Collections.Specialized" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Reflection" %>
<%@ Import Namespace="SharpSword.WebApi" %>
<%@ Import Namespace="SharpSword.Tools" %>
<%@ Import Namespace="SharpSword.Tools.Actions" %>

<script runat="server">
    public RequestContext RequestContext;
    public ActionResult ActionResult;
</script>

<%  
    var responseDto = this.ActionResult.Data as IEnumerable<MethodInfo>;
    int index = 0;
%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>系统所有自定义SQL语句方法，基于框架SharpSword：<%:ApiVersion.Version %></title>
    <style type="text/css">
        html {
            height: 100%;
            margin: 0px;
        }

        body {
            height: 100%;
            width: 100%;
            font: normal 12px/1.5 'Arial', 'SimSun', 'Tahoma', 'Helvetica', 'sans-serif','Consolas';
            text-align: left;
            overflow-x: hidden;
        }

        .l {
            padding-left: 5px;
        }

        .detail {
            margin-left: auto;
            margin-right: auto;
            width: 99%;
            margin-top: 10px;
        }

            .detail td {
                border-bottom: 1px dotted #ccc;
                border-right: 1px dotted #ccc;
                padding-left: 5px;
                line-height: 30px;
            }
    </style>
</head>
<body style="font-family: Consolas; padding: 0px; margin: 0px">
    <table style="height: 30px; line-height: 30px; width: 100%; margin: 0px; background: #305c9a; color: #ffffff; font-size: 14px; font-family: 微软雅黑; padding: 0px; border-collapse: collapse;">
        <tr>
            <td style="padding: 0px; font-weight: bold; line-height: 30px; height: 30px; font-size: 14px;">系统自定义SQL语句方法集</td>
            <td style="text-align: right; padding-right: 20px; width: 60px; background: #c00;">
                <a href="/Api/Index" style="color: #ffffff; font-weight: bold;">返回</a>
            </td>
        </tr>
    </table>

    <table class="detail" style="border-collapse: collapse; border: 1px solid #ccc; padding: 0 10px 0 10px; color: #ffffff;">
        <tr style="background: #ff6a00; font-weight: bold; color: #fff; height: 30px;">
            <th style="width: 50px; text-align: center">编号</th>
            <th class="l" style="width: 300px; padding: 0 10px 0 10px">方法名称</th>
            <th class="l" style="width: 500px; padding: 0 10px 0 10px">所属类型</th>
            <th class="l">程序集</th>
            <th class="l" style="width: 100px">程序集版本</th>
        </tr>
        <%foreach (var @event in responseDto)
            {
                index++; %>
        <tr style="background: #ffffff; color: black;">
            <td style="text-align: center"><%:index %></td>
            <td class="l"><%:@event.Name %></td>
            <td class="l"><%:@event.DeclaringType.ToString() %></td>
            <td class="l"><%:@event.DeclaringType.Assembly.GetName().Name %></td>
            <td class="l"><%:@event.DeclaringType.Assembly.GetName().Version %></td>
        </tr>
        <%} %>
    </table>

</body>
</html>
