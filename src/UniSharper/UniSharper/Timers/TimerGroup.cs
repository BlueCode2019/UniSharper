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

namespace UniSharper.Timers
{
    /// <summary>
    /// Class used internally to store a group of <see cref="ITimer"/>.
    /// </summary>
    /// <seealso cref="ITimerList"/>
    public class TimerGroup : ITimerList
    {
        #region Fields

        private LinkedList<ITimer> timers;

        private bool isRemovingItem;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerGroup"/> class.
        /// </summary>
        public TimerGroup()
        {
            isRemovingItem = false;
            timers = new LinkedList<ITimer>();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TimerGroup"/> class.
        /// </summary>
        /// <param name="timers">The timers array.</param>
        public TimerGroup(params ITimer[] timers)
        {
            this.timers = new LinkedList<ITimer>(timers);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the number of <see cref="ITimer"/> contained in this <see cref="TimerGroup"/>.
        /// </summary>
        /// <value>The number of <see cref="ITimer"/> contained in this <see cref="TimerGroup"/>.</value>
        public int Count
        {
            get
            {
                return timers.Count;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds an <see cref="ITimer"/> item to this <see cref="TimerGroup"/>.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to add.</param>
        /// <exception cref="ArgumentNullException"><c>timer</c> is <c>null</c>.</exception>
        public void Add(ITimer timer)
        {
            if (timer == null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            timers.AddUnique(timer);
        }

        /// <summary>
        /// Removes all <see cref="ITimer"/> contained in this <see cref="TimerGroup"/>.
        /// </summary>
        public void Clear()
        {
            timers.Clear();
        }

        /// <summary>
        /// Determines whether the specified <see cref="ITimer"/> contained in this <see cref="TimerGroup"/>.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to locate.</param>
        /// <returns>
        /// <c>true</c> if <see cref="ITimer"/> is found in this <see cref="TimerGroup"/>; otherwise, <c>false</c>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><c>timer</c> is <c>null</c>.</exception>
        public bool Contains(ITimer timer)
        {
            if (timer == null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            return timers.Contains(timer);
        }

        /// <summary>
        /// Performs the specified action on each <see cref="ITimer"/> of the <see cref="TimerGroup"/>.
        /// </summary>
        /// <param name="action">
        /// The <see cref="Action{ITimer}"/> delegate to perform on each <see cref="ITimer"/> of the
        /// <see cref="TimerGroup"/>.
        /// </param>
        /// <exception cref="ArgumentNullException"><c>action</c> is <c>null</c>.</exception>
        public void ForEach(Action<ITimer> action)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action));
            }

            if (isRemovingItem)
            {
                return;
            }

            foreach (ITimer timer in timers)
            {
                if (timer != null)
                {
                    action.Invoke(timer);
                }
            }
        }

        /// <summary>
        /// Pauses all timers contained in the <see cref="TimerGroup"/>.
        /// </summary>
        public void PauseAll()
        {
            ForEach((timer) =>
            {
                timer.Pause();
            });
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="TimerGroup"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="TimerGroup"/>.</param>
        /// <returns>
        /// <c>true</c> if item was successfully removed from the <see cref="TimerGroup"/>;
        /// otherwise, <c>false</c>. This method also returns <c>false</c> if item is not found in
        /// the original <see cref="TimerGroup"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException"><c>timer</c> is <c>null</c>.</exception>
        public bool Remove(ITimer timer)
        {
            if (timer == null)
            {
                throw new ArgumentNullException(nameof(timer));
            }

            bool success = false;
            isRemovingItem = true;
            success = timers.Remove(timer);
            isRemovingItem = false;
            return success;
        }

        /// <summary>
        /// Resets all timers contained in the <see cref="TimerGroup"/>.
        /// </summary>
        public void ResetAll()
        {
            ForEach((timer) =>
            {
                timer.Reset();
            });
        }

        /// <summary>
        /// Resumes all timers contained in <see cref="TimerGroup"/>.
        /// </summary>
        public void ResumeAll()
        {
            ForEach((timer) =>
            {
                timer.Resume();
            });
        }

        /// <summary>
        /// Starts all timers contained in the <see cref="TimerGroup"/>.
        /// </summary>
        public void StartAll()
        {
            ForEach((timer) =>
            {
                timer.Start();
            });
        }

        /// <summary>
        /// Stops all timers contained in the <see cref="TimerGroup"/>.
        /// </summary>
        public void StopAll()
        {
            ForEach((timer) =>
            {
                timer.Stop();
            });
        }

        #endregion Methods
    }
}