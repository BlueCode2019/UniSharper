using NUnit.Framework;

namespace UniSharper.Configuration.Tests
{
    internal class ConfigurationPathTest
    {
        [Test]
        public void CombineWithEmptySegmentLeavesDelimiter()
        {
            Assert.AreEqual("parent:", ConfigurationPath.Combine("parent", ""));
            Assert.AreEqual("parent::", ConfigurationPath.Combine("parent", "", ""));
            Assert.AreEqual("parent:::key", ConfigurationPath.Combine("parent", "", "", "key"));
        }

        [Test]
        public void GetLastSegmenGetSectionKeyTests()
        {
            Assert.Null(ConfigurationPath.GetSectionKey(null));
            Assert.AreEqual("", ConfigurationPath.GetSectionKey(""));
            Assert.AreEqual("", ConfigurationPath.GetSectionKey(":::"));
            Assert.AreEqual("c", ConfigurationPath.GetSectionKey("a::b:::c"));
            Assert.AreEqual("", ConfigurationPath.GetSectionKey("a:::b:"));
            Assert.AreEqual("key", ConfigurationPath.GetSectionKey("key"));
            Assert.AreEqual("key", ConfigurationPath.GetSectionKey(":key"));
            Assert.AreEqual("key", ConfigurationPath.GetSectionKey("::key"));
            Assert.AreEqual("key", ConfigurationPath.GetSectionKey("parent:key"));
        }

        //[Test]
        //public void GetParentPathTests()
        //{
        //    Assert.Null(ConfigurationPath.GetParentPath(null));
        //    Assert.Null(ConfigurationPath.GetParentPath(""));
        //    Assert.Equals("::", ConfigurationPath.GetParentPath(":::"));
        //    Assert.Equals("a::b::", ConfigurationPath.GetParentPath("a::b:::c"));
        //    Assert.Equals("a:::b", ConfigurationPath.GetParentPath("a:::b:"));
        //    Assert.Null(ConfigurationPath.GetParentPath("key"));
        //    Assert.Equals("", ConfigurationPath.GetParentPath(":key"));
        //    Assert.Equals(":", ConfigurationPath.GetParentPath("::key"));
        //    Assert.Equals("parent", ConfigurationPath.GetParentPath("parent:key"));
        //}
    }
}