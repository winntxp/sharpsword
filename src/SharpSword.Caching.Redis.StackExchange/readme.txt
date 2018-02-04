https://github.com/imperugo/StackExchange.Redis.Extensions

  <configSections>

    <!--Redis.StackExchange客户端的具体缓存实现2.2.3.ServiceCenter.Api.Core.Caching.Redis.StackExchange-->
    <section name="redisCacheClient" type="StackExchange.Redis.Extensions.Core.Configuration.RedisCachingSectionHandler, StackExchange.Redis.Extensions.Core" />
	
  </configSections>

  <!--Redis.StackExchange缓存实现配置2.2.3.ServiceCenter.Api.Core.Caching.Redis.StackExchange-->
  <redisCacheClient allowAdmin="true" ssl="false" connectTimeout="5000" database="0" password="password">
    <hosts>
      <add host="127.0.0.1" cachePort="6379" />
    </hosts>
  </redisCacheClient>