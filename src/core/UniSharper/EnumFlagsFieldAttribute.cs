/*
 *	The MIT License (MIT)
 *
 *	Copyright (c) 2018 Jerry Lee
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
using UnityEngine;

namespace UniSharper
{
    /// <summary>
    /// Represents the enum property with Flags attribute declaration.
    /// </summary>
    /// <seealso cref="PropertyAttribute"/>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public class EnumFlagsFieldAttribute : PropertyAttribute
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumFlagsFieldAttribute"/> class.
        /// </summary>
        public EnumFlagsFieldAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumFlagsFieldAttribute"/> class.
        /// </summary>
        /// <param name="label">The caption/label for the attribute.</param>
        public EnumFlagsFieldAttribute(string label)
            : this()
        {
            Label = label;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the caption/label for the attribute.
        /// </summary>
        /// <value>The caption/label for the attribute.</value>
        public string Label
        {
            get;
            private set;
        }

        #endregion Properties
    }
}