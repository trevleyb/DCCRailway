using DCCRailway.Common.Helpers;

namespace DCCRailway.Common.Test.Utility;

[TestFixture]
public class ByteComparison {
    [Test]
    public void ByteComparisonTest() {
        byte[] expected = { (byte)'!', 0x0D, 0x0A };
        byte[] tooSmall = { (byte)'!', 0x0D };
        byte[] tooLarge = { (byte)'!', 0x0D, 0x0A, 0x0F };
        byte[] noMatch1 = { (byte)'@', 0x0D, 0x0A };
        byte[] noMatch2 = { (byte)'!', 0x0D, 0x0C };

        Assert.That(expected.Compare(new byte[] { (byte)'!', 0x0D, 0x0A }), "Arrays should Match");
        Assert.That(expected?.Compare(null), Is.False, "Empty Array2 should not match");
        Assert.That(expected?.Compare(tooSmall), Is.False);
        Assert.That(expected?.Compare(tooLarge), Is.False);
        Assert.That(expected?.Compare(noMatch1), Is.False);
        Assert.That(expected?.Compare(noMatch2), Is.False);
    }

    [Test]
    public void IntToArrayTest() {
        Assert.That(99.ToByteArray().Compare(new byte[] { 0, 99 }));
        var cv1 = 01;
        Assert.That(cv1.ToByteArray().Compare(new byte[] { 0, 1 }));
        var cv2 = 99;
        Assert.That(cv2.ToByteArray().Compare(new byte[] { 0, 99 }));
        var cv3 = 254;
        Assert.That(cv3.ToByteArray().Compare(new byte[] { 0, 254 }));
        var cv4 = 255;
        Assert.That(cv4.ToByteArray().Compare(new byte[] { 0, 255 }));
        var cv5 = 256;
        Assert.That(cv5.ToByteArray().Compare(new byte[] { 1, 0 }));
        var cv6 = 257;
        Assert.That(cv6.ToByteArray().Compare(new byte[] { 1, 1 }));
    }

    [Test]
    public void ByteIncreaseSingleTest() {
        var set1 = new byte[] { 1, 2 };
        Assert.That(set1.Compare(new byte[] { 1, 2 }));
        var set2 = set1.AddToArray(3);
        Assert.That(set2.Compare(new byte[] { 1, 2, 3 }));
        var set3 = set2.AddToArray(0, false);
        Assert.That(set3.Compare(new byte[] { 0, 1, 2, 3 }));
    }

    [Test]
    public void ByteIncreaseMultiTest() {
        var set1 = new byte[] { 1, 2 };
        var set2 = new byte[] { 3, 4 };

        Assert.That(set1.Compare(new byte[] { 1, 2 }));
        Assert.That(set2.Compare(new byte[] { 3, 4 }));

        Assert.That(set1.AddToArray(set2).Compare(new byte[] { 1, 2, 3, 4 }));
        Assert.That(set1.AddToArray(set2, false).Compare(new byte[] { 3, 4, 1, 2 }));
    }
}