/* ****************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/20/2016 11:06:31 AM
 * ****************************************************************/
using SharpSword.Events;
using SharpSword.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SharpSword.Tools.Actions
{
    /// <summary>
    /// 获取所有定义的事件
    /// </summary>
    [ActionName("Api.Events"), DisablePackageSdk, AllowAnonymous, EnableRecordApiLog(true), EnableAjaxRequest]
    [DisableDataSignatureTransmission, ResultCache(15)]
    //[View("SharpSword.WebApi.Views.Api.Events.aspx")]
    public class EventsAction : ActionBase<NullRequestDto, IEnumerable<EventsAction.ResponseDto>>
    {
        /// <summary>
        /// 输出对象
        /// </summary>
        public class ResponseDto : ResponseDtoBase
        {
            /// <summary>
            /// 事件类型
            /// </summary>
            public Type EventType { get; private set; }

            /// <summary>
            /// 对应事件定义的处理类集合
            /// </summary>
            public IEnumerable<Type> HandlerTypes { get; private set; }

            /// <summary>
            /// 
            /// </summary>
            /// <param name="evenType">事件类型</param>
            /// <param name="handlerTypes">对应事件定义的处理类集合</param>
            public ResponseDto(Type evenType, IEnumerable<Type> handlerTypes)
            {
                this.EventType = evenType;
                this.HandlerTypes = handlerTypes;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private readonly ITypeFinder _typeFinder;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="typeFinder"></param>
        public EventsAction(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override ActionResult<IEnumerable<ResponseDto>> Execute()
        {
            //获取所有的事件集合
            var eventTypes =
                this._typeFinder.FindClassesOfType<IEventData>()
                    .OrderBy(t => t.Assembly.GetName().Name)
                    .ThenBy(type => type.Name);


            //获取所有实现了处理事件接口的类型
            var eventHandlers =
                this._typeFinder.FindClassesOfType(typeof(IEventHandler<>)).Where(type => !type.IsProxyType()).ToArray();

            IList<ResponseDto> responseDtos = new List<ResponseDto>();

            foreach (var eventType in eventTypes)
            {
                IList<Type> eventHandlersCollectionList = new List<Type>();

                //获取事件订阅的处理类
                foreach (var eventHandler in eventHandlers)
                {
                    //获取订阅类所有的接口
                    var interfaces = eventHandler.GetInterfaces();

                    foreach (var @interface in interfaces)
                    {
                        if (!typeof(IEventHandler).IsAssignableFrom(@interface))
                        {
                            continue;
                        }

                        var genericArguments = @interface.GetGenericArguments();
                        if (genericArguments.Length != 1)
                        {
                            continue;
                        }

                        if (genericArguments[0].IsAssignableFrom(eventType))
                        {
                            eventHandlersCollectionList.Add(eventHandler);
                        }
                    }
                }

                //添加时间对应的处理类
                responseDtos.Add(new ResponseDto(eventType, eventHandlersCollectionList));
            }

            //返回事件集合
            return this.SuccessActionResult(responseDtos);
        }
    }
}
