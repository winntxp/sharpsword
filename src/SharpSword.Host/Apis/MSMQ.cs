/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 8/15/2017 12:30:01 PM
 * ****************************************************************/
using SharpSword.MQ;
using SharpSword.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace SharpSword.Host.Apis
{
    /// <summary>
    /// 
    /// </summary>
    [ActionName("MSMQ"), ResponseFormat(ResponseFormat.JSON | ResponseFormat.XML), EnableRecordApiLog(false)]
    [Description("MSMQ")]
    public class MSMQ : ActionBase<MSMQ.MSMQRequestDto, string>
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IMessagePublisher _messagePublisher;

        /// <summary>
        /// 上送参数对象
        /// </summary>
        public class MSMQRequestDto : RequestDtoBase
        {
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
        /// 
        /// </summary>
        /// <param name="messagePublisher"></param>
        public MSMQ(IMessagePublisher messagePublisher)
        {
            this._messagePublisher = messagePublisher;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<string> Execute()
        {

            string m = System.Guid.NewGuid().ToString();

            bool result = this._messagePublisher.Publish(m, "message");

            if (result)
                return this.SuccessActionResult(m);
            else
                return this.ErrorActionResult("失败");
        }

    }
}
