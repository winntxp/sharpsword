/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2015/11/20 8:43:03
 * ****************************************************************/

namespace SharpSword.WebApi
{
    /// <summary>
    /// 所有格式化器必须继承此抽象类；具体实现类里只要重写方法SerializedActionresultToString()即可
    /// 由于系统自带的XML格式化无法针对匿名对象实现格式化，所以需要在实现类里自己去使用递归方式去探测每一个属性对象，然后进行格式化
    /// </summary>
    public interface IMediaTypeFormatter
    {
        /// <summary>
        /// 对象资源格式化成字符串
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <param name="actionResult">ActionResult对象</param>
        /// <returns>返回特定的ActionResult对象序列化字符串</returns>
        string SerializedActionResultToString(RequestContext requestContext, ActionResult actionResult);
    }
}
