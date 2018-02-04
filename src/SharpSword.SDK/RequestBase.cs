/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/4/2015 8:32:48 AM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.SDK
{
    /// <summary>
    /// 使用默认基类；使用了一些默认的约定
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class RequestBase<T> : IApiRequest<T> where T : ResponseBase
    {
        /// <summary>
        /// 方法名称，对应于接口
        /// </summary>
        /// <returns></returns>
        public abstract string GetApiName();

        /// <summary>
        /// 接口版本号，默认不指定版本号
        /// </summary>
        /// <returns></returns>
        public virtual string GetVersion()
        {
            return string.Empty;
        }

        /// <summary>
        /// 默认使用http-POST提交，需要改成GET的，请在实现类里重写
        /// </summary>
        /// <returns></returns>
        public virtual HttpMethod GetHttpMethod()
        {
            return HttpMethod.POST;
        }

        /// <summary>
        /// 需要替换的一些东西，默认为空
        /// </summary>
        /// <returns></returns>
        public virtual IDictionary<string, string> GetReplaces()
        {
            return new ApiDictionary();
        }

        /// <summary>
        /// 返回当前调用的JSON数据
        /// </summary>
        /// <returns></returns>
        public virtual string GetRequestJsonData()
        {
            return this.ToJson();
        }
    }
}
