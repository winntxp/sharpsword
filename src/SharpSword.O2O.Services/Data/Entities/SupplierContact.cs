/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class SupplierContact
    {

        /// <summary>
        /// 
        /// </summary>
        public int SupplierContactsId { get; set; } // SupplierContactsID (Primary key)

        /// <summary>
        /// 
        /// </summary>
        public string ContactsName { get; set; } // ContactsName (length: 32)

        /// <summary>
        /// 
        /// </summary>
        public string ContactsTel { get; set; } // ContactsTel (length: 32)

        /// <summary>
        /// 
        /// </summary>
        public long? SupplierId { get; set; } // SupplierID

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

        /// <summary>
        /// 
        /// </summary>
        public int? OrderNumber { get; set; } // OrderNumber

        /// <summary>
        /// 
        /// </summary>
        public SupplierContact()
        {
            CreateTime = System.DateTime.Now;
            ModifyTime = System.DateTime.Now;
            OrderNumber = 0;
        }
    }

}
