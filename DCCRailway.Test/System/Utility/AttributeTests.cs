using DCCRailway.Manufacturer.NCE.Commands;
using DCCRailway.Utilities;
using NUnit.Framework;

namespace DCCRailway.Test.System.Utility;

[TestFixture]
public class AttributeTests {
    [Test]
    public void TestICommand() {
        var cmd  = new NCEDummyCmd();
        var attr = cmd.Info();
        Assert.That(attr, Is.Not.Null,"Does the command include the appropriate Attributes?");
    }
}