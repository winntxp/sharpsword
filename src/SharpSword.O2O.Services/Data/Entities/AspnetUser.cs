/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/
using SharpSword.Domain.Entitys;
using System;

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class AspnetUser : Entity
    {

        ///<summary>
        /// 用户编号
        ///</summary>
        public long UserId { get; set; } // UserId (Primary key)

        ///<summary>
        /// 用户名称
        ///</summary>
        public string UserName { get; set; } // UserName (length: 256)

        ///<summary>
        /// 别名
        ///</summary>
        public string LoweredUserName { get; set; } // LoweredUserName (length: 256)

        ///<summary>
        /// 手机号码
        ///</summary>
        public string MobilePin { get; set; } // MobilePIN (length: 16)

        ///<summary>
        /// 绑定的微信号
        ///</summary>
        public string WechatNo { get; set; } // WechatNO (length: 64)

        ///<summary>
        /// 微信头像
        ///</summary>
        public string WechatImage { get; set; } // WechatImage (length: 250)

        ///<summary>
        /// 微信昵称
        ///</summary>
        public string WechatNickname { get; set; } // WechatNickname (length: 64)

        ///<summary>
        /// 微信登录状态(1、登录，0、退出)
        ///</summary>
        public int? WechatLoginState { get; set; } // WechatLoginState

        ///<summary>
        /// 最后活动日期
        ///</summary>
        public DateTime? LastActivityDate { get; set; } // LastActivityDate

        ///<summary>
        /// 密码
        ///</summary>
        public string Password { get; set; } // Password (length: 128)

        ///<summary>
        /// 密码格式
        ///</summary>
        public int PasswordFormat { get; set; } // PasswordFormat

        ///<summary>
        /// 密码盐值
        ///</summary>
        public string PasswordSalt { get; set; } // PasswordSalt (length: 128)

        ///<summary>
        /// 电子邮件
        ///</summary>
        public string Email { get; set; } // Email (length: 256)

        ///<summary>
        /// 密保问题
        ///</summary>
        public string PasswordQuestion { get; set; } // PasswordQuestion (length: 256)

        ///<summary>
        /// 密保答案
        ///</summary>
        public string PasswordAnswer { get; set; } // PasswordAnswer (length: 128)

        ///<summary>
        /// 是否禁用（1、禁用，0、正常）
        ///</summary>
        public int IsApproved { get; set; } // IsApproved

        ///<summary>
        /// 是否锁定（1、锁定，0、正常）
        ///</summary>
        public int IsLockedOut { get; set; } // IsLockedOut

        ///<summary>
        /// 是否删除（1、删除，0、正常）
        ///</summary>
        public int IsDelete { get; set; } // IsDelete

        ///<summary>
        /// 创建日期
        ///</summary>
        public System.DateTime CreateDate { get; set; } // CreateDate

        ///<summary>
        /// 最后登录日期
        ///</summary>
        public System.DateTime? LastLoginDate { get; set; } // LastLoginDate

        ///<summary>
        /// 最后修改密码日期
        ///</summary>
        public System.DateTime? LastPasswordChangedDate { get; set; } // LastPasswordChangedDate

        ///<summary>
        /// 最后锁定日期
        ///</summary>
        public System.DateTime? LastLockoutDate { get; set; } // LastLockoutDate

        ///<summary>
        /// 密码错误次数
        ///</summary>
        public int FailedPasswordAttemptCount { get; set; } // FailedPasswordAttemptCount

        ///<summary>
        /// 密码错误开始时间
        ///</summary>
        public System.DateTime? FailedPasswordAttemptWindowStart { get; set; } // FailedPasswordAttemptWindowStart

        ///<summary>
        /// 密保答案错误次数
        ///</summary>
        public int FailedPasswordAnswerAttemptCount { get; set; } // FailedPasswordAnswerAttemptCount

        ///<summary>
        /// 密保答案错误开始时间
        ///</summary>
        public System.DateTime? FailedPasswordAnswerAttemptWindowStart { get; set; } // FailedPasswordAnswerAttemptWindowStart

        ///<summary>
        /// 评论
        ///</summary>
        public string Comment { get; set; } // Comment (length: 1073741823)

        ///<summary>
        /// 性别
        ///</summary>
        public int? Gender { get; set; } // Gender

        ///<summary>
        /// 生日
        ///</summary>
        public System.DateTime? BirthDate { get; set; } // BirthDate

        ///<summary>
        /// 用户类型（1、会员，2、门店超级管理员，3、门店管理员）
        ///</summary>
        public int? UserRole { get; set; } // UserRole

        ///<summary>
        /// 开放编号
        ///</summary>
        public string OpenId { get; set; } // OpenId (length: 128)

        ///<summary>
        /// 开放类型
        ///</summary>
        public string OpenIdType { get; set; } // OpenIdType (length: 200)

        ///<summary>
        /// IP地址
        ///</summary>
        public string Ip { get; set; } // IP (length: 50)

        ///<summary>
        /// 是否绑定手机
        ///</summary>
        public int? IsBindMobile { get; set; } // IsBindMobile

        ///<summary>
        /// 是否绑定电子邮件
        ///</summary>
        public int? IsBindEmail { get; set; } // IsBindEmail

        ///<summary>
        /// 电子邮件验证码
        ///</summary>
        public string EmailVerifiCode { get; set; } // EmailVerifiCode (length: 50)

        ///<summary>
        /// 手机验证码
        ///</summary>
        public string MobileVerifiCode { get; set; } // MobileVerifiCode (length: 50)

        ///<summary>
        /// 密码强度
        ///</summary>
        public int? PasswordIntensity { get; set; } // PasswordIntensity

        ///<summary>
        /// 门店分享ID号（会员注册时从门店分享的链接带入的门店分享ID号）
        ///</summary>
        public int? ShareSupplierId { get; set; } // ShareSupplierID

        ///<summary>
        /// 创建时间
        ///</summary>
        public System.DateTime CreateTime { get; set; } // CreateTime

        ///<summary>
        /// 创建用户ID
        ///</summary>
        public int? CreateUserId { get; set; } // CreateUserID

        ///<summary>
        /// 创建用户名称
        ///</summary>
        public string CreateUserName { get; set; } // CreateUserName (length: 32)

        ///<summary>
        /// 最新修改删除时间
        ///</summary>
        public System.DateTime ModifyTime { get; set; } // ModifyTime

        ///<summary>
        /// 最后修改删除用户ID
        ///</summary>
        public int? ModifyUserId { get; set; } // ModifyUserID

        ///<summary>
        /// 最后修改删除用户名称
        ///</summary>
        public string ModifyUserName { get; set; } // ModifyUserName (length: 32)

        ///<summary>
        /// 当前所在门店编号
        ///</summary>
        public int? CurrentSupplierId { get; set; } // CurrentSupplierID

        /// <summary>
        /// 
        /// </summary>
        public AspnetUser()
        {
            WechatLoginState = 0;
            IsApproved = 0;
            IsLockedOut = 0;
            IsDelete = 0;
            PasswordIntensity = 1;
            CreateTime = DateTime.Now;
            ModifyTime = DateTime.Now;
        }
    }

}
