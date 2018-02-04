/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/2/17 15:26:10
 * ****************************************************************/
using System.Collections.Generic;
using System.Linq;

namespace SharpSword
{
    /// <summary>
    /// 验证返回结果对象
    /// </summary>
    public class DtoValidatorResult
    {
        /// <summary>
        /// 我们将DTO验证的成功返回对象缓存起来
        /// </summary>
        private static DtoValidatorResult _instance = new DtoValidatorResult(null);

        /// <summary>
        /// 验证成功
        /// </summary>
        public static DtoValidatorResult Success
        {
            get
            {
                return _instance;
            }
        }

        /// <summary>
        /// 验证失败返回对象
        /// </summary>
        /// <param name="errors">验证错误集合</param>
        /// <returns></returns>
        public static DtoValidatorResult Fail(IEnumerable<DtoValidatorResultError> errors = null)
        {
            return new DtoValidatorResult(errors);
        }

        /// <summary>
        /// 初始化验证结果对象
        /// </summary>
        /// <param name="errors">验证错误集合</param>
        public DtoValidatorResult(IEnumerable<DtoValidatorResultError> errors = null)
        {
            this.Errors = errors ?? new List<DtoValidatorResultError>();
        }

        /// <summary>
        /// 错误信息集合;如果为没有错误，则返回一个空的错误信息集合
        /// </summary>
        public IEnumerable<DtoValidatorResultError> Errors { get; private set; }

        /// <summary>
        /// 是否验证成功
        /// </summary>
        public bool IsValid => !this.Errors.Any();
    }
}
