using DCCRailway.Common.Utilities;

namespace DCCRailway.Common.Test.Utility;

[TestFixture]
public class ExtensionMethodsTest {
    [Test]
    public void Invert_ShouldReturnInverseValue() {
        // Arrange
        byte input = 0b10101010;

        // Act
        var result = input.Invert();

        // Assert
        Assert.That(result, Is.EqualTo(0b01010101));
    }

    [Test]
    public void SetBit_ShouldSetBitValue() {
        // Arrange
        byte input = 0b00000000;

        // Act
        var result1 = input.SetBit(3, true);
        var result2 = input.SetBit(5, true);

        // Assert
        Assert.That(result1, Is.EqualTo(0b00001000));
        Assert.That(result2, Is.EqualTo(0b00100000));
    }

    [Test]
    public void SetBit_ShouldSetAllBits() {
        // Arrange
        byte input     = 0b00000000;
        byte testValue = 0;

        for (var i = 0; i <= 7; i++) {
            testValue += (byte)(1 << i);
            input     =  input.SetBit(i, true);
            Assert.That(testValue, Is.EqualTo(input));
        }

        // Assert
        Assert.That(input, Is.EqualTo(0b11111111));
    }

    [Test]
    public void FormatBits_ShouldReturnFormattedBitsString() {
        // Arrange
        byte input = 0b10101010;

        // Act
        var result = input.FormatBits();

        // Assert
        Assert.That(result, Is.EqualTo("1-0-1-0-1-0-1-0"));
    }

    [Test]
    public void GetBit_ShouldReturnBitValue() {
        // Arrange
        byte input       = 0b10101010;
        var  inputString = input.FormatBits();

        // Act
        var result0 = input.GetBit(0);
        var result1 = input.GetBit(1);
        var result2 = input.GetBit(2);
        var result3 = input.GetBit(3);
        var result4 = input.GetBit(4);
        var result5 = input.GetBit(5);
        var result6 = input.GetBit(6);
        var result7 = input.GetBit(7);

        // Assert
        Assert.IsFalse(result0);
        Assert.IsTrue(result1);
        Assert.IsFalse(result2);
        Assert.IsTrue(result3);
        Assert.IsFalse(result4);
        Assert.IsTrue(result5);
        Assert.IsFalse(result6);
        Assert.IsTrue(result7);
    }

    [Test]
    public void AddToArray_ShouldAddByteToArray() {
        // Arrange
        var  input   = new byte[] { 0x01, 0x02, 0x03 };
        byte newByte = 0x04;

        // Act
        var result1 = input.AddToArray(newByte);
        var result2 = input.AddToArray(new byte[] { 0x05, 0x06 });

        // Assert
        Assert.That(result1, Is.EqualTo(new byte[] { 0x01, 0x02, 0x03, 0x04 }));
        Assert.That(result2, Is.EqualTo(new byte[] { 0x01, 0x02, 0x03, 0x05, 0x06 }));
    }

    [Test]
    public void ToByteArray_ShouldConvertIntToByteArray() {
        // Arrange
        var input = 256;

        // Act
        var result = input.ToByteArray();

        // Assert
        Assert.That(result, Is.EqualTo(new byte[] { 0x01, 0x00 }));
    }

    [Test]
    public void ToByteArray_ShouldConvertStringToByteArray() {
        // Arrange
        var input = "Hello";

        // Act
        var result = input.ToByteArray();

        // Assert
        Assert.That(result, Is.EqualTo(new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F }));
    }

    [Test]
    public void ToDisplayValues_ShouldReturnFormattedByteValuesString() {
        // Arrange
        var input = new byte[] { 0x01, 0x02, 0x03 };

        // Act
        var result = input.ToDisplayValues();

        // Assert
        Assert.That(result, Is.EqualTo("01-02-03"));
    }

    [Test]
    public void ToDisplayChars_ShouldReturnFormattedCharString() {
        // Arrange
        var input = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };

        // Act
        var result = input.ToDisplayChars();

        // Assert
        Assert.That(result, Is.EqualTo("H-e-l-l-o"));
    }

    [Test]
    public void ToDisplayValueChars_ShouldReturnFormattedValueCharsString() {
        // Arrange
        var input = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };

        // Act
        var result = input.ToDisplayValueChars();

        // Assert
        Assert.That(result, Is.EqualTo("48-65-6C-6C-6F (H-e-l-l-o)"));
    }

    [Test]
    public void FromByteArray_ShouldConvertByteArrayToString() {
        // Arrange
        var input = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };

        // Act
        var result = input.FromByteArray();

        // Assert
        Assert.That(result, Is.EqualTo("Hello"));
    }

    [Test]
    public void Compare_ShouldCompareByteArrays() {
        // Arrange
        var array1 = new byte[] { 0x01, 0x02, 0x03 };
        var array2 = new byte[] { 0x01, 0x02, 0x03 };
        var array3 = new byte[] { 0x01, 0x02, 0x04 };

        // Act
        var result1 = array1.Compare(array2);
        var result2 = array1.Compare(array3);

        // Assert
        Assert.IsTrue(result1);
        Assert.IsFalse(result2);
    }
}