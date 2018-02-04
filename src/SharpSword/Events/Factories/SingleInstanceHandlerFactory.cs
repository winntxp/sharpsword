/* *******************************************************
 * SharpSword zhangliang4629@163.com 10/3/2016 5:02:01 PM
 * ****************************************************************/

namespace SharpSword.Events.Factories
{
    /// <summary>
    /// 
    /// </summary>
    internal class SingleInstanceHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public IEventHandler HandlerInstance { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public SingleInstanceHandlerFactory(IEventHandler handler)
        {
            this.HandlerInstance = handler;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEventHandler GetHandler()
        {
            return this.HandlerInstance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handler"></param>
        public void ReleaseHandler(IEventHandler handler) { }
    }
}
