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
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace UniSharper.Configuration
{
    /// <summary>
    /// Base class for file based <see cref="ConfigurationProvider"/>.
    /// </summary>
    /// <seealso cref="ConfigurationProvider"/>
    public abstract class FileConfigurationProvider : ConfigurationProvider
    {
        /// <summary>
        /// Initializes a new instance with the specified source.
        /// </summary>
        /// <param name="source">The source settings.</param>
        public FileConfigurationProvider(FileConfigurationSource source)
            : base()
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Source = source;
        }

        /// <summary>
        /// The source settings for this provider.
        /// </summary>
        public FileConfigurationSource Source { get; }

        /// <summary>
        /// Loads the contents of the file at the full path.
        /// </summary>
        /// <exception cref="System.NullReferenceException"><c>FullPath</c> is <c>null</c>.</exception>
        /// <exception cref="System.IO.FileNotFoundException">
        /// If Optional is <c>false</c> on the source and a file does not exist at specified Path.
        /// </exception>
        public override void Load()
        {
            if (string.IsNullOrEmpty(Source.FullPath))
            {
                throw new NullReferenceException(string.Format("{0} is null.", nameof(Source.FullPath)));
            }

            if (!File.Exists(Source.FullPath))
            {
                if (Source.Optional)
                {
                    Data = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                }
                else
                {
                    StringBuilder error = new StringBuilder($"The configuration file '{Source.FullPath}' was not found and is not optional.");
                    throw new FileNotFoundException(error.ToString());
                }
            }
            else
            {
                FileStream stream = null;

                try
                {
                    using (stream = File.OpenRead(Source.FullPath))
                    {
                        Load(stream);
                    }
                }
                catch (Exception e)
                {
                    bool ignoreException = false;

                    if (Source.OnLoadException != null)
                    {
                        var exceptionContext = new FileLoadExceptionContext
                        {
                            Provider = this,
                            Exception = e
                        };
                        Source.OnLoadException.Invoke(exceptionContext);
                        ignoreException = exceptionContext.Ignore;
                    }

                    if (!ignoreException)
                    {
                        throw e;
                    }
                }
                finally
                {
                    if (stream != null)
                    {
                        stream.Close();
                        stream = null;
                    }
                }
            }
        }

        /// <summary>
        /// Loads this provider's data from a stream.
        /// </summary>
        /// <param name="stream">The stream to read.</param>
        public abstract void Load(FileStream stream);
    }
}