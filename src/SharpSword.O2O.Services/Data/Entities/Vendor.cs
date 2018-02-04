/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 08/23/2017 15:31:02
 * ****************************************************************/
using SharpSword.Domain.Entitys;

namespace SharpSword.O2O.Data.Entities
{
    /// <summary>
    /// 
    /// </summary>
    public class Vendor : Entity
    {
        ///<summary>
        /// 供应商ID
        ///</summary>
        public int VendorId { get; set; }

        ///<summary>
        /// 供应商类型ID
        ///</summary>
        public int? VendorTypeId { get; set; }

        ///<summary>
        /// 供应商名称
        ///</summary>
        public string VendorName { get; set; }

        ///<summary>
        /// 省
        ///</summary>
        public int ProvinceCode { get; set; }

        ///<summary>
        /// 市
        ///</summary>
        public int CityCode { get; set; }

        ///<summary>
        /// 区
        ///</summary>
        public int CountyCode { get; set; }

        ///<summary>
        /// 详细地址
        ///</summary>
        public string Address { get; set; }

        ///<summary>
        /// 是否删除（0、未删除；1、已删除）
        ///</summary>
        public int IsDeleted { get; set; }

        ///<summary>
        /// 联络人
        ///</summary>
        public string LinkMan { get; set; }

        ///<summary>
        /// 电话
        ///</summary>
        public string Telephone { get; set; }

        ///<summary>
        /// 区域ID
        ///</summary>
        public int? RegionId { get; set; }

        ///<summary>
        /// 供应商状态(1、冻结，0、正常)
        ///</summary>
        public int? VendorState { get; set; }

        ///<summary>
        /// 供应Logo
        ///</summary>
        public string VendorLogo { get; set; }

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
        /// 最后修改时间
        ///</summary>
        public System.DateTime ModifyTime { get; set; }

        ///<summary>
        /// 最后修改用户ID
        ///</summary>
        public int? ModifyUserId { get; set; }

        ///<summary>
        /// 最后修改用户名称
        ///</summary>
        public string ModifyUserName { get; set; }

        ///<summary>
        /// 供应商编码(V2.2+)
        ///</summary>
        public string VendorCode { get; set; }

        ///<summary>
        /// 法定代表人(V2.2+)
        ///</summary>
        public string LegalPeople { get; set; }

        ///<summary>
        /// 营业面积(V2.2+)
        ///</summary>
        public string VendorArea { get; set; }

        ///<summary>
        /// 营业执照(V2.2+)
        ///</summary>
        public string BusiLicenseFullName { get; set; }

        ///<summary>
        /// 食品流通许可证(V2.2+)
        ///</summary>
        public string FoodCirculationLicense { get; set; }

        ///<summary>
        /// 账户户名(V2.2+)
        ///</summary>
        public string BankAccountName { get; set; }

        ///<summary>
        /// 开户行(V2.2+)
        ///</summary>
        public string BankType { get; set; }

        ///<summary>
        /// 银行帐号(V2.2+)
        ///</summary>
        public string BankAccount { get; set; }

        ///<summary>
        /// 营业执照图片(V2.2+)
        ///</summary>
        public string BusiLicenseFullNameImgSrc { get; set; }

        ///<summary>
        /// 食品流通许可证图片(V2.2+)
        ///</summary>
        public string FoodCirculationLicenseImgSrc { get; set; }

        ///<summary>
        /// 供应商简称(V2.3+)
        ///</summary>
        public string VendorShortName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Vendor()
        {
            IsDeleted = 0;
            VendorState = 0;
        }
    }

}
