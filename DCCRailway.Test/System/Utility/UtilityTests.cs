using DCCRailway.Utilities;
using NUnit.Framework;

namespace DCCRailway.Test.System.Utility;

[TestFixture]
public class UtilityTests {
    [Test]
    public void SplitInterFaceTest() {
        var fullname = "Controller.Test.Utility";
        Assert.That(InterfaceUtility.SplitInterfaceName(fullname), Is.EqualTo("Utility"));
    }
}