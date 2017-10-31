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
using System.Net;
using System.Text;

namespace UniSharper.Net.Http
{
    /// <summary>
    /// A base class representing an HTTP entity body and content headers.
    /// </summary>
    public abstract class HttpMessageEntity
    {
        private WebHeaderCollection headers;

        private byte[] data;

        private Encoding encoding = Encoding.UTF8;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMessageEntity"/> class.
        /// </summary>
        internal HttpMessageEntity()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMessageEntity"/> class with the <see
        /// cref="Encoding"/> of the entity.
        /// </summary>
        /// <param name="encoding">The <see cref="Encoding"/> of the entity.</param>
        /// <exception cref="ArgumentNullException"><c>encoding</c> is <c>null</c>.</exception>
        internal HttpMessageEntity(Encoding encoding)
            : this()
        {
            if (encoding == null)
            {
                throw new ArgumentNullException(nameof(encoding));
            }

            Encoding = encoding;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the <see cref="Encoding"/> of the entity.
        /// </summary>
        /// <value>The <see cref="Encoding"/> of the entity.</value>
        protected Encoding Encoding
        {
            get
            {
                return encoding;
            }

            set
            {
                if (value != null)
                {
                    encoding = value;
                }
            }
        }

        /// <summary>
        /// Gets or sets the headers of the entity.
        /// </summary>
        /// <value>The headers of the entity.</value>
        public WebHeaderCollection Headers
        {
            get
            {
                if (headers == null)
                {
                    headers = new WebHeaderCollection();
                }

                return headers;
            }

            protected set
            {
                if (value != null)
                {
                    headers = value;
                }
            }
        }

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
        /// Gets the bytes from data interpreted as a string.
        /// </summary>
        /// <value>The bytes from data interpreted as a string.</value>
        public string Text
        {
            get
            {
                if (data != null)
                {
                    return encoding.GetString(data);
                }

                return null;
            }
        }

        #endregion Properties
    }
}