/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 7/27/2017 11:13:37 AM
 * ****************************************************************/
using System.Collections.Generic;
using SharpSword.WebApi;
using System;

namespace SharpSword.Host.Apis
{
    /// <summary>
    /// 接收客户端回送的店铺信息
    /// </summary>
    [ActionName("MALL.Shop.FeedBack"), ResponseFormat(ResponseFormat.JSON | ResponseFormat.XML)]
    public class MallShopFeedBack : ActionBase<MallShopFeedBack.MallShopFeedBackRequestDto, MallShopFeedBack.MallShopFeedBackResponseDto>
    {
        /// <summary>
        /// 
        /// </summary>
        private static IList<string> _shops = new List<string>();
        private readonly string CurrentVersion = "2.7.6.815";

        /// <summary>
        /// 上送参数对象
        /// </summary>
        public class MallShopFeedBackRequestDto : RequestDtoBase
        {
            /// <summary>
            /// 店铺名称
            /// </summary>
            public string ShopName { get; set; }

            /// <summary>
            /// 版本
            /// </summary>
            public string Version { get; set; }

            /// <summary>
            /// 自定义校验上送参数
            /// </summary>
            /// <returns></returns>
            public override IEnumerable<DtoValidatorResultError> Valid()
            {
                return base.Valid();
            }
        }

        /// <summary>
        /// 下送数据对象
        /// </summary>
        public class MallShopFeedBackResponseDto : ResponseDtoBase
        {
            /// <summary>
            /// 
            /// </summary>
            public bool IsPayed { get; set; }

            /// <summary>
            /// 是否有新版本
            /// </summary>
            public bool HasNewVersion { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        static MallShopFeedBack()
        {
            _shops.Add("帝豪家纺旗舰店（官方直营）");
            _shops.Add("五台山杂粮直营店");
            _shops.Add("西凉花蜂蜜直营店");
            _shops.Add("石溪葡萄旗舰店");
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<MallShopFeedBackResponseDto> Execute()
        {
            //记录下日志
            this.Logger.Warning("店铺名称：{0}，版本：{1}，请求IP地址：{2}"
                       .With(this.RequestDto.ShopName,
                             this.RequestDto.Version,
                             this.RequestContext.HttpContext.Request.GetClientIp()));

            var hasNewVersion = false;
            if (this.RequestDto.Version.IsNullOrEmpty())
            {
                hasNewVersion = true;
            }
            else
            {
                var clientVersion = Version.Parse(this.RequestDto.Version);
                var currentVersion = Version.Parse(this.CurrentVersion);
                hasNewVersion = currentVersion > clientVersion;
            }

            return this.SuccessActionResult(new MallShopFeedBackResponseDto()
            {
                IsPayed = _shops.Contains(this.RequestDto.ShopName),
                HasNewVersion = hasNewVersion
            });
        }

    }
}
