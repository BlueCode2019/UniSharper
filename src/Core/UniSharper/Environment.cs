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
    /// Provides information about, and means to manipulate, the current environment. This class
    /// cannot be inherited.
    /// </summary>
    public static class Environment
    {
        #region Fields

        /// <summary>
        /// Gets a <see cref="Version"/> object that describes the major, minor, build, and revision
        /// numbers of the library runtime.
        /// </summary>
        public static readonly Version Version = typeof(Environment).Assembly.GetName().Version;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the path to the StreamingAssets folder
        /// </summary>
        /// <value>The path to the StreamingAssets folder.</value>
        public static string StreamingAssetsPath
        {
            get
            {
                string path = null;

                switch (Application.platform)
                {
                    case RuntimePlatform.Android:
                        path = string.Format("jar:file:///{0}!/assets/", Application.dataPath);
                        break;

                    case RuntimePlatform.IPhonePlayer:
                        path = string.Format("file://{0}/Raw/", Application.dataPath);
                        break;

                    case RuntimePlatform.WindowsEditor:
                    case RuntimePlatform.WindowsPlayer:
                    default:
                        path = Application.streamingAssetsPath;
                        break;
                }

                return path;
            }
        }

        #endregion Properties
    }
}