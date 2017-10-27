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

using System.Text;

namespace UniSharper.Net.Http
{
    /// <summary>
    /// A base class representing an HTTP entity body and content headers.
    /// </summary>
    public abstract class HttpMessageEntity
    {
        private byte[] data;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMessageEntity"/> class.
        /// </summary>
        internal HttpMessageEntity()
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the binary data of the message entity.
        /// </summary>
        /// <value>The binary data of the message entity.</value>
        public byte[] Data
        {
            get
            {
                return data;
            }

            protected set
            {
                data = value;
            }
        }

        /// <summary>
        /// Gets the bytes from data interpreted as a UTF8 string.
        /// </summary>
        /// <value>The bytes from data interpreted as a UTF8 string.</value>
        public string Text
        {
            get
            {
                if (data != null)
                {
                    return Encoding.UTF8.GetString(data);
                }

                return null;
            }
        }

        #endregion Properties
    }
}