/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/18 11:44:59
 * ****************************************************************/
using SharpSword.Timing;
using System;
using System.Threading;

namespace SharpSword.WebApi
{
    /// <summary>
    /// API日志记录器发布者，给所有订阅者进行接口信息发布
    /// </summary>
    internal class ApiAccessRecordPublisher : IApiAccessRecordPublisher, ISingletonDependency
    {
        /// <summary>
        /// 
        /// </summary>
        private string _id = Guid.NewGuid().ToString();

        /// <summary>
        /// 发布日志访问消息；会逐个调用订阅者，进行发布
        /// </summary>
        /// <param name="actionResultString">执行结果的字符串</param>
        /// <param name="requestContext">当前请求上下文</param>
        /// <param name="actionLifeTime">Action对象的执行时间对象</param>
        public void Publish(string actionResultString, RequestContext requestContext, IDateTimeRange actionLifeTime)
        {
            //不存在接口描述信息
            if (requestContext.ActionDescriptor.IsNull())
            {
                return;
            }

            //不记录访问日志，直接返回
            if (!requestContext.ActionDescriptor.EnableRecordApiLog)
            {
                return;
            }

            //获取当前操作用户信息
            var currentUserIdentity = requestContext.GetCurrentUser(() => new UserIdentity("unknown", "unknown"));

            //构造出记录器需要的参数
            var args = new ApiAccessRecorderArgs()
            {
                ActionName = requestContext.ActionDescriptor.ActionName,
                Sign = requestContext.RequestParams.Sign,
                TimeStamp = requestContext.RequestParams.TimeStamp,
                Version = requestContext.ActionDescriptor.Version,
                AuthorName = requestContext.ActionDescriptor.AuthorName,
                RequestStartTime = actionLifeTime.StartTime,
                RequestEndTime = actionLifeTime.EndTime,
                RequestMilliseconds = (actionLifeTime.EndTime - actionLifeTime.StartTime).TotalMilliseconds.ToString("0.0000").As<double>(),
                HttpMethod = requestContext.HttpContext.Request.HttpMethod,
                Ip = requestContext.HttpContext.Request.GetClientIp(),
                RequestData = requestContext.RequestParams.Data ?? string.Empty,
                ResponseData = actionResultString,
                ResponseFormat = requestContext.RequestParams.Format,
                RequestId = requestContext.RequestParams.RequestId,
                UserId = currentUserIdentity.UserId,
                UserName = currentUserIdentity.UserName
            };

            ThreadPool.QueueUserWorkItem(state =>
            {
                try
                {
                    ServicesContainer.Current.Resolve<IApiAccessRecorder>().Record(args);
                }
                catch (Exception exc)
                {
                    ServicesContainer.Current.Resolve<ILogger<ApiAccessRecordPublisher>>().Error(exc);
                }
            });
        }
    }
}
