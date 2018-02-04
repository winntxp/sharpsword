/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/2/2017 10:07:09 AM
 * ****************************************************************/
using SharpSword.Configuration;
using SharpSword.Configuration.WebConfig;
using System;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 下单服务配置对象，全局配置对象
    /// </summary>
    [ConfigurationSectionName("o2o.orderservices.config"), Serializable, FailReturnDefault]
    public class GlobalConfig : ConfigurationSectionHandlerBase
    {
        /// <summary>
        /// 下单排队最大队列长度，默认为0，（不限制），如果>0则代表队列不能超过指定数字
        /// </summary>
        public long MaxQueueLength { get; set; } = 0;

        /// <summary>
        /// 用户可以购买的最大份数
        /// </summary>
        public int MaxUserCanBuy { get; set; } = 999;

        /// <summary>
        /// 活动商品缓存时间（单位：分钟） - 默认30分钟
        /// </summary>
        public int PresaleProductCacheTime { get; set; } = 60 * 24 * 7;

        /// <summary>
        /// 门店信息缓存时间（单位：分钟） - 默认30分钟
        /// </summary>
        public int StoreCacheTime { get; set; } = 30;

        /// <summary>
        /// 订单未支付过期时间（单位：分钟） - 默认30分钟
        /// </summary>
        public int OrderExpiredTime { get; set; } = 30;

        /// <summary>
        /// 订单支付后，自动完成时间（单位：分钟） - 默认7天
        /// </summary>
        public int OrderFinishedTime { get; set; } = 60 * 24 * 7;

        #region 以下配置一旦系统确定下来，切记勿修改，一旦修改会出现数据分布出现错误(只有在数据进行扩容的时候需要修改)

        /// <summary>
        /// 区域维度 数据拆分数据连接字符串前缀，web.config 数据库连接字符串配置格式为：AreaOrder.0 或者 AreaOrder.1
        /// </summary>
        public string AreaOrderDbSplitPrefix { get; set; } = "AREAORDER.";

        /// <summary>
        /// 订单区域维度拆分数据表个数
        /// </summary>
        public int AreaOrderTableSplitNumber { get; set; } = 8;

        /// <summary>
        /// 用户维度数据拆分数据连接字符串前缀
        /// </summary>
        public string UserOrderDbSplitPrefix { get; set; } = "USERORDER.";

        /// <summary>
        /// 用户数据拆分数据连接字符串前缀
        /// </summary>
        public string UserDbSplitPrefix { get; set; } = "USER.";

        /// <summary>
        /// 全局数据拆分数据连接字符串前缀
        /// </summary>
        public string GlobalDbSplitPrefix { get; set; } = "GLOBAL";

        /// <summary>
        /// 同步队列数目
        /// 队列数量，将相同的订单路由到同一队列，这样保证同一订单事件的连续性
        /// </summary>
        public int SyncQueuesNumber = 4;

        #endregion
    }
}
