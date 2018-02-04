/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/7/2016 4:15:40 PM
 * ****************************************************************/
using SharpSword.Configuration;
using SharpSword.Configuration.WebConfig;
using System;

namespace SharpSword.DynamicApi
{
    [Serializable, FailReturnDefault, ConfigurationSectionName("sharpsword.module.dynamicapiconfig")]
    public class DynamicApiConfig : ConfigurationSectionHandlerBase
    {
        /// <summary>
        /// 
        /// </summary>
        public DynamicApiConfig()
        {
            this.ActionNameSpace = "DynaimcApi.Actions";
            this.DynamicDirectory = "~/App_Data/DynamicApi";
            this.WorkMode = WorkMode.Dynamic | WorkMode.Develop;
        }

        /// <summary>
        /// 动态接口命名空间
        /// </summary>
        public string ActionNameSpace { get; set; }

        /// <summary>
        /// 动态API生成的程序集保存的路径
        /// </summary>
        public string DynamicDirectory { get; set; }

        /// <summary>
        /// 接口API允许模式，默认为:Dynamic
        /// </summary>
        public WorkMode WorkMode { get; set; }
    }
}
