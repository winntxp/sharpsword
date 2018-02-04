/* ****************************************************************
 * SharpSword zhangliang4629@163.com 10/3/2016 5:01:37 PM
 * ****************************************************************/
using System;

namespace SharpSword.Events.Factories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="THandler"></typeparam>
    internal class TransientEventHandlerFactory<THandler> : IEventHandlerFactory
        where THandler : IEventHandler, new()
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEventHandler GetHandler()
        {
            return new THandler();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public void ReleaseHandler(IEventHandler handler)
        {
            if (handler is IDisposable)
            {
                (handler as IDisposable).Dispose();
            }
        }
    }
}
