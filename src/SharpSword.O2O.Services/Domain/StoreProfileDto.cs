/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 11:31:23 AM
 * ****************************************************************/

namespace SharpSword.O2O.Services.Domain
{
    /// <summary>
    /// 门店信息
    /// </summary>
    public class StoreProfileDto
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        /// 门店编号
        /// </summary>
        public string StoreNo { get; set; }

        /// <summary>
        /// 门店名称
        /// </summary>
        public string StoreName { get; set; }

        /// <summary>
        /// 门店状态（1、冻结，0、正常）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 是否被删除
        /// </summary>
        public int IsDeleted { get; set; }

        /// <summary>
        /// 区域全称
        /// </summary>
        public string AreaFullName { get; set; }

        /// <summary>
        /// 详细地址
        /// </summary>
        public string DetailAddress { get; set; }

        /// <summary>
        /// 线路ID
        /// </summary>
        public int LineID { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        public string LineName { get; set; }

        /// <summary>
        /// 配送顺序
        /// </summary>
        public int LineSort { get; set; }

        /// <summary>
        /// 区域ID
        /// </summary>
        public int AreaID { get; set; }

        /// <summary>
        /// 配送员ID
        /// </summary>
        public int DistributionClerkID { get; set; }

        /// <summary>
        /// 配送员姓名
        /// </summary>
        public string DistributionClerkName { get; set; }
    }
}
