/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/2/2015 09:32:16 PM
 * ****************************************************************/

namespace SharpSword.SDK
{
    /// <summary>
    /// 根据API接口名称获取调用服务器API接口地址，一般在统一SDK调用，但是服务器不一样的情况下
    /// 获取服务器地址委托，参数1为接口名称，参数2为返回值
    /// 为什么定义此委托，当我们将所有接口打包成一个DLL的时候，
    /// 其实后端可能是几个服务器，所以我们需要根据接口名称来判断连接那个服务器
    /// 此接口还具有A/B测试功能，当我们需要进行A/B测试的时候，我们只要重写此URL获取，让不同组或者不同用户迁移到接口的不同版本上进行测试
    /// 参数：api接口名称，比如：Api.Mall.Get 返回值为：API接口服务器地址
    /// </summary>
    public interface IServerUrlFactory
    {
        /// <summary>
        /// 获取api接口服务器地址
        /// </summary>
        /// <param name="apiName">api名称比如：Api.Server.GetTime</param>
        /// <param name="version">接口版本，如：1.0（因为在实际中，我们可能不同版本的接口放在不同服务器，一般在接口平滑升级需要用到）</param>
        /// <returns> 返回如：http://www.sharpsword.com/api </returns>
        string GetApiServerUrl(string apiName, string version);
    }
}
