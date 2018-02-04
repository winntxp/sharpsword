::https://technet.microsoft.com/en-us/library/ec52c53b-6aff-4d76-995e-3d222588bf32
::http://blog.csdn.net/wulex/article/details/61916128

cd /d %~dp0

::新建站点

@set "sitePath=%cd%\SharpSword.Host"
@echo %sitePath%

@set "siteName=www.sharpsword.com"
@echo 新建站点 %siteName%
@C:\Windows\System32\inetsrv\appcmd.exe add apppool /name:%siteName% /managedRuntimeVersion:"v4.0"
@C:\Windows\System32\inetsrv\appcmd.exe add site /name:%siteName% /bindings:http://%siteName%:80 /applicationDefaults.applicationPool:%siteName% /physicalPath:%sitePath%

::设置HOST
set PATH=%SystemRoot%\system32\drivers\etc\hosts 
set put=127.0.0.1 %siteName%
echo %put% >> %PATH%
 
Pause

::删除站点
@C:\Windows\System32\inetsrv\appcmd.exe delete site %siteName%


