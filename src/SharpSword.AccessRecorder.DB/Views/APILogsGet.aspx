<!--
 *************************************************************
 * SharpSword zhangliang4629@163.com <%:DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss.fff") %>
 * ***********************************************************
-->

<%@ Page Language="C#" %>
<%@ Assembly Name="SharpSword" %>
<%@ Assembly Name="SharpSword.EntityFramework" %>
<%@ Assembly Name="SharpSword.AccessRecorder.DB" %>

<%@ Assembly Name="Autofac" %>
<%@ Import Namespace="SharpSword" %>
<%@ Import Namespace="SharpSword.WebApi" %>
<%@ Import Namespace="SharpSword.AccessRecorder.DB" %>
<%@ Import Namespace="SharpSword.AccessRecorder.DB.Domain" %>
<%@ Import Namespace="System.Collections.Specialized" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="Autofac" %>
<%@ Import Namespace="Autofac.Core.Lifetime" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="SharpSword.Domain.Entitys" %>

<script runat="server">
    public RequestContext RequestContext;
    public ActionResult ActionResult;
</script>

<%   
    //上送参数
    var requestDto = this.RequestContext.RequestDto as ApiLogGetAction.ApiLogGetActionRequestDto;

    //输出的数据
    var responseDto = this.ActionResult.Data as ApiLogGetAction.ApiLogGetActionResponseDto;
%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>API接口日志访问插件，基于框架SharpSword：<%:ApiVersion.Version %></title>
    <style type="text/css">
        html { height: 100%; margin: 0px; }
        body { height: 100%; width: 100%; background: #f4f4f4; color: #333; font: normal 12px/1.5 'Arial', 'SimSun', 'Tahoma', 'Helvetica', 'sans-serif','Consolas'; text-align: left; overflow-x: hidden; }
        b { font-size: 18px; }
    </style>
</head>
<body style="font-family: Consolas; padding: 0px; margin: 0px">
    <table style="height: 30px; line-height: 30px; width: 100%; margin: 10px;">
        <tr>
            <td><b>ActionName:</b> </td>
        </tr>
        <tr>
            <td><%:responseDto.AccessRecorder.ApiName %></td>
        </tr>
        <tr>
            <td><b>RequestDto:</b></td>
        </tr>
        <tr>
            <td>
                <textarea style="width: 99%; height: 200px"><%:responseDto.ResponseData.RequestData %></textarea></td>
        </tr>
        <tr>
            <td><b>ResponseDto:</b></td>
        </tr>
        <tr>
            <td>
                <textarea style="width: 99%; height: 500px"><%:responseDto.ResponseData.ResponseData %></textarea></td>
        </tr>
    </table>
</body>
</html>
