/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/29/2017 5:16:33 PM
 * ****************************************************************/
using System.ComponentModel;

namespace SharpSword.O2O.Services
{
    /// <summary>
    /// 订单处理状态
    /// </summary>
    public enum OrderProgressStatus
    {
        /// <summary>
        /// 排队中(系统还未曾消费过排队信息)
        /// </summary>
        [Description("排队中")]
        Queuing = -1,

        /// <summary>
        /// 处理订单成功（系统已经将订单存储到存储介质）
        /// </summary>
        [Description("处理订单成功")]
        OrderCreateSuccess = 1,

        /// <summary>
        /// 处理订单失败(系统在处理订单的时候出现失败)
        /// </summary>
        [Description("处理订单失败")]
        OrderCreateFail = 0,

        /// <summary>
        /// 未知状态（此状态99%不会发生，但是在系统出现异常，数据丢失，比如排队信息丢失，或者处理反馈结果丢失）
        /// </summary>
        [Description("未知状态")]
        Unkonw = 1000
    }
}