using DCCRailway.Core.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DCCRailway.Test.Utility;

[TestClass]
public class ByteComparison {
    [TestMethod]
    public void ByteComparisonTest() {
        byte[] expected = { (byte)'!', 0x0D, 0x0A };
        byte[] tooSmall = { (byte)'!', 0x0D };
        byte[] tooLarge = { (byte)'!', 0x0D, 0x0A, 0x0F };
        byte[] noMatch1 = { (byte)'@', 0x0D, 0x0A };
        byte[] noMatch2 = { (byte)'!', 0x0D, 0x0C };

        Assert.IsTrue(expected.Compare(new byte[] { (byte)'!', 0x0D, 0x0A }), "Arrays should Match");
        Assert.IsFalse(expected?.Compare(null), "Empty Array2 should not match");
        Assert.IsFalse(expected?.Compare(tooSmall));
        Assert.IsFalse(expected?.Compare(tooLarge));
        Assert.IsFalse(expected?.Compare(noMatch1));
        Assert.IsFalse(expected?.Compare(noMatch2));
    }

    [TestMethod]
    public void IntToArrayTest() {
        Assert.IsTrue(99.ToByteArray().Compare(new byte[] { 0, 99 }));
        var cv1 = 01;
        Assert.IsTrue(cv1.ToByteArray().Compare(new byte[] { 0, 1 }));
        var cv2 = 99;
        Assert.IsTrue(cv2.ToByteArray().Compare(new byte[] { 0, 99 }));
        var cv3 = 254;
        Assert.IsTrue(cv3.ToByteArray().Compare(new byte[] { 0, 254 }));
        var cv4 = 255;
        Assert.IsTrue(cv4.ToByteArray().Compare(new byte[] { 0, 255 }));
        var cv5 = 256;
        Assert.IsTrue(cv5.ToByteArray().Compare(new byte[] { 1, 0 }));
        var cv6 = 257;
        Assert.IsTrue(cv6.ToByteArray().Compare(new byte[] { 1, 1 }));
    }

    [TestMethod]
    public void ByteIncreaseSingleTest() {
        var set1 = new byte[] { 1, 2 };
        Assert.IsTrue(set1.Compare(new byte[] { 1, 2 }));
        var set2 = set1.AddToArray(3);
        Assert.IsTrue(set2.Compare(new byte[] { 1, 2, 3 }));
        var set3 = set2.AddToArray(0, false);
        Assert.IsTrue(set3.Compare(new byte[] { 0, 1, 2, 3 }));
    }

    [TestMethod]
    public void ByteIncreaseMultiTest() {
        var set1 = new byte[] { 1, 2 };
        var set2 = new byte[] { 3, 4 };

        Assert.IsTrue(set1.Compare(new byte[] { 1, 2 }));
        Assert.IsTrue(set2.Compare(new byte[] { 3, 4 }));

        Assert.IsTrue(set1.AddToArray(set2).Compare(new byte[] { 1, 2, 3, 4 }));
        Assert.IsTrue(set1.AddToArray(set2, false).Compare(new byte[] { 3, 4, 1, 2 }));
    }
}