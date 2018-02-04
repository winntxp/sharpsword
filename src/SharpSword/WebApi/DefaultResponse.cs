/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using System;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Web;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 字符串输出器默认实现
    /// </summary>
    internal class DefaultResponse : IResponse
    {
        /// <summary>
        /// 自定义一些输出头信息，实现类无需了解
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        private void AddCustomerResponseHeaders(RequestContext requestContext)
        {
            ////输出一些头信息到客户端
            //requestContext.HttpContext.Response.Headers.Add("Api-Action-Name",
            //    requestContext.RawRequestParams.ActionName ?? string.Empty);

            ////版本
            //requestContext.HttpContext.Response.Headers.Add("Api-Version",
            //    requestContext.ActionDescriptor.IsNull() ? "" : requestContext.ActionDescriptor.Version);

            ////集群，分布式服务器编号
            //requestContext.HttpContext.Response.Headers.Add("Api-Server-Name", requestContext.SysOptions.ServerName);

            //把上下文保存的一些自定义打点数据输出到客户端头部，方便调试
            //foreach (var item in requestContext.AdditionDatas)
            //{
            //    requestContext.HttpContext.Response.Headers.Add(item.Key, (item.Value is DateTime) ? ((DateTime)item.Value).ToString("yyyy/MM/dd HH:mm:ss.ffffff") : item.Value.ToString());
            //}
        }

        /// <summary>
        /// 压缩数据
        /// </summary>
        /// <param name="requestContext"></param>
        private void ResponseCompress(RequestContext requestContext)
        {
            if (requestContext.IsNull())
            {
                return;
            }

            if (requestContext.ActionDescriptor.IsNull())
            {
                return;
            }

            var httpResponse = requestContext.HttpContext.Response;

            //防止代理服务器将不同的压缩数据进行缓存，我们开启Vary https://www.w3.org/Protocols/rfc2616/rfc2616-sec14.html
            httpResponse.AppendHeader("Vary", "Content-Encoding");

            //服务器端是否强制使用GZip压缩
            var gzipCompressAttributes = requestContext.ActionDescriptor.GetCustomAttributes<GZipCompressAttribute>();
            if (gzipCompressAttributes.Any())
            {
                SetGZipContentEncoding(httpResponse);
                return;
            }

            //服务器端是否强制使用deflate压缩
            var deflateCompressAttributes = requestContext.ActionDescriptor.GetCustomAttributes<DeflateCompressAttribute>();
            if (deflateCompressAttributes.Any())
            {
                SetDeflateContentEncoding(httpResponse);
                return;
            }

            //根据客户端提交的可接受数据类型来进行数据压缩
            var acceptEncoding = requestContext.HttpContext.Request.Headers["Accept-Encoding"];
            if (acceptEncoding.IsNullOrEmpty())
            {
                return;
            }

            //GZip
            var acceptEncodings = acceptEncoding.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (acceptEncodings.FirstOrDefault() == "gzip")
            {
                SetGZipContentEncoding(httpResponse);
                return;
            }

            //deflate
            if (acceptEncodings.FirstOrDefault() == "deflate")
            {
                SetDeflateContentEncoding(httpResponse);
                return;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpResponse"></param>
        /// <param name="stream"></param>
        /// <param name="contentEncoding"></param>
        private void SetContentEncoding(HttpResponseBase httpResponse, Stream stream, string contentEncoding)
        {
            httpResponse.Filter = stream;
            httpResponse.AppendHeader("Content-Encoding", contentEncoding);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpResponse"></param>
        private void SetGZipContentEncoding(HttpResponseBase httpResponse)
        {
            SetContentEncoding(httpResponse, new GZipStream(httpResponse.Filter, CompressionMode.Compress), "gzip");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="httpResponse"></param>
        private void SetDeflateContentEncoding(HttpResponseBase httpResponse)
        {
            SetContentEncoding(httpResponse, new DeflateStream(httpResponse.Filter, CompressionMode.Compress), "deflate");
        }

        /// <summary>
        /// 输出格式化数据到客户端
        /// </summary>
        /// <param name="requestContext">当前请求上下文</param>
        /// <param name="responseFormat">输出格式化类型</param>
        /// <param name="serializedActionResultString">格式化了的ActionResult对象字符串</param>
        public void Write(RequestContext requestContext, ResponseFormat responseFormat, string serializedActionResultString)
        {
            //参数值为null
            requestContext.CheckNullThrowArgumentNullException(nameof(requestContext));
            requestContext.HttpContext.CheckNullThrowArgumentNullException(nameof(requestContext.HttpContext));

            //添加一些自定义的header头信息
            this.AddCustomerResponseHeaders(requestContext);

            //TODO 压缩数据 2017-08-28 标注掉
            //this.ResponseCompress(requestContext);

            //字符串格式为XML
            if (responseFormat == ResponseFormat.XML)
            {
                requestContext.HttpContext.Response.ContentType = "application/xml; charset=utf-8";
            }

            //字符串格式为JSON
            if (responseFormat == ResponseFormat.JSON)
            {
                requestContext.HttpContext.Response.ContentType = "application/json; charset=utf-8";
            }

            //HTML
            if (responseFormat == ResponseFormat.VIEW)
            {
                requestContext.HttpContext.Response.ContentType = "text/html; charset=utf-8";
            }

            //输出格式化数据给客户端
            requestContext.HttpContext.Response.Write(serializedActionResultString ?? string.Empty);
        }
    }
}