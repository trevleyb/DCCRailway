using NUnit.Framework;
using DCCRailway.System.Attributes;

namespace DCCRailway.System.Test;

public class CommandAttributeTest {

    public class CommandAttributeTests
    {
        private CommandAttribute _commandAttribute;

        [SetUp]
        public void Setup()
        {
            _commandAttribute = new CommandAttribute("TestName","TestDescription","1.0",new string[] {"Adapter1", "Adapter2"}, new string[] {"2.0","2.1"});
        }

        [Test]
        public void TestInitilization()
        {
            Assert.That(_commandAttribute.Name, Is.EqualTo("TestName"));
            Assert.That(_commandAttribute.Description, Is.EqualTo("TestDescription"));
            Assert.That(_commandAttribute.Version, Is.EqualTo("1.0"));
            Assert.That(_commandAttribute.SupportedAdapters, Has.Member("Adapter1"));
            Assert.That(_commandAttribute.SupportedVersions, Has.Member("2.0"));
        }

        [Test]
        public void TestShouldInclude()
        {
            Assert.IsTrue(_commandAttribute.ShouldInclude("Adapter1", "2.0"));
            Assert.IsFalse(_commandAttribute.ShouldInclude("Adapter3", "2.1"));
        }
    }
}