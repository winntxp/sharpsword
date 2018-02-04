/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/20 9:02:06
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 上送参数对象绑定器接口
    /// </summary>
    public interface IRequestParamsBinder
    {
        /// <summary>
        /// 获取上送的参数对象
        /// </summary>
        /// <returns></returns>
        RequestParams GetRequestParams();
    }
}
