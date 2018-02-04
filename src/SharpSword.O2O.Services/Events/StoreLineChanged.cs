/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 9/8/2017 3:27:07 PM
 * ****************************************************************/
using System;

namespace SharpSword.O2O.Services.Events
{
    /// <summary>
    /// 门店修改配送线路
    /// </summary>
    [Serializable]
    public class StoreLineChanged : IEvent
    {
        /// <summary>
        /// 门店编号
        /// </summary>
        public long StoreId { get; set; }

        /// <summary>
        /// 线路编号
        /// </summary>
        public int LineId { get; set; }

        /// <summary>
        /// 线路名称
        /// </summary>
        public string LineName { get; set; }

        /// <summary>
        /// 线路顺序 
        /// </summary>
        public int LineSort { get; set; }
    }
}
