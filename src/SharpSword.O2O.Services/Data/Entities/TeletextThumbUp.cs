/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/

namespace SharpSword.Host.Data.Entities
{

    // TeletextThumbUp
    public class TeletextThumbUp
    {

        ///<summary>
        /// 主键ID
        ///</summary>
        public long ThumbUpId { get; set; } // ThumbUpID (Primary key)

        ///<summary>
        /// 图文直播ID
        ///</summary>
        public long? TeletextId { get; set; } // TeletextID

        ///<summary>
        /// 用户ID
        ///</summary>
        public long? UserId { get; set; } // UserID

        ///<summary>
        /// 微信OpenID
        ///</summary>
        public string OpenId { get; set; } // OpenID (length: 128)

        ///<summary>
        /// 微信昵称
        ///</summary>
        public string NickName { get; set; } // NickName (length: 64)

        ///<summary>
        /// 微信头像
        ///</summary>
        public string HandImage { get; set; } // HandImage (length: 250)

        ///<summary>
        /// 点赞时间
        ///</summary>
        public System.DateTime? ThunbUpTime { get; set; } // ThunbUpTime

        // Foreign keys

        /// <summary>
        /// Parent Teletext pointed by [TeletextThumbUp].([TeletextId]) (FK_TELETEXTTHUMBUP_REFERENCE_TELETEXT)
        /// </summary>
        public virtual Teletext Teletext { get; set; } // FK_TELETEXTTHUMBUP_REFERENCE_TELETEXT

        public TeletextThumbUp()
        {
            ThunbUpTime = System.DateTime.Now;
        }
    }

}
