/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/11 12:30:46
 * ****************************************************************/
using System;
using System.Text;

namespace SharpSword.OAuth
{
    /// <summary>
    /// 表示授权成功后平台返回的数据。
    /// </summary>
    [Serializable]
    public class AuthorizationResult
    {
        /// <summary>
        /// 
        /// </summary>
        public AuthorizationResult()
        {
            this.TokenType = "Bearer";
        }

        /// <summary>
        /// 重写下tostring方法，显示跟家详细信息
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("访问令牌:{0}，", this.Token);
            stringBuilder.AppendFormat("token的类型:{0}，", this.TokenType);
            stringBuilder.AppendFormat("token到期时间:{0}，", this.ExpireAt);
            stringBuilder.AppendFormat("刷新令牌:{0}，", this.RefreshToken);
            stringBuilder.AppendFormat("刷新令牌的到期时间:{0}，", this.RefreshExpireAt);
            stringBuilder.AppendFormat("用户名:{0}，", this.UserName);
            stringBuilder.AppendFormat("唯一标识:{0}，", this.OpenId);
            stringBuilder.AppendFormat("平台头像:{0}", this.HeadImg);
            return stringBuilder.ToString();
        }

        /// <summary>
        /// 获取Token值。
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 获取token的类型
        /// </summary>
        public string TokenType { get; set; }

        /// <summary>
        /// 获取Token失效时间。
        /// </summary>
        public DateTime ExpireAt { get; set; }

        /// <summary>
        /// 获取刷新令牌。
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// 获取刷新令牌的到期时间。
        /// </summary>
        public DateTime RefreshExpireAt { get; set; }

        /// <summary>
        /// 获取用户在Etp的用户名。
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 头像图片地址
        /// </summary>
        public string HeadImg { get; set; }

        /// <summary>
        /// 获取用户在平台的唯一标识。
        /// </summary>
        public string OpenId { get; set; }
    }

}