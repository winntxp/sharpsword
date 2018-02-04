/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/14/2016 10:11:22 AM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.Configuration
{
    /// <summary>
    /// 配置文件获取过程创建器
    /// </summary>
    public interface ISettingFactoryBuilder
    {
        /// <summary>
        /// 创建配置文件获取过程
        /// </summary>
        /// <typeparam name="TSetting">配置文件类型</typeparam>
        /// <returns>配置参数对象获取工厂</returns>
        IEnumerable<ISettingFactory> Find<TSetting>() where TSetting : ISetting, new();

        /// <summary>
        /// 释放创建的对象
        /// </summary>
        /// <param name="settingFactorys">当前注册的所有配置获取工厂</param>
        void Release(IEnumerable<ISettingFactory> settingFactorys);
    }
}