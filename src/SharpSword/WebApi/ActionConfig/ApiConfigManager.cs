/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/14 14:02:06
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 系统接口配置表，初次加载的时候进行初始化
    /// </summary>
    public class ApiConfigManager
    {
        /// <summary>
        /// 用于保存接口配置
        /// </summary>
        private static readonly ActionConfigCollection Instance = new ActionConfigCollection();

        /// <summary>
        /// 返回接口配置表
        /// </summary>
        public static IActionConfigCollection Configs
        {
            get
            {
                return Instance;
            }
        }

        /// <summary>
        /// 全局接口兜底配置
        /// </summary>
        public static ActionConfigItem GlobalActionConfig
        {
            get { return Instance.GlobalActionConfig; }
        }
    }
}
