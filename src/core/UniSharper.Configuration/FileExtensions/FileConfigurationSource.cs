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

namespace UniSharper.Configuration
{
    /// <summary>
    /// Represents a base class for file based <see cref="IConfigurationSource"/>.
    /// </summary>
    /// <seealso cref="UniSharper.Configuration.IConfigurationSource"/>
    public abstract class FileConfigurationSource : IConfigurationSource
    {
        #region Properties

        /// <summary>
        /// Gets or sets the base path.
        /// </summary>
        /// <value>The base path.</value>
        public string BasePath { get; set; }

        /// <summary>
        /// The file name under the base path.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Will be called if an uncaught exception occurs in FileConfigurationProvider.Load.
        /// </summary>
        public Action<FileLoadExceptionContext> OnLoadException { get; set; }

        /// <summary>
        /// Determines if loading the file is optional.
        /// </summary>
        public bool Optional { get; set; }

        /// <summary>
        /// Gets the full path of the configuration file.
        /// </summary>
        /// <exception cref="System.NullReferenceException"><see cref="FileName"/> is <c>null</c>.</exception>
        public string FullPath
        {
            get
            {
                if (FileName == null)
                {
                    throw new NullReferenceException(string.Format("{0} is null.", nameof(FileName)));
                }

                return System.IO.Path.Combine(BasePath, FileName);
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Builds the <see cref="IConfigurationProvider"/> for this source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>A <see cref="IConfigurationProvider"/></returns>
        public abstract IConfigurationProvider Build(IConfigurationBuilder builder);

        /// <summary>
        /// Called to use any default settings on the builder like the FileProvider or FileLoadExceptionHandler.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        public void EnsureDefaults(IConfigurationBuilder builder)
        {
            BasePath = BasePath ?? builder.GetBasePath();
            OnLoadException = OnLoadException ?? builder.GetFileLoadExceptionHandler();
        }

        #endregion Methods
    }
}