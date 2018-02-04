/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/12/29 12:25:01
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// 动态编译外部的CS类接口，外部类不要包含在程序集里（属性设置成内容或者无）
    /// 目的：有时候需要动态注册一些配置；有些配置比较复杂，需要使用动态的.NET代码，
    /// 因此设计此接口，允许框架在加载的时候，动态生成程序集，加载实现此接口的类型，动态注册
    /// 注意此接口，接口框架并不会自动搜索（因为实现此类的文件都是以文件的访问放置于服务器的，方便正式环境下手工修改配置）
    /// </summary>
    public interface IDynamicCompiledDependencyRegistar
    {
        /// <summary>
        /// 动态注册一些配置操作
        /// </summary>
        void Register(GlobalConfiguration globalConfiguration);
    }
}
