/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/

using SharpSword.Domain.Entitys;

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class StoreProfile : Entity
    {
        ///<summary>
        /// 门店主键编号
        ///</summary>
        public long SupplierId { get; set; }

        ///<summary>
        /// 门店编号（加盟编号）
        ///</summary>
        public string SupplierNo { get; set; }

        ///<summary>
        /// 区域ID
        ///</summary>
        public int? OperationAreaId { get; set; }

        ///<summary>
        /// 线路ID
        ///</summary>
        public int? LineId { get; set; }

        ///<summary>
        /// 店铺名称
        ///</summary>
        public string StoreName { get; set; }

        ///<summary>
        /// 店铺LOG
        ///</summary>
        public string LogoUrl { get; set; }

        ///<summary>
        /// 店铺简介
        ///</summary>
        public string StoreDescription { get; set; }

        ///<summary>
        /// 经营类型
        ///</summary>
        public int? BusinessType { get; set; }

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
        /// 地区编号
        ///</summary>
        public int RegionId { get; set; }

        ///<summary>
        /// 详细地址
        ///</summary>
        public string DetailAddress { get; set; }

        ///<summary>
        /// 经度
        ///</summary>
        public double? MapX { get; set; }

        ///<summary>
        /// 纬度
        ///</summary>
        public double? MapY { get; set; }

        ///<summary>
        /// 邮编
        ///</summary>
        public string Zipcode { get; set; }

        ///<summary>
        /// 店铺介绍
        ///</summary>
        public string Introduction { get; set; }

        ///<summary>
        /// 线路顺序
        ///</summary>
        public int? LineSort { get; set; }

        ///<summary>
        /// 联系人
        ///</summary>
        public string Contacts { get; set; }

        ///<summary>
        /// 门店开发人员
        ///</summary>
        public string SupplierDeveloper { get; set; }

        ///<summary>
        /// 门店状态
        ///</summary>
        public int? SupplierState { get; set; }

        ///<summary>
        /// 删除状态（1、已删除，0、正常）
        ///</summary>
        public int? IsDeleted { get; set; }

        ///<summary>
        /// 联系电话
        ///</summary>
        public string ContactsTel { get; set; }

        ///<summary>
        /// 创建时间
        ///</summary>
        public System.DateTime? CreateTime { get; set; }

        ///<summary>
        /// 创建用户ID
        ///</summary>
        public int? CreateUserId { get; set; }

        ///<summary>
        /// 创建用户名称
        ///</summary>
        public string CreateUserName { get; set; }

        ///<summary>
        /// 最后修改时间
        ///</summary>
        public System.DateTime? ModifyTime { get; set; }

        ///<summary>
        /// 最后修改用户ID
        ///</summary>
        public int? ModifyUserId { get; set; }

        ///<summary>
        /// 最后修改用户名称
        ///</summary>
        public string ModifyUserName { get; set; }

        ///<summary>
        /// 门店微信群名称
        ///</summary>
        public string WeChatGroupName { get; set; }

        ///<summary>
        /// 营业面积(V2.2+)
        ///</summary>
        public string ShopArea { get; set; }

        ///<summary>
        /// 营业执照(V2.2+)
        ///</summary>
        public string BusiLicenseFullName { get; set; }

        ///<summary>
        /// 食品流通许可证(V2.2+)
        ///</summary>
        public string FoodCirculationLicense { get; set; }

        ///<summary>
        /// 营业执照图片地址(V2.2+)
        ///</summary>
        public string BusiLicenseFullNameImgSrc { get; set; }

        ///<summary>
        /// 食品流通许可证图片地址(V2.2+)
        ///</summary>
        public string FoodCirculationLicenseImgSrc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public StoreProfile()
        {
            OperationAreaId = 1;
            SupplierState = 0;
            IsDeleted = 0;
        }
    }

}
