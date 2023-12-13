using DCCRailway.System.Utilities;
using NUnit.Framework;

namespace DCCRailway.Test.Utility;

[TestFixture]
public class UtilityTests {
    [Test]
    public void SplitInterFaceTest() {
        var fullname = "System.Test.Utility";
        Assert.That(InterfaceUtility.SplitInterfaceName(fullname), Is.EqualTo("Utility"));
    }
}