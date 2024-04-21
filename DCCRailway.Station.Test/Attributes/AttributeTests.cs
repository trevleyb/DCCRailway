using DCCRailway.Station.Attributes;
using DCCRailway.Station.NCE.Commands;
using NUnit.Framework;

namespace DCCRailway.Test.System.Utility;

[TestFixture]
public class AttributeTests {
    [Test]
    public void TestICommand() {
        var cmd  = new NCEDummyCmd();
        var attr = cmd.AttributeInfo();
        Assert.That(attr, Is.Not.Null, "Does the command include the appropriate AttributeInfo?");
    }
}