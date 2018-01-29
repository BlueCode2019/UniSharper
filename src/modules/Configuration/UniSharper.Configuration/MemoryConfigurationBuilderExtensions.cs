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

namespace UniSharper.Configuration
{
    /// <summary>
    /// IConfigurationBuilder extension methods for the MemoryConfigurationProvider.
    /// </summary>
    public static class MemoryConfigurationBuilderExtensions
    {
        #region Methods

        /// <summary>
        /// Adds the memory configuration provider to <paramref name="configurationBuilder"/>.
        /// </summary>
        /// <param name="configurationBuilder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        /// <exception cref="System.ArgumentNullException"><c>configurationBuilder</c> is <c>null</c>.</exception>
        public static IConfigurationBuilder AddInMemoryCollection(this IConfigurationBuilder configurationBuilder)
        {
            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            configurationBuilder.Add(new MemoryConfigurationSource());
            return configurationBuilder;
        }

        /// <summary>
        /// Adds the memory configuration provider to <paramref name="configurationBuilder"/>.
        /// </summary>
        /// <param name="configurationBuilder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="initialData">The data to add to memory configuration provider.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        /// <exception cref="System.ArgumentNullException"><c>configurationBuilder</c> is <c>null</c>.</exception>
        public static IConfigurationBuilder AddInMemoryCollection(
            this IConfigurationBuilder configurationBuilder,
            IEnumerable<KeyValuePair<string, string>> initialData)
        {
            if (configurationBuilder == null)
            {
                throw new ArgumentNullException(nameof(configurationBuilder));
            }

            configurationBuilder.Add(new MemoryConfigurationSource { InitialData = initialData });
            return configurationBuilder;
        }

        #endregion Methods
    }
}