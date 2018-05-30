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

using ReSharp.Patterns;
using System;
using UniSharper.Timers;
using UnityEditor;
using UnityEngine;

namespace UniSharperEditor.Timers
{
    /// <summary>
    /// The <see cref="EditorTimerManager"/> is a convenience class for managing editor timers. This
    /// class cannot be inherited.
    /// </summary>
    /// <seealso cref="Singleton{EditorTimerManager}"/>
    [InitializeOnEditorStartup(int.MaxValue)]
    internal sealed class EditorTimerManager : Singleton<EditorTimerManager>
    {
        #region Fields

        private double lastTime = 0.0d;

        private ITimerList timerList;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes static members of the <see cref="EditorTimerManager"/> class.
        /// </summary>
        static EditorTimerManager()
        {
            Instance.Initialize();
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="EditorTimerManager"/> class from being created.
        /// </summary>
        private EditorTimerManager()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the number of <see cref="ITimer"/> contained in this <see cref="EditorTimerManager"/>.
        /// </summary>
        /// <value>The number of <see cref="ITimer"/> contained in this <see cref="EditorTimerManager"/>.</value>
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

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds an <see cref="ITimer"/> item to this <see cref="EditorTimerManager"/>.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to add.</param>
        public void Add(ITimer timer)
        {
            timerList.Add(timer);
        }

        /// <summary>
        /// Removes all <see cref="ITimer"/> contained in this <see cref="EditorTimerManager"/>.
        /// </summary>
        public void Clear()
        {
            timerList.Clear();
        }

        /// <summary>
        /// Determines whether the specified <see cref="ITimer"/> contained in this <see cref="EditorTimerManager"/>.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to locate.</param>
        /// <returns>
        /// <c>true</c> if <see cref="ITimer"/> is found in this <see cref="EditorTimerManager"/>;
        /// otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(ITimer timer)
        {
            return timerList.Contains(timer);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            lastTime = EditorApplication.timeSinceStartup;
            timerList = new TimerGroup();
            EditorApplication.update += OnEditorUpdate;
        }

        /// <summary>
        /// Removes the first occurrence of a specific <see cref="ITimer"/> from this <see cref="EditorTimerManager"/>.
        /// </summary>
        /// <param name="timer">The <see cref="ITimer"/> to be removed.</param>
        /// <returns>
        /// <c>true</c> if <see cref="ITimer"/> was successfully removed from this <see
        /// cref="EditorTimerManager"/>; otherwise, <c>false</c>. This method also returns
        /// <c>false</c> if the specified <see cref="ITimer"/> is not found.
        /// </returns>
        public bool Remove(ITimer timer)
        {
            return timerList.Remove(timer);
        }

        /// <summary>
        /// Called when Unity editor update.
        /// </summary>
        private void OnEditorUpdate()
        {
            float deltaTime = (float)(EditorApplication.timeSinceStartup - lastTime);
            lastTime = EditorApplication.timeSinceStartup;

            if (timerList != null)
            {
                timerList.ForEach((timer) =>
                {
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