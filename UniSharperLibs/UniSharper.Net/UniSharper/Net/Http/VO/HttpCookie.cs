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

namespace UniSharper.Net.Http.VO
{
    /// <summary>
    /// Representation of an HTTP cookie.
    /// </summary>
    public class HttpCookie
    {
        #region Properties

        public string Comment { get; set; }

        public Uri CommentUri { get; set; }

        public bool Discard { get; set; }

        public string Domain { get; set; }

        public bool Expired { get; set; }

        public DateTime Expires { get; set; }

        public bool HttpOnly { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }

        public string Port { get; set; }

        public bool Secure { get; set; }

        public DateTime TimeStamp { get; set; }

        public string Value { get; set; }

        public int Version { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("Comment={0}, CommentUri={1}, Discard={2}, Domain={3}, Expired={4}, Expires={5}, HttpOnly={6}, Name={7}, Path={8}, Port={9}, Secure={10}, TimeStamp={11}, Value={12}, Version={13}",
                Comment, CommentUri, Discard, Domain, Expired, Expires, HttpOnly, Name, Path, Port, Secure, TimeStamp, Value, Version);
        }

        #endregion Methods
    }
}