/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/28 13:18:30
 * ****************************************************************/
using SharpSword.Domain.Entitys;
using SharpSword.Domain.Repositories;
using SharpSword.Domain.Uow;
using SharpSword.WebApi;
using System.Linq;

namespace SharpSword.AccessRecorder.DB
{
    /// <summary>
    /// 日志访问插件
    /// </summary>
    [ActionName("API.Logs.List")]
    [EnableRecordApiLog(false), DisablePackageSdk, AllowAnonymous, DisableDataSignatureTransmission, ResultCache(10)]
    public class ApiLogsAction : ActionBase<ApiLogsAction.ApiLogsActionRequestDto, PagedList<Domain.AccessRecorder>>
    {
        /// <summary>
        /// 上送的参数对象
        /// </summary>
        public class ApiLogsActionRequestDto : PageListRequestDtoBase
        {
            /// <summary>
            /// 接口名称
            /// </summary>
            public string ApiName { get; set; }

            /// <summary>
            /// IP地址，方便筛选客户端访问
            /// </summary>
            public string Ip { get; set; }

            /// <summary>
            /// 使用时间（毫秒）
            /// </summary>
            public int? UsedTime { get; set; }

            /// <summary>
            /// 总记录数
            /// </summary>
            public int? TotalCount { get; set; }

            /// <summary>
            /// 修改下上送参数
            /// </summary>
            public override void BeforeValid()
            {
                if (this.PageIndex <= 0) this.PageIndex = 1;
                if (this.PageSize <= 0) this.PageSize = 50;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<Domain.AccessRecorder> _accessRecorderRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessRecorderRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        public ApiLogsAction(IRepository<Domain.AccessRecorder> accessRecorderRepository, IUnitOfWorkManager unitOfWorkManager)
        {
            this._accessRecorderRepository = accessRecorderRepository;
            this._unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<PagedList<Domain.AccessRecorder>> Execute()
        {
            using (var uow = this._unitOfWorkManager.Begin())
            {
                //查询表
                var query = this._accessRecorderRepository.TableNoTracking;

                //指定了接口名称
                if (!this.RequestDto.ApiName.IsNullOrEmpty())
                {
                    query = query.Where(o => o.ApiName == this.RequestDto.ApiName);
                }

                //查询IP
                if (!this.RequestDto.Ip.IsNullOrEmpty())
                {
                    query = query.Where(o => o.Ip == this.RequestDto.Ip);
                }

                //毫秒数
                if (this.RequestDto.UsedTime.HasValue && this.RequestDto.UsedTime > 0)
                {
                    query = query.Where(o => o.UsedTime >= this.RequestDto.UsedTime);
                }

                //排序
                query = query.OrderByDescending(o => o.Id);

                //返回分页集合对象
                var pagedList = new PagedList<Domain.AccessRecorder>(query, this.RequestDto.PageIndex - 1, this.RequestDto.PageSize, this.RequestDto.TotalCount);

                //返回查询集合
                return this.SuccessActionResult(pagedList);
            }
        }
    }
}
