using DCCRailway.System.NCE.Commands;

namespace DCCRailway.Test.Utility; 

using DCCRailway.System.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[TestClass]
public class AttributeTests {
    [TestMethod]
    public void TestICommand() {
        var cmd = new NCEDummyCmd();
        var attr = cmd.Info();
        Assert.IsNotNull(attr,"Does the command include the appropriate Attributes?");
    }
}