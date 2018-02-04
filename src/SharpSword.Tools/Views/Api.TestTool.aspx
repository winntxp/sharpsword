<!--
 *************************************************************
 * SharpSword zhangliang@sharpsword.com.cn <%:DateTime.Now.ToString("yyyy\\/MM\\/dd HH:mm:ss.fff") %>
 * ***********************************************************
-->

<%@ Page Language="C#" %>

<%@ Assembly Name="SharpSword.Tools" %>
<%@ Assembly Name="SharpSword" %>
<%@ Assembly Name="Autofac" %>
<%@ Import Namespace="SharpSword" %>
<%@ Import Namespace="SharpSword.WebApi" %>
<%@ Import Namespace="System.Collections.Specialized" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Collections.Generic" %>
<%@ Import Namespace="System.Web" %>
<%@ Import Namespace="System.Linq" %>
<%@ Import Namespace="SharpSword.Tools" %>

<script runat="server">
    public RequestContext RequestContext;
    public ActionResult ActionResult;
</script>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>API接口调试插件，基于框架SharpSword：<%:ApiVersion.Version %></title>
    <script type="text/javascript" src="/GetResource?resourceName=jquery-1.9.1.min.js,jquery.autocomplete.js"></script>
    <style type="text/css">
        html {
            height: 100%;
            margin: 0px;
        }

        body {
            height: 100%;
            width: 100%;
            background: #f4f4f4;
            color: #333;
            font: normal 12px/1.5 'Arial', 'SimSun', 'Tahoma', 'Helvetica', 'sans-serif','Consolas';
            text-align: left;
            overflow-x: hidden;
        }

        b {
            font-size: 12px;
        }

        .l {
            text-align: right;
            width: 100px;
            padding: 5px 10px 5px 0px;
            font-weight: bold;
            font-size: 14px;
        }

        input, select, textarea {
            box-sizing: border-box;
            width: 99%;
        }

        input, select, textarea, fieldset {
            border-collapse: separate;
            -webkit-border-radius: 5px;
            -moz-border-radius: 5px;
            border-radius: 5px; /* W3C syntax */
        }

        input, select, textarea {
            padding: 0.5em 0.5em;
            border: 1px solid #d4d4d4;
            margin: 0;
            text-decoration: none;
            text-shadow: 1px 1px 0 #fff;
            font: 12px/normal '宋体';
            color: #333;
            white-space: pre-wrap;
            cursor: pointer;
            outline: none;
            background-color: #FFFFFF;
            background-image: -webkit-gradient(linear, 0 0, 0 100%, from(#f4f4f4), to(#FFFFFF));
            background-image: -moz-linear-gradient(#f4f4f4, #FFFFFF);
            background-image: -o-linear-gradient(#f4f4f4, #ececec);
            background-image: linear-gradient(#f4f4f4, #FFFFFF);
            -webkit-background-clip: padding;
            -moz-background-clip: padding;
            -o-background-clip: padding-box; /*background-clip: padding-box;*/ /* commented out due to Opera 11.10 bug */
            -webkit-border-radius: 0.2em;
            -moz-border-radius: 0.2em;
            border-radius: 0.2em; /* IE hacks */
            zoom: 0;
            *display: inline;
        }

            input:focus, textarea:focus {
                border-color: rgba(82, 168, 236, 0.8);
                outline: 0;
                outline: thin dotted;
                -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(82,168,236,.6);
                -moz-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(82,168,236,.6);
                box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(82,168,236,.6);
            }

            input.yellow {
                background: url();
                background-color: #FAFFBD;
            }

            select.yellow {
                background: url();
                background-color: #FAFFBD;
            }

            textarea.yellow {
                background: url();
                background-color: #FAFFBD;
            }

            input[type="checkbox"] {
                border: 0px;
                padding: 0px;
            }

            input.datetime {
                width: 75px;
            }

            select, textarea, input[type="text"] {
                font-family: 微软雅黑;
                font-size: 14px;
            }
    </style>
    <link rel="stylesheet" type="text/css" href="/GetResource?resourceName=buttons.css,jquery.autocomplete.css" />

</head>
<body style="font-family: Consolas; padding: 0px; margin: 0px">
    <table style="height: 30px; line-height: 30px; width: 100%; margin: 0px; background: #305c9a; color: #ffffff; font-size: 14px; font-family: 微软雅黑; padding: 0px; border-collapse: collapse;">
        <tr>
            <td style="padding: 0px; font-weight: bold; line-height: 30px;">API接口调试工具</td>
            <td style="text-align: right; padding-right: 20px; width: 60px; background: #c00;">
                <a href="/" style="color: #ffffff; font-weight: bold;">返回</a>
            </td>
        </tr>
    </table>
    <form action="/Api" method="post" target="_blank" style="margin: 0px; width: 100%;">
        <table style="height: 30px; line-height: 30px; width: 100%; margin: 10px auto 10px auto;">
            <tr>
                <td style="width: 100px;" class="l">ActionName:</td>
                <td>
                    <input name="ActionName" id="ActionName" class="yellow" type="text" placeholder="可以直接输入接口名称关键词" />
                    <input id="hidActionName" type="hidden" />
                    <script type="text/javascript">
                        $('#ActionName').autocomplete({
                            serviceUrl: '/ApiTool/ActionsGet',
                            width: 600,
                            onSelect: function (suggestion) {
                                if ($("#hidActionName").val() == suggestion.data) {
                                    return false;
                                }
                                $.ajax({
                                    type: "post",
                                    url: "/ApiTool/GetRequestDto",
                                    data: "actionName=" + suggestion.data,
                                    dataType: "html",
                                    cache: false,
                                    success: function (data) {
                                        $("#data").val(data);
                                        $("#hidActionName").val(suggestion.data);
                                    },
                                    error: function (e, textStatus, errorThrown) {
                                        console.log(textStatus + ":" + errorThrown);
                                    }
                                });
                            }
                        });
                    </script>
                </td>
            </tr>
            <tr>
                <td class="l">Format:</td>
                <td>
                    <select name="Format" class="yellow">
                        <option value="JSON">JSON</option>
                        <option value="XML">XML</option>
                        <option value="View">View</option>
                    </select>
                </td>
            </tr>
            <tr>
                <td class="l">AppKey:</td>
                <td>
                    <input name="AppKey" value="" type="text" />
                </td>
            </tr>
            <tr>
                <td class="l">Version:</td>
                <td>
                    <input name="Version" value="" type="text" placeholder="不填写接口版本，系统会自动选择最新接口版本，这在平滑接口版本迭代里比较重要(建议客户端不传送此值)" />
                </td>
            </tr>
            <tr>
                <td class="l">TimeStamp:</td>
                <td>
                    <input name="TimeStamp" value="<%:DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") %>" type="text" />
                </td>
            </tr>
            <tr>
                <td class="l">RequestId:</td>
                <td>
                    <input name="RequestId" value="<%:System.Guid.NewGuid().ToString("N").ToUpper() %>.0" type="text" placeholder="客户端请求ID，建议每次请求都传不一样的字符串，比如：GUID" />
                </td>
            </tr>
            <tr>
                <td class="l">Data:</td>
                <td>
                    <textarea id="data" name="Data" style="height: 200px" class="yellow">{}</textarea>
                </td>
            </tr>
            <tr>
                <td class="l">Sign:</td>
                <td>
                    <input name="Sign" value="" type="text" />
                </td>
            </tr>
            <tr>
                <td class="l"></td>
                <td style="padding: 10px 0px 10px 0px;">
                    <input type="submit" value="提交请求" class="button icon add" style="font-weight: bold;" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>API接口框架上送参数固定为上列参数,参数说明为：<br />
                    <b>ActionName：</b>接口名称<br />
                    <b>Format：</b>期望对应Response返回对象的数据格式，可选值为:JSON,VIEW,XML<br />
                    <b>AppKey：</b>客户端ID信息，一般为服务器分配，方便跟踪调用者或者对指定客户端进行接口权限控制<br />
                    <b>Version：</b>接口版本号，如果一个接口同名，可以指定版本号来确定调用哪个接口版本，一般在接口升级或者版本迭代需要切换接口内部逻辑的时候使用（不影响原先接口），在不指定版本号的情况下，框架会自动选择最新版本号同名接口（如果有多个版本情况下）<br />
                    <b>TimeStamp：</b>客户端调用接口时间，格式为：yyyy-MM-dd HH:mm:ss，防止接口被拦截重复提交，后台可以根据此时间来判断接口合法性<br />
                    <b>RequestId：</b>每次请求唯一编号，此编号由调用端生成，建议使用GUID<br />
                    <b>Data：</b>正在需要上送的业务逻辑参数上送对象JSON字符串，此字符串对应的是接口服务层的RequestDto对象<br />
                    <b>Sign：</b>上送数据签名(根据与接口服务器约定，使用AppSecret加密密钥对上送参数进行签名)，主要作用为防止上送数据在传输过程中被篡改(注意：API接口框架并没有给出具体的数据签名方式，但是定义校验签名的接口，需要在实际开发中根据业务，实现下框架定义的IAuthentication接口即可)
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
