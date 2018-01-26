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
using System.Linq;

namespace UniSharper.Configuration
{
    /// <summary>
    /// The root node for a configuration.
    /// </summary>
    /// <seealso cref="UniSharper.Configuration.IConfigurationRoot"/>
    public class ConfigurationRoot : IConfigurationRoot
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationRoot"/> class with a list of providers.
        /// </summary>
        /// <param name="providers">The <see cref="IConfigurationProvider"/> s for this configuration.</param>
        public ConfigurationRoot(IList<IConfigurationProvider> providers)
        {
            if (providers == null)
            {
                throw new ArgumentNullException(nameof(providers));
            }

            Providers = providers;

            foreach (IConfigurationProvider provider in providers)
            {
                provider.Load();
            }
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// The <see cref="IConfigurationProvider"/> s for this configuration.
        /// </summary>
        /// <value>The providers.</value>
        public IEnumerable<IConfigurationProvider> Providers
        {
            get;
            private set;
        }

        #endregion Properties

        #region Indexers

        /// <summary>
        /// Gets or sets the <see cref="System.String"/> with the specified key.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <returns>System.String.</returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        public string this[string key]
        {
            get
            {
                foreach (IConfigurationProvider provider in Providers.Reverse())
                {
                    string value;

                    if (provider.TryGet(key, out value))
                    {
                        return value;
                    }
                }

                return null;
            }

            set
            {
                if (!Providers.Any())
                {
                    throw new InvalidOperationException(ExceptionMessages.NoSourcesErrorMessage);
                }

                foreach (IConfigurationProvider provider in Providers)
                {
                    provider.Set(key, value);
                }
            }
        }

        #endregion Indexers

        #region Methods

        /// <summary>
        /// Gets a configuration sub-section with the specified key.
        /// </summary>
        /// <param name="key">The key of the configuration section.</param>
        /// <returns>The <see cref="IConfigurationSection"/>.</returns>
        /// <remarks>
        /// This method will never return <c>null</c>. If no matching sub-section is found with the
        /// specified key, an empty <see cref="IConfigurationSection"/> will be returned.
        /// </remarks>
        public IConfigurationSection GetSection(string key)
        {
            return new ConfigurationSection(this, key);
        }

        /// <summary>
        /// Gets the immediate children sub-sections.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IConfigurationSection> GetChildren() => GetChildrenImplementation(null);

        internal IEnumerable<IConfigurationSection> GetChildrenImplementation(string path)
        {
            return Providers
                .Aggregate(Enumerable.Empty<string>(),
                    (seed, source) => source.GetChildKeys(seed, path))
                .Distinct()
                .Select(key => GetSection(path == null ? key : ConfigurationPath.Combine(path, key)));
        }

        #endregion Methods
    }
}