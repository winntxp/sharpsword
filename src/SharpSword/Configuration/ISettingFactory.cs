/* *******************************************************
 * SharpSword zhangliang4629@163.com 10/14/2016 10:22:00 AM
 * ****************************************************************/
using System;

namespace SharpSword.Configuration
{
    /// <summary>
    /// 配置文件获取工厂，不同配置数据源需要实现不同的ISettingFactory接口
    /// </summary>
    public interface ISettingFactory
    {
        /// <summary>
        /// 支持哪一类配置文件，此类型必须要实现或者继承:ISetting接口，
        /// 一般来说，一种类型的配置参数对象需要定义一个区别于其他数据源类型的配置参数接口
        /// </summary>
        Type Supported { get; }

        /// <summary>
        /// 优先级，针对相同Supported属性，我们才配置参数的时候，根据优先级进行选取
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// 获取配置文件对象
        /// </summary>
        /// <returns>配置参数对象</returns>
        TSetting Get<TSetting>() where TSetting : ISetting, new();
    }
}
