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

using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Internal;

namespace UniSharper.Net.Http
{
    /// <summary>
    /// Represents form data to post to web servers.
    /// </summary>
    public class HttpFormData
    {
        private WWWForm form;

        /// <summary>
        /// Initializes a new instance of the <see cref="HttpFormData"/> class.
        /// </summary>
        public HttpFormData()
        {
            form = new WWWForm();
        }

        #region Properties

        /// <summary>
        /// Gets the headers.
        /// </summary>
        /// <value>The headers.</value>
        public Dictionary<string, string> Headers
        {
            get
            {
                return form.headers;
            }
        }

        /// <summary>
        /// Gets the the correct request headers for posting the form.
        /// </summary>
        /// <value>The correct request headers.</value>
        public byte[] Data
        {
            get
            {
                return form.data;
            }
        }

        #endregion Properties

        #region Public Methods

        /// <summary>
        /// Adds a simple field to the form.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="value">The <see cref="string"/> value.</param>
        public void AddField(string fieldName, string value)
        {
            AddField(fieldName, value, Encoding.UTF8);
        }

        /// <summary>
        /// Adds a simple field to the form.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="value">The <see cref="string"/> value.</param>
        /// <param name="e">The <see cref="Encoding"/> of the <see cref="string"/> value.</param>
        public void AddField(string fieldName, string value, Encoding e)
        {
            form.AddField(fieldName, value, e);
        }

        /// <summary>
        /// Adds binary data to the form.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="contents">The binary data.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="mimeType">Type of the MIME.</param>
        public void AddBinaryData(string fieldName, byte[] contents, string fileName = null, string mimeType = null)
        {
            form.AddBinaryData(fieldName, contents, fileName, mimeType);
        }

        #endregion Public Methods
    }
}