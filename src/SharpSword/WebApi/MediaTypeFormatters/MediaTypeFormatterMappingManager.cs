/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 6/29/2016 1:43:14 PM
 * ****************************************************************/
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 根据请求获取最格式的输出(此了会先搜索用户是否指定了Format参数，
    /// 如果未指定，会自动搜索浏览器请求头AcceptTypes来根据返回内容的权重返回接口格式化器)
    /// </summary>
    internal class MediaTypeFormatterMappingManager
    {
        /// <summary>
        /// 接收的类型对象
        /// </summary>
        private class MediaTypeHeaderValue
        {
            /// <summary>
            /// mine类型
            /// </summary>
            public string MineType { get; set; }

            /// <summary>
            /// 类型的权重0-1之间
            /// </summary>
            public float Q { get; set; }
        }

        /// <summary>
        /// 用于内容协商
        /// </summary>
        private static readonly Dictionary<string, ResponseFormat> MineTypeMapping = new Dictionary<string, ResponseFormat>();

        /// <summary>
        /// 
        /// </summary>
        private readonly RequestContext _requestContext;

        /// <summary>
        /// 初始化系统内容协商配置
        /// </summary>
        static MediaTypeFormatterMappingManager()
        {
            MineTypeMapping.Add("text/html", ResponseFormat.VIEW);
            MineTypeMapping.Add("text/xml", ResponseFormat.XML);
            MineTypeMapping.Add("application/xhtml+xml", ResponseFormat.VIEW);
            MineTypeMapping.Add("application/xml", ResponseFormat.XML);
            MineTypeMapping.Add("application/json", ResponseFormat.JSON);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        public MediaTypeFormatterMappingManager(RequestContext requestContext)
        {
            requestContext.CheckNullThrowArgumentNullException(nameof(requestContext));
            this._requestContext = requestContext;
        }

        /// <summary>
        /// 获取输出格式化器
        /// </summary>
        /// <returns></returns>
        public ResponseFormat GetResponseFormat()
        {
            return new Enum<ResponseFormat>().GetItem(this._requestContext.RawRequestParams.Format, () =>
            {
                IList<MediaTypeHeaderValue> mediaTypeValues = new List<MediaTypeHeaderValue>();

                //获取客户端所有能接受的MineType类型
                // ReSharper disable once PossibleNullReferenceException
                foreach (var item in this._requestContext.HttpContext.Request.AcceptTypes)
                {
                    var mineTypeInfo = item.Split(new char[] { ';' });
                    if (mineTypeInfo.Length == 1)
                    {
                        mediaTypeValues.Add(new MediaTypeHeaderValue() { MineType = mineTypeInfo[0], Q = 0 });
                    }
                    else
                    {
                        mediaTypeValues.Add(new MediaTypeHeaderValue()
                        {
                            MineType = mineTypeInfo[0],
                            Q = mineTypeInfo[1].Split(new char[] { '=' })[1].AsFloat()
                        });
                    }
                }

                //按照权重大小排序
                foreach (var item in mediaTypeValues.OrderByDescending(o => o.Q))
                {
                    if (MineTypeMapping.Keys.Contains(item.MineType))
                    {
                        return MineTypeMapping[item.MineType];
                    }
                }

                //都找不到的情况下，返回JSON数据
                return ResponseFormat.JSON;
            });
        }
    }
}
