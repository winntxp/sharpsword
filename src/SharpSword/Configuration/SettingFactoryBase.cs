/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/19/2016 1:28:43 PM
 * ****************************************************************/
using SharpSword.Localization;
using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace SharpSword.Configuration
{
    /// <summary>
    /// 配置参数获取工厂抽象基类
    /// </summary>
    public abstract class SettingFactoryBase : ISettingFactory
    {
        /// <summary>
        /// 日志记录器，运行时系统框架会自动进行赋值
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public SettingFactoryBase()
        {
            this.Logger = NullLogger.Instance;
            //this.L = NullLocalizer.Instance;
        }

        /// <summary>
        /// 系统框架默认的优先级
        /// </summary>
        protected int DefaultPriority { get { return int.MinValue; } }

        /// <summary>
        /// 我们默认将系统框架级别的创建工厂优先级定义成最下，让扩展有机会
        /// </summary>
        public virtual int Priority { get { return this.DefaultPriority; } }

        /// <summary>
        /// 支持的配置参数对象类型
        /// </summary>
        public abstract Type Supported { get; }

        /// <summary>
        /// 保留下扩展
        /// </summary>
        /// <typeparam name="TSetting"></typeparam>
        /// <returns></returns>
        TSetting ISettingFactory.Get<TSetting>()
        {
            var setting = this.Get<TSetting>();

            //当返回null的时候，我们不进行属性默认值设置
            if (setting.IsNull())
            {
                return setting;
            }

            //我们处理下定义了【DefaultValueAttribute】特性的
            var properties = setting.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
                                              .Where(p => p.CanWrite && !p.IsSpecialName);
            foreach (var property in properties)
            {
                //获取属性默认值
                var typeDefaultValue = property.PropertyType.DefaultValue();

                //当属性从未赋值过，我们就取属性上面定义的默认值特性
                if (property.GetValue(setting) != typeDefaultValue || !property.IsDefined<DefaultValueAttribute>())
                {
                    continue;
                }

                //待转换成的数据类型
                var convertType = property.PropertyType;
                //判断下映射实体属性是否是可空类型;是空类型需要特殊处理
                if (property.PropertyType.IsNullable())
                {
                    var nullableConverter = new NullableConverter(property.PropertyType);
                    convertType = nullableConverter.UnderlyingType;
                }

                //特性定义的默认值
                var defaultValue = property.GetSingleAttributeOrNull<DefaultValueAttribute>().Value;

                //让开开期间设置错误的，直接抛出异常
                try
                {
                    property.SetValue(setting, Convert.ChangeType(defaultValue, convertType));
                }
                catch (Exception exc)
                {
                    this.Logger.Error(exc);
                    throw new SharpSwordCoreException(exc.Message, exc);
                }

            }

            return setting;
        }

        /// <summary>
        /// 获取参数对象
        /// </summary>
        /// <typeparam name="TSetting"></typeparam>
        /// <returns></returns>
        public abstract TSetting Get<TSetting>() where TSetting : ISetting, new();
    }
}
