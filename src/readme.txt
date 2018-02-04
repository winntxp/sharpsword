需要运行此项目请按照下列顺序即可

运行此演示，请确认本地安装了MSSQL，并且我们需要账户：sa ，密码为：123456 ，系统运行后会自动帮您创建一个V200的测试数据库 
如果您的MSSQL数据库账户sa密码非123456或者非MSSQL数据库(如MySql)，可以先修改项目:SharpSword.Host.V20里的web.config文件。 
修改节点为：connectionStrings.apiV200，将connectionString属性替换成您是数据库服务器即可。 


1.使用VS2012或以上版本打开解决方案:SharpSword.sln，并且编译项目:SharpSword.Host

2.运行IISBuilder.bat批处理文件，创建IIS站点（如果还未安装IIS7.5以上，需要先安装）

3.更改本机HOST文件,新建一行
	127.0.0.1 www.sharpsword.com



4.在浏览器输入：www.sharpsword.com 即可运行项目