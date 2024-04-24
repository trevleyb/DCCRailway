using DCCRailway.Station.Attributes;
using DCCRailway.Station.NCE.Commands;

namespace DCCRailway.System.Test.Attributes;

[TestFixture]
public class AttributeTests {
    [Test]
    public void TestICommand() {
        var cmd  = new NCEDummyCmd();
        var attr = cmd.AttributeInfo();
        Assert.That(attr, Is.Not.Null, "Does the command include the appropriate AttributeInfo?");
    }
}