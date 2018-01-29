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
    /// Represents a section of application configuration values.
    /// </summary>
    /// <seealso cref="UniSharper.Configuration.IConfigurationSection"/>
    public class ConfigurationSection : IConfigurationSection
    {
        #region Fields

        private string key;
        private ConfigurationRoot root;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationSection"/> class.
        /// </summary>
        /// <param name="root">The configuration root.</param>
        /// <param name="path">The path to this section.</param>
        /// <exception cref="System.ArgumentNullException">
        /// <para><c>root</c> is <c>null</c>.</para>
        /// <para>-or-</para>
        /// <para><c>path</c> is <c>null</c>.</para>
        /// </exception>
        public ConfigurationSection(ConfigurationRoot root, string path)
        {
            if (root == null)
            {
                throw new ArgumentNullException(nameof(root));
            }

            if (path == null)
            {
                throw new ArgumentNullException(nameof(path));
            }

            this.root = root;
            Path = path;
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Gets the key this section occupies in its parent.
        /// </summary>
        public string Key
        {
            get
            {
                if (key == null)
                {
                    // Key is calculated lazily as last portion of Path
                    key = ConfigurationPath.GetSectionKey(Path);
                }

                return key;
            }
        }

        /// <summary>
        /// Gets the full path to this section from the <see cref="IConfigurationRoot"/>.
        /// </summary>
        public string Path
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets or sets the section value.
        /// </summary>
        public string Value
        {
            get
            {
                return root[Path];
            }
            set
            {
                root[Path] = value;
            }
        }

        #endregion Properties

        #region Indexers

        /// <summary>
        /// Gets or sets the value corresponding to a configuration key.
        /// </summary>
        /// <param name="key">The configuration key.</param>
        /// <returns>The configuration value.</returns>
        public string this[string key]
        {
            get
            {
                return root[ConfigurationPath.Combine(Path, key)];
            }
            set
            {
                root[ConfigurationPath.Combine(Path, key)] = value;
            }
        }

        #endregion Indexers

        #region Methods

        /// <summary>
        /// Gets the immediate descendant configuration sub-sections.
        /// </summary>
        /// <returns>The configuration sub-sections.</returns>
        public IEnumerable<IConfigurationSection> GetChildren() => root.GetChildrenImplementation(Path);

        /// <summary>
        /// Gets a configuration sub-section with the specified key.
        /// </summary>
        /// <param name="key">The key of the configuration section.</param>
        /// <returns>The <see cref="IConfigurationSection"/>.</returns>
        /// <remarks>
        /// This method will never return <c>null</c>. If no matching sub-section is found with the
        /// specified key, an empty <see cref="IConfigurationSection"/> will be returned.
        /// </remarks>
        public IConfigurationSection GetSection(string key) => root.GetSection(ConfigurationPath.Combine(Path, key));

        #endregion Methods
    }
}