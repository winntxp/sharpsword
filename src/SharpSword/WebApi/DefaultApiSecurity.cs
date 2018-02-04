/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/8 18:27:43
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 系统框架默认的安全接口，此接口什么都没有做，直接原路返回，即上送参数解密，下送参数解密；
    /// 具体实现类可以继承此类，对上送参数解密，下送数据加密，已经数据签名进行校验等
    /// </summary>
    public class DefaultApiSecurity : IApiSecurity
    {
        /// <summary>
        /// 获取待签名的键值对，为空或者为null的属性不会添加到字典表，已经安装key键进行排序(a-z)
        /// </summary>
        /// <param name="rawRequestParams">请求的原始上送参数对象</param>
        /// <returns></returns>
        protected virtual string GetSignRequestParamsString(RequestParams rawRequestParams)
        {
            //根据上送参数进行排序，排除掉Sign参数
            var keyvalue = rawRequestParams.GetAttributes(false, false)
                                            .Where(o => !o.Key.Equals("Sign", StringComparison.OrdinalIgnoreCase))
                                            .Select(o => new KeyValuePair<string, string>(o.Key, o.Value.ToString()))
                                            .ToList();

            //返回待签名的上送数据
            return string.Join("", keyvalue.Select(o => o.Value).ToArray());
        }

        /// <summary>
        /// 直接返回上送data数据
        /// </summary>
        /// <param name="rawRequestParams">上送参数对象</param>
        /// <returns>返回解密对象结果</returns>
        public virtual RequestParamsDecryptResult RequestParamsDecrypt(RequestParams rawRequestParams)
        {
            //创建一个新的解密上送参数对象(防止直接应用，修改的时候出现问题)
            var decryptedRequestParams = rawRequestParams.MapTo<RequestParams>();

            //返回默认解密结果
            return new RequestParamsDecryptResult(true, "OK", rawRequestParams, decryptedRequestParams);
        }

        /// <summary>
        /// 直接返回下送数据
        /// </summary>
        /// <param name="actionResultString">actionResult对象格式化字符串</param>
        /// <param name="decryptedRequestParams">解密后的上送参数对象</param>
        /// <returns></returns>
        public virtual string ResponseEncrypt(RequestParams decryptedRequestParams, string actionResultString)
        {
            //如果每个客户端都是不同的加密方式，可以根据decryptedRequestParams参数里的appKey来获取特定客户端加密约定，
            //具体怎么获取可以从数据库，从配置文件，从其他存储方式等等
            return actionResultString;
        }
    }
}
