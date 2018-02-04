使用时候请在web.config配置如下节点，HOST项目里直接引用此项目即可

<?xml version="1.0" encoding="utf-8" ?>
<configuration>

    <configSections>
        <section name="redisCacheClient" type="StackExchange.Redis.Extensions.Core.Configuration.RedisCachingSectionHandler, StackExchange.Redis.Extensions.Core" />
    </configSections>

    <redisCacheClient allowAdmin="true" ssl="false" connectTimeout="5000" database="0" password="my password">
        <hosts>
            <add host="127.0.0.1" cachePort="6379"/>
        </hosts>
    </redisCacheClient>
	
 <runtime>
    <assemblyBinding xmlns="urn:schemas-microsoft-com:asm.v1">
      <!--版本重定向-->
      <dependentAssembly>
        <assemblyIdentity name="Newtonsoft.Json" publicKeyToken="30ad4fe6b2a6aeed" />
        <bindingRedirect oldVersion="4.5.0.0-8.0.0.0" newVersion="8.0.0.0" />
      </dependentAssembly>
    </assemblyBinding>
  </runtime>

</configuration>


