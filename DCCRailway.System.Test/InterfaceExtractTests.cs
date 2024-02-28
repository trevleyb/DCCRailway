using DCCRailway.System.Controllers;

namespace DCCRailway.System.Test;

[TestFixture]
public class InterfaceExtractTests {
    [Test]
    public void SplitInterFaceTest() {
        var fullname = "Controller.Test.Utility";
        Assert.That(InterfaceUtility.SplitInterfaceName(fullname), Is.EqualTo("Utility"));
    }
}
