/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/25 11:52:01
 * ****************************************************************/
using SharpSword;
using SharpSword.WebApi;
using System.Linq;

namespace ServiceCenter.Product.Api.Authentication
{
    /// <summary>
    /// 将APPKEY保持在了数据库里进行校验；只要实现IsValid方法就可以了
    /// </summary>
    public class DbAuthentication : DefaultAuthentication
    {
        /// <summary>
        /// 数据签名
        /// </summary>
        /// <param name="signParams">待签名的数据</param>
        /// <returns></returns>
        private static string Sign(SignParamsDictionary<string, string> signParams)
        {
            //只进行值的签名
            string joinStr = string.Join("", (from item in signParams.Values select item.Value).ToArray());
            return MD5.Encrypt(joinStr).ToUpper();
        }

        /// <summary>
        /// 根据提交上来的请求上下文，校验上送数据包签名是否合法，防止传输途中数据包被篡改
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <returns></returns>
        private static bool CheckSign(RequestContext requestContext)
        {
            //接口名称未提交
            if (requestContext.RawRequestParams.ActionName.IsNullOrEmpty())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// 校验APPKEY准确性
        /// </summary>
        /// <param name="requestContext">提交的上下文信息</param>
        /// <returns>通过就返回true，失败返回false</returns>
        public override AuthenticationResult Valid(RequestContext requestContext)
        {

            //验证时间戳的合法性

            //校验appkey合法性

            //校验数据签名是否正确

            //校验appkey是否具有接口的调用权限等

            return AuthenticationResult.Success;
        }
    }
}
