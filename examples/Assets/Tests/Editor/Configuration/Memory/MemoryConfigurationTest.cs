using NUnit.Framework;
using System.Collections.Generic;

namespace UniSharper.Configuration.Memory.Tests
{
    public class MemoryConfigurationTest
    {
        #region Methods

        [Test]
        public void SettingValueUpdatesAllConfigurationProviders()
        {
            // Arrange
            var dict = new Dictionary<string, string>()
            {
                {"Key1", "Value1"},
                {"Key2", "Value2"}
            };

            var memConfigSrc1 = new TestMemorySourceProvider(dict);
            var memConfigSrc2 = new TestMemorySourceProvider(dict);
            var memConfigSrc3 = new TestMemorySourceProvider(dict);

            var configurationBuilder = new ConfigurationBuilder();

            configurationBuilder.Add(memConfigSrc1);
            configurationBuilder.Add(memConfigSrc2);
            configurationBuilder.Add(memConfigSrc3);

            var config = configurationBuilder.Build();

            // Act
            config["Key1"] = "NewValue1";
            config["Key2"] = "NewValue2";

            var memConfigProvider1 = memConfigSrc1.Build(configurationBuilder);
            var memConfigProvider2 = memConfigSrc2.Build(configurationBuilder);
            var memConfigProvider3 = memConfigSrc3.Build(configurationBuilder);

            // Assert
            Assert.AreEqual("NewValue1", config["Key1"]);
            Assert.AreEqual("NewValue1", memConfigProvider1.Get("Key1"));
            Assert.AreEqual("NewValue1", memConfigProvider2.Get("Key1"));
            Assert.AreEqual("NewValue1", memConfigProvider3.Get("Key1"));
            Assert.AreEqual("NewValue2", config["Key2"]);
            Assert.AreEqual("NewValue2", memConfigProvider1.Get("Key2"));
            Assert.AreEqual("NewValue2", memConfigProvider2.Get("Key2"));
            Assert.AreEqual("NewValue2", memConfigProvider3.Get("Key2"));
        }

        #endregion Methods

        #region Classes

        public class TestMemorySourceProvider : MemoryConfigurationProvider, IConfigurationSource
        {
            #region Constructors

            public TestMemorySourceProvider(Dictionary<string, string> initialData)
                : base(new MemoryConfigurationSource { InitialData = initialData })
            { }

            #endregion Constructors

            #region Methods

            public IConfigurationProvider Build(IConfigurationBuilder builder)
            {
                return this;
            }

            #endregion Methods
        }

        #endregion Classes
    }
}