/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/14 14:00:20
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口配置表，此配置表为一个只读表
    /// </summary>
    internal class ActionConfigCollection : Dictionary<string, ActionConfigItem>, IActionConfigCollection
    {
        /// <summary>
        /// 接口配置配置键，{接口名称}`${版本号}
        /// </summary>
        private readonly string _itemActionKey = "{0}`${1}";

        /// <summary>
        /// 系统框架级别全局配置键
        /// </summary>
        private readonly string _sysActionKey = "";

        /// <summary>
        /// 兜底配置，即在配置，特性配置，都未找到的情况下获取此配置
        /// </summary>
        internal readonly ActionConfigItem GlobalActionConfig = new ActionConfigItem()
        {
            AllowAnonymous = false,
            EnableAjaxRequest = false,
            EnableRecordApiLog = true,
            HttpMethod = HttpMethod.GET | HttpMethod.POST,
            Obsolete = false,
            RequireHttps = false,
            CanPackageToSdk = true,
            DataSignatureTransmission = true,
            GroupName = null,
            CacheTime = 0,
            CachePrefix = null,
            CacheKeyIgnoreUserIdAndUserName = true,
            UnloadCacheKeys = null,
            RouteUrl = null
        };

        /// <summary>
        /// 忽略键大小写
        /// </summary>
        public ActionConfigCollection()
            : base(StringComparer.OrdinalIgnoreCase)
        {
        }

        /// <summary>
        /// 添加一个接口配置，此配置为全局
        /// </summary>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="value">接口配置对象</param>
        public IActionConfigCollection Register(string actionName, ActionConfigItem value)
        {
            actionName.CheckNullThrowArgumentNullException(nameof(actionName));
            value.CheckNullThrowArgumentNullException(nameof(value));

            //存在数据，直接覆盖掉（差异加增加）
            if (this.ContainsKey(actionName))
            {
                //获取原始的配置
                var rawConfig = this[actionName];

                //将新的配置覆盖到原始配置上面
                this.OverrideActionConfig(rawConfig, value);

                //删除原始配置
                base.Remove(actionName);

                //重新添加
                base.Add(actionName, rawConfig);
            }
            else
            {
                base.Add(actionName, value);
            }

            //返回当前配置表
            return (IActionConfigCollection)this;
        }

        /// <summary>
        /// 添加一个接口配置，此接口只对特定版本起作用
        /// </summary>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="version">接口版本，接口版本格式为：1.0</param>
        /// <param name="value">接口配置对象</param>
        public IActionConfigCollection Register(string actionName, string version, ActionConfigItem value)
        {
            actionName.CheckNullThrowArgumentNullException(nameof(actionName));
            version.CheckNullThrowArgumentNullException(nameof(version));
            value.CheckNullThrowArgumentNullException(nameof(value));
            return this.Register(this._itemActionKey.With(actionName, version), value);
        }

        /// <summary>
        /// 添加一个接口配置，此配置为系统框架级别全局配置（定义了此配置，将会覆盖掉系统框架配置）
        /// </summary>
        /// <param name="value">接口配置对象</param>
        public IActionConfigCollection Register(ActionConfigItem value)
        {
            value.CheckNullThrowArgumentNullException(nameof(value));
            return this.Register(this._sysActionKey, value);
        }

        /// <summary>
        /// 重写索引器，在不存在键的时候，不抛出异常，而直接返回null
        /// 此索引器返回的是一个全新的配置对象，而不是一个引用配置表里的对象；目的防止程序运行时调用者意外的修改原始配置
        /// </summary>
        /// <param name="actionName">接口名称</param>
        /// <returns>在配置表不存在的情况下会返回null</returns>
        public new ActionConfigItem this[string actionName]
        {
            get
            {
                return this.ContainsKey(actionName) ? base[actionName].MapTo<ActionConfigItem>()
                                                    : null;
            }
        }

        /// <summary>
        /// 在不存在键的时候，不抛出异常，而直接返回null
        /// 此索引器返回的是一个全新的配置对象，而不是一个引用配置表里的对象；目的防止程序运行时调用者意外的修改原始配置
        /// </summary>
        /// <param name="actionName">接口名称，大小写不敏感</param>
        /// <param name="version">接口版本</param>
        /// <returns>返回接口配置对象</returns>
        public ActionConfigItem this[string actionName, string version]
        {
            get
            {
                //框架级别自定义配置
                var sysGlobalConfigItem = this[this._sysActionKey];
                if (sysGlobalConfigItem.IsNull())
                {
                    //初始化一个完全空的配置，注意此配置不能使用全局兜底配置
                    sysGlobalConfigItem = new ActionConfigItem();
                }

                //接口不分版本号配置(重新映射一个新对象，防止修改原始配置)
                this.OverrideActionConfig(sysGlobalConfigItem, this[actionName]);

                //特定版本配置
                this.OverrideActionConfig(sysGlobalConfigItem, this[this._itemActionKey.With(actionName, version)]);

                //返回覆盖后的全局配置
                return sysGlobalConfigItem;
            }
        }

        /// <summary>
        /// 重写基础接口配置信息
        /// </summary>
        /// <param name="baseConfig">基础配置</param>
        /// <param name="overrideConfig">重写后的配置</param>
        private void OverrideActionConfig(ActionConfigItem baseConfig, ActionConfigItem overrideConfig)
        {
            //基础配置不能为null
            baseConfig.CheckNullThrowArgumentNullException(nameof(baseConfig));

            //重写配置为空，直接跳出
            if (overrideConfig.IsNull())
            {
                return;
            }

            //基础配置
            var baseConfigPropertys = baseConfig.GetType().GetPropertiesInfo();

            //重写配置
            var newConfigPropertys = overrideConfig.GetType().GetPropertiesInfo();

            //循环将当前接口版本的配置（有配置的赋值到全部版本上面）
            foreach (var newConfigProperty in newConfigPropertys)
            {
                //获取当前版本属性值
                var value = newConfigProperty.GetValue(overrideConfig);

                //未配置，直接取全局的值
                if (value.IsNull())
                {
                    continue;
                }

                //搜索对应的属性
                var baseProperty = baseConfigPropertys.FirstOrDefault(
                    o => o.Name.Equals(newConfigProperty.Name, StringComparison.OrdinalIgnoreCase));

                //找到对应的属性，赋值
                if (!baseProperty.IsNull())
                {
                    baseProperty.SetValue(baseConfig, value);
                }
            }
        }

        /// <summary>
        ///  返回所有的配置信息，此配置信息获取的是全新的（即原始配置文件的一个拷贝集合）
        /// </summary>
        /// <returns></returns>
        public IEnumerable<KeyValuePair<string, ActionConfigItem>> GetConfigs()
        {
            return this.Select(current => new KeyValuePair<string, ActionConfigItem>(current.Key,
                current.Value.MapTo<ActionConfigItem>()));
        }
    }
}
