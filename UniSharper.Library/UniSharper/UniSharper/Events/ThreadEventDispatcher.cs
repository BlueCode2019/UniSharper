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
using System.Collections.Generic;

namespace UniSharper.Events
{
    /// <summary>
    /// This class provides event dispatcher and listen between Unity main thread and sub-thread.
    /// Since Unity do not allow
    /// </summary>
    /// <seealso cref="IThreadEventDispatcher"/>
    public class ThreadEventDispatcher : IThreadEventDispatcher
    {
        private Dictionary<string, List<Action<Event>>> listeners = null;
        private Dictionary<string, List<Action<Event>>> pendingListeners = null;

        private Queue<Event> eventQueue;
        private Queue<Event> pendingEventQueue;

        private bool pendingFlag = false;

        private readonly object syncRoot = new object();

        /// <summary>
        /// Initializes a new instance of the <see cref="ThreadEventDispatcher"/> class.
        /// </summary>
        public ThreadEventDispatcher()
        {
            listeners = new Dictionary<string, List<Action<Event>>>();
            pendingListeners = new Dictionary<string, List<Action<Event>>>();

            eventQueue = new Queue<Event>();
            pendingEventQueue = new Queue<Event>();
        }

        #region Interface IThreadEventDispatcher

        /// <summary>
        /// Registers an event listener to receive an event notification.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The delegate to handle the event.</param>
        public void AddEventListener(string eventType, Action<Event> listener)
        {
            lock (syncRoot)
            {
                if (pendingFlag)
                {
                    // Add to pending listeners
                    pendingListeners.AddUnique(eventType, new List<Action<Event>>());
                    pendingListeners[eventType].AddUnique(listener);
                }
                else
                {
                    // Add to listeners.
                    listeners.AddUnique(eventType, new List<Action<Event>>());
                    listeners[eventType].AddUnique(listener);
                }
            }
        }

        /// <summary>
        /// Dispatches en <see cref="Event"/>.
        /// </summary>
        /// <param name="e">The <see cref="Event"/> object.</param>
        public void DispatchEvent(Event e)
        {
            lock (syncRoot)
            {
                if (!HasEventListener(e.EventType))
                {
                    return;
                }

                if (pendingFlag)
                {
                    pendingEventQueue.Enqueue(e);
                }
                else
                {
                    eventQueue.Enqueue(e);
                }
            }
        }

        /// <summary>
        /// Checks whether this <see cref="ThreadEventDispatcher"/> has any listener registered for a
        /// specific type of event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <returns>
        /// <c>true</c> if any listener of the specified type of event is registered; <c>false</c> otherwise.
        /// </returns>
        public bool HasEventListener(string eventType)
        {
            return (listeners.ContainsKey(eventType) && listeners[eventType].Count > 0)
                || (pendingListeners.ContainsKey(eventType) && pendingListeners[eventType].Count > 0);
        }

        /// <summary>
        /// Checks whether this <see cref="IThreadEventDispatcher"/> has the delegate listener
        /// registered for a specific type of event.
        /// </summary>
        /// <param name="eventType">The type of event.</param>
        /// <param name="listener">The delegate to locate.</param>
        /// <returns>
        /// <c>true</c> if a listener of the specified type of event is registered; <c>false</c> otherwise.
        /// </returns>
        public bool HasEventListener(string eventType, Action<Event> listener)
        {
            return (listeners.ContainsKey(eventType) && listeners[eventType].Contains(listener))
                || (pendingListeners.ContainsKey(eventType) && pendingListeners[eventType].Contains(listener));
        }

        public void RemoveAllEventListeners()
        {
            lock (syncRoot)
            {
                if (pendingFlag)
                {
                }
                else
                {
                }
            }
        }

        public void RemoveEventListener(string eventType, Action<Event> listener)
        {
            lock (syncRoot)
            {
            }
        }

        public void RemoveEventListeners(string eventType)
        {
            lock (syncRoot)
            {
            }
        }

        public void Update()
        {
            lock (syncRoot)
            {
            }
        }

        #endregion Interface IThreadEventDispatcher
    }
}