using System;

namespace UniSharper.Configuration.Tests
{
    internal static class ConfigurationProviderExtensions
    {
        public static string Get(this IConfigurationProvider provider, string key)
        {
            string value;

            if (!provider.TryGet(key, out value))
            {
                throw new InvalidOperationException("Key not found");
            }

            return value;
        }
    }
}