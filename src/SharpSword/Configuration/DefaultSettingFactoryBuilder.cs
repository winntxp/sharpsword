/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/14/2016 10:11:22 AM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.Configuration
{
    /// <summary>
    /// 默认配置文件获取工厂创建器
    /// </summary>
    internal class DefaultSettingFactoryBuilder : ISettingFactoryBuilder
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IEnumerable<ISettingFactory> _settingFactorys;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="settingFactorys">配置文件工厂集合</param>
        public DefaultSettingFactoryBuilder(IEnumerable<ISettingFactory> settingFactorys)
        {
            this._settingFactorys = settingFactorys ?? Enumerable.Empty<ISettingFactory>();
        }

        /// <summary>
        /// 根据配置文件类型，从预先注册的工厂里获取对应的工厂
        /// </summary>
        /// <typeparam name="TSetting">配置文件类型</typeparam>
        /// <returns></returns>
        public IEnumerable<ISettingFactory> Find<TSetting>() where TSetting : ISetting, new()
        {
            //能够处理同一类型的配置工厂集合
            return this._settingFactorys.Where(settingFactory => settingFactory.Supported.IsAssignableFrom(typeof(TSetting))).ToList();
        }

        /// <summary>
        /// 释放对象，我们直接看对象是否实现了IDisposable接口
        /// </summary>
        /// <param name="settingFactorys"></param>
        public void Release(IEnumerable<ISettingFactory> settingFactorys)
        {
            foreach (var settingFactory in settingFactorys)
            {
                if (!(settingFactory is IDisposable))
                {
                    continue;
                }

                 ((IDisposable)settingFactorys).Dispose();
            }
        }
    }
}
