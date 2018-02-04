/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/21 17:22:38
 * ****************************************************************/
using SharpSword.Auditing;
using SharpSword.Domain.Repositories;
using SharpSword.Domain.Services;
using SharpSword.Domain.Uow;
using SharpSword.Timing;
using SharpSword.WebApi;
using System;
using System.Transactions;

namespace SharpSword.AccessRecorder.DB
{
    /// <summary>
    /// 将访问记录记录到数据库;方便管理统计接口访问量
    /// </summary>
    public class ApiAccessRecorder : IApiAccessRecorder, ISharpSwordServices
    {
        /// <summary>
        /// 日志记录器
        /// </summary>
        public ILogger<ApiAccessRecorder> Logger { get; set; }

        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<Domain.AccessRecorder> _accessRecorderRepository;
        private readonly IRepository<Domain.ActionDescriptor> _actionDescriptorRepository;
        private readonly IRepository<Domain.Response> _responseRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly DataBaseAccessRecorderConfig _config;

        /// <summary>
        /// 将访问记录记录到数据库;方便管理统计接口访问量
        /// </summary>
        /// <param name="accessRecorderRepository"></param>
        /// <param name="actionDescriptorRepository"></param>
        /// <param name="responseRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="config"></param>
        public ApiAccessRecorder(IRepository<Domain.AccessRecorder> accessRecorderRepository,
                                      IRepository<Domain.ActionDescriptor> actionDescriptorRepository,
                                      IRepository<Domain.Response> responseRepository,
                                      IUnitOfWorkManager unitOfWorkManager,
                                      DataBaseAccessRecorderConfig config)
        {
            this.Logger = GenericNullLogger<ApiAccessRecorder>.Instance;
            this._accessRecorderRepository = accessRecorderRepository;
            this._actionDescriptorRepository = actionDescriptorRepository;
            this._responseRepository = responseRepository;
            this._unitOfWorkManager = unitOfWorkManager;
            this._config = config;
        }

        /// <summary>
        /// 实现API记录器
        /// 方法里尽量做到快速的记录，不要进行大的操作，从而影响到API整体框架的性能
        /// </summary>
        /// <param name="args"></param>
        [DisableValidation, Audited, UnitOfWork(scope: TransactionScopeOption.Suppress)]
        public void Record(ApiAccessRecorderArgs args)
        {
            //设置为不记录日志
            if (!this._config.IsNull() && !this._config.IsEnabled)
            {
                return;
            }

            try
            {
                //构造日志记录对象
                var entity = new Domain.AccessRecorder()
                {
                    ApiName = args.ActionName,
                    Created = Clock.Now,
                    ResponseFormat = args.ResponseFormat,
                    Ip = args.Ip,
                    Sign = args.Sign,
                    TimeStamp = args.TimeStamp,
                    StartTime = args.RequestStartTime,
                    EndTime = args.RequestEndTime,
                    UsedTime = args.RequestMilliseconds,
                    HttpMethod = args.HttpMethod,
                    Author = args.AuthorName,
                    Version = args.Version,
                    UserId = args.UserId,
                    UserName = args.UserName ?? string.Empty,
                    RequestId = args.RequestId
                };

                using (var uow = this._unitOfWorkManager.Begin())
                {
                    //记录日志
                    this._accessRecorderRepository.Add(entity);

                    //保存下数据到数据库(为或获取到当前ID编号)
                    this._unitOfWorkManager.Current.SaveChanges();

                    //详情
                    this._responseRepository.Add(new Domain.Response()
                    {
                        Id = entity.Id,
                        RequestData = args.RequestData,
                        ResponseData = args.ResponseData
                    });

                    uow.Complete();
                }

                ////查询是否存在API记录信息
                //var apiDescriptor = this._actionDescriptorRepository.Table.FirstOrDefault(o => o.ActionName == args.ActionName);

                ////新增接口访问
                //if (apiDescriptor.IsNull())
                //{
                //    this._actionDescriptorRepository.Add(new Domain.ActionDescriptor()
                //    {
                //        ActionName = args.ActionName,
                //        AuthorName = args.AuthorName,
                //        Description = args.Description,
                //        HttpMethod = args.HttpMethod.ToString(),
                //        IsObsolete = args.IsObsolete,
                //        IsRequireHttps = args.RequireHttps,
                //        RequiredUserIdAndUserName = args.RequiredUserIdAndUserName,
                //        Version = args.Version,
                //        AccessCount = 1,
                //        LastAccessTime = Clock.Now //null
                //    });
                //}
                ////已经存在了就修改访问记录数（新增记录数）
                //else
                //{
                //    apiDescriptor.AccessCount++;
                //    apiDescriptor.LastAccessTime = Clock.Now;
                //    this._actionDescriptorRepository.Update(apiDescriptor);
                //}
            }
            catch (Exception exc)
            {
                //将错误记录到日志
                this.Logger.Error(exc);
            }
        }
    }
}
