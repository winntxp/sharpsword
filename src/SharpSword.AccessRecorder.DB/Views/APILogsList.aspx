<!--
 *************************************************************
 * SharpSword zhangliang@sharpsword.com.cn <%:DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss.fff") %>
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
    var requestDto = this.RequestContext.RequestDto as ApiLogsAction.ApiLogsActionRequestDto;

    //输出的数据
    var responseDto = this.ActionResult.Data as PagedList<AccessRecorder>;
 
%>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>API接口日志访问插件，基于框架SharpSword：<%:ApiVersion.Version %></title>
    <script type="text/javascript" src="/GetResource?resourceName=jquery-1.9.1.min.js"></script>
    <script type="text/javascript" src="/GetResource?resourceName=jquery.autocomplete.js"></script>
    <style type="text/css">
        html { height: 100%; margin: 0px; }
        body { height: 100%; width: 100%; background: #f4f4f4; color: #333; font: normal 12px/1.5 'Arial', 'SimSun', 'Tahoma', 'Helvetica', 'sans-serif','Consolas'; text-align: left; overflow-x: hidden; }
        a:hover { color: #C00; text-decoration: underline; }
        .c { text-align: center; }
        .id { text-align: center; border-right: 1px solid #ccc; width: 60px; }
        .an { padding-left: 4px; padding-right: 4px; word-break: break-all; width: 300px; }
        .ver { width: 80px; padding-left: 5px; padding-right: 5px; }
        .hm { width: 60px; padding-left: 5px; padding-right: 5px; }
        .ut { width: 80px; padding-left: 5px; padding-right: 5px; }
        .cd { width: 80px; padding-left: 5px; padding-right: 5px; }
        .rf { width: 60px; padding-left: 5px; padding-right: 5px; }
        .un { width: 60px; padding-left: 5px; padding-right: 5px; }
        .ip { width: 80px; padding-left: 5px; padding-right: 5px; }
        .rd { word-break: break-all; padding-left: 5px; padding-right: 5px; }
         .ts { width: 100px; padding-left: 5px; padding-right: 5px; }
        .t { background: #ffffff; }
        .l { border-bottom: 1px dotted #ccc; }
        .lb { border-left: 1px solid #ccc; }
        .rb { border-right: 1px solid #ccc; }
        .dataTable tr:hover { background: #f4f4f4; }
        .dataTable tr:hover td { background: none; }

        input { box-sizing: border-box; width: 99%; font-size: 12px; font-family: 微软雅黑; font-weight: bold; }
        input { border-collapse: separate; -webkit-border-radius: 5px; -moz-border-radius: 5px; border-radius: 5px; /* W3C syntax */ }
        input { padding: 0.5em 0.5em; border: 1px solid #d4d4d4; margin: 0; text-decoration: none; text-shadow: 1px 1px 0 #fff; font: 12px/normal '宋体'; color: #333; white-space: pre-wrap; cursor: pointer; outline: none; background-color: #FFFFFF; background-image: -webkit-gradient(linear, 0 0, 0 100%, from(#f4f4f4), to(#FFFFFF)); background-image: -moz-linear-gradient(#f4f4f4, #FFFFFF); background-image: -o-linear-gradient(#f4f4f4, #ececec); background-image: linear-gradient(#f4f4f4, #FFFFFF); -webkit-background-clip: padding; -moz-background-clip: padding; -o-background-clip: padding-box; /*background-clip: padding-box;*/ /* commented out due to Opera 11.10 bug */ -webkit-border-radius: 0.2em; -moz-border-radius: 0.2em; border-radius: 0.2em; /* IE hacks */ zoom: 0; *display: inline; }
        input:focus, textarea:focus { border-color: rgba(82, 168, 236, 0.8); outline: 0; outline: thin dotted; -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(82,168,236,.6); -moz-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(82,168,236,.6); box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(82,168,236,.6); }

    </style>
    <link rel="stylesheet" type="text/css" href="/GetResource?resourceName=buttons.css" />
    <link rel="stylesheet" type="text/css" href="/GetResource?resourceName=jquery.autocomplete.css" />
</head>
<body style="font-family: Consolas; padding: 0px; margin: 0px">
    <table style="height: 24px; line-height: 24px; width: 100%; margin: 0px; background: #305c9a; color: #ffffff;">
        <tr>
            <td style="width: 500px">TotalCount:<%:responseDto.TotalCount %>，TotalPages: <%:responseDto.TotalPages %>，PageSize:<%:responseDto.PageSize %>，PageIndex:<%:responseDto.PageIndex+1 %></td>
            <td style="text-align: center;">
                <%if (responseDto.HasPreviousPage)
                  { %>
                <a style="color: #ffffff;" href="/Api?ActionName=API.Logs.List&format=view&data=<%:new {requestDto.PageSize,PageIndex= requestDto.PageIndex-1,requestDto.ApiName,requestDto.Ip,responseDto.TotalCount,requestDto.UsedTime }.Serialize2Josn().UrlEncode()%>">Prev</a>
                <%} %>
                <%for (int i = requestDto.PageIndex > 10 ? requestDto.PageIndex - 9 : 0; i < requestDto.PageIndex + 10 && i < responseDto.TotalPages; i++)
                  {%>
                <a style="color: #ffffff;" href="/Api?ActionName=API.Logs.List&format=view&data=<%:new { requestDto.PageSize, PageIndex = i+1, requestDto.ApiName,requestDto.Ip,responseDto.TotalCount,requestDto.UsedTime }.Serialize2Josn().UrlEncode()%>"><%:i+1 %></a>
                <%}%>
                <%if (responseDto.HasNextPage)
                  { %>
                <a style="color: #ffffff;" href="/Api?ActionName=API.Logs.List&format=view&data=<%:new {requestDto.PageSize,PageIndex= requestDto.PageIndex+1,requestDto.ApiName,requestDto.Ip,responseDto.TotalCount,requestDto.UsedTime }.Serialize2Josn().UrlEncode()%>">Next</a>
                <%} %>
            </td>
            <td style="text-align: right; padding-right: 10px; width: 400px;">CachedAt:<%:this.ActionResult.CachedTime %>，CacheTime:(<%:this.RequestContext.ActionDescriptor.Cache.CacheTime %>)Minute
            </td>
        </tr>
    </table>
    <form action="/logs/s" method="get">
        <table style="height: 30px; line-height: 30px; margin: 0px; padding: 5px;">
            <tr>
                <td>接口名称：
                    <input name="apiname" id="apiname" value="<%:requestDto.ApiName %>" style="width: 400px" /></td>
                <td>IP：
                    <input name="ip" value="<%:requestDto.Ip %>" style="width: 200px;" /></td>
                <td>UsedTime：
                    >=<input name="usedTime" value="<%:requestDto.UsedTime %>" style="width: 60px;" />ms</td>
                <td style="width: 80px;">
                    <input type="submit" value="搜索" class="button icon add" />
                    <script type="text/javascript">
                        $('#apiname').autocomplete({
                            serviceUrl: '/logs/actionsget',
                            width: 400,
                            onSelect: function (suggestion) {
                            }
                        });
                    </script>
                </td>
            </tr>
        </table>
    </form>
    <table class="dataTable" style="width: 100%; margin: 0px; padding: 0px; border-collapse: collapse;">
        <tr style="background: #305c9a; height: 30px; color: #ffffff;">
            <th class="c">Id</th>
            <th class="an">ApiName</th>
            <th class="cd">Created</th>
            <th class="ut">UsedTime(ms)</th>
            <th class="ver">Version</th>
            <th class="hm">HttpMethod</th>
            <th class="ip">IP</th>
            <th class="rf">ResponseFormat</th>
            <th class="ut">Sign</th>
            <th class="ts">TimeStamp</th>
            <th class="un">UserName</th>
        </tr>
        <%foreach (var item in responseDto)
          { %>
        <tr class="t">
            <td class="l id"><a onclick="" target="_blank" href="/Api?ActionName=API.Logs.Get&format=view&data=<%:new {item.Id}.Serialize2Josn().UrlEncode()%>"><%:item.Id %></a></td>
            <td class="l an">
                <a target="_blank" href="/Api?ActionName=API.Logs.List&format=view&data=<%:new {item.ApiName,item.Id }.Serialize2Josn().UrlEncode()%>"><%:item.ApiName %></a>
            </td>
            <td class="l cd">
                <%:item.Created %>
            </td>
            <td class="l ut">
                <span style="color: <%=(item.UsedTime>500?"red":"")%>"><%:item.UsedTime %></span>
            </td>
            <td class="l ver">
                <%:item.Version %>
            </td>
            <td class="l hm">
                <%:item.HttpMethod %>
            </td>
            <td class="l ip">
                <%:item.Ip %>
            </td>
            <td class="l rf">
                <%:item.ResponseFormat %>
            </td>
            <td class="l rf">
                <%:item.Sign %>
            </td>
            <td class="l rf">
                <%:item.TimeStamp%>
            </td>
            <td class="l un">
                <%:item.UserName %>
            </td>
        </tr>
        <%} %>
    </table>
    <table style="height: 24px; line-height: 24px; width: 100%; margin: 0px; background: #305c9a; color: #ffffff;">
        <tr>
            <td style="width: 500px">TotalCount:<%:responseDto.TotalCount %>，TotalPages: <%:responseDto.TotalPages %>，PageSize:<%:responseDto.PageSize %>，PageIndex:<%:responseDto.PageIndex+1 %></td>
            <td style="text-align: center;">
                <%if (responseDto.HasPreviousPage)
                  { %>
                <a style="color: #ffffff;" href="/Api?ActionName=API.Logs.List&format=view&data=<%:new {requestDto.PageSize,PageIndex= requestDto.PageIndex-1,requestDto.ApiName,requestDto.Ip,responseDto.TotalCount,requestDto.UsedTime }.Serialize2Josn().UrlEncode()%>">Prev</a>
                <%} %>
                <%for (int i = requestDto.PageIndex > 10 ? requestDto.PageIndex - 9 : 0; i < requestDto.PageIndex + 10 && i < responseDto.TotalPages; i++)
                  {%>
                <a style="color: #ffffff;" href="/Api?ActionName=API.Logs.List&format=view&data=<%:new { requestDto.PageSize, PageIndex = i+1, requestDto.ApiName,requestDto.Ip,responseDto.TotalCount,requestDto.UsedTime }.Serialize2Josn().UrlEncode()%>"><%:i+1 %></a>
                <%}%>
                <%if (responseDto.HasNextPage)
                  { %>
                <a style="color: #ffffff;" href="/Api?ActionName=API.Logs.List&format=view&data=<%:new {requestDto.PageSize,PageIndex= requestDto.PageIndex+1,requestDto.ApiName,requestDto.Ip,responseDto.TotalCount,requestDto.UsedTime }.Serialize2Josn().UrlEncode()%>">Next</a>
                <%} %>
            </td>
            <td style="text-align: right; padding-right: 10px; width: 400px;">CachedAt:<%:this.ActionResult.CachedTime %>，CacheTime:(<%:this.RequestContext.ActionDescriptor.Cache.CacheTime %>)Minute
            </td>
        </tr>
    </table>
</body>
</html>
