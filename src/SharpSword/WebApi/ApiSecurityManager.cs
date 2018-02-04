/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/18 18:29:37
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 接口上送，下送参数加解密管理器
    /// </summary>
    internal class ApiSecurityManager
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IApiSecurity _apiSecurity;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="apiSecurity">加解密接口</param>
        public ApiSecurityManager(IApiSecurity apiSecurity)
        {
            this._apiSecurity = apiSecurity;
        }

        /// <summary>
        /// 如果接口设置为不走解密流程，即配置了DisableDataSignatureTransmissionAttribute特性或者将接口描述
        /// 对象属性DataSignatureTransmission=false情况下，将直接返回原始的上送请求参数(即返回解密的参数对象与原始上送的参数对象完全一致)；
        /// 默认情况下将走IApiSecurity接口处理流程
        /// </summary>
        /// <param name="rawRequestParams">原始的上送参数对象</param>
        /// <param name="actionDescriptor">接口描述对象</param>
        /// <returns></returns>
        public virtual RequestParamsDecryptResult RequestParamsDecrypt(RequestParams rawRequestParams, IActionDescriptor actionDescriptor)
        {
            return actionDescriptor.DataSignatureTransmission
                ? this._apiSecurity.RequestParamsDecrypt(rawRequestParams)
                : new RequestParamsDecryptResult(true, "OK", rawRequestParams, ObjectMapManager.Provider.MapTo<RequestParams>(rawRequestParams));
        }

        /// <summary>
        /// 接口如果设置不走加密流程，下送数据将直接返回
        /// </summary>
        /// <param name="actionResultString">actionResult对象格式化字符串</param>
        /// <param name="decryptedRequestParams">解密后的上送参数对象</param>
        /// <param name="actionDescriptor">接口描述对象</param>
        /// <returns></returns>
        public virtual string ResponseEncrypt(RequestParams decryptedRequestParams, IActionDescriptor actionDescriptor, string actionResultString)
        {
            return actionDescriptor.DataSignatureTransmission
                ? this._apiSecurity.ResponseEncrypt(decryptedRequestParams, actionResultString)
                : actionResultString;
        }
    }
}
