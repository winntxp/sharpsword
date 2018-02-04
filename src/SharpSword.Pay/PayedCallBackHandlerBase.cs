/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/27/2016 2:29:27 PM
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.Web;

namespace SharpSword.Pay
{
    /// <summary>
    /// 支付成功后本地处理业务逻辑抽象类(我们抽象出3个模板方法，对接新的第三方支付的时候，我们只要重写这3个方法即可)
    /// </summary>
    /// <typeparam name="TConfig">实现IPayConfig的支付配置类</typeparam>
    public abstract class PayedCallBackHandlerBase<TConfig> : IPayedCallBackHandler where TConfig : IPayConfig
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="config">实现IPayConfig的支付配置类</param>
        public PayedCallBackHandlerBase(TConfig config)
        {
            config.CheckNullThrowArgumentNullException(nameof(config));
            this.PayConfig = config;
        }

        /// <summary>
        /// 支付配置文件
        /// </summary>
        protected TConfig PayConfig { get; private set; }

        /// <summary>
        /// 支付完成(这里主要进行具体的业务操作，比如修改本地单据为已经支付状态)
        /// </summary>
        public Action<PayCallBackContext, Trade> PayStart { get; set; }

        /// <summary>
        /// 支付失败（在支付或者更新的时候有任何错误，此方法都会被触发）
        /// </summary>
        public Action<PayCallBackContext, Exception> PayError { get; set; }

        /// <summary>
        /// 返回给第三方支付平台本地是否处理成功消息
        /// </summary>
        public Action<PayCallBackContext, string> PayFeedBack { get; set; }

        /// <summary>
        /// 处理支付回调
        /// </summary>
        /// <param name="httpContext">http请求上下文</param>
        void IPayedCallBackHandler.Execute(HttpContextBase httpContext)
        {
            httpContext.CheckNullThrowArgumentNullException(nameof(httpContext));

            //获取从第三方传递给本地的参数
            var arguments = this.GetPostArguments(httpContext);

            //构造上下文
            var context = new PayCallBackContext(httpContext, arguments);

            try
            {
                //校验第三方平台递交过来的信息是否合法
                var result = this.VerifyData(context);
                if (result.IsNull())
                {
                    throw new Exception("数据校验不能返回null");
                }

                //校验失败，我们直接抛出异常
                if (result.Status == VerifyDataResultStatus.FAIL)
                {
                    throw new Exception(result.Message, result.Exception);
                }

                //获取交易信息
                var trade = this.GetTrade(context);
                if (trade.IsNull())
                {
                    throw new Exception("获取交易信息失败");
                }

                //未定义事件处理
                if (this.PayStart.IsNull())
                {
                    throw new Exception("未定义IPayedCallBackHandler.PayStart事件处理委托");
                }

                //校验成功，我们将具体业务实现委托给外部
                this.PayStart(context, trade);

                //返回消息给第三方支付平台，我们交给外部委托去实现输出
                this.ExecutePayFeedBack(context, this.GetSuccessFeedBackMessage(context, trade));

            }
            catch (Exception exc)
            {
                //当我们外部未指定错误处理委托的时候，我们直接抛出异常
                if (!this.PayError.IsNull())
                {
                    this.PayError(context, exc);
                }

                //返回消息给第三方支付平台
                this.ExecutePayFeedBack(context, exc.Message);
            }
        }

        /// <summary>
        /// 获取第三方支付平台POST过来的参数信息（一般支付平台异步Notify传输过来的数据）
        /// </summary>
        /// <param name="httpContext">当前请求上下文</param>
        /// <returns>传输的参数字典，key:参数名称，value:参数值</returns>
        private Dictionary<string, string> GetPostArguments(HttpContextBase httpContext)
        {
            var arguments = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (string key in httpContext.Request.Form.Keys)
            {
                arguments.Add(key, httpContext.Request.Form[key].ToString());
            }
            foreach (string key in httpContext.Request.QueryString.Keys)
            {
                arguments.Add(key, httpContext.Request.QueryString[key].ToString());
            }
            return arguments;
        }

        /// <summary>
        /// 执行支付处理反馈给第三方平台
        /// </summary>
        /// <param name="context">支付上下文</param>
        /// <param name="message">本地处理支付处理后，将成功或者失败的消息反馈给支付平台</param>
        private void ExecutePayFeedBack(PayCallBackContext context, string message)
        {
            if (!this.PayFeedBack.IsNull())
            {
                this.PayFeedBack(context, message);
            }
        }

        /// <summary>
        /// 开始支付（具体实现类里主要检测第三方平台传来的参数验证等等，参数校验失败等，请直接抛出异常）
        /// 为什么需要检测POST过来参数，因为我们无法保证POST过来的数据就是第三方支付平台传输过来的。因此
        /// 我们需要在具体的实现类里来进行参数的合法性校验，比如，检测数据签名是否正确，请求对应的支付平台
        /// 查询此次POST是否是从支付平台传输的
        /// </summary>
        /// <param name="context">支付上下文</param>
        /// <returns>支付正确性校验结果对象</returns>
        protected abstract VerifyDataResult VerifyData(PayCallBackContext context);

        /// <summary>
        /// 根据第三方支付平台返回的数据进行解析，解析出通用的交易信息
        /// 我们需要从支付平台POST过来的参数获取一些交易信息，这样才能方便我们抽象出交易，供我们外部调用使用
        /// 此具体实现里，可以不处理异常，直接让其抛出即可，系统框架会自动捕捉并处理
        /// </summary>
        /// <param name="context">支付上下文</param>
        /// <returns>获取第三方支付平台反馈回来的交易对象s</returns>
        protected abstract Trade GetTrade(PayCallBackContext context);

        /// <summary>
        /// 获取反馈给第三方支付平台，本地支付处理结果(比如：支付notify接口需要返回OK 来反馈给支付宝支付处理成功，不需要再次发送支付消息了)
        /// </summary>
        /// <param name="context">支付上下文</param>
        /// <param name="trade">通用抽象的交易信息</param>
        /// <returns>获取本地支付处理成功反馈消息</returns>
        protected abstract string GetSuccessFeedBackMessage(PayCallBackContext context, Trade trade);
    }
}
