/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 11/2/2015 8:32:16 PM
 * ****************************************************************/
using System.Collections.Generic;

namespace SharpSword.SDK
{
    /// <summary>
    /// TOP请求接口。
    /// </summary>
    public interface IApiRequest<T> where T : ResponseBase
    {
        /// <summary>
        /// 获取TOP的API名称。比如：Product.Add
        /// </summary>
        /// <returns>API名称</returns>
        string GetApiName();

        /// <summary>
        /// 获取接口版本
        /// </summary>
        /// <returns></returns>
        string GetVersion();

        /// <summary>
        /// 提交方式
        /// </summary>
        /// <returns>POST/GET</returns>
        HttpMethod GetHttpMethod();

        /// <summary>
        /// 提交的Data消息体对象
        /// </summary>
        /// <returns>如果参数请求类定义了Data属性，那么方法进行封装Data格式化成JSON</returns>
        string GetRequestJsonData();

        /// <summary>
        /// 需要替换的一些东西，比如有时候可能JSON返回不是规则的，就需要进行进行将一些关键词进行替换掉 
        /// </summary>
        /// <returns></returns>
        IDictionary<string, string> GetReplaces();
    }
}
