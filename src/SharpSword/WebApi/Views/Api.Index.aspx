﻿<!--
 **************************************************************************
 * SharpSword zhangliang@sharpsword.com.cn <%:DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss.fff") %>
 * ************************************************************************
-->

<%@ Page Language="C#" %>

<%@ Assembly Name="SharpSword" %>
<%@ Import Namespace="SharpSword" %>
<%@ Import Namespace="System.Collections.Specialized" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="SharpSword.WebApi" %>
<%@ Import Namespace="SharpSword.WebApi.Actions" %>

<script runat="server">
    public RequestContext RequestContext;
    public ActionResult ActionResult;
</script>

<%
    var responseDto = this.ActionResult.Data as IndexAction.IndexActionResponseDto;
    if (responseDto.IsNull() || responseDto.ActionDescriptors == null || responseDto.ApiPluginDescriptors == null)
    {
        Response.Write(this.ActionResult.Info);
        return;
    }
%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title><%="SharpSword系统组件查看器，基于框架SharpSword" %>：<%:ApiVersion.Version %></title>
    <style type="text/css">
        html { height: 100%; margin: 0px; }

        body { height: 100%; width: 100%; margin: 0px; background: #f4f4f4; color: #333; font: normal 12px/1.5 'Arial', 'SimSun', 'Tahoma', 'Helvetica', 'sans-serif','Consolas'; text-align: left; overflow-x: hidden; }

        a { text-decoration: none; outline: 0; cursor: pointer; color: blueviolet; }

        a:hover { color: #305c9a; text-decoration: underline; }

        .header { padding: 0px; background: #C00; background: #323b88; width: 100%; margin: 0px; height: 40px; font-size: 18px; font-weight: bold; line-height: 40px; color: #ffffff; position: fixed; }

        .header .lft { margin-left: 10px; float: left; }

        .header .lft .title { font-size: 22px;; font-family: wf_segoe-ui_light,"Segoe UI Light","Segoe WP Light",wf_segoe-ui_normal,"Segoe UI",Segoe,"Segoe WP",Tahoma,Verdana,Arial,sans-serif; }

        .header .rgt { padding-right: 10px; font-size: 12px; float: right; width: 200px; text-align: right; }

        .header .rgt a { color: #ffffff; text-decoration: underline; }

        .header .rgt a:hover { color: #305c9a; text-decoration: underline; }

        .main { margin-top: 0px; padding: 0px; width: 100%; border-collapse: collapse; top: 40px; }

        .main b { font-size: 24px; }

        .main .lft { background: #305c9a; color: #ffffff; width: 300px; padding-top: 10px; }

        .main .lft a { color: #ffffff; }

        .main .lft a:hover { color: #ffffff; text-decoration: underline; }

        .main .lft ul li { font-size: 10px; line-height: 18px; }

        .main .rgt { padding-left: 20px; background: #ffffff; padding-top: 10px; }

        .main .rgt .title { font-size: 24px; }

        .main .by { font-size: 12px; color: brown; }

        .main .rgt ul li { font-size: 12px; color: #008080; line-height: 20px; }

        img { padding: 0px; width: 50px; height: 50px; border: 1px solid #dedede; -moz-border-radius: 5px; /* Gecko browsers */ -webkit-border-radius: 5px; /* Webkit browsers */ border-radius: 5px; /* W3C syntax */ }
    </style>
</head>
<body style="font-family: 微软雅黑;">
    <table class="header">
        <tr>
            <td class="lft"><span class="title">SharpSword</span> &nbsp;&nbsp;<span style="font-size: 9px;">Powered by：V<%:ApiVersion.Version %>
               ，HOST：<%:this.RequestContext.HttpContext.Request.LocalAddr() %>

               ，Runtime Version:<%:Environment.Version.Major %>.<%:Environment.Version.Minor %>.<%:Environment.Version.Build %>.<%:Environment.Version.Revision %>
            </span>
            </td>
            <td class="rgt">描述(<a target="_blank" href="/Api/Help/Xml">Xml</a>,<a target="_blank" href="/Api/Help/Json">Json</a>,<a target="_blank" href="/Api/Help/View">View</a>)</td>
        </tr>
    </table>
    <table style="margin: 0px; padding: 0px; width: 100%; border-collapse: collapse; margin-left: -2px;">
        <tr>
            <td style="padding-top: 45px;">
                <table class="main">
                    <tr>
                        <td class="rgt" valign="top">
                            <b style="color: #305c9a">项目已加载组件/插件(<%: responseDto.ApiPluginDescriptors.Count() %>)</b>
                            <hr />
                            <% foreach (var item in responseDto.ApiPluginDescriptors)
                                { %>
                            <table style="width: 98%; margin-top: 10px; border-bottom: 1px dotted #ccc;">
                                <tr>
                                    <td valign="top" style="width: 50px;">
                                        <img src="<%: item.Logo.IsNullOrEmptyForDefault(() => "/GetResource?resourceName=SystemPlugin.png", logo => logo) %>" />
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td class="title" valign="top">
                                                    <%if (!item.IndexUrl.IsNullOrEmpty())
                                                        { %>
                                                    <a href="<%: item.IndexUrl.IsNullOrEmptyForDefault(() => "javascript:void();", indexUrl => indexUrl) %>" target="_blank"><%: item.DisplayName %></a><span class="by">(By:<%: item.Author %>，Version:<%: item.Version %>，Hotswap:<%:item.Hotswap %>)</span>
                                                    <%}
                                                        else
                                                        { %>
                                                    <%: item.DisplayName %><span class="by">(By:<%: item.Author %>，Version:<%: item.Version %>，Hotswap:<%:item.Hotswap %>)</span>
                                                    <%} %>
                                                </td>
                                            </tr>
                                            <% if (!item.Description.IsNullOrEmpty())
                                                {%>
                                            <tr>
                                                <td>&nbsp;<span style="color: #cc0000">Description：</span></td>
                                            </tr>
                                            <tr>
                                                <td style="padding-left: 25px; padding-top: 0px;">
                                                    <pre style="color: dimgray"><%: item.Description.Trim().HtmlEncode() %></pre>
                                                </td>
                                            </tr>
                                            <% } %>
                                            <% if (item.ReferencedAssemblies.Any())
                                                { %>
                                            <tr>
                                                <td>&nbsp;<span style="color: #cc0000">Dependencies：</span>
                                                    <ul>
                                                        <% foreach (var referencedAssembly in item.ReferencedAssemblies.OrderBy(o => o))
                                                            { %>
                                                        <li><%: referencedAssembly %></li>
                                                        <% } %>
                                                    </ul>
                                                </td>
                                            </tr>
                                            <% } %>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <% } %>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%; height: 80px; background: #f4f4f4; border-top: 1px solid #ccc;">
        <tr>
            <td style="text-align: center;">Copyright © 2015-<%:DateTime.Now.Year %> SharpSword</td>
        </tr>
    </table>
</body>
</html>
