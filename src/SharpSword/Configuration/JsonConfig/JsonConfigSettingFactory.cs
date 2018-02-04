/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/21/2016 9:12:37 AM
 * ****************************************************************/
using SharpSword.Serializers;
using System;
using System.Linq;

namespace SharpSword.Configuration.JsonConfig
{
    /// <summary>
    /// JSON配置参数创建工厂
    /// </summary>
    public class JsonConfigSettingFactory : SettingFactoryBase
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IJsonSerializer _jsonJosnSerializer;
        private readonly IConfigurationReader _configurationReader;

        /// <summary>
        /// JSON配置参数默认的路径
        /// </summary>
        private const string DefaultVirtualPath = "~/App_Data/Config/Json/{0}.json";

        /// <summary>
        /// 工厂支持处理的数据类型
        /// </summary>
        public override Type Supported
        {
            get { return typeof(IJsonConfiguration); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="jsonJosnSerializer">序列化接口</param>
        /// <param name="configurationReader">配置文件资源读取器</param>
        public JsonConfigSettingFactory(IJsonSerializer jsonJosnSerializer, IConfigurationReader configurationReader)
        {
            jsonJosnSerializer.CheckNullThrowArgumentNullException(nameof(jsonJosnSerializer));
            jsonJosnSerializer.CheckNullThrowArgumentNullException(nameof(configurationReader));
            this._jsonJosnSerializer = jsonJosnSerializer;
            this._configurationReader = configurationReader;
        }

        /// <summary>
        /// 我们定义此方法，方便其他存储介质来继承使用了JSON数据格式的方式来重写
        /// </summary>
        /// <typeparam name="TSetting"></typeparam>
        /// <param name="setting">配置文件内容字符串，比如：XML、JSON或者自定义的格式</param>
        /// <returns></returns>
        private TSetting Get<TSetting>(string setting) where TSetting : new()
        {
            //为空我们直接返回null
            if (setting.IsNullOrEmpty())
            {
                return default(TSetting);
            }

            try
            {
                return this._jsonJosnSerializer.Deserialize<TSetting>(setting);
            }
            catch (Exception exception)
            {
                //记录日志
                this.Logger.Error(exception);

                //错误错误，我们直接抛出异常，方便开发人员负责信息进行粘贴
                throw new SharpSwordCoreException("无效的JSON格式，正确的配置格式应该为：\r\n{0}，\r\n错误信息：{1}".With(
                    new TSetting().Serialize2Josn(), exception.Message));
            }
        }

        /// <summary>
        /// 根据定义的类型获取配置参数JSON字符串
        /// </summary>
        /// <typeparam name="TSetting"></typeparam>
        /// <returns></returns>
        protected virtual string GetSettingJsonString<TSetting>() where TSetting : new()
        {
            //参数类型
            var settingType = typeof(TSetting);

            //参数配置特性，检测是否手工配置了虚拟路径
            if (settingType.IsDefined(typeof(ConfigurationVirtualPathAttribute), false))
            {
                var configurationVirtualPathAttribute =
                    settingType.GetCustomAttributes(typeof(ConfigurationVirtualPathAttribute), false)
                        .Cast<ConfigurationVirtualPathAttribute>()
                        .First();

                //读取配置文件
                var configContentString = this._configurationReader.Read(configurationVirtualPathAttribute.VirtualPath,
                                                                         configurationVirtualPathAttribute.VirtualPathType);

                //资源存在我们直接返回，否则我们将继续后续默认文件读取
                if (!configContentString.IsNullOrEmpty())
                {
                    return configContentString;
                }
            }

            //默认使用默认的路径
            var defaultVirtualPath = DefaultVirtualPath.With(settingType.FullName);
            return this._configurationReader.Read(defaultVirtualPath, ConfigurationVirtualPathType.FILE);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSetting"></typeparam>
        /// <exception cref="SharpSwordCoreException">序列化失败抛出异常</exception>
        /// <returns></returns>
        public override TSetting Get<TSetting>()
        {
            var jsonString = this.GetSettingJsonString<TSetting>();
            return this.Get<TSetting>(jsonString.Trim());
        }
    }
}

