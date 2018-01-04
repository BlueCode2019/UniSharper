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

using UniSharper.Timers;

namespace UniSharperEditor.Timers
{
    /// <summary>
    /// Base implementation of interface <see cref="ITimer"/> for Editor.
    /// </summary>
    /// <seealso cref="Timer"/>
    internal class EditorTimer : Timer
    {
        private bool disposed;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditorTimer"/> class.
        /// </summary>
        /// <param name="interval">
        /// The time, in seconds, between <see cref="E:UniSharper.Timers.Timer.Ticking"/> events.
        /// </param>
        /// <param name="repeatCount">The repeat count.</param>
        /// <param name="autoStart">
        /// if set to <c>true</c> invoke the method <see cref="M:UniSharper.Timers.Timer.Start"/> automatically.
        /// </param>
        public EditorTimer(float interval, uint repeatCount = 0, bool autoStart = true)
            : base(interval, repeatCount, true, autoStart)
        {
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected override void Initialize()
        {
            EditorTimerManager.Instance.Add(this);
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        /// unmanaged resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            if (!disposed && disposing)
            {
                EditorTimerManager.Instance.Remove(this);
            }

            disposed = true;
            TimerState = TimerState.Stop;
        }
    }
}