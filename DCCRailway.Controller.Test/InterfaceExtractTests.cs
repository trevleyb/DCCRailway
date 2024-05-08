using DCCRailway.Controller.Controllers;

namespace DCCRailway.System.Test;

[TestFixture]
public class InterfaceExtractTests {
    [Test]
    public void SplitInterFaceTest() {
        var fullname = "CommandStation.Test.Utility";
        Assert.That(InterfaceUtility.SplitInterfaceName(fullname), Is.EqualTo("Utility"));
    }
}