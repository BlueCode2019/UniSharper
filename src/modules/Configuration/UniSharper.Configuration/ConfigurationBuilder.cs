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
    /// Used to build key/value based configuration settings for use in an application.
    /// </summary>
    /// <seealso cref="IConfigurationBuilder"/>
    public class ConfigurationBuilder : IConfigurationBuilder
    {
        #region Properties

        /// <summary>
        /// Gets a key/value collection that can be used to share data between the <see
        /// cref="IConfigurationBuilder"/> and the registered <see cref="IConfigurationSource"/> s.
        /// </summary>
        /// <value>The properties.</value>
        public IDictionary<string, object> Properties { get; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets the sources used to obtain configuration values
        /// </summary>
        /// <value>The sources.</value>
        public IList<IConfigurationSource> Sources { get; } = new List<IConfigurationSource>();

        #endregion Properties

        #region Methods

        /// <summary>
        /// Adds a new configuration source.
        /// </summary>
        /// <param name="source">The configuration source to add.</param>
        /// <returns>The same <see cref="IConfigurationBuilder"/>.</returns>
        /// <exception cref="System.ArgumentNullException"><c>source</c> is <c>null</c>.</exception>
        public IConfigurationBuilder Add(IConfigurationSource source)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            Sources.Add(source);
            return this;
        }

        /// <summary>
        /// Builds an <see cref="IConfiguration"/> with keys and values from the set of sources
        /// registered in <see cref="Sources"/>.
        /// </summary>
        /// <returns>
        /// An <see cref="IConfigurationRoot"/> with keys and values from the registered sources.
        /// </returns>
        public IConfigurationRoot Build()
        {
            IList<IConfigurationProvider> providers = new List<IConfigurationProvider>();

            foreach (IConfigurationSource source in Sources)
            {
                IConfigurationProvider provider = source.Build(this);
                providers.Add(provider);
            }
            return new ConfigurationRoot(providers);
        }

        #endregion Methods
    }
}