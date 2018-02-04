<!--
 *************************************************************
 * SharpSword zhangliang@sharpsword.com.cn <%:DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss.fff") %>
 * ***********************************************************
-->

<%@ Page Language="C#" %>

<%@ Assembly Name="SharpSword.TaskManagement" %>
<%@ Assembly Name="SharpSword" %>
<%@ Assembly Name="Autofac" %>
<%@ Import Namespace="SharpSword.WebApi" %>
<%@ Import Namespace="System.Collections.Specialized" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="SharpSword.TaskManagement.Actions" %>

<script runat="server">
    public RequestContext RequestContext;
    public ActionResult ActionResult;
</script>

<%  
    var responseDto = this.ActionResult.Data as List<TaskManagerAction.TaskThreadDto>;

    var @namespace = this.RequestContext.ActionDescriptor.ActionType.Assembly.GetName().Name;
%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>任务查看器，基于框架SharpSword：<%:ApiVersion.Version %></title>
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

        .taskThread .head td {
            padding-left: 5px;
            font-weight: bold;
        }

        .taskDetail td {
            border-bottom: 1px dotted #ccc;
            border-right: 1px dotted #ccc;
        }
    </style>
</head>
<body style="font-family: Consolas; padding: 0px; margin: 0px">
    <table style="height: 30px; line-height: 30px; width: 100%; margin: 0px; background: #305c9a; color: #ffffff; font-size: 14px; font-family: 微软雅黑; padding: 0px; border-collapse: collapse;">
        <tr>
            <td style="width: 30px; vertical-align: bottom; height: 30px; line-height: 30px;">
                <img src="/GetResource?resourceName=<%:@namespace %>.Resource.Logo.png" style="width: 30px; height: 30px; margin-top: 5px;" /></td>
            <td style="padding: 0px; font-weight: bold; line-height: 30px; height: 30px; font-size: 14px;">API接口框架作业任务管理器 </td>
            <td style="text-align: right; padding-right: 20px; width: 60px; background: #c00;">
                <a href="/" style="color: #ffffff; font-weight: bold;">返回</a>
            </td>
        </tr>
    </table>

    <%foreach (var taskThreadDto in responseDto)
        { %>

    <table class="taskThread" style="height: 30px; line-height: 30px; margin: 10px auto 10px auto; border-collapse: collapse; background: #ccc; width: 98%;">
        <tr class="head" style="background: #ff6a00; color: #ffffff;">
            <td style="width: 100px">Seconds：<%:taskThreadDto.Seconds %></td>
            <td style="width: 200px">IsRunning：<%:taskThreadDto.IsRunning %></td>
            <td>&nbsp;</td>
            <td style="width: 300px; text-align: right;">Started：<%:taskThreadDto.Started.ToString("yyyy/MM/dd HH:mm:ss") %></td>
            <td style="width: 300px; text-align: right;">LastRuned：<%:taskThreadDto.LastRuned.ToString("yyyy/MM/dd HH:mm:ss") %></td>
        </tr>
        <tr>
            <td class="l" colspan="5" style="padding: 0px;">
                <table class="taskDetail" style="border-collapse: collapse; border: 1px solid #ccc; padding: 0 10px 0 10px; color: #ffffff; width: 100%;">
                    <tr style="background: #ccc; font-weight: bold; color: #305c9a">
                        <th class="l" style="width: 300px">Name</th>
                        <th class="l" style="width: 100px">IsRunning</th>
                        <th class="l" style="width: 200px">RunOnOneWebFarmInstance</th>
                        <th class="l" style="width: 140px">LeasedUntil</th>
                        <th class="l" style="width: 200px">LeasedByMachineName</th>
                        <th class="l">TaskType</th>
                        <th class="l" style="width: 100px">Enabled</th>
                        <th class="l" style="width: 100px">StopOnError</th>
                    </tr>
                    <%foreach (var task in taskThreadDto.Tasks)
                        { %>
                    <tr style="background: #ffffff; color: black;">
                        <td class="l"><%:task.Name %></td>
                        <td class="l" style="color: green"><%:task.IsRunning %></td>
                        <td class="l"><%:task.RunOnOneWebFarmInstance %></td>
                        <td class="l"><%:task.LeasedUntil.HasValue? task.LeasedUntil.Value.ToString("yyyy/MM/dd HH:mm:ss"):"" %></td>
                        <td class="l"><%:task.LeasedByMachineName %></td>
                        <td class="l"><%:task.TaskType %></td>
                        <td class="l"><%:task.Enabled %></td>
                        <td class="l"><%:task.StopOnError %></td>
                    </tr>
                    <%} %>
                </table>
            </td>
        </tr>
    </table>
    <%} %>
</body>
</html>
