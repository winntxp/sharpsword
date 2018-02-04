/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System.Linq;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 默认的输出接口创建器
    /// </summary>
    public class DefaultMediaTypeFormatterFactory : IMediaTypeFormatterFactory
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iocResolver"></param>
        public DefaultMediaTypeFormatterFactory(IIocResolver iocResolver)
        {
            this._iocResolver = iocResolver;
        }

        /// <summary>
        /// 根据指定的格式化枚举，创建对应的格式化输出器
        /// </summary>
        /// <param name="format">格式化枚举</param>
        /// <returns>格式化输出器</returns>
        public IMediaTypeFormatter Create(ResponseFormat format)
        {
            switch (format)
            {
                //TODO:2017-08-31 我们直接返回下
                case ResponseFormat.XML:
                    //return this._iocResolver.ResolveAll<IMediaTypeFormatter>("Xml_MediaTypeFormatter").First();
                    return XmlMediaTypeFormatter.Instance;
                case ResponseFormat.JSON:
                    //return this._iocResolver.ResolveAll<IMediaTypeFormatter>("Json_MediaTypeFormatter").First();
                    return JsonMediaTypeFormatter.Instance;
                case ResponseFormat.VIEW:
                    return this._iocResolver.ResolveAll<IMediaTypeFormatter>("View_MediaTypeFormatter").First();
                default:
                    // return this._iocResolver.ResolveAll<IMediaTypeFormatter>("Json_MediaTypeFormatter").First();
                    return JsonMediaTypeFormatter.Instance;
            }
        }
    }
}