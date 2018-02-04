/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/12 11:33:56
 * ****************************************************************/
using System;
using System.Collections.Generic;

namespace SharpSword.WebApi
{
    /// <summary>
    /// 安卓客户端SDK开发包生成器
    /// </summary>
    public class AndroidSdkCodeGenerator : SdkCodeGeneratorBase
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDocResourceManager"></param>
        protected AndroidSdkCodeGenerator(ActionDocResourceManager actionDocResourceManager) : base(actionDocResourceManager)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override Dictionary<string, string> CreateTypeMapping()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public override KeyValuePair<string, string> GeneratorRequest(IActionDescriptor actionDescriptor)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionDescriptor"></param>
        /// <returns></returns>
        public override KeyValuePair<string, string> GeneratorResponse(IActionDescriptor actionDescriptor)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 
        /// </summary>
        public override string Language
        {
            get { throw new NotImplementedException(); }
        }
    }
}
