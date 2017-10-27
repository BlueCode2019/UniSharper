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
    /// Represents a HTTP response message including the status code and data.
    /// </summary>
    public class HttpResponseMessage : IDisposable
    {
        private HttpRequestMessage requestMessage;

        private WebHeaderCollection headers;

        private HttpStatusCode statusCode;

        private HttpResponseMessageEntity entity;

        private Version version;

        private string reasonPhrase;

        private bool disposed;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseMessage"/> class.
        /// </summary>
        public HttpResponseMessage()
        {
            version = HttpUtility.DefaultVersion;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpResponseMessage"/> class with a specific
        /// <see cref="HttpStatusCode"/>.
        /// </summary>
        /// <param name="statusCode">The status code of the HTTP response.</param>
        public HttpResponseMessage(HttpStatusCode statusCode)
            : this()
        {
            this.statusCode = statusCode;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets the HTTP message version.
        /// </summary>
        /// <value>The HTTP message version. The default is 1.1.</value>
        /// <exception cref="ArgumentNullException"><c>value</c> is <c>null</c>.</exception>
        public Version Version
        {
            get
            {
                return version;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                CheckDisposed();
                version = value;
            }
        }

        /// <summary>
        /// Gets or sets the content of a HTTP response message.
        /// </summary>
        /// <value>The entity of the HTTP response message.</value>
        public HttpResponseMessageEntity Entity
        {
            get
            {
                return entity;
            }
            set
            {
                CheckDisposed();
                entity = value;
            }
        }

        /// <summary>
        /// Gets or sets the status code of the HTTP response.
        /// </summary>
        /// <value>The status code of the HTTP response.</value>
        /// <exception cref="ArgumentOutOfRangeException"><c>value</c> is out of range.</exception>
        public HttpStatusCode StatusCode
        {
            get
            {
                return statusCode;
            }
            set
            {
                if (value < 0 || value > (HttpStatusCode)999)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                CheckDisposed();
                statusCode = value;
            }
        }

        /// <summary>
        /// Gets or sets the reason phrase which typically is sent by servers together with the
        /// status code.
        /// </summary>
        /// <value>The reason phrase sent by the server.</value>
        /// <exception cref="FormatException">The reason phrase must not contain new-line characters.</exception>
        public string ReasonPhrase
        {
            get
            {
                if (reasonPhrase != null)
                {
                    return reasonPhrase;
                }

                return HttpStatusDescription.Get(StatusCode);
            }
            set
            {
                if (value != null && ContainsNewLineCharacter(value))
                {
                    throw new FormatException("The reason phrase must not contain new-line characters.");
                }

                CheckDisposed();
                reasonPhrase = value;
            }
        }

        /// <summary>
        /// Gets the collection of HTTP response headers.
        /// </summary>
        /// <value>The collection of HTTP response headers.</value>
        /// <exception cref="ArgumentNullException"><c>value</c> is <c>null</c>.</exception>
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

            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException(nameof(value));
                }

                CheckDisposed();
                headers = value;
            }
        }

        /// <summary>
        /// Gets or sets the request message which led to this response message.
        /// </summary>
        /// <value>The request message which led to this response message.</value>
        public HttpRequestMessage RequestMessage
        {
            get
            {
                return requestMessage;
            }
            set
            {
                CheckDisposed();
                requestMessage = value;
            }
        }

        /// <summary>
        /// Gets a value that indicates if the HTTP response was successful.
        /// </summary>
        /// <value>
        /// A value that indicates if the HTTP response was successful. <c>true</c> if <see
        /// cref="StatusCode"/> was in the range 200-299; otherwise <c>false</c>.
        /// </value>
        public bool IsSuccess
        {
            get
            {
                return statusCode >= HttpStatusCode.OK && statusCode <= (HttpStatusCode)299;
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("StatusCode: ");
            stringBuilder.Append((int)statusCode);
            stringBuilder.Append(", ReasonPhrase: '");
            stringBuilder.Append(ReasonPhrase ?? "<null>");
            stringBuilder.Append("', Version: ");
            stringBuilder.Append(version);
            stringBuilder.Append(", Content: ");
            stringBuilder.Append((entity == null) ? "<null>" : entity.GetType().FullName);
            stringBuilder.Append(", Headers:\r\n");
            stringBuilder.Append(HttpUtility.DumpHeaders(headers));
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Releases the unmanaged resources and disposes of the managed resources used by the <see cref="HttpResponseMessage"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="HttpResponseMessage"/> and
        /// optionally disposes of the managed resources.
        /// </summary>
        /// <param name="disposing">
        /// <c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only
        /// unmanaged resources.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing && !disposed)
            {
                disposed = true;
            }
        }

        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> if the <see cref="HttpResponseMessage"/>
        /// is in the disposed state.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// Cannot access a disposed object. Object name: 'ReSharp.Net.Http.HttpResponseMessage'.
        /// </exception>
        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        /// <summary>
        /// Determines whether the <see cref="string"/> contains new line character.
        /// </summary>
        /// <param name="value">The <see cref="string"/> to check.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="string"/> contains new line character; otherwise, <c>false</c>.
        /// </returns>
        private bool ContainsNewLineCharacter(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                char c = value[i];
                if (c == '\r' || c == '\n')
                {
                    return true;
                }
            }
            return false;
        }
    }
}