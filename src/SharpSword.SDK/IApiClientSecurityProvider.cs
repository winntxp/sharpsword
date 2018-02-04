/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/8 18:19:35
 * ****************************************************************/

namespace SharpSword.SDK
{
    /// <summary>
    /// 下送数据解密结果
    /// </summary>
    public class ResponseDecryptResult
    {
        /// <summary>
        /// 解密是否成功
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// 解密结果
        /// </summary>
        public string Data { get; private set; }

        /// <summary>
        /// 构造函数里必须穿是否成功和解密后的字符串
        /// </summary>
        /// <param name="isSuccess">解密是否成功，解密失败返回false</param>
        /// <param name="decryptedData">解密结果</param>
        public ResponseDecryptResult(bool isSuccess, string decryptedData)
        {
            this.IsSuccess = isSuccess;
            this.Data = decryptedData;
        }
    }

    /// <summary>
    /// 客户端API接口加密解密接口
    /// </summary>
    public interface IApiClientSecurityProvider
    {
        /// <summary>
        /// 上送的Data数据加密方法；
        /// </summary>
        /// <param name="data">上送data参数</param>
        /// <returns>加密后的数据</returns>
        string RequestEncrypt(string data);

        /// <summary>
        /// 下送数据解密方法
        /// </summary>
        /// <param name="actionResultString">下送的JSON或者XML或者View数据</param>
        /// <returns>解密后的数据</returns>
        ResponseDecryptResult ResponseDecrypt(string actionResultString);
    }
}
