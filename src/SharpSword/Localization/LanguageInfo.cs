/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 12/20/2016 10:32:46 AM
 * ****************************************************************/

namespace SharpSword.Localization
{
    /// <summary>
    /// 
    /// </summary>
    public class LanguageInfo
    {
        /// <summary>
        /// 如: "en-US" 英语，"zh-CN" 简体中文
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 语言图片地址
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 是否是默认语言
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="displayName"></param>
        /// <param name="icon"></param>
        /// <param name="isDefault"></param>
        public LanguageInfo(string name, string displayName, string icon = null, bool isDefault = false)
        {
            this.Name = name;
            this.DisplayName = displayName;
            this.Icon = icon;
            this.IsDefault = isDefault;
        }
    }
}
