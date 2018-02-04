/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/3/28 13:18:30
 * ****************************************************************/
using SharpSword.Domain.Repositories;
using SharpSword.Domain.Uow;
using SharpSword.WebApi;
using System.Linq;

namespace SharpSword.AccessRecorder.DB
{
    /// <summary>
    /// 日志访问插件
    /// </summary>
    [ActionName("API.Logs.Get")]
    [EnableRecordApiLog(false), DisablePackageSdk, AllowAnonymous, DisableDataSignatureTransmission]
    public class ApiLogGetAction : ActionBase<ApiLogGetAction.ApiLogGetActionRequestDto, ApiLogGetAction.ApiLogGetActionResponseDto>
    {
        /// <summary>
        /// 上送的参数对象
        /// </summary>
        public class ApiLogGetActionRequestDto : RequestDtoBase
        {
            /// <summary>
            /// 访问日志编号
            /// </summary>
            public int Id { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        public class ApiLogGetActionResponseDto
        {
            /// <summary>
            /// 
            /// </summary>
            public Domain.AccessRecorder AccessRecorder { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public Domain.Response ResponseData { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly IRepository<Domain.AccessRecorder> _accessRecorderRepository;
        private readonly IRepository<Domain.Response> _responseRepository;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessRecorderRepository"></param>
        /// <param name="responseRepository"></param>
        /// <param name="unitOfWorkManager"></param>
        public ApiLogGetAction(IRepository<Domain.AccessRecorder> accessRecorderRepository,
                               IRepository<Domain.Response> responseRepository,
                               IUnitOfWorkManager unitOfWorkManager)
        {
            this._accessRecorderRepository = accessRecorderRepository;
            this._responseRepository = responseRepository;
            this._unitOfWorkManager = unitOfWorkManager;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<ApiLogGetAction.ApiLogGetActionResponseDto> Execute()
        {
            using (var uow = this._unitOfWorkManager.Begin())
            {
                return this.SuccessActionResult(new ApiLogGetActionResponseDto()
                {
                    //访问记录
                    AccessRecorder = this._accessRecorderRepository.TableNoTracking.FirstOrDefault(o => o.Id == this.RequestDto.Id),
                    //输出详情
                    ResponseData = this._responseRepository.TableNoTracking.FirstOrDefault(o => o.Id == this.RequestDto.Id)
                });
            }
        }
    }
}
