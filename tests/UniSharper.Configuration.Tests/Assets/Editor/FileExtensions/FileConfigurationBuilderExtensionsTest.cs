using NUnit.Framework;
using System;
using System.IO;

namespace UniSharper.Configuration.Tests
{
    public class FileConfigurationBuilderExtensionsTest
    {
        [Test]
        public void SetBasePath_ThrowsIfBasePathIsNull()
        {
            // Arrange
            var configurationBuilder = new ConfigurationBuilder();

            // Act and Assert
            var ex = Assert.Throws<ArgumentNullException>(() => configurationBuilder.SetBasePath(null));
            Assert.AreEqual("basePath", ex.ParamName);
        }

        [Test]
        public void SetBasePath_CheckPropertiesValueOnBuilder()
        {
            var expectedBasePath = Directory.GetCurrentDirectory();
            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.SetBasePath(expectedBasePath);
            string basePath = configurationBuilder.GetBasePath();
            Assert.NotNull(basePath);
            Assert.AreEqual(expectedBasePath, basePath);
        }
    }
}