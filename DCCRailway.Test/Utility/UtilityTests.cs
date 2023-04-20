using DCCRailway.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DCCRailway.Test.Utility; 

[TestClass]
public class UtilityTests {
    [TestMethod]
    public void SplitInterFaceTest() {
        var fullname = "System.Test.Utility";
        Assert.AreEqual(InterfaceUtility.SplitInterfaceName(fullname), "Utility");
    }
}