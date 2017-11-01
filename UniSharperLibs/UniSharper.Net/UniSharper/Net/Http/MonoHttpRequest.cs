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
using System.IO;
using System.Net;
using UniSharper.Threading;

namespace UniSharper.Net.Http
{
    /// <summary>
    /// Provides a base class for sending HTTP requests and receiving HTTP responses from a resource
    /// identified by a URI in Mono environment.
    /// </summary>
    /// <seealso cref="ISyncObject"/>
    /// <seealso cref="IDisposable"/>
    public class MonoHttpRequest : ISyncObject, IDisposable
    {
        private bool disposed;

        private HttpWebRequest httpWebRequest;

        private HttpRequestMessage requestMessage;

        private HttpWebResponse httpWebResponse;

        private HttpResponseMessage responseMessage;

        private Exception exceptionCaught;

        private Action<HttpResponseMessage> responseCallback;

        private Action<Exception> catchExceptionCallback;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="MonoHttpRequest"/> class with <see cref="HttpRequestMessage"/>.
        /// </summary>
        /// <param name="requestMessage">The request message.</param>
        /// <param name="proxy">The proxy information for the request.</param>
        /// <exception cref="ArgumentNullException"><c>requestMessage</c> is <c>null</c>.</exception>
        public MonoHttpRequest(HttpRequestMessage requestMessage, IWebProxy proxy = null)
        {
            if (requestMessage == null)
            {
                throw new ArgumentNullException(nameof(requestMessage));
            }

            this.requestMessage = requestMessage;
            httpWebRequest = (HttpWebRequest)WebRequest.Create(requestMessage.RequestUri);
            httpWebRequest.Method = requestMessage.MethodString;
            httpWebRequest.Proxy = proxy;

            Initialize();
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the message for sending the HTTP request.
        /// </summary>
        /// <value>The message for sending the HTTP request.</value>
        protected HttpRequestMessage HttpRequestMessage
        {
            get
            {
                return requestMessage;
            }
        }

        /// <summary>
        /// Gets the <see cref="HttpWebRequest"/> for sending the HTTP request.
        /// </summary>
        /// <value>The <see cref="HttpWebRequest"/> for sending the HTTP request.</value>
        protected HttpWebRequest HttpWebRequest
        {
            get
            {
                return httpWebRequest;
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="HttpWebResponse"/> of the HTTP request returned.
        /// </summary>
        /// <value>The <see cref="HttpWebResponse"/> of the HTTP request returned.</value>
        protected HttpWebResponse HttpWebResponse
        {
            get
            {
                return httpWebResponse;
            }

            set
            {
                httpWebResponse = value;
            }
        }

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

        #region Static Methods

        /// <summary>
        /// Creates and sends a GET request.
        /// </summary>
        /// <param name="requestUriString">A URI string that identifies the Internet resource.</param>
        /// <param name="query">A <see cref="HttpUrlQuery"/> representing URL query.</param>
        /// <returns>
        /// <see cref="HttpResponseMessage"/> containing all information about HTTP response.
        /// </returns>
        public static HttpResponseMessage Get(string requestUriString, HttpUrlQuery query = null)
        {
            HttpRequestMessage reqMessage = new HttpRequestMessage(requestUriString, query);

            using (MonoHttpRequest request = new MonoHttpRequest(reqMessage))
            {
                HttpResponseMessage respMessage = request.SendRequest();
                return respMessage;
            }
        }

        /// <summary>
        /// Send a GET request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="requestUriString">The request URI string.</param>
        /// <param name="query">A <see cref="HttpUrlQuery"/> representing URL query.</param>
        /// <param name="responseCallback">The response callback.</param>
        /// <param name="catchExceptionCallback">The catch exception callback.</param>
        /// <returns>The <see cref="MonoHttpRequest"/> to handle the HTTP request.</returns>
        public static MonoHttpRequest GetAsync(string requestUriString, HttpUrlQuery query = null,
            Action<HttpResponseMessage> responseCallback = null, Action<Exception> catchExceptionCallback = null)
        {
            HttpRequestMessage reqMessage = new HttpRequestMessage(requestUriString, query);
            MonoHttpRequest request = new MonoHttpRequest(reqMessage);
            request.SendRequestAsync(responseCallback, catchExceptionCallback);
            return request;
        }

        /// <summary>
        /// Creates and sends a POST request.
        /// </summary>
        /// <param name="requestUriString">A URI string that identifies the Internet resource.</param>
        /// <param name="formData">
        /// The <see cref="HttpFormData"/> representing the form data to POST to the server.
        /// </param>
        /// <returns>
        /// <see cref="HttpResponseMessage"/> containing all information about HTTP response.
        /// </returns>
        public static HttpResponseMessage Post(string requestUriString, HttpFormData formData = null)
        {
            HttpRequestMessage reqMessage = new HttpRequestMessage(requestUriString, HttpMethod.Post);
            reqMessage.Entity = new HttpRequestMessageEntity(formData);

            using (MonoHttpRequest request = new MonoHttpRequest(reqMessage))
            {
                HttpResponseMessage respMessage = request.SendRequest();
                return respMessage;
            }
        }

        /// <summary>
        /// Send a POST request to the specified Uri as an asynchronous operation.
        /// </summary>
        /// <param name="requestUriString">The request URI string.</param>
        /// <param name="formData">
        /// The <see cref="HttpFormData"/> representing the form data to POST to the server.
        /// </param>
        /// <param name="responseCallback">The response callback.</param>
        /// <param name="catchExceptionCallback">The catch exception callback.</param>
        /// <returns>The <see cref="MonoHttpRequest"/> to handle the HTTP request.</returns>
        public static MonoHttpRequest PostAsync(string requestUriString, HttpFormData formData = null,
            Action<HttpResponseMessage> responseCallback = null, Action<Exception> catchExceptionCallback = null)
        {
            HttpRequestMessage reqMessage = new HttpRequestMessage(requestUriString, HttpMethod.Post);
            reqMessage.Entity = new HttpRequestMessageEntity(formData);
            MonoHttpRequest request = new MonoHttpRequest(reqMessage);
            request.SendRequestAsync(responseCallback, catchExceptionCallback);
            return request;
        }

        #endregion Static Methods

        /// <summary>
        /// Synchronizes data between threads.
        /// </summary>
        public void Synchronize()
        {
            // Invoke the callback of getting response
            if (responseMessage != null && responseCallback != null)
            {
                responseCallback.Invoke(responseMessage);
                responseMessage = null;
                responseCallback = null;
                Synchronizer.Instance.Remove(this);
            }

            // Invoke the callback of catching exception
            if (exceptionCaught != null && catchExceptionCallback != null)
            {
                catchExceptionCallback.Invoke(exceptionCaught);
                exceptionCaught = null;
                catchExceptionCallback = null;
                Synchronizer.Instance.Remove(this);
            }
        }

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
        /// Sends the request asynchronously.
        /// </summary>
        /// <param name="responseCallback">The response callback.</param>
        /// <param name="catchExceptionCallback">The catch exception callback.</param>
        public void SendRequestAsync(Action<HttpResponseMessage> responseCallback, Action<Exception> catchExceptionCallback = null)
        {
            CheckDisposed();

            this.responseCallback = responseCallback;
            this.catchExceptionCallback = catchExceptionCallback;

            Synchronizer.Instance.Add(this);
            HttpWebRequest.BeginGetResponse(new AsyncCallback(GetResponseCallback), this);
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

                responseCallback = null;
                catchExceptionCallback = null;

                Synchronizer.Instance.Remove(this);

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
        protected virtual void Initialize()
        {
            KeepAlive = false;

            SetRequestHeaders();
            SetRequestMessageEntity();
        }

        /// <summary>
        /// Sets the headers of HTTP request.
        /// </summary>
        protected void SetRequestHeaders()
        {
            if (requestMessage.Headers.Count > 0)
            {
                foreach (string key in requestMessage.Headers.AllKeys)
                {
                    string value = requestMessage.Headers[key];

                    if (key.Equals("Content-Type"))
                    {
                        httpWebRequest.ContentType = value;
                    }
                    else
                    {
                        httpWebRequest.Headers.Add(key, value);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the message entity of the HTTP request.
        /// </summary>
        protected void SetRequestMessageEntity()
        {
            if (requestMessage.Entity != null && requestMessage.Entity.Data != null)
            {
                using (Stream requestStream = httpWebRequest.GetRequestStream())
                {
                    requestStream.Write(requestMessage.Entity.Data, 0, requestMessage.Entity.Data.Length);
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
        protected void CheckDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(GetType().FullName);
            }
        }

        /// <summary>
        /// The callback of getting response.
        /// </summary>
        /// <param name="ar">The <see cref="IAsyncResult"/> object.</param>
        private void GetResponseCallback(IAsyncResult ar)
        {
            try
            {
                HttpWebResponse = (HttpWebResponse)HttpWebRequest.EndGetResponse(ar);

                if (HttpWebResponse != null)
                {
                    HttpResponseMessage message = new HttpResponseMessage(HttpWebResponse.StatusCode);
                    message.Version = HttpWebResponse.ProtocolVersion;
                    message.RequestMessage = HttpRequestMessage;
                    message.Headers = HttpWebResponse.Headers;
                    message.Entity = new HttpResponseMessageEntity(HttpWebResponse.GetResponseStream(), HttpWebResponse.ContentLength);
                    responseMessage = message;
                }
            }
            catch (Exception ex)
            {
                exceptionCaught = ex;
            }
        }
    }
}