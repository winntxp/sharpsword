/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/21/2016 9:45:58 AM
 * ****************************************************************/
using System.Configuration;

namespace SharpSword.Configuration.WebConfig
{
    /// <summary>
    /// 基于web.config参数配置需要继承的接口，在定义基于web.config的配置参数时候，为了方便我们可以继承
    /// ConfigurationSectionHandlerBase抽象类，无需重写任何方法即可方便实现参数自动获取
    /// </summary>
    public interface IWebConfigConfiguration : IConfigurationSectionHandler, ISetting { }
}
