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
    /// Extension methods for configuration classes.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Adds a new configuration source.
        /// </summary>
        /// <param name="builder">The <see cref="IConfigurationBuilder"/> to add to.</param>
        /// <param name="configureSource">Configures the source secrets.</param>
        /// <returns>The <see cref="IConfigurationBuilder"/>.</returns>
        public static IConfigurationBuilder Add<TSource>(this IConfigurationBuilder builder, Action<TSource> configureSource) where TSource : IConfigurationSource, new()
        {
            var source = new TSource();
            configureSource?.Invoke(source);
            return builder.Add(source);
        }

        /// <summary>
        /// Shorthand for GetSection("ConnectionStrings")[name].
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="name">The connection string key.</param>
        /// <returns></returns>
        public static string GetConnectionString(this IConfiguration configuration, string name)
        {
            return configuration?.GetSection("ConnectionStrings")?[name];
        }

        /// <summary>
        /// Get the enumeration of key value pairs within the <see cref="IConfiguration"/>
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/> to enumerate.</param>
        /// <returns>An enumeration of key value pairs.</returns>
        public static IEnumerable<KeyValuePair<string, string>> AsEnumerable(this IConfiguration configuration) => configuration.AsEnumerable(makePathsRelative: false);

        /// <summary>
        /// Get the enumeration of key value pairs within the <see cref="IConfiguration"/>
        /// </summary>
        /// <param name="configuration">The <see cref="IConfiguration"/> to enumerate.</param>
        /// <param name="makePathsRelative">
        /// If true, the child keys returned will have the current configuration's Path trimmed from
        /// the front.
        /// </param>
        /// <returns>An enumeration of key value pairs.</returns>
        public static IEnumerable<KeyValuePair<string, string>> AsEnumerable(this IConfiguration configuration, bool makePathsRelative)
        {
            var stack = new Stack<IConfiguration>();
            stack.Push(configuration);
            var rootSection = configuration as IConfigurationSection;
            var prefixLength = (makePathsRelative && rootSection != null) ? rootSection.Path.Length + 1 : 0;
            while (stack.Count > 0)
            {
                var config = stack.Pop();
                // Don't include the sections value if we are removing paths, since it will be an
                // empty key
                if (config is IConfigurationSection && (!makePathsRelative || config != configuration))
                {
                    IConfigurationSection section = config as IConfigurationSection;
                    yield return new KeyValuePair<string, string>(section.Path.Substring(prefixLength), section.Value);
                }
                foreach (var child in config.GetChildren())
                {
                    stack.Push(child);
                }
            }
        }

        /// <summary>
        /// Determines whether the section has a <see cref="IConfigurationSection.Value"/> or has children
        /// </summary>
        public static bool Exists(this IConfigurationSection section)
        {
            if (section == null)
            {
                return false;
            }
            return section.Value != null || section.GetChildren().Any();
        }
    }
}