﻿/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 12:18:52 PM
 * ****************************************************************/
using SharpSword.WebApi;
using System.Linq;

namespace SharpSword.Host
{
    /// <summary>
    /// 接口权限校验器
    /// </summary>
    public class DefaultAuthentication : IAuthentication, ISingletonDependency
    {
        /// <summary>
        /// 
        /// </summary>
        public int Order => int.MaxValue;

        /// <summary>
        /// 
        /// </summary>
        public ILogger Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DefaultAuthentication()
        {
            this.Logger = GenericNullLogger<DefaultAuthentication>.Instance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext"></param>
        /// <returns></returns>
        public AuthenticationResult Valid(RequestContext requestContext)
        {
            //获取上送的参数信息
            var requestParams = requestContext.RawRequestParams.GetAttributes().Where(x => x.Key != "Sign");

            var signStr = "{0}{1}{0}".With("123456", string.Join("", (from item in requestParams select item.Value).ToList()));

            //进行参数签名
            var sign = MD5.Encrypt(signStr).ToUpper();

            //校验参数签名
            if (sign != requestContext.RawRequestParams.Sign)
            {
                return AuthenticationResult.Fail("数据签名错误");
            }

            //校验通过
            return AuthenticationResult.Success;
        }
    }
}