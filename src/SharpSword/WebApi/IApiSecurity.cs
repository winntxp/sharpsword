/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/8 18:19:35
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 上送参数解密结果
    /// </summary>
    public class RequestParamsDecryptResult
    {
        /// <summary>
        /// 解密是否成功
        /// </summary>
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// 解密成功或者失败返回的信息
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// 原始上送请求参数对象
        /// </summary>
        public RequestParams RawRequestParams { get; private set; }

        /// <summary>
        /// 解密后的上送参数对象
        /// </summary>
        public RequestParams DecryptedRequestParams { get; private set; }

        /// <summary>
        /// 构造函数里必须穿是否成功和解密后的字符串
        /// </summary>
        /// <param name="isSuccess">解密是否成功，解密失败返回false</param>
        /// <param name="message">解密是否成功消息</param>
        /// <param name="rawRequestParams">原始上送的参数对象</param>
        /// <param name="decryptedRequestParams">解密后的上送参数对象，解密后的上送参数对象，请new出一个新的上送参数对象</param>
        public RequestParamsDecryptResult(bool isSuccess, string message, RequestParams rawRequestParams, RequestParams decryptedRequestParams)
        {
            rawRequestParams.CheckNullThrowArgumentNullException(nameof(rawRequestParams));
            decryptedRequestParams.CheckNullThrowArgumentNullException(nameof(decryptedRequestParams));
            this.IsSuccess = isSuccess;
            this.Message = message;
            this.RawRequestParams = rawRequestParams;
            this.DecryptedRequestParams = decryptedRequestParams;
        }
    }

    /// <summary>
    /// 接口上送参数，下送数据加密解密接口；在实现类里请不要抛出任何异常
    /// 注意：此接口为单一注册接口，最后注册的实现会覆盖掉前面注册的实现
    /// </summary>
    public interface IApiSecurity
    {
        /// <summary>
        /// 上送的参数对象进行解密，具体根据对那部分进行解密，需要在实际项目里进行双方约定
        /// </summary>
        /// <param name="rawRequestParams">上送参数对象</param>
        /// <returns>解密后的数据对象</returns>
        RequestParamsDecryptResult RequestParamsDecrypt(RequestParams rawRequestParams);

        /// <summary>
        /// 下送数据加密方法
        /// </summary>
        /// <param name="decryptedRequestParams">
        /// 解密后的上送参数对象，
        /// 为什么要定义此解密后的参数原因：由于有可能会根据不同的AppKey生成不同的加密解密方式，
        /// 因此需要将上送的参数对象传入，用于差异化加密</param>
        /// <param name="actionResultString">下送的JSON或者XML或者View数据加密</param>
        /// <returns>加密后的数据</returns>
        string ResponseEncrypt(RequestParams decryptedRequestParams, string actionResultString);
    }
}
