using DCCRailway.Controller.Attributes;

namespace DCCRailway.Controller.Test;

public class CommandAttributeTest {
    public class CommandAttributeTests {
        private CommandAttribute _commandAttribute;

        [SetUp]
        public void Setup() {
            _commandAttribute = new CommandAttribute("TestName", "TestDescription", "1.0", new[] { "Adapter1", "Adapter2" }, new[] { "2.0", "2.1" });
        }

        [Test]
        public void TestInitilization() {
            Assert.That(_commandAttribute.Name, Is.EqualTo("TestName"));
            Assert.That(_commandAttribute.Description, Is.EqualTo("TestDescription"));
            Assert.That(_commandAttribute.Version, Is.EqualTo("1.0"));
        }
    }
}