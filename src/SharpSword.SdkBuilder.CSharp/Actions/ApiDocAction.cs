/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 2016/4/20 13:26:25
 * ****************************************************************/
using SharpSword.WebApi;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SharpSword.SdkBuilder.CSharp.Actions
{
    /// <summary>
    /// 接口在线文档生成器接口
    /// </summary>
    [ActionName("Api.Doc"), DisablePackageSdk, EnableRecordApiLog(true), Version(0, 0), AllowAnonymous, DisableDataSignatureTransmission]
    public class ApiDocAction : ActionBase<ApiDocAction.ApiDocActionRequestDto, ApiDocAction.ApiDocActionResponseDto>
    {
        /// <summary>
        /// 上送的参数对象
        /// </summary>
        public class ApiDocActionRequestDto : RequestDtoBase
        {
            /// <summary>
            /// 接口名称
            /// </summary>
            [Required]
            public string ActionName { get; set; }

            /// <summary>
            /// 接口版本（不传入的话，会显示版本号最大的同名接口）
            /// </summary>
            public string Version { get; set; }
        }

        /// <summary>
        /// 下送的数据
        /// </summary>
        public class ApiDocActionResponseDto
        {
            /// <summary>
            /// 接口描述对象
            /// </summary>
            public ActionDescriptor ActionDescriptor { get; set; }

            /// <summary>
            /// 请求参数data数据JSON串
            /// </summary>
            public string RequestDtoJson { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public IDictionary<Type, IEnumerable<ComplexObjPropertyTypeDescriptor>> RequestTypes { get; set; }

            /// <summary>
            /// 
            /// </summary>
            public IDictionary<Type, IEnumerable<ComplexObjPropertyTypeDescriptor>> ResponseTypes { get; set; }

            /// <summary>
            /// 输出JSON串
            /// </summary>
            public string ResponseDtoJson { get; set; }

            /// <summary>
            /// 输出XML串
            /// </summary>
            public string ResponseDtoXml { get; set; }

        }

        /// <summary>
        /// 
        /// </summary>
        private readonly IActionSelector _actionSelector;
        private readonly IApiDocBuilder _apiDocBuilder;
        private readonly IMediaTypeFormatterFactory _mediaTypeFormatterFactory;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionSelector">接口查找器</param>
        /// <param name="apiDocBuilder">接口仿真数据提供器</param>
        /// <param name="mediaTypeFormatterFactory">格式化器工厂</param>
        public ApiDocAction(
            IActionSelector actionSelector,
            IApiDocBuilder apiDocBuilder,
            IMediaTypeFormatterFactory mediaTypeFormatterFactory)
        {
            this._actionSelector = actionSelector;
            this._apiDocBuilder = apiDocBuilder;
            this._mediaTypeFormatterFactory = mediaTypeFormatterFactory;
        }

        /// <summary>
        /// 执行业务逻辑
        /// </summary>
        /// <returns></returns>
        public override ActionResult<ApiDocActionResponseDto> Execute()
        {
            //接口描述对象
            var actionDescriptor = this._actionSelector.GetActionDescriptor(this.RequestDto.ActionName, this.RequestDto.Version);

            //上送的数据
            string requestDtoJson = this._apiDocBuilder.CreateInstance(actionDescriptor.RequestDtoType).Serialize2Josn();

            //下送的数据
            var actionResult = new ActionResult()
            {
                Data = this._apiDocBuilder.CreateInstance(actionDescriptor.ResponseDtoType),
                Flag = ActionResultFlag.SUCCESS,
                Info = "OK"
            };

            //代码生成器基类
            CodeGeneratorBase codeGeneratorBase = (CodeGeneratorBase)this._apiDocBuilder;

            //上送的对象信息
            var requestTypes = new List<Type> { actionDescriptor.RequestDtoType };
            codeGeneratorBase.GetComplexObjTypes(actionDescriptor.RequestDtoType, requestTypes);
            var requestTypeDescriptors = new Dictionary<Type, IEnumerable<ComplexObjPropertyTypeDescriptor>>();
            requestTypes.ForEach(o =>
            {
                requestTypeDescriptors.Add(o, codeGeneratorBase.GetComplexObjPropertyTypeDescriptors(o));
            });

            //下送对象信息
            var responseTypes = new List<Type>();
            if (codeGeneratorBase.IsComplexType(actionDescriptor.ResponseDtoType) && !codeGeneratorBase.IsCollection(actionDescriptor.ResponseDtoType))
            {
                responseTypes.Add(actionDescriptor.ResponseDtoType);
            }
            codeGeneratorBase.GetComplexObjTypes(actionDescriptor.ResponseDtoType, responseTypes);
            var responseTypeDescriptors = new Dictionary<Type, IEnumerable<ComplexObjPropertyTypeDescriptor>>();
            responseTypes.ForEach(o =>
            {
                responseTypeDescriptors.Add(o, codeGeneratorBase.GetComplexObjPropertyTypeDescriptors(o));
            });

            //返回
            return this.SuccessActionResult(new ApiDocActionResponseDto()
            {
                ActionDescriptor = actionDescriptor,
                RequestDtoJson = requestDtoJson,
                RequestTypes = requestTypeDescriptors,
                ResponseTypes = responseTypeDescriptors,
                ResponseDtoJson = this._mediaTypeFormatterFactory.Create(ResponseFormat.JSON).SerializedActionResultToString(this.RequestContext, actionResult),
                ResponseDtoXml = this._mediaTypeFormatterFactory.Create(ResponseFormat.XML).SerializedActionResultToString(this.RequestContext, actionResult)
            });
        }
    }
}
