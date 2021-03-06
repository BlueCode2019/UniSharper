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

using System;

namespace UniSharper.Timers
{
    /// <summary>
    /// Base implementation of interface <see cref="ITimer"/>.
    /// </summary>
    /// <seealso cref="ITimer"/>
    /// <seealso cref="IDisposable"/>
    public class Timer : ITimer, IDisposable
    {
        #region Fields

        private bool disposed;

        private float time;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Timer"/> class, and sets the Interval
        /// property to the specified number of seconds, the RepeatCount property, the
        /// IgnoreTimeScale property and a <see cref="bool"/> value to determine to invoke the method
        /// <see cref="Start"/> automatically.
        /// </summary>
        /// <param name="interval">The time, in seconds, between <see cref="Ticking"/> events.</param>
        /// <param name="repeatCount">The repeat count.</param>
        /// <param name="ignoreTimeScale">A value indicating whether to ignore time scale of Unity.</param>
        /// <param name="autoStart">
        /// if set to <c>true</c> invoke the method <see cref="Start"/> automatically.
        /// </param>
        public Timer(float interval, uint repeatCount = 0, bool ignoreTimeScale = false, bool autoStart = true)
        {
            TimerState = TimerState.Stop;
            time = 0f;

            Interval = interval;
            RepeatCount = repeatCount;
            IgnoreTimeScale = ignoreTimeScale;

            Initialize();

            if (autoStart)
            {
                Start();
            }
        }

        #endregion Constructors

        #region Events

        /// <summary>
        /// Occurs when the timer completed, ticking count equals to the <see cref="RepeatCount"/>.
        /// </summary>
        public event TimerCompletedEventHandler Completed;

        /// <summary>
        /// Occurs when call the method <see cref="Pause"/>.
        /// </summary>
        public event TimerPausedEventHandler Paused;

        /// <summary>
        /// Occurs when call the method <see cref="Reset"/>.
        /// </summary>
        public event TimerResetedEventHandler Reseted;

        /// <summary>
        /// Occurs when call the method <see cref="Resume"/>.
        /// </summary>
        public event TimerResumedEventHandler Resumed;

        /// <summary>
        /// Occurs when call the method <see cref="Start"/>.
        /// </summary>
        public event TimerStartedEventHandler Started;

        /// <summary>
        /// Occurs when call the method <see cref="Stop"/>.
        /// </summary>
        public event TimerStoppedEventHandler Stopped;

        /// <summary>
        /// Occurs when the specified timer interval has elapsed.
        /// </summary>
        public event TimerTickingEventHandler Ticking;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets the current ticking count of <see cref="Timer"/>.
        /// </summary>
        /// <value>The current ticking count of <see cref="Timer"/>.</value>
        public uint CurrentCount
        {
            get;
            protected set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="Timer"/> ignore time scale of Unity.
        /// </summary>
        /// <value><c>true</c> if ignore time scale of Unity; otherwise, <c>false</c>.</value>
        public bool IgnoreTimeScale
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the time, in seconds, between <see cref="Ticking"/> events.
        /// </summary>
        /// <value>The time, in seconds, between <see cref="Ticking"/> events.</value>
        public float Interval
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the repeat count of <see cref="Timer"/>.
        /// </summary>
        /// <value>The repeat count of <see cref="Timer"/>.</value>
        public uint RepeatCount
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the state of the <see cref="Timer"/>.
        /// </summary>
        /// <value>The state of the <see cref="Timer"/>.</value>
        public TimerState TimerState
        {
            get;
            protected set;
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting
        /// unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Pauses timing.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><c>UniSharper.Timers.Timer</c> is disposed.</exception>
        public void Pause()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (TimerState != TimerState.Pause)
            {
                TimerState = TimerState.Pause;

                if (Paused != null)
                {
                    Paused.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Resets the state of <see cref="ITimer"/>.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><c>UniSharper.Timers.Timer</c> is disposed.</exception>
        public void Reset()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            Stop();

            CurrentCount = 0;
            time = 0f;

            if (Reseted != null)
            {
                Reseted.Invoke(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Resumes timing.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><c>UniSharper.Timers.Timer</c> is disposed.</exception>
        public void Resume()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (TimerState == TimerState.Pause)
            {
                TimerState = TimerState.Running;

                if (Resumed != null)
                {
                    Resumed.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Starts timing.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><c>UniSharper.Timers.Timer</c> is disposed.</exception>
        public void Start()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (TimerState != TimerState.Running)
            {
                TimerState = TimerState.Running;

                if (Started != null)
                {
                    Started.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Stops timing.
        /// </summary>
        /// <exception cref="ObjectDisposedException"><c>UniSharper.Timers.Timer</c> is disposed.</exception>
        public void Stop()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }

            if (TimerState != TimerState.Stop)
            {
                TimerState = TimerState.Stop;

                if (Stopped != null)
                {
                    Stopped.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Updates the time of timing by delta time.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        public void Tick(float deltaTime)
        {
            if (TimerState != TimerState.Running)
            {
                return;
            }

            time += deltaTime;

            if (time >= Interval)
            {
                CurrentCount++;

                // Raise Ticking event
                if (Ticking != null)
                {
                    Ticking.Invoke(this, new TimerTickingEventArgs(CurrentCount));
                }

                if (RepeatCount != 0 && CurrentCount >= RepeatCount)
                {
                    Reset();

                    if (Completed != null)
                    {
                        Completed.Invoke(this, EventArgs.Empty);
                    }
                }

                time = time - Interval;
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        /// unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                if (TimerManager.Instance)
                {
                    TimerManager.Instance.Remove(this);
                }
            }

            disposed = true;
            TimerState = TimerState.Stop;
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected virtual void Initialize()
        {
            if (TimerManager.Instance)
            {
                TimerManager.Instance.Add(this);
            }
        }

        #endregion Methods
    }
}