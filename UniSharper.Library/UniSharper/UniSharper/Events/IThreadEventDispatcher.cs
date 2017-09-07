/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2017 Jerry Lee
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

namespace UniSharper.Events
{
    /// <summary>
    /// The IThreadEventDispatcher interface defines methods for adding or removing event listeners,
    /// checks whether specific types of event listeners are registered, and dispatches events for
    /// child thread.
    /// </summary>
    public interface IThreadEventDispatcher
    {
        /// <summary>
        /// Update is called every frame or fixed frequency.
        /// </summary>
        void Update();

        /// <summary>
        /// Registers an event listener to receive an event notification.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The delegate to handle the event.</param>
        void AddEventListener(string eventType, Action<Event> listener);

        /// <summary>
        /// Dispatches en <see cref="Event"/>.
        /// </summary>
        /// <param name="e">The <see cref="Event"/> object.</param>
        void DispatchEvent(Event e);

        /// <summary>
        /// Checks whether this <see cref="IThreadEventDispatcher"/> has any listener registered for
        /// a specific type of event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <returns>
        /// <c>true</c> if any listener of the specified type of event is registered; <c>false</c> otherwise.
        /// </returns>
        bool HasEventListener(string eventType);

        /// <summary>
        /// Checks whether this <see cref="IThreadEventDispatcher"/> has the delegate listener
        /// registered for a specific type of event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The delegate to locate.</param>
        /// <returns>
        /// <c>true</c> if a listener of the specified type of event is registered; <c>false</c> otherwise.
        /// </returns>
        bool HasEventListener(string eventType, Action<Event> listener);

        /// <summary>
        /// Removes a listener.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The delegate to be removed.</param>
        void RemoveEventListener(string eventType, Action<Event> listener);

        /// <summary>
        /// Removes the event listeners registered for the specific type of event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        void RemoveEventListeners(string eventType);

        /// <summary>
        /// Removes all event listeners of this <see cref="IThreadEventDispatcher"/>.
        /// </summary>
        void RemoveAllEventListeners();
    }
}