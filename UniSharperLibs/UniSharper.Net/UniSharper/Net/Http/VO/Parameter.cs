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

namespace UniSharper.Net.Http.VO
{
    /// <summary>
    /// Parameter container for requests
    /// </summary>
    public class Parameter
    {
        #region Properties

        /// <summary>
        /// Gets or sets the name of the parameter.
        /// </summary>
        /// <value>The name of the parameter.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the parameter.
        /// </summary>
        /// <value>A value of the <see cref="ParameterType"/>.</value>
        public ParameterType Type { get; set; }

        /// <summary>
        /// Gets or sets the value of the parameter.
        /// </summary>
        /// <value>The value of the parameter.</value>
        public string Value { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Returns a <see cref="string"/> that represents this instance.
        /// </summary>
        /// <returns>A <see cref="string"/> that represents this instance.</returns>
        public override string ToString()
        {
            return string.Format("{0}={1}, Type={2}", Name, Value, Type);
        }

        #endregion Methods
    }
}