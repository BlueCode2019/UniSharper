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
using System.Collections.Generic;

namespace UniSharper.Net.Http
{
    /// <summary>
    /// A class representing an HTTP request entity body and content headers.
    /// </summary>
    /// <seealso cref="RHttpMessageEntity"/>
    public class HttpRequestMessageEntity : HttpMessageEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestMessageEntity"/> class with the
        /// binary data content.
        /// </summary>
        /// <param name="data">The binary data content.</param>
        public HttpRequestMessageEntity(byte[] data)
        {
            Data = data;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpRequestMessageEntity"/> class with a
        /// list of <see cref="HttpFormItem"/>.
        /// </summary>
        /// <param name="formItems">A list of <see cref="HttpFormItem"/>.</param>
        /// <exception cref="ArgumentNullException"><c>formItems</c> is <c>null</c>.</exception>
        public HttpRequestMessageEntity(List<HttpFormItem> formItems)
        {
            if (formItems == null)
            {
                throw new ArgumentNullException(nameof(formItems));
            }
        }
    }
}