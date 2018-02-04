/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class OperationArea
    {
        ///<summary>
        /// 区域ID
        ///</summary>
        public int OperationAreaId { get; set; }

        ///<summary>
        /// 区域名称
        ///</summary>
        public string OperationAreaName { get; set; }

        ///<summary>
        /// 省
        ///</summary>
        public int? ProvinceId { get; set; }

        ///<summary>
        /// 市
        ///</summary>
        public int? CityId { get; set; }

        ///<summary>
        /// 县
        ///</summary>
        public int? CountyId { get; set; }

        ///<summary>
        /// 经度
        ///</summary>
        public double? MapX { get; set; }

        ///<summary>
        /// 纬度
        ///</summary>
        public double? MapY { get; set; }

        ///<summary>
        /// 删除状态（1、删除，0、正常）
        ///</summary>
        public int? IsDelete { get; set; }

        ///<summary>
        /// 创建时间
        ///</summary>
        public System.DateTime CreateTime { get; set; }

        ///<summary>
        /// 创建用户ID
        ///</summary>
        public int? CreateUserId { get; set; }

        ///<summary>
        /// 创建用户名称
        ///</summary>
        public string CreateUserName { get; set; }

        ///<summary>
        /// 最新修改删除时间
        ///</summary>
        public System.DateTime ModifyTime { get; set; }

        ///<summary>
        /// 最后修改删除用户ID
        ///</summary>
        public int? ModifyUserId { get; set; }

        ///<summary>
        /// 最后修改删除用户名称
        ///</summary>
        public string ModifyUserName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public OperationArea()
        {
            IsDelete = 0;
            CreateTime = System.DateTime.Now;
            ModifyTime = System.DateTime.Now;
        }
    }

}
