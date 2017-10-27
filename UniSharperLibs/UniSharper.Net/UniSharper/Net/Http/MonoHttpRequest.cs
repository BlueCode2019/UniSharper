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
using System.Collections;
using System.IO;
using System.Net;

namespace UniSharper.Net.Http
{
    /// <summary>
    /// Provides a base class for sending HTTP requests and receiving HTTP responses from a resource
    /// identified by a URI in Mono environment.
    /// </summary>
    /// <seealso cref="IDisposable"/>
    public class MonoHttpRequest : IDisposable
    {
        private bool disposed;

        private HttpWebRequest httpWebRequest;

        private HttpRequestMessage requestMessage;

        private HttpWebResponse httpWebResponse;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoHttpRequest"/> class with <see cref="HttpRequestMessage"/>.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <exception cref="ArgumentNullException"><c>requestMessage</c> is <c>null</c>.</exception>
        public MonoHttpRequest(HttpRequestMessage requestMessage)
        {
            if (requestMessage == null)
            {
                throw new ArgumentNullException(nameof(requestMessage));
            }

            this.requestMessage = requestMessage;
            httpWebRequest = (HttpWebRequest)WebRequest.Create(requestMessage.RequestUri);
            Initialize();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets or sets a value that indicates whether to make a persistent connection to the
        /// Internet resource.
        /// </summary>
        /// <value>
        /// <c>true</c> if the request to the Internet resource should contain a Connection HTTP
        /// header with the value Keep-alive; otherwise, <c>false</c>. The default is <c>true</c>.
        /// </value>
        public bool KeepAlive
        {
            get
            {
                return httpWebRequest.KeepAlive;
            }
            set
            {
                CheckDisposed();
                httpWebRequest.KeepAlive = value;
            }
        }

        #endregion Properties

        #region Pubic Methods

        /// <summary>
        /// Sends the HTTP request.
        /// </summary>
        /// <param name="autoDispose">if set to <c>true</c> dispose this instance automatically.</param>
        /// <returns>
        /// <see cref="HttpResponseMessage"/> containing all information about HTTP response.
        /// </returns>
        public HttpResponseMessage SendRequest()
        {
            CheckDisposed();

            httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            HttpResponseMessage message = new HttpResponseMessage(httpWebResponse.StatusCode);
            message.Version = httpWebResponse.ProtocolVersion;
            message.RequestMessage = requestMessage;
            message.Headers = httpWebResponse.Headers;
            message.Entity = new HttpResponseMessageEntity(httpWebResponse.GetResponseStream(), httpWebResponse.ContentLength);
            return message;
        }

        /// <summary>
        /// Releases the unmanaged resources and disposes of the managed resources used by the <see cref="HttpRequestMessage"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Pubic Methods

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="MonoHttpRequest"/> and optionally
        /// disposes of the managed resources.
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

                if (requestMessage != null)
                {
                    requestMessage.Dispose();
                }

                if (httpWebResponse != null)
                {
                    httpWebResponse.Close();
                }

                if (httpWebRequest != null)
                {
                    httpWebRequest.Abort();
                }
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            if (httpWebRequest != null)
            {
                httpWebRequest.Method = requestMessage.MethodString;

                if (requestMessage.Headers.Count > 0)
                {
                    foreach (DictionaryEntry entry in requestMessage.Headers)
                    {
                        string key = entry.Key.ToString();
                        string value = entry.Value.ToString();
                        httpWebRequest.Headers.Add(key, value);
                    }
                }

                // Write the HTTP message entity
                if (requestMessage.Entity != null && requestMessage.Entity.Data != null)
                {
                    using (Stream requestStream = httpWebRequest.GetRequestStream())
                    {
                        requestStream.Write(requestMessage.Entity.Data, 0, requestMessage.Entity.Data.Length);
                    }
                }
            }
        }

        /// <summary>
        /// Throws an <see cref="ObjectDisposedException"/> if the <see cref="MonoHttpRequest"/> is
        /// in the disposed state.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// Cannot access a disposed object. Object name: 'ReSharp.Net.Http.MonoHttpRequest'.
        /// </exception>
        private void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }
    }
}