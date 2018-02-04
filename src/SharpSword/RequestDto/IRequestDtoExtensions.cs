/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/05/2015 9:09:00 AM
 * ****************************************************************/

namespace SharpSword
{
    /// <summary>
    /// 将DTO转化成T类型
    /// </summary>
    public static class IRequestDtoExtensions
    {
        /// <summary>
        /// 上送参数是否需要上送用户名称和用户id编号
        /// </summary>
        /// <param name="requestDto">入参接口</param>
        /// <returns></returns>
        public static bool RequiredUserIdAndUserName(this IRequestDto requestDto)
        {
            return requestDto is IRequiredUser;
        }
    }
}
