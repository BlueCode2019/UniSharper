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
    /// Represents a HTTP request message.
    /// </summary>
    /// <seealso cref="IDisposable"/>
    public class HttpRequestMessage : IDisposable
    {
        private Uri requestUri;

        private HttpUrlQuery urlQuery;

        private HttpMethod method;

        private WebHeaderCollection headers;

        private HttpRequestMessageEntity entity;

        private Version version;

        private bool disposed;

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestMessage"/> class.
        /// </summary>
        public HttpRequestMessage()
        {
            headers = new WebHeaderCollection();
            version = HttpUtility.DefaultVersion;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestMessage"/> class with a requested
        /// <see cref="Uri"/> and the HTTP method.
        /// </summary>
        /// <param name="requestUri">A <see cref="Uri"/> containing the URI of the requested resource.</param>
        /// <param name="method">The HTTP method.</param>
        /// <exception cref="ArgumentNullException"><c>requestUri</c> is <c>null</c>.</exception>
        public HttpRequestMessage(Uri requestUri, HttpMethod method = HttpMethod.Get)
            : this()
        {
            if (requestUri == null)
            {
                throw new ArgumentNullException(nameof(requestUri));
            }

            this.requestUri = requestUri;
            this.method = method;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestMessage"/> class with a reuqested
        /// <see cref="Uri"/>, a <see cref="HttpUrlQuery"/> representing URL query and the HTTP method.
        /// </summary>
        /// <param name="requestUri">A <see cref="Uri"/> containing the URI of the requested resource.</param>
        /// <param name="query">A <see cref="HttpUrlQuery"/> representing URL query.</param>
        /// <param name="method">The HTTP method.</param>
        /// <exception cref="ArgumentNullException"><c>query</c> is <c>null</c>.</exception>
        public HttpRequestMessage(Uri requestUri, HttpUrlQuery query, HttpMethod method = HttpMethod.Get)
            : this(requestUri, method)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            UrlQuery = query;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestMessage"/> class with a requested
        /// URI string and the HTTP method.
        /// </summary>
        /// <param name="requestUriString">A URI string that identifies the Internet resource.</param>
        /// <param name="method">The HTTP method.</param>
        public HttpRequestMessage(string requestUriString, HttpMethod method = HttpMethod.Get)
            : this(new Uri(requestUriString), method)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestMessage"/> class with a requested
        /// URI string, a <see cref="HttpUrlQuery"/> representing URL query and the HTTP method.
        /// </summary>
        /// <param name="requestUriString">A URI string that identifies the Internet resource.</param>
        /// <param name="query">A <see cref="HttpUrlQuery"/> representing URL query.</param>
        /// <param name="method">The HTTP method.</param>
        public HttpRequestMessage(string requestUriString, HttpUrlQuery query, HttpMethod method = HttpMethod.Get)
            : this(new Uri(requestUriString), query, method)
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the request URI.
        /// </summary>
        /// <value>The request URI.</value>
        public Uri RequestUri
        {
            get
            {
                return requestUri;
            }
            set
            {
                if (value != null && value.IsAbsoluteUri && !HttpUtility.IsHttpUri(value))
                {
                    throw new ArgumentException("Only 'http' and 'https' schemes are allowed.", nameof(value));
                }

                CheckDisposed();
                requestUri = value;
            }
        }

        /// <summary>
        /// Gets or sets the query information included in the URI.
        /// </summary>
        /// <value>The query information included in the URI.</value>
        public HttpUrlQuery UrlQuery
        {
            get
            {
                return urlQuery;
            }

            set
            {
                CheckDisposed();

                if (value != null)
                {
                    urlQuery = value;
                    string queryString = urlQuery.ToString();

                    if (!string.IsNullOrEmpty(queryString))
                    {
                        UriBuilder builder = new UriBuilder(requestUri);
                        builder.Query = queryString;
                        requestUri = builder.Uri;
                    }
                }
            }
        }

        /// <summary>
        /// Gets or sets the <see cref="HttpMethod"/> for the request.
        /// </summary>
        /// <value>The <see cref="HttpMethod"/> to use to contact the Internet resource.</value>
        public HttpMethod Method
        {
            get
            {
                return method;
            }
            set
            {
                CheckDisposed();
                method = value;
            }
        }

        /// <summary>
        /// Gets the collection of the name/value pairs that make up the HTTP headers.
        /// </summary>
        /// <value>
        /// A <see cref="WebHeaderCollection"/> that contains the name/value pairs that make up the
        /// headers for the HTTP request.
        /// </value>
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
        }

        /// <summary>
        /// Gets or sets the content of a HTTP request message.
        /// </summary>
        /// <value>The entity of the HTTP request message.</value>
        public HttpRequestMessageEntity Entity
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
        /// Gets the a URI string that identifies the Internet resource.
        /// </summary>
        /// <value>A URI string that identifies the Internet resource.</value>
        public string RequestUriString
        {
            get
            {
                return RequestUri.AbsoluteUri;
            }
        }

        /// <summary>
        /// Gets or sets the method of <see cref="string"/> for the request.
        /// </summary>
        /// <value>
        /// The request method to use to contact the Internet resource. The default value is GET.
        /// </value>
        internal string MethodString
        {
            get
            {
                return Method.ToString().ToUpper();
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Adds a header with the specified name and value.
        /// </summary>
        /// <param name="name">The specified header to add.</param>
        /// <param name="value">The content of the header.</param>
        public void AddHeader(string name, string value)
        {
            Headers.Add(name, value);
        }

        /// <summary>
        /// Adds the specified header with the specified value.
        /// </summary>
        /// <param name="header">The specified <see cref="HttpRequestHeader"/> to add.</param>
        /// <param name="value">The content of the header.</param>
        public void AddHeader(HttpRequestHeader header, string value)
        {
            Headers.Add(header, value);
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("Method: ");
            stringBuilder.Append(method);
            stringBuilder.Append(", RequestUri: '");
            stringBuilder.Append((requestUri == null) ? "<null>" : requestUri.ToString());
            stringBuilder.Append("', Version: ");
            stringBuilder.Append(version);
            stringBuilder.Append(", Content: ");
            stringBuilder.Append((entity == null) ? "<null>" : entity.GetType().FullName);
            stringBuilder.Append(", Headers:\r\n");
            stringBuilder.Append(HttpUtility.DumpHeaders(headers));
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Releases the unmanaged resources and disposes of the managed resources used by the <see cref="HttpRequestMessage"/>.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion Public Methods

        /// <summary>
        /// Releases the unmanaged resources used by the <see cref="HttpRequestMessage"/> and
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
        /// Throws an <see cref="ObjectDisposedException"/> if the <see cref="HttpRequestMessage"/>
        /// is in the disposed state.
        /// </summary>
        /// <exception cref="ObjectDisposedException">
        /// Cannot access a disposed object. Object name: 'ReSharp.Net.Http.HttpRequestMessage'.
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