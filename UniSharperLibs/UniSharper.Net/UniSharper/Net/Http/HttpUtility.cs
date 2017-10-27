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
    /// Provides utility methods to help deal with HTTP.
    /// </summary>
    internal static class HttpUtility
    {
        /// <summary>
        /// The default version for HTTP.
        /// </summary>
        internal static readonly Version DefaultVersion = HttpVersion.Version11;

        /// <summary>
        /// Determines whether the <see cref="Uri"/> is URI for HTTP.
        /// </summary>
        /// <param name="uri">The <see cref="Uri"/> to check.</param>
        /// <returns><c>true</c> if the <see cref="Uri"/> is URI for HTTP; otherwise, <c>false</c>.</returns>
        internal static bool IsHttpUri(Uri uri)
        {
            string scheme = uri.Scheme;
            return string.Compare("http", scheme, StringComparison.OrdinalIgnoreCase) == 0 || string.Compare("https", scheme, StringComparison.OrdinalIgnoreCase) == 0;
        }

        /// <summary>
        /// Dumps the headers.
        /// </summary>
        /// <param name="headers">The headers.</param>
        /// <returns>The string format of headers.</returns>
        internal static string DumpHeaders(WebHeaderCollection headers)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("{\r\n");

            foreach (string key in headers)
            {
                stringBuilder.Append("  ");
                stringBuilder.Append(key);
                stringBuilder.Append(": ");
                stringBuilder.Append(headers[key]);
                stringBuilder.Append("\r\n");
            }

            stringBuilder.Append('}');
            return stringBuilder.ToString();
        }
    }
}