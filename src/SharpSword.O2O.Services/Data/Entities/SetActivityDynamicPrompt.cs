/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/

namespace SharpSword.Host.Data.Entities
{

    // SetActivityDynamicPrompt
    public class SetActivityDynamicPrompt
    {

        ///<summary>
        /// 自增编号
        ///</summary>
        public int DynamicPromptId { get; set; } // DynamicPromptID (Primary key)

        ///<summary>
        /// 名称
        ///</summary>
        public string DynamicPromptName { get; set; } // DynamicPromptName (length: 128)

        ///<summary>
        /// 提示内容
        ///</summary>
        public string Content { get; set; } // Content (length: 512)

        ///<summary>
        /// 展示开始时间(yyyy-MM-dd HH:mm:ss)
        ///</summary>
        public System.DateTime? DisplayStartTime { get; set; } // DisplayStartTime

        ///<summary>
        /// 展示截止时间(yyyy-MM-dd HH:mm:ss)
        ///</summary>
        public System.DateTime? DisplayEndTime { get; set; } // DisplayEndTime

        ///<summary>
        /// 温馨提示
        ///</summary>
        public string Reminder { get; set; } // Reminder (length: 512)

        ///<summary>
        /// 启用状态（0:关闭;1:启动）
        ///</summary>
        public int? Status { get; set; } // Status

        ///<summary>
        /// 删除状态（0:正常;1:删除）
        ///</summary>
        public int? IsDeleted { get; set; } // IsDeleted

        ///<summary>
        /// 创建时间
        ///</summary>
        public System.DateTime? CreateTime { get; set; } // CreateTime

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
        public System.DateTime? ModifyTime { get; set; } // ModifyTime

        ///<summary>
        /// 最后修改删除用户ID
        ///</summary>
        public int? ModifyUserId { get; set; } // ModifyUserID

        ///<summary>
        /// 最后修改删除用户名称
        ///</summary>
        public string ModifyUserName { get; set; } // ModifyUserName (length: 32)

        public SetActivityDynamicPrompt()
        {
            IsDeleted = 0;
        }
    }

}
