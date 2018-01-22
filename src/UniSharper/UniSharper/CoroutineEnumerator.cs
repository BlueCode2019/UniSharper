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

using System.Collections;
using System.Collections.Generic;

namespace UniSharper
{
    /// <summary>
    /// Support a simple chain execution for <see cref="IEnumerator"/>.
    /// </summary>
    /// <seealso cref="IEnumerator"/>
    public class CoroutineEnumerator : IEnumerator
    {
        #region Fields

        private Queue<IEnumerator> coroutineQueue;
        private object current;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CoroutineEnumerator"/> class with some coroutines.
        /// </summary>
        /// <param name="coroutines">The coroutines.</param>
        public CoroutineEnumerator(params IEnumerator[] coroutines)
        {
            coroutineQueue = new Queue<IEnumerator>(coroutines);
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the element in the collection at the current position of the enumerator.
        /// </summary>
        /// <value>The element in the collection at the current position of the enumerator.</value>
        public object Current
        {
            get
            {
                return current;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Enqueues the specified coroutine.
        /// </summary>
        /// <param name="coroutine">The coroutine.</param>
        public void Enqueue(IEnumerator coroutine)
        {
            coroutineQueue.Enqueue(coroutine);
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection.
        /// </summary>
        /// <returns>
        /// <c>true</c> if the enumerator was successfully advanced to the next element; <c>false</c>
        /// if the enumerator has passed the end of the collection.
        /// </returns>
        public bool MoveNext()
        {
            while (coroutineQueue.Count > 0)
            {
                current = coroutineQueue.Dequeue();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the enumerator to its initial position, which is before the first element in the collection.
        /// </summary>
        public void Reset()
        {
            coroutineQueue.Clear();
        }

        #endregion Methods
    }
}