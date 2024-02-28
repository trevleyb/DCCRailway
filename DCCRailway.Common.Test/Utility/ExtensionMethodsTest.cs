using NUnit.Framework;
using System;

namespace DCCRailway.Common.Utilities.Tests
{
    [TestFixture]
    public class ExtensionMethodsTest
    {
        [Test]
        public void Invert_ShouldReturnInverseValue()
        {
            // Arrange
            byte input = 0b10101010;

            // Act
            byte result = input.Invert();

            // Assert
            Assert.AreEqual(0b01010101, result);
        }

        [Test]
        public void SetBit_ShouldSetBitValue()
        {
            // Arrange
            byte input = 0b00000000;

            // Act
            byte result1 = input.SetBit(3, true);
            byte result2 = input.SetBit(5, true);

            // Assert
            Assert.AreEqual(0b00001000, result1);
            Assert.AreEqual(0b00100000, result2);
        }
        
        [Test]
        public void SetBit_ShouldSetAllBits()
        {
            // Arrange
            byte input     = 0b00000000;
            byte testValue = 0;
            
            for (int i = 0; i <= 7; i++) {
                testValue += (byte)((1 << i));
                input      = input.SetBit(i, true);
                Assert.That(testValue, Is.EqualTo(input));
            }
            
            // Assert
            Assert.AreEqual(0b11111111, input);
        }

        [Test]
        public void FormatBits_ShouldReturnFormattedBitsString()
        {
            // Arrange
            byte input = 0b10101010;

            // Act
            string result = input.FormatBits();

            // Assert
            Assert.AreEqual("1-0-1-0-1-0-1-0", result);
        }

        [Test]
        public void GetBit_ShouldReturnBitValue()
        {
            // Arrange
            byte input       = 0b10101010;
            var  inputString = input.FormatBits();
            
            // Act
            bool result0 = input.GetBit(0);
            bool result1 = input.GetBit(1);
            bool result2 = input.GetBit(2);
            bool result3 = input.GetBit(3);
            bool result4 = input.GetBit(4);
            bool result5 = input.GetBit(5);
            bool result6 = input.GetBit(6);
            bool result7 = input.GetBit(7);

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
        public void AddToArray_ShouldAddByteToArray()
        {
            // Arrange
            byte[] input = new byte[] { 0x01, 0x02, 0x03 };
            byte newByte = 0x04;

            // Act
            byte[] result1 = input.AddToArray(newByte);
            byte[] result2 = input.AddToArray(new byte[] { 0x05, 0x06 });

            // Assert
            Assert.AreEqual(new byte[] { 0x01, 0x02, 0x03, 0x04 }, result1);
            Assert.AreEqual(new byte[] { 0x01, 0x02, 0x03, 0x05, 0x06 }, result2);
        }

        [Test]
        public void ToByteArray_ShouldConvertIntToByteArray()
        {
            // Arrange
            int input = 256;

            // Act
            byte[] result = input.ToByteArray();

            // Assert
            Assert.AreEqual(new byte[] { 0x01, 0x00 }, result);
        }

        [Test]
        public void ToByteArray_ShouldConvertStringToByteArray()
        {
            // Arrange
            string input = "Hello";

            // Act
            byte[] result = input.ToByteArray();

            // Assert
            Assert.AreEqual(new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F }, result);
        }

        [Test]
        public void ToDisplayValues_ShouldReturnFormattedByteValuesString()
        {
            // Arrange
            byte[] input = new byte[] { 0x01, 0x02, 0x03 };

            // Act
            string result = input.ToDisplayValues();

            // Assert
            Assert.AreEqual("01-02-03", result);
        }

        [Test]
        public void ToDisplayChars_ShouldReturnFormattedCharString()
        {
            // Arrange
            byte[] input = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };

            // Act
            string result = input.ToDisplayChars();

            // Assert
            Assert.AreEqual("H-e-l-l-o", result);
        }

        [Test]
        public void ToDisplayValueChars_ShouldReturnFormattedValueCharsString()
        {
            // Arrange
            byte[] input = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };

            // Act
            string result = input.ToDisplayValueChars();

            // Assert
            Assert.AreEqual("48-65-6C-6C-6F (H-e-l-l-o)", result);
        }

        [Test]
        public void FromByteArray_ShouldConvertByteArrayToString()
        {
            // Arrange
            byte[] input = new byte[] { 0x48, 0x65, 0x6C, 0x6C, 0x6F };

            // Act
            string result = input.FromByteArray();

            // Assert
            Assert.AreEqual("Hello", result);
        }

        [Test]
        public void Compare_ShouldCompareByteArrays()
        {
            // Arrange
            byte[] array1 = new byte[] { 0x01, 0x02, 0x03 };
            byte[] array2 = new byte[] { 0x01, 0x02, 0x03 };
            byte[] array3 = new byte[] { 0x01, 0x02, 0x04 };

            // Act
            bool result1 = array1.Compare(array2);
            bool result2 = array1.Compare(array3);

            // Assert
            Assert.IsTrue(result1);
            Assert.IsFalse(result2);
        }
    }
}