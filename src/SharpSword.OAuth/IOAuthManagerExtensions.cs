/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/

namespace SharpSword.OAuth
{
    public static class IOAuthManagerExtensions
    {
        /// <summary>
        /// 随机获取指定平台下面配置的某个APP信息
        /// </summary>
        /// <param name="oauthManager"></param>
        /// <param name="platformName"></param>
        /// <returns></returns>
        public static App GetApp(this IOAuthManager oauthManager, string platformName)
        {
            foreach (var item in oauthManager.Apps)
            {
                if (item.Platform.Name == platformName)
                {
                    return item;
                }
            }

            return null;
        }
    }
}
