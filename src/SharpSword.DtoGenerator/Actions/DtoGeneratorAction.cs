/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/6 8:37:50
 * ****************************************************************/
using SharpSword.Data;
using SharpSword.EntityFramework;
using SharpSword.WebApi;

namespace SharpSword.DtoGenerator.Actions
{
    /// <summary>
    /// DTO生成器插件
    /// </summary>
    [ActionName("API.DtoGenerator")]
    [DisablePackageSdk, EnableRecordApiLog(true), DisableDataSignatureTransmission, AllowAnonymous, ResultCache(5)]
    public class DtoGeneratorAction : ActionBase<NullRequestDto, DtoGeneratorAction.DtoGeneratorActionResponseDto>
    {
        /// <summary>
        /// 返回对象
        /// </summary>
        public class DtoGeneratorActionResponseDto : ResponseDtoBase
        {
            /// <summary>
            /// 数据访问者信息
            /// </summary>
            public string Connection { get; set; }

            /// <summary>
            /// 当前连接的数据库字符串
            /// </summary>
            public string ConnectionString { get; set; }

            /// <summary>
            /// 当前插件配置信息
            /// </summary>
            public DtoGeneratorConfig Config { get; set; }
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly IDbContext _dbContext;
        private readonly DtoGeneratorConfig _dtoGeneratorConfig;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="dtoGeneratorConfig"></param>
        public DtoGeneratorAction(DtoGeneratorDbContext dbContext, DtoGeneratorConfig dtoGeneratorConfig)
        {
            dbContext.CheckNullThrowArgumentNullException(nameof(dbContext));
            dtoGeneratorConfig.CheckNullThrowArgumentNullException(nameof(dtoGeneratorConfig));

            this._dbContext = dbContext;
            this._dtoGeneratorConfig = dtoGeneratorConfig;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<DtoGeneratorActionResponseDto> Execute()
        {
            //转型下，必须继承DbContextBase
            var dbContext = _dbContext as DbContextBase;
            dbContext.CheckNullThrowArgumentNullException(nameof(dbContext));

            //当前数据库连接信息 
            var connection = dbContext.Database.Connection;

            //返回输出对象
            var responseDto = new DtoGeneratorActionResponseDto
            {
                Connection = connection.ToString(),
                ConnectionString = connection.ConnectionString,
                Config = this._dtoGeneratorConfig
            };

            //返回
            return this.SuccessActionResult(responseDto);
        }
    }
}
