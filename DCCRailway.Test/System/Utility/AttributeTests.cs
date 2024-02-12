using DCCRailway.System.NCE.Commands;
using DCCRailway.System.Utilities;
using NUnit.Framework;

namespace DCCRailway.Test.Utility;

[TestFixture]
public class AttributeTests {
    [Test]
    public void TestICommand() {
        var cmd  = new NCEDummyCmd();
        var attr = cmd.Info();
        Assert.That(attr, Is.Not.Null,"Does the command include the appropriate Attributes?");
    }
}