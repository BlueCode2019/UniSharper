using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UniSharper.Configuration.Tests
{
    [TestFixture]
    public class ConfigurationTest
    {
        #region Methods

        [Theory]
        public void AsEnumerateFlattensIntoDictionaryTest([Values(true, false)] bool removePath)
        {
            // Arrange
            var dic1 = new Dictionary<string, string>()
            {
                {"Mem1", "Value1"},
                {"Mem1:", "NoKeyValue1"},
                {"Mem1:KeyInMem1", "ValueInMem1"},
                {"Mem1:KeyInMem1:Deep1", "ValueDeep1"}
            };
            var dic2 = new Dictionary<string, string>()
            {
                {"Mem2", "Value2"},
                {"Mem2:", "NoKeyValue2"},
                {"Mem2:KeyInMem2", "ValueInMem2"},
                {"Mem2:KeyInMem2:Deep2", "ValueDeep2"}
            };
            var dic3 = new Dictionary<string, string>()
            {
                {"Mem3", "Value3"},
                {"Mem3:", "NoKeyValue3"},
                {"Mem3:KeyInMem3", "ValueInMem3"},
                {"Mem3:KeyInMem3:Deep3", "ValueDeep3"}
            };
            var memConfigSrc1 = new MemoryConfigurationSource { InitialData = dic1 };
            var memConfigSrc2 = new MemoryConfigurationSource { InitialData = dic2 };
            var memConfigSrc3 = new MemoryConfigurationSource { InitialData = dic3 };

            var configurationBuilder = new ConfigurationBuilder();

            // Act
            configurationBuilder.Add(memConfigSrc1);
            configurationBuilder.Add(memConfigSrc2);
            configurationBuilder.Add(memConfigSrc3);
            var config = configurationBuilder.Build();
            var dict = config.AsEnumerable(makePathsRelative: removePath).ToDictionary(k => k.Key, v => v.Value);

            // Assert
            Assert.AreEqual("Value1", dict["Mem1"]);
            Assert.AreEqual("NoKeyValue1", dict["Mem1:"]);
            Assert.AreEqual("ValueDeep1", dict["Mem1:KeyInMem1:Deep1"]);
            Assert.AreEqual("ValueInMem2", dict["Mem2:KeyInMem2"]);
            Assert.AreEqual("Value2", dict["Mem2"]);
            Assert.AreEqual("NoKeyValue2", dict["Mem2:"]);
            Assert.AreEqual("ValueDeep2", dict["Mem2:KeyInMem2:Deep2"]);
            Assert.AreEqual("Value3", dict["Mem3"]);
            Assert.AreEqual("NoKeyValue3", dict["Mem3:"]);
            Assert.AreEqual("ValueInMem3", dict["Mem3:KeyInMem3"]);
            Assert.AreEqual("ValueDeep3", dict["Mem3:KeyInMem3:Deep3"]);
        }

        [Test]
        public void AsEnumerateStripsKeyFromChildren()
        {
            // Arrange
            var dic1 = new Dictionary<string, string>()
            {
                {"Mem1", "Value1"},
                {"Mem1:", "NoKeyValue1"},
                {"Mem1:KeyInMem1", "ValueInMem1"},
                {"Mem1:KeyInMem1:Deep1", "ValueDeep1"}
            };
            var dic2 = new Dictionary<string, string>()
            {
                {"Mem2", "Value2"},
                {"Mem2:", "NoKeyValue2"},
                {"Mem2:KeyInMem2", "ValueInMem2"},
                {"Mem2:KeyInMem2:Deep2", "ValueDeep2"}
            };
            var dic3 = new Dictionary<string, string>()
            {
                {"Mem3", "Value3"},
                {"Mem3:", "NoKeyValue3"},
                {"Mem3:KeyInMem3", "ValueInMem3"},
                {"Mem3:KeyInMem4", "ValueInMem4"},
                {"Mem3:KeyInMem3:Deep3", "ValueDeep3"},
                {"Mem3:KeyInMem3:Deep4", "ValueDeep4"}
            };
            var memConfigSrc1 = new MemoryConfigurationSource { InitialData = dic1 };
            var memConfigSrc2 = new MemoryConfigurationSource { InitialData = dic2 };
            var memConfigSrc3 = new MemoryConfigurationSource { InitialData = dic3 };

            var configurationBuilder = new ConfigurationBuilder();

            // Act
            configurationBuilder.Add(memConfigSrc1);
            configurationBuilder.Add(memConfigSrc2);
            configurationBuilder.Add(memConfigSrc3);

            var config = configurationBuilder.Build();

            var dict = config.GetSection("Mem1").AsEnumerable(makePathsRelative: true).ToDictionary(k => k.Key, v => v.Value);
            Assert.AreEqual(3, dict.Count);
            Assert.AreEqual("NoKeyValue1", dict[""]);
            Assert.AreEqual("ValueInMem1", dict["KeyInMem1"]);
            Assert.AreEqual("ValueDeep1", dict["KeyInMem1:Deep1"]);

            var dict2 = config.GetSection("Mem2").AsEnumerable(makePathsRelative: true).ToDictionary(k => k.Key, v => v.Value);
            Assert.AreEqual(3, dict2.Count);
            Assert.AreEqual("NoKeyValue2", dict2[""]);
            Assert.AreEqual("ValueInMem2", dict2["KeyInMem2"]);
            Assert.AreEqual("ValueDeep2", dict2["KeyInMem2:Deep2"]);

            var dict3 = config.GetSection("Mem3").AsEnumerable(makePathsRelative: true).ToDictionary(k => k.Key, v => v.Value);
            Assert.AreEqual(5, dict3.Count);
            Assert.AreEqual("NoKeyValue3", dict3[""]);
            Assert.AreEqual("ValueInMem3", dict3["KeyInMem3"]);
            Assert.AreEqual("ValueInMem4", dict3["KeyInMem4"]);
            Assert.AreEqual("ValueDeep3", dict3["KeyInMem3:Deep3"]);
            Assert.AreEqual("ValueDeep4", dict3["KeyInMem3:Deep4"]);
        }

        [Test]
        public void LoadAndCombineKeyValuePairsFromDifferentConfigurationProviders()
        {
            // Arrange
            var dic1 = new Dictionary<string, string>()
            {
                {"Mem1:KeyInMem1", "ValueInMem1"}
            };
            var dic2 = new Dictionary<string, string>()
            {
                {"Mem2:KeyInMem2", "ValueInMem2"}
            };
            var dic3 = new Dictionary<string, string>()
            {
                {"Mem3:KeyInMem3", "ValueInMem3"}
            };
            var memConfigSrc1 = new MemoryConfigurationSource { InitialData = dic1 };
            var memConfigSrc2 = new MemoryConfigurationSource { InitialData = dic2 };
            var memConfigSrc3 = new MemoryConfigurationSource { InitialData = dic3 };

            var configurationBuilder = new ConfigurationBuilder();

            // Act
            configurationBuilder.Add(memConfigSrc1);
            configurationBuilder.Add(memConfigSrc2);
            configurationBuilder.Add(memConfigSrc3);

            var config = configurationBuilder.Build();

            var memVal1 = config["mem1:keyinmem1"];
            var memVal2 = config["Mem2:KeyInMem2"];
            var memVal3 = config["MEM3:KEYINMEM3"];

            // Assert
            Assert.IsTrue(configurationBuilder.Sources.Contains(memConfigSrc1));
            Assert.IsTrue(configurationBuilder.Sources.Contains(memConfigSrc2));
            Assert.IsTrue(configurationBuilder.Sources.Contains(memConfigSrc3));

            Assert.AreEqual("ValueInMem1", memVal1);
            Assert.AreEqual("ValueInMem2", memVal2);
            Assert.AreEqual("ValueInMem3", memVal3);

            Assert.AreEqual("ValueInMem1", config["mem1:keyinmem1"]);
            Assert.AreEqual("ValueInMem2", config["Mem2:KeyInMem2"]);
            Assert.AreEqual("ValueInMem3", config["MEM3:KEYINMEM3"]);
            Assert.Null(config["NotExist"]);
        }

        [Test]
        public void NewConfigurationProviderOverridesOldOneWhenKeyIsDuplicated()
        {
            // Arrange
            var dic1 = new Dictionary<string, string>()
                {
                    {"Key1:Key2", "ValueInMem1"}
                };
            var dic2 = new Dictionary<string, string>()
                {
                    {"Key1:Key2", "ValueInMem2"}
                };
            var memConfigSrc1 = new MemoryConfigurationSource { InitialData = dic1 };
            var memConfigSrc2 = new MemoryConfigurationSource { InitialData = dic2 };

            var configurationBuilder = new ConfigurationBuilder();

            // Act
            configurationBuilder.Add(memConfigSrc1);
            configurationBuilder.Add(memConfigSrc2);

            var config = configurationBuilder.Build();

            // Assert
            Assert.AreEqual("ValueInMem2", config["Key1:Key2"]);
        }

        [Test]
        public void CanGetConfigurationSection()
        {
            // Arrange
            var dic1 = new Dictionary<string, string>()
            {
                {"Data:DB1:Connection1", "MemVal1"},
                {"Data:DB1:Connection2", "MemVal2"}
            };
            var dic2 = new Dictionary<string, string>()
            {
                {"DataSource:DB2:Connection", "MemVal3"}
            };
            var dic3 = new Dictionary<string, string>()
            {
                {"Data", "MemVal4"}
            };
            var memConfigSrc1 = new MemoryConfigurationSource { InitialData = dic1 };
            var memConfigSrc2 = new MemoryConfigurationSource { InitialData = dic2 };
            var memConfigSrc3 = new MemoryConfigurationSource { InitialData = dic3 };

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.Add(memConfigSrc1);
            configurationBuilder.Add(memConfigSrc2);
            configurationBuilder.Add(memConfigSrc3);

            var config = configurationBuilder.Build();

            // Act
            var configFocus = config.GetSection("Data");

            var memVal1 = configFocus["DB1:Connection1"];
            var memVal2 = configFocus["DB1:Connection2"];
            var memVal3 = configFocus["DB2:Connection"];
            var memVal4 = configFocus["Source:DB2:Connection"];
            var memVal5 = configFocus.Value;

            // Assert
            Assert.AreEqual("MemVal1", memVal1);
            Assert.AreEqual("MemVal2", memVal2);
            Assert.AreEqual("MemVal4", memVal5);

            Assert.AreEqual("MemVal1", configFocus["DB1:Connection1"]);
            Assert.AreEqual("MemVal2", configFocus["DB1:Connection2"]);
            Assert.Null(configFocus["DB2:Connection"]);
            Assert.Null(configFocus["Source:DB2:Connection"]);
            Assert.AreEqual("MemVal4", configFocus.Value);
        }

        [Test]
        public void CanGetConnectionStrings()
        {
            // Arrange
            var dic1 = new Dictionary<string, string>()
            {
                {"ConnectionStrings:DB1:Connection1", "MemVal1"},
                {"ConnectionStrings:DB1:Connection2", "MemVal2"}
            };
            var dic2 = new Dictionary<string, string>()
            {
                {"ConnectionStrings:DB2:Connection", "MemVal3"}
            };
            var memConfigSrc1 = new MemoryConfigurationSource { InitialData = dic1 };
            var memConfigSrc2 = new MemoryConfigurationSource { InitialData = dic2 };

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.Add(memConfigSrc1);
            configurationBuilder.Add(memConfigSrc2);

            var config = configurationBuilder.Build();

            // Act
            var memVal1 = config.GetConnectionString("DB1:Connection1");
            var memVal2 = config.GetConnectionString("DB1:Connection2");
            var memVal3 = config.GetConnectionString("DB2:Connection");

            // Assert
            Assert.AreEqual("MemVal1", memVal1);
            Assert.AreEqual("MemVal2", memVal2);
            Assert.AreEqual("MemVal3", memVal3);
        }

        [Test]
        public void CanGetConfigurationChildren()
        {
            // Arrange
            var dic1 = new Dictionary<string, string>()
            {
                {"Data:DB1:Connection1", "MemVal1"},
                {"Data:DB1:Connection2", "MemVal2"}
            };
            var dic2 = new Dictionary<string, string>()
            {
                {"Data:DB2Connection", "MemVal3"}
            };
            var dic3 = new Dictionary<string, string>()
            {
                {"DataSource:DB3:Connection", "MemVal4"}
            };
            var memConfigSrc1 = new MemoryConfigurationSource { InitialData = dic1 };
            var memConfigSrc2 = new MemoryConfigurationSource { InitialData = dic2 };
            var memConfigSrc3 = new MemoryConfigurationSource { InitialData = dic3 };

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.Add(memConfigSrc1);
            configurationBuilder.Add(memConfigSrc2);
            configurationBuilder.Add(memConfigSrc3);

            var config = configurationBuilder.Build();

            // Act
            var configSections = config.GetSection("Data").GetChildren().ToList();

            // Assert
            Assert.AreEqual(2, configSections.Count());
            Assert.AreEqual("MemVal1", configSections.FirstOrDefault(c => c.Key == "DB1")["Connection1"]);
            Assert.AreEqual("MemVal2", configSections.FirstOrDefault(c => c.Key == "DB1")["Connection2"]);
            Assert.AreEqual("MemVal3", configSections.FirstOrDefault(c => c.Key == "DB2Connection").Value);
            Assert.False(configSections.Exists(c => c.Key == "DB3"));
            Assert.False(configSections.Exists(c => c.Key == "DB3"));
        }

        [Test]
        public void SourcesReturnsAddedConfigurationProviders()
        {
            // Arrange
            var dict = new Dictionary<string, string>()
            {
                {"Mem:KeyInMem", "MemVal"}
            };
            var memConfigSrc1 = new MemoryConfigurationSource { InitialData = dict };
            var memConfigSrc2 = new MemoryConfigurationSource { InitialData = dict };
            var memConfigSrc3 = new MemoryConfigurationSource { InitialData = dict };

            var configurationBuilder = new ConfigurationBuilder();

            // Act
            configurationBuilder.Add(memConfigSrc1);
            configurationBuilder.Add(memConfigSrc2);
            configurationBuilder.Add(memConfigSrc3);

            configurationBuilder.Build();

            // Assert
            Assert.AreEqual(new[] { memConfigSrc1, memConfigSrc2, memConfigSrc3 }, configurationBuilder.Sources);
        }

        [Test]
        public void SetValueThrowsExceptionNoSourceRegistered()
        {
            // Arrange
            var configurationBuilder = new ConfigurationBuilder();
            var config = configurationBuilder.Build();

            var expectedMsg = ExceptionMessages.NoSourcesErrorMessage;

            // Act
            var ex = Assert.Throws<InvalidOperationException>(() => config["Title"] = "Welcome");

            // Assert
            Assert.AreEqual(expectedMsg, ex.Message);
        }

        [Test]
        public void KeyStartingWithColonMeansFirstSectionHasEmptyName()
        {
            // Arrange
            var dict = new Dictionary<string, string>
            {
                { ":Key2", "value" }
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(dict);
            var config = configurationBuilder.Build();

            // Act
            var children = config.GetChildren().ToArray();

            // Assert
            Assert.AreEqual(1, children.Length);
            Assert.AreEqual(string.Empty, children.First().Key);
            Assert.AreEqual(1, children.First().GetChildren().Count());
            Assert.AreEqual("Key2", children.First().GetChildren().First().Key);
        }

        [Test]
        public void KeyWithDoubleColonHasSectionWithEmptyName()
        {
            // Arrange
            var dict = new Dictionary<string, string>
            {
                { "Key1::Key3", "value" }
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(dict);
            var config = configurationBuilder.Build();

            // Act
            var children = config.GetChildren().ToArray();

            // Assert
            Assert.AreEqual(1, children.Length);
            Assert.AreEqual("Key1", children.First().Key);
            Assert.AreEqual(1, children.First().GetChildren().Count());
            Assert.AreEqual(string.Empty, children.First().GetChildren().First().Key);
            Assert.AreEqual(1, children.First().GetChildren().First().GetChildren().Count());
            Assert.AreEqual("Key3", children.First().GetChildren().First().GetChildren().First().Key);
        }

        [Test]
        public void KeyEndingWithColonMeansLastSectionHasEmptyName()
        {
            // Arrange
            var dict = new Dictionary<string, string>
            {
                { "Key1:", "value" }
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(dict);
            var config = configurationBuilder.Build();

            // Act
            var children = config.GetChildren().ToArray();

            // Assert
            Assert.AreEqual(1, children.Length);
            Assert.AreEqual("Key1", children.First().Key);
            Assert.AreEqual(1, children.First().GetChildren().Count());
            Assert.AreEqual(string.Empty, children.First().GetChildren().First().Key);
        }

        [Test]
        public void SectionWithValueExists()
        {
            // Arrange
            var dict = new Dictionary<string, string>()
            {
                {"Mem1", "Value1"},
                {"Mem1:KeyInMem1", "ValueInMem1"},
                {"Mem1:KeyInMem1:Deep1", "ValueDeep1"}
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(dict);
            var config = configurationBuilder.Build();

            // Act
            var sectionExists1 = config.GetSection("Mem1").Exists();
            var sectionExists2 = config.GetSection("Mem1:KeyInMem1").Exists();
            var sectionNotExists = config.GetSection("Mem2").Exists();

            // Assert
            Assert.True(sectionExists1);
            Assert.True(sectionExists2);
            Assert.False(sectionNotExists);
        }

        [Test]
        public void SectionWithChildrenExists()
        {
            // Arrange
            var dict = new Dictionary<string, string>()
            {
                {"Mem1:KeyInMem1", "ValueInMem1"},
                {"Mem1:KeyInMem1:Deep1", "ValueDeep1"},
                {"Mem2:KeyInMem2:Deep1", "ValueDeep2"}
            };
            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.AddInMemoryCollection(dict);
            var config = configurationBuilder.Build();

            // Act
            var sectionExists1 = config.GetSection("Mem1").Exists();
            var sectionExists2 = config.GetSection("Mem2").Exists();
            var sectionNotExists = config.GetSection("Mem3").Exists();

            // Assert
            Assert.True(sectionExists1);
            Assert.True(sectionExists2);
            Assert.False(sectionNotExists);
        }

        [Test]
        public void NullSectionDoesNotExist()
        {
            // Arrange Act
            var sectionExists = ConfigurationExtensions.Exists(null);

            // Assert
            Assert.False(sectionExists);
        }

        #endregion Methods
    }
}