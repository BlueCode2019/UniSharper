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
    /// Extension methods for <see cref="FileConfigurationProvider"/>.
    /// </summary>
    public static class FileConfigurationExtensions
    {
        #region Fields

        private const string BasePath = "BasePath";

        private const string FileLoadExceptionHandlerKey = "FileLoadExceptionHandler";

        #endregion Fields

        #region Methods

        /// <summary>
        /// Gets the base path of configuration files.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.ArgumentNullException"><c>builder</c> is <c>null</c>.</exception>
        public static string GetBasePath(this IConfigurationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (builder.Properties.ContainsKey(BasePath))
            {
                return builder.Properties[BasePath] as string;
            }

            return null;
        }

        /// <summary>
        /// Gets a default action to be invoked for file-based providers when an error occurs.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/>.</param>
        /// <returns>The Action to be invoked on a file load exception.</returns>
        /// <exception cref="System.ArgumentNullException"><c>builder</c> is <c>null</c>.</exception>
        public static Action<FileLoadExceptionContext> GetFileLoadExceptionHandler(this IConfigurationBuilder builder)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (builder.Properties.ContainsKey(FileLoadExceptionHandlerKey))
            {
                return builder.Properties[FileLoadExceptionHandlerKey] as Action<FileLoadExceptionContext>;
            }

            return null;
        }

        /// <summary>
        /// Sets the base path of configuration files.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="basePath">The absolute path.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <para><c>builder</c> is <c>null</c>.</para>
        /// <para>-or-</para>
        /// <para><c>basePath</c> is <c>null</c>.</para>
        /// </exception>
        public static void SetBasePath(this IConfigurationBuilder builder, string basePath)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (basePath == null)
            {
                throw new ArgumentNullException(nameof(basePath));
            }

            builder.Properties[BasePath] = basePath;
        }

        /// <summary>
        /// Sets a default action to be invoked for file-based providers when an error occurs.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="handler">The Action to be invoked on a file load exception.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        /// <exception cref="System.ArgumentNullException"><c>builder</c> is <c>null</c>.</exception>
        public static IConfigurationBuilder SetFileLoadExceptionHandler(this IConfigurationBuilder builder, Action<FileLoadExceptionContext> handler)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            builder.Properties[FileLoadExceptionHandlerKey] = handler;
            return builder;
        }

        #endregion Methods
    }
}