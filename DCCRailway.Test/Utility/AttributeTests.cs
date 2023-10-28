using DCCRailway.System.NCE.Commands;
using DCCRailway.System.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DCCRailway.Test.Utility;

[TestClass]
public class AttributeTests {
    [TestMethod]
    public void TestICommand() {
        var cmd  = new NCEDummyCmd();
        var attr = cmd.Info();
        Assert.IsNotNull(attr, "Does the command include the appropriate Attributes?");
    }
}