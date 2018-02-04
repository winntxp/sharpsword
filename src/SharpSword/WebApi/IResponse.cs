/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// action执行结果输出器
    /// </summary>
    public interface IResponse
    {
        /// <summary>
        /// 格式化输出ActionResult对象到客户端
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <param name="responseFormat">actionResultString格式化数据类型；XML/JSON</param>
        /// <param name="serializedActionResultString">执行结果对象</param>
        /// <returns></returns>
        void Write(RequestContext requestContext, ResponseFormat responseFormat, string serializedActionResultString);
    }
}