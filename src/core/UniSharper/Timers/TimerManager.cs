﻿/*
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

using ReSharp.Patterns;
using System;
using UniRx;
using UniSharper.Patterns;
using UnityEngine;

namespace UniSharper.Timers
{
    /// <summary>
    /// The <see cref="TimerManager"/> is a convenience class for managing all <see cref="ITimer"/> s
    /// at runtime. This class cannot be inherited.
    /// </summary>
    /// <seealso cref="SingletonMonoBehaviour{TimerManager}"/>
    /// <seealso cref="ITimerList"/>
    public sealed class TimerManager : Singleton<TimerManager>
    {
        #region Fields

        private ITimerList timerList;

        #endregion Fields

        #region Constructors

        private TimerManager()
        {
            Observable.EveryUpdate().Subscribe(Update);

            try
            {
                MainThreadDispatcher.OnApplicationQuitAsObservable().Subscribe(OnApplicationQuit);
                MainThreadDispatcher.OnApplicationPauseAsObservable().Subscribe(OnApplicationPause);
            }
            catch (Exception)
            {
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the number of <see cref="ITimer"/> elements contained in the <see cref="TimerManager"/>.
        /// </summary>
        /// <value>The number of <see cref="ITimer"/> elements contained in the <see cref="TimerManager"/>.</value>
        public int Count
        {
            get
            {
                if (timerList != null)
                {
                    return timerList.Count;
                }

                return 0;
            }
        }

        /// <summary>
        /// Gets the timer list.
        /// </summary>
        /// <value>The timer list.</value>
        private ITimerList TimerList
        {
            get
            {
                if (timerList == null)
                {
                    timerList = new TimerGroup();
                }

                return timerList;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds an <see cref="ITimer"/> item.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to be added.</param>
        public void Add(ITimer timer)
        {
            TimerList.Add(timer);
        }

        /// <summary>
        /// Removes all <see cref="ITimer"/> items.
        /// </summary>
        public void Clear()
        {
            TimerList.Clear();
        }

        /// <summary>
        /// Determines whether the <see cref="TimerManager"/> contains a specific <see cref="ITimer"/>.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to locate in the <see cref="TimerManager"/>.</param>
        /// <returns>
        /// <c>true</c> if <see cref="ITimer"/> item is found in the <see cref="TimerManager"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(ITimer timer)
        {
            return TimerList.Contains(timer);
        }

        /// <summary>
        /// Pauses all timers in the <see cref="TimerManager"/>.
        /// </summary>
        public void PauseAll()
        {
            TimerList.PauseAll();
        }

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="TimerManager"/>.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to be removed.</param>
        /// <returns>
        /// <c>true</c> if item was successfully removed; otherwise, <c>false</c>. This method also
        /// returns <c>false</c> if item is not found in the <see cref="TimerManager"/>.
        /// </returns>
        public bool Remove(ITimer timer)
        {
            return TimerList.Remove(timer);
        }

        /// <summary>
        /// Resets all timers in the <see cref="TimerManager"/>.
        /// </summary>
        public void ResetAll()
        {
            TimerList.ResetAll();
        }

        /// <summary>
        /// Resumes all timers in <see cref="TimerManager"/>.
        /// </summary>
        public void ResumeAll()
        {
            TimerList.ResumeAll();
        }

        /// <summary>
        /// Starts all timers in the <see cref="TimerManager"/>.
        /// </summary>
        public void StartAll()
        {
            TimerList.StartAll();
        }

        /// <summary>
        /// Stops all timers in the <see cref="TimerManager"/>.
        /// </summary>
        public void StopAll()
        {
            TimerList.StopAll();
        }

        /// <summary>
        /// This function is called when the application pauses.
        /// </summary>
        /// <param name="pauseStatus"><c>true</c> if the application is paused, else <c>false</c>.</param>
        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
            {
                // Pause all timers.
                TimerList.PauseAll(true);
            }
            else
            {
                // Resume all timers.
                ResumeAll();
            }
        }

        /// <summary>
        /// This function is called when the application quit.
        /// </summary>
        private void OnApplicationQuit(Unit unit)
        {
            timerList = null;
        }

        /// <summary>
        /// Update is called every frame.
        /// </summary>
        private void Update(long frameCount)
        {
            if (timerList != null)
            {
                timerList.ForEach((timer) =>
                {
                    float deltaTime = timer.IgnoreTimeScale ? Time.unscaledDeltaTime : Time.deltaTime;

                    try
                    {
                        timer.Tick(deltaTime);
                    }
                    catch (Exception exception)
                    {
                        Debug.LogException(exception);
                    }
                });
            }
        }

        #endregion Methods
    }
}