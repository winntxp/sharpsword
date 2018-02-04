<!--
 *********************************************************
 * SharpSword zhangliang@sharpsword.com.cn <%:DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss.fff") %>
 * *******************************************************
-->
<%@ Page Language="C#" %>
<%@ Assembly Name="SharpSword" %>
<%@ Assembly Name="SharpSword.SdkBuilder.CSharp" %>
<%@ Import Namespace="SharpSword" %>
<%@ Import Namespace="System.Collections.Specialized" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="SharpSword.WebApi" %>
<%@ Import Namespace="SharpSword.SdkBuilder.CSharp" %>
<%@ Import Namespace="SharpSword.SdkBuilder.CSharp.Actions" %>

<script runat="server">
    public RequestContext RequestContext;
    public ActionResult ActionResult;
</script>

<%
    var responseDto = this.ActionResult.Data as ApiDescriptorAction.ApiDescriptorActionResponseDto;
%>

<b>ActionDescriptor - <%:responseDto.ActionDescriptor.ActionName %>(v<%:responseDto.ActionDescriptor.Version %>)</b>
<hr/>
<ul>
    <%foreach (var item in responseDto.ActionDescriptor.GetAttributes()){ %>
    <li><%:item.Key %>：<%:item.Value.Serialize2Josn() %></li>
    <%} %>
</ul>