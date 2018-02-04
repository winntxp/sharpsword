/******************************************************************
 * SharpSword zhangliang@sharpsword.com.cn 10/23/2015 5:04:21 PM
 * ****************************************************************/
using SharpSword.Resource;
using System;

namespace SharpSword.WebApi
{
    /// <summary>
    /// action激活器实现
    /// </summary>
    public class DefaultActionActivator : IActionActivator
    {
        /// <summary>
        /// 
        /// </summary>
        private readonly IIocResolver _iocResolver;

        /// <summary>
        /// 为了不让写日志出错（属性注入是惰性注入，因此需要进行构造函数指定一个空的日志实现）
        /// </summary>
        /// <param name="iocResolver"></param>
        public DefaultActionActivator(IIocResolver iocResolver)
        {
            this._iocResolver = iocResolver;
        }

        /// <summary>
        /// 创建指定类型的 Action 对象
        /// </summary>
        /// <param name="actionType">实现ActionBase的实现类类型</param>
        /// <returns></returns>
        public virtual IAction Create(Type actionType)
        {
            //actionType不能为null
            actionType.CheckNullThrowArgumentNullException(nameof(actionType));

            //没有继承自ActionBase抽象基类
            if (!actionType.IsAssignableToActionBase())
            {
                throw new SharpSwordCoreException(CoreResource.DefaultActionActivator_TypeError
                    .With(actionType.FullName));
            }

            //从IOC容器里创建一个新的Action接口对象
            try
            {
                //return (IAction)ServicesContainer.Current.Resolver(actionType);

                //TODO:触发下创建成功事件？

                //从IOC容器里创建接口
                return (IAction)this._iocResolver.Resolve(actionType);
            }
            catch (Exception exception)
            {
                //this.Logger.Error(exception.StackTrace);
                throw new SharpSwordCoreException(exception.Message, exception.InnerException);
            }
        }
    }
}