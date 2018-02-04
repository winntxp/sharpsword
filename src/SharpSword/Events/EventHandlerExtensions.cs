/* *******************************************************
 * SharpSword zhangliang4629@163.com 10/25/2016 12:26:30 PM
 * ****************************************************************/
using System;

namespace SharpSword.Events
{
    /// <summary>
    /// 
    /// </summary>
    public static class EventHandlerExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <param name="sender"></param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender)
        {
            eventHandler.InvokeSafely(sender, EventArgs.Empty);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="eventHandler"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void InvokeSafely(this EventHandler eventHandler, object sender, EventArgs e)
        {
            if (eventHandler.IsNull())
            {
                return;
            }

            eventHandler(sender, e);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TEventArgs"></typeparam>
        /// <param name="eventHandler"></param>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void InvokeSafely<TEventArgs>(this EventHandler<TEventArgs> eventHandler, object sender, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (eventHandler.IsNull())
            {
                return;
            }

            eventHandler(sender, e);
        }
    }
}
