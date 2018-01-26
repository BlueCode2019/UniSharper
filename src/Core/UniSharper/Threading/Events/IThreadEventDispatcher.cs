/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2018 Jerry Lee
 *
 *	Permission is hereby granted, free of charge, to any person obtaining a copy
 *	of this software and associated documentation files (the "Software"), to deal
 *	in the Software without restriction, including without limitation the rights
 *	to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 *	copies of the Software, and to permit persons to whom the Software is
 *	furnished to do so, subject to the following conditions:
 *
 *	The above copyright notice and this permission notice shall be included in all
 *	copies or substantial portions of the Software.
 *
 *	THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 *	IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 *	FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 *	AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 *	LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 *	OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 *	SOFTWARE.
 */

using System;

namespace UniSharper.Threading.Events
{
    /// <summary>
    /// The <see cref="IThreadEventDispatcher"/> interface defines methods for adding or removing
    /// event listeners, checks whether specific types of event listeners are registered, and
    /// dispatches events for child thread.
    /// </summary>
    /// <seealso cref="ISynchronizedObject"/>
    public interface IThreadEventDispatcher : ISynchronizedObject
    {
        /// <summary>
        /// Registers an event listener to receive an event notification.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The delegate to handle the event.</param>
        /// <exception cref="ArgumentNullException">
        /// <para><c>eventType</c> is <c>null</c> or <c>empty</c>.</para>
        /// - or -
        /// <para><c>listener</c> is <c>null</c>.</para>
        /// </exception>
        void AddEventListener(string eventType, Action<Event> listener);

        /// <summary>
        /// Dispatches en <see cref="Event"/>.
        /// </summary>
        /// <param name="e">The <see cref="Event"/> object.</param>
        /// <exception cref="ArgumentNullException"><c>e</c> is <c>null</c>.</exception>
        void DispatchEvent(Event e);

        /// <summary>
        /// Checks whether this <see cref="IThreadEventDispatcher"/> has listeners registered for a
        /// specific type of event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <returns>
        /// <c>true</c> if listeners of the specified type of event are registered; <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException"><c>eventType</c> is <c>null</c> or <c>empty</c>.</exception>
        bool HasEventListeners(string eventType);

        /// <summary>
        /// Checks whether this <see cref="IThreadEventDispatcher"/> has the delegate listener
        /// registered for a specific type of event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The delegate to locate.</param>
        /// <returns>
        /// <c>true</c> if a listener of the specified type of event is registered; <c>false</c> otherwise.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <para><c>eventType</c> is <c>null</c> or <c>empty</c>.</para>
        /// - or -
        /// <para><c>listener</c> is <c>null</c>.</para>
        /// </exception>
        bool HasEventListener(string eventType, Action<Event> listener);

        /// <summary>
        /// Removes a listener.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The delegate to be removed.</param>
        /// <exception cref="ArgumentNullException">
        /// <para><c>eventType</c> is <c>null</c> or <c>empty</c>.</para>
        /// - or -
        /// <para><c>listener</c> is <c>null</c>.</para>
        /// </exception>
        void RemoveEventListener(string eventType, Action<Event> listener);

        /// <summary>
        /// Removes the event listeners registered for the specific type of event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <exception cref="ArgumentNullException"><c>eventType</c> is <c>null</c> or <c>empty</c>.</exception>
        void RemoveEventListeners(string eventType);

        /// <summary>
        /// Removes all event listeners of this <see cref="IThreadEventDispatcher"/>.
        /// </summary>
        void RemoveAllEventListeners();
    }
}