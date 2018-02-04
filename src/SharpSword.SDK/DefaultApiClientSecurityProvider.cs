/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/21 17:01:09
 * ****************************************************************/

namespace SharpSword.SDK
{
    /// <summary>
    /// 默认加密解密什么都不做
    /// </summary>
    internal class DefaultApiClientSecurityProvider : IApiClientSecurityProvider
    {
        /// <summary>
        /// 
        /// </summary>
        private static DefaultApiClientSecurityProvider _instance = new DefaultApiClientSecurityProvider();

        /// <summary>
        /// 
        /// </summary>
        public static DefaultApiClientSecurityProvider Instance
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private DefaultApiClientSecurityProvider() { }

        /// <summary>
        /// 给上送的DATA业务参数加密
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public string RequestEncrypt(string data)
        {
            return data;
        }

        /// <summary>
        /// 对下送的整个数据进行解密
        /// </summary>
        /// <param name="actionResultString"></param>
        /// <returns></returns>
        public ResponseDecryptResult ResponseDecrypt(string actionResultString)
        {
            return new ResponseDecryptResult(true, actionResultString);
        }
    }
}
