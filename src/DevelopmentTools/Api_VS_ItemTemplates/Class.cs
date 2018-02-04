/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn $time$
 * ****************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
$if$ ($targetframeworkversion$ >= 3.5)using System.Linq;
$endif$using System.Text;
$if$ ($targetframeworkversion$ >= 4.5)using System.Threading.Tasks;
$endif$
using SharpSword;
using SharpSword.WebApi;

namespace $rootnamespace$
{
    /// <summary>
    /// 
    /// </summary>
    [ActionName("$safeitemrootname$"), ResponseFormat(ResponseFormat.JSON | ResponseFormat.XML)]
    [Description("$safeitemrootname$")]
    public class $safeitemrootname$ : ActionBase<$safeitemrootname$.$safeitemrootname$RequestDto, $safeitemrootname$.$safeitemrootname$ResponseDto>
    {
        /// <summary>
        /// 上送参数对象
        /// </summary>
        public class $safeitemrootname$RequestDto : RequestDtoBase
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
        /// 下送数据对象
        /// </summary>
        public class $safeitemrootname$ResponseDto : ResponseDtoBase
        {

        }

        /// <summary>
        /// ctor
        /// </summary>
	public $safeitemrootname$()
        {
        
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<$safeitemrootname$ResponseDto> Execute()
        {
            throw new NotImplementedException();
        }

    }
}
