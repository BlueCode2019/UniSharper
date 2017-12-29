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

namespace UniSharper.Net.Http
{
    /// <summary>
    /// A helper class for retrieving and comparing standard HTTP methods and for creating new HTTP methods.
    /// </summary>
    /// <seealso cref="IEquatable{HttpMethod}"/>
    public class HttpMethod : IEquatable<HttpMethod>
    {
        #region Fields

        private static readonly HttpMethod deleteMethod = new HttpMethod("DELETE");

        private static readonly HttpMethod getMethod = new HttpMethod("GET");

        private static readonly HttpMethod headMethod = new HttpMethod("HEAD");

        private static readonly HttpMethod mergeMethod = new HttpMethod("MERGE");

        private static readonly HttpMethod optionsMethod = new HttpMethod("OPTIONS");

        private static readonly HttpMethod patchMethod = new HttpMethod("PATCH");

        private static readonly HttpMethod postMethod = new HttpMethod("POST");

        private static readonly HttpMethod putMethod = new HttpMethod("PUT");

        private string method;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpMethod"/> class with a specific HTTP method.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <exception cref="ArgumentException">The parameter 'method' cannot be null or empty.</exception>
        public HttpMethod(string method)
        {
            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentException(string.Format("The parameter '{0}' cannot be null or empty.", nameof(method)));
            }

            this.method = method;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Represents an HTTP DELETE protocol method.
        /// </summary>
        /// <value>Returns <see cref="HttpMethod"/>.</value>
        public static HttpMethod Delete
        {
            get
            {
                return deleteMethod;
            }
        }

        /// <summary>
        /// Represents an HTTP GET protocol method.
        /// </summary>
        /// <value>Returns <see cref="HttpMethod"/>.</value>
        public static HttpMethod Get
        {
            get
            {
                return getMethod;
            }
        }

        /// <summary>
        /// Represents an HTTP HEAD protocol method.
        /// </summary>
        /// <value>Returns <see cref="HttpMethod"/>.</value>
        public static HttpMethod Head
        {
            get
            {
                return headMethod;
            }
        }

        /// <summary>
        /// Represents an HTTP MERGE protocol method.
        /// </summary>
        /// <value>Returns <see cref="HttpMethod"/>.</value>
        public static HttpMethod Merge
        {
            get
            {
                return mergeMethod;
            }
        }

        /// <summary>
        /// Represents an HTTP OPTIONS protocol method.
        /// </summary>
        /// <value>Returns <see cref="HttpMethod"/>.</value>
        public static HttpMethod Options
        {
            get
            {
                return optionsMethod;
            }
        }

        /// <summary>
        /// Represents an HTTP PATCH protocol method.
        /// </summary>
        /// <value>Returns <see cref="HttpMethod"/>.</value>
        public static HttpMethod Patch
        {
            get
            {
                return patchMethod;
            }
        }

        /// <summary>
        /// Represents an HTTP POST protocol method.
        /// </summary>
        /// <value>Returns <see cref="HttpMethod"/>.</value>
        public static HttpMethod Post
        {
            get
            {
                return postMethod;
            }
        }

        /// <summary>
        /// Represents an HTTP PUT protocol method.
        /// </summary>
        /// <value>Returns <see cref="HttpMethod"/>.</value>
        public static HttpMethod Put
        {
            get
            {
                return putMethod;
            }
        }

        /// <summary>
        /// An HTTP method.
        /// </summary>
        /// <value>An HTTP method represented as a <see cref="string"/>.</value>
        public string Method
        {
            get
            {
                return method;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Indicates whether two <see cref="HttpMethod"/> objects are not equal.
        /// </summary>
        /// <param name="left">The <see cref="HttpMethod"/> to compare to right.</param>
        /// <param name="right">The <see cref="HttpMethod"/> to compare to left.</param>
        /// <returns><c>true</c> if <c>left</c> is not equal to <c>right</c>; otherwise, <c>false</c>.</returns>
        public static bool operator !=(HttpMethod left, HttpMethod right)
        {
            return !(left == right);
        }

        /// <summary>
        /// Indicates whether two <see cref="HttpMethod"/> objects are equal.
        /// </summary>
        /// <param name="left">The <see cref="HttpMethod"/> to compare to right.</param>
        /// <param name="right">The <see cref="HttpMethod"/> to compare to left.</param>
        /// <returns><c>true</c> if <c>left</c> is equal to <c>right</c>; otherwise, <c>false</c>.</returns>
        public static bool operator ==(HttpMethod left, HttpMethod right)
        {
            if (left == null)
            {
                return right == null;
            }
            if (right == null)
            {
                return left == null;
            }
            return left.Equals(right);
        }

        /// <summary>
        /// Indicates whether the <see cref="HttpMethod"/> is equal to another <see cref="HttpMethod"/>.
        /// </summary>
        /// <param name="other">A <see cref="HttpMethod"/> to compare with this <see cref="HttpMethod"/>.</param>
        /// <returns>
        /// <c>true</c> if the <see cref="HttpMethod"/> is equal to the other <see
        /// cref="HttpMethod"/>; otherwise, <c>false</c>.
        /// </returns>
        public bool Equals(HttpMethod other)
        {
            return other != null && (method == other.method || string.Compare(method, other.method, StringComparison.OrdinalIgnoreCase) == 0);
        }

        /// <summary>
        /// Returns a value indicating whether this instance is equal to a specified object.
        /// </summary>
        /// <param name="obj">An object to compare with this instance, or <c>null</c>.</param>
        /// <returns>
        /// <c>true</c> if obj is an instance of <see cref="HttpMethod"/> and equals the value of
        /// this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            return Equals(obj as HttpMethod);
        }

        /// <summary>
        /// Returns a hash code for this instance.
        /// </summary>
        /// <returns>
        /// A hash code for this instance, suitable for use in hashing algorithms and data structures
        /// like a hash table.
        /// </returns>
        public override int GetHashCode()
        {
            return method.ToUpperInvariant().GetHashCode();
        }

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            return method.ToString();
        }

        #endregion Methods
    }
}