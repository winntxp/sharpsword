

此项目为API系统框架缓存实现，使用Memcached作为缓存服务器；

项目使用方式：
1.接口HOST项目引用此项目
2.接口HOST项目web.config文件配置如下节点

缓存web.config节点配置类;无需手工配置，API系统框架会自动将此配置文件赋值给对应的换成类；XML配置文件格式如下：系统框架IOC容器会自动注册此配置

<configuration>

	<configSections>
		<section name="memcachedManagerConfig" type="ServiceCenter.Api.Core.Memcached.MemcachedManagerConfig,ServiceCenter.Api.Core.Memcached"/>
	</configSections>

	<memcachedManagerConfig servers="127.0.0.1:11211,192.168.0.2:11211"/>

<configuration>