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

namespace UniSharper.Timers
{
    /// <summary>
    /// Defines methods to manipulate timer object.
    /// </summary>
    public interface ITimer
    {
        #region Events

        /// <summary>
        /// Occurs when the timer completed, ticking count equals to the <see cref="RepeatCount"/>.
        /// </summary>
        event TimerCompletedEventHandler Completed;

        /// <summary>
        /// Occurs when call the method <see cref="Pause"/> of this <see cref="ITimer"/>.
        /// </summary>
        event TimerPausedEventHandler Paused;

        /// <summary>
        /// Occurs when call the method <see cref="Reset"/> of this <see cref="ITimer"/>.
        /// </summary>
        event TimerResetedEventHandler Reseted;

        /// <summary>
        /// Occurs when call the method <see cref="Resume"/> of this <see cref="ITimer"/>.
        /// </summary>
        event TimerResumedEventHandler Resumed;

        /// <summary>
        /// Occurs when call the method <see cref="Start"/> of this <see cref="ITimer"/>.
        /// </summary>
        event TimerStartedEventHandler Started;

        /// <summary>
        /// Occurs when call the method <see cref="Stop"/> of this <see cref="ITimer"/>.
        /// </summary>
        event TimerStoppedEventHandler Stopped;

        /// <summary>
        /// Occurs when the specified timer interval has elapsed.
        /// </summary>
        event TimerTickingEventHandler Ticking;

        #endregion Events

        #region Properties

        /// <summary>
        /// Gets the current ticking count of <see cref="ITimer"/>.
        /// </summary>
        /// <value>The current ticking count of <see cref="ITimer"/>.</value>
        uint CurrentCount { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the <see cref="ITimer"/> ignore time scale of Unity.
        /// </summary>
        /// <value><c>true</c> if ignore time scale of Unity; otherwise, <c>false</c>.</value>
        bool IgnoreTimeScale { get; set; }

        /// <summary>
        /// Gets or sets the time, in seconds, between <see cref="Ticking"/> events.
        /// </summary>
        /// <value>The time, in seconds, between <see cref="Ticking"/> events.</value>
        float Interval { get; set; }

        /// <summary>
        /// Gets or sets the repeat count of <see cref="ITimer"/>.
        /// </summary>
        /// <value>The repeat count of <see cref="ITimer"/>.</value>
        uint RepeatCount { get; set; }

        /// <summary>
        /// Gets the state of the <see cref="ITimer"/>.
        /// </summary>
        /// <value>The state of the <see cref="ITimer"/>.</value>
        TimerState TimerState { get; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Pauses timing.
        /// </summary>
        void Pause();

        /// <summary>
        /// Resets the state of <see cref="ITimer"/>.
        /// </summary>
        void Reset();

        /// <summary>
        /// Resumes timing.
        /// </summary>
        void Resume();

        /// <summary>
        /// Starts timing.
        /// </summary>
        void Start();

        /// <summary>
        /// Stops timing.
        /// </summary>
        void Stop();

        /// <summary>
        /// Updates the time of timing by delta time.
        /// </summary>
        /// <param name="deltaTime">The delta time.</param>
        void Tick(float deltaTime);

        #endregion Methods
    }
}